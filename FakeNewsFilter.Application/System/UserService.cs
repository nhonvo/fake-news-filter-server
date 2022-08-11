using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using FakeNewsFilter.ViewModel.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FakeNewsFilter.Utilities.Exceptions;
using System.Collections.Generic;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Application.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using FakeNewsFilter.Data.Enums;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using System.Net.Mail;
using SmtpClient = System.Net.Mail.SmtpClient;
using System.Net;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace FakeNewsFilter.Application.System
{
    public interface IUserService
    {
        Task<ApiResult<TokenResult>> Authencate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<List<UserViewModel>>> GetUsers();
        Task<ApiResult<bool>> Update(UserUpdateRequest request);
        Task<ApiResult<UserViewModel>> GetById(Guid id);
        Task<ApiResult<bool>> Delete(string UserId);
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
        Task<ApiResult<TokenResult>> SignInFacebook(string accessToken);
        Task<ApiResult<TokenResult>> SignInGoogle(string accessToken);
        Task<ApiResult<TokenResult>> SignInApple(string accessToken);
        Task<ApiResult<ForgotPassword>> SendPasswordResetCode(string Email);
        Task<ApiResult<ForgotPassword>> ResetPassword(string email, string opt, string newPassword);
        Task<ApiResult<TokenResult>> RefreshTokenAsync(string token);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly FacebookAuthService _facebookAuthService;

        private readonly IConfiguration _config;

        private readonly IMapper _mapper;

        private readonly RoleManager<Role> _roleManager;

        private FileStorageService _storageService;


        public UserService(ApplicationDBContext context, UserManager<User> userManager,
            SignInManager<User> signInManager, FacebookAuthService facebookAuthService, IConfiguration config,
            IMapper mapper, RoleManager<Role> roleManager, FileStorageService storageService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _facebookAuthService = facebookAuthService;
            _config = config;
            _mapper = mapper;
            _roleManager = roleManager;
            FileStorageService.USER_CONTENT_FOLDER_NAME = "images/avatars";
            _storageService = storageService;
        }


        //Tạo Token
        private async Task<TokenResult> GenerateUserTokenAsync(User user, string avatar)
        {
            var token_result = new TokenResult();

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Uri, avatar ?? "default.png"),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Role, roles == null ? "Subscriber" : string.Join(";", roles)),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddDays(10);

            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds);

            var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);

            token_result.UserId = user.Id;

            token_result.Expires = expires;

            token_result.Token = tokenResult;

            var checkToken = _context.RefreshTokens.FirstOrDefault(f => f.UserId == user.Id);

            var refreshToken = new RefreshToken
            {
                Token = tokenResult,
                UserId = user.Id,
                Expires = expires,
                Created = DateTime.UtcNow
            };

            //check if refresh token is not exist, then add new one
            if (checkToken == null)
            {
                _context.RefreshTokens.Add(refreshToken);
            }
            //if refresh token is exist and valid, then update it
            else if (checkToken.Expires > DateTime.UtcNow)
            {
                checkToken.Token = tokenResult;
                checkToken.Expires = expires;
                checkToken.Created = DateTime.UtcNow;
            }
            //if refresh token is exist and expired, then delete it and add new one
            else
            {
                _context.RefreshTokens.Remove(checkToken);
                _context.RefreshTokens.Add(refreshToken);
            }
            await _context.SaveChangesAsync();
            return token_result;
        }

        //Refresh Token
        public async Task<ApiResult<TokenResult>> RefreshTokenAsync(string token)
        {
            var user = _context.Users.Include(i => i.RefreshTokens)
                .SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                //Token không tồn tại ở bất kỳ user nào
                return new ApiErrorResult<TokenResult>(404, "TokenNotExist");
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                //Token không hoạt động
                return new ApiErrorResult<TokenResult>(400, "TokenNotActive");
            }

            //Thu hồi Token hiện tại
            refreshToken.Revoked = DateTime.UtcNow;

            //Tạo Token mới và cập nhật lại trên DB
            var res = await GenerateUserTokenAsync(user, null);


            var token_result = new TokenResult
            {
                UserId = res.UserId,
                Expires = res.Expires,
                Token = res.Token,
            };

            return new ApiSuccessResult<TokenResult>("RefreshTokenSuccess", token_result);
        }


        //Đăng nhập
        public async Task<ApiResult<TokenResult>> Authencate(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName) ??
                           await _userManager.FindByEmailAsync(request.UserName);

                if (user == null) return new ApiErrorResult<TokenResult>(404, "AccountDoesNotExist");

                var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

                if (!result.Succeeded)
                {
                    return new ApiErrorResult<TokenResult>(400, "LoginUnsuccessful");
                }

                var avatar = _context.Media.Where(m => m.MediaId == user.AvatarId).Select(m => m.PathMedia)
                    .FirstOrDefault();

                var tokenResult = await GenerateUserTokenAsync(user, avatar);

                return new ApiSuccessResult<TokenResult>("LoginSuccessful", tokenResult);
            }

            catch (FakeNewsException e)
            {
                return new ApiErrorResult<TokenResult>(500, "ErrorSystem: " + e.Message);
            }
        }

        //Đăng ký người dùng mới
        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user != null)
                {
                    return new ApiErrorResult<bool>(404, "UsernameIsvAvailable");
                }

                user = await _userManager.FindByEmailAsync(request.Email);

                if (user != null)
                {
                    return new ApiErrorResult<bool>(404, "EmailIsAvailable");
                }

                user = new User()
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    Name = request.Name,
                };
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Subscriber");

                    return new ApiSuccessResult<bool>("RegisterSuccessful", false);
                }

                else
                {
                    List<IdentityError> errorList = result.Errors.ToList();
                    var errors = string.Join(", ", errorList.Select(e => e.Description));
                    return new ApiErrorResult<bool>(400, "RegisterUnsuccessful " + errors);
                }
            }

            catch (FakeNewsException e)
            {
                return new ApiErrorResult<bool>(500, "ErrorSystem: " + e.Message);
            }
        }


        //Lấy danh sách người dùng
        public async Task<ApiResult<List<UserViewModel>>> GetUsers()
        {
            try
            {
                var query = _userManager.Users;

                var userList = await (from x in query
                    select new
                    {
                        UserId = x.Id,
                        Email = x.Email,
                        FullName = x.Name,
                        Status = x.Status,
                        UserName = x.UserName,
                        Avatar = _context.Media.Where(m => m.MediaId == x.AvatarId).Select(m => m.PathMedia)
                            .FirstOrDefault(),
                        RoleNames = (from userRole in x.UserRoles
                            join role in _roleManager.Roles
                                on userRole.RoleId
                                equals role.Id
                            select role.Name).ToList()
                    }).Select(
                    p => new UserViewModel
                    {
                        UserId = p.UserId,
                        Email = p.Email,
                        FullName = p.FullName,
                        Status = p.Status,
                        UserName = p.UserName,
                        Avatar = p.Avatar,
                        Roles = p.RoleNames
                    }
                ).ToListAsync();


                return new ApiSuccessResult<List<UserViewModel>>("LoadingListUsersSuccessful", userList);
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<List<UserViewModel>>(500, "ErrorSystem: " + e.Message);
            }
        }

        //Cập nhật người dùng
        public async Task<ApiResult<bool>> Update(UserUpdateRequest request)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

                user.Email = request.Email ?? user.Email;
                user.Name = request.Name ?? user.Name;

                //Lưu Avatar vào Host
                if (request.MediaFile != null)
                {
                    var thumb = _context.Media.FirstOrDefault(i => i.MediaId == user.AvatarId);

                    //Thêm mới Avatar nếu Tài khoản chưa có
                    if (thumb == null)
                    {
                        user.Avatar = new Media()
                        {
                            Caption = "Avatar User",
                            DateCreated = DateTime.Now,
                            FileSize = request.MediaFile.Length,
                            PathMedia = await this.SaveFile(request.MediaFile),
                            Type = MediaType.Image,
                            SortOrder = 1
                        };
                    }
                    else
                    {
                        //Cập nhật Avatar
                        if (thumb.PathMedia != null)
                        {
                            await _storageService.DeleteFileAsync(thumb.PathMedia);
                        }

                        thumb.FileSize = request.MediaFile.Length;
                        thumb.PathMedia = await SaveFile(request.MediaFile);

                        _context.Media.Update(thumb);
                    }
                }

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new ApiSuccessResult<bool>("UpdateUserSuccessful", false);
                }

                return new ApiErrorResult<bool>(400, "UpdateUnsuccessful");
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<bool>(500, "ErrorSystem: " + e.Message);
            }
        }

        //Lấy Id của người dùng
        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());

                if (user == null)
                {
                    return new ApiErrorResult<UserViewModel>(404, "AccountDoesNotExist");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var avatar = _context.Media.Where(m => m.MediaId == user.AvatarId).Select(m => m.PathMedia)
                    .FirstOrDefault();

                var userVm = _mapper.Map<UserViewModel>(user);

                userVm.Roles = roles;
                userVm.Avatar = avatar;

                return new ApiSuccessResult<UserViewModel>("GetInfoUserSuccessful", userVm);
            }

            catch (FakeNewsException e)
            {
                return new ApiErrorResult<UserViewModel>(500, "ErrorSystem: " + e.Message);
            }
        }

        //Xoá người dùng
        public async Task<ApiResult<bool>> Delete(string UserId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null)
                {
                    return new ApiErrorResult<bool>(404, "AccountDoesNotExist");
                }

                //Xoá Avatar ra khỏi Source
                var avatar = _context.Media.SingleOrDefault(x => x.MediaId == user.AvatarId);

                if (avatar != null)
                {
                    if (avatar.PathMedia != null)
                        await _storageService.DeleteFileAsync(avatar.PathMedia);
                    _context.Media.Remove(avatar);
                }

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return new ApiSuccessResult<bool>("DeleteSuccessfull", false);

                return new ApiErrorResult<bool>(400, "DeleteUnsuccessfull");
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<bool>(500, "ErrorSystem: " + e.Message);
            }
        }

        //Gán quyền người dùng
        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>(404, "AccountDoesNotExist");
            }

            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();

            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }

            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();

            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ApiSuccessResult<bool>("RoleAssignSuccessful", false);
        }

        //Lưu ảnh
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        //Đăng nhập bằng Facebook
        public async Task<ApiResult<TokenResult>> SignInFacebook(string accessToken)
        {
            try
            {
                var validatedTokenResult = await _facebookAuthService.ValidationAcessTokenAsync(accessToken);

                if (!validatedTokenResult.Data.IsValid)
                {
                    return new ApiErrorResult<TokenResult>(400, "InvalidFacebookToken");
                }

                //Lấy thông tin User Facebook từ AccessToken
                var userInfo = await _facebookAuthService.GetUsersInfoAsync(accessToken);

                //Cắt Email thành UsernName
                string userName = (userInfo.Email).Split('@')[0];

                //Tạo FullName
                string fullName = (userInfo.LastName + userInfo.FirstName).ToString();

                //Kiểm tra user đã liên kết với Facebook chưa
                var checkLinked = await _signInManager.ExternalLoginSignInAsync("Facebook", userInfo.Id, false);

                //Nếu đã có liên kết trước đó (Email tồn tại) thì tiến hành đăng nhập luôn
                if (checkLinked.Succeeded)
                {
                    var exist_user = await _userManager.FindByEmailAsync(userInfo.Email);

                    var avatar = _context.Media.Where(m => m.MediaId == exist_user.AvatarId).Select(m => m.PathMedia)
                        .FirstOrDefault();

                    var tokenResult = await GenerateUserTokenAsync(exist_user, avatar);

                    return new ApiSuccessResult<TokenResult>("LoginFacebookSuccessful", tokenResult);
                }
                else //Chưa được liên kết với Facebook
                {
                    var info = new ExternalLoginInfo(ClaimsPrincipal.Current, "Facebook", userInfo.Id, null);

                    //Kiểm tra tồn tại Email
                    var exist_user = await _userManager.FindByEmailAsync(userInfo.Email);

                    if (exist_user != null)
                    {
                        //Gán tài khoản Facebook vào tài khoản đã có sẵn
                        var result = await _userManager.AddLoginAsync(exist_user, info);
                        if (result.Succeeded)
                        {
                            var avatar = _context.Media.Where(m => m.MediaId == exist_user.AvatarId)
                                .Select(m => m.PathMedia).FirstOrDefault();

                            var tokenResult = await GenerateUserTokenAsync(exist_user, avatar);

                            return new ApiSuccessResult<TokenResult>("LinkedFacebookSuccessful", tokenResult);
                        }
                        else
                        {
                            return new ApiErrorResult<TokenResult>(400, "ErrorLinkedFacebook");
                        }
                    }
                    else //Tài khoản đăng nhập lần đầu
                    {
                        var indentityuser = new User
                        {
                            Id = Guid.NewGuid(),
                            Email = userInfo.Email,
                            UserName = userName,
                            Name = fullName
                        };

                        var result = await _userManager.CreateAsync(indentityuser);

                        if (result.Succeeded)
                        {
                            result = await _userManager.AddLoginAsync(indentityuser, info);

                            if (!result.Succeeded)
                            {
                                return new ApiErrorResult<TokenResult>(400, "ErrorLinkedFacebook");
                            }

                            var roles = await _userManager.AddToRoleAsync(indentityuser, "Subscriber");


                            var tokenResult = await GenerateUserTokenAsync(exist_user, null);

                            return new ApiSuccessResult<TokenResult>("LoginFacebookSuccessful", tokenResult);
                        }
                        else
                        {
                            List<IdentityError> errorList = result.Errors.ToList();
                            var errors = string.Join(", ", errorList.Select(e => e.Description));
                            return new ApiErrorResult<TokenResult>(400, "RegisterFacebookUnsuccessful " + errors);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return new ApiErrorResult<TokenResult>(500, "ErrorSystem" + e.Message);
            }
        }

        //Đăng nhập bằng Google
        public async Task<ApiResult<TokenResult>> SignInGoogle(string accessToken)
        {
            try
            {
                Payload payload = await ValidateAsync(accessToken);


                if (payload == null)
                {
                    return new ApiErrorResult<TokenResult>(404, "InvalidGoogleToken");
                }


                //Kiểm tra user đã liên kết với Google chưa
                var checkLinked = await _signInManager.ExternalLoginSignInAsync("Google", payload.Subject, false);

                //Cắt Email thành UsernName
                string userName = (payload.Email).Split('@')[0];

                //Tạo FullName
                string fullName = (payload.FamilyName + payload.GivenName).ToString();

                //Nếu đã có liên kết trước đó (Email) thì tiến hành đăng nhập luôn
                if (checkLinked.Succeeded)
                {
                    var exist_user = await _userManager.FindByEmailAsync(payload.Email);

                    var avatar = _context.Media.Where(m => m.MediaId == exist_user.AvatarId).Select(m => m.PathMedia)
                        .FirstOrDefault();

                    var tokenResult = await GenerateUserTokenAsync(exist_user, avatar);

                    return new ApiSuccessResult<TokenResult>("LoginGoogleSuccessful", tokenResult);
                }
                else //Chưa được liên kết với Facebook
                {
                    var info = new ExternalLoginInfo(ClaimsPrincipal.Current, "Google", payload.Subject, null);

                    //Kiểm tra tồn tại Email và Username
                    var exist_user = await _userManager.FindByEmailAsync(payload.Email);

                    if (exist_user != null)
                    {
                        //Gán tài khoản Facebook vào tài khoản đã có sẵn
                        var result = await _userManager.AddLoginAsync(exist_user, info);

                        if (result.Succeeded)
                        {
                            var avatar = _context.Media.Where(m => m.MediaId == exist_user.AvatarId)
                                .Select(m => m.PathMedia).FirstOrDefault();

                            var tokenResult = await GenerateUserTokenAsync(exist_user, avatar);

                            return new ApiSuccessResult<TokenResult>("LinkedGoogleSuccessful", tokenResult);
                        }
                        else
                        {
                            return new ApiErrorResult<TokenResult>(400, "ErrorLinkedGoogle");
                        }
                    }
                    else //Tài khoản đăng nhập lần đầu
                    {
                        var indentityuser = new User
                        {
                            Id = Guid.NewGuid(),
                            Email = payload.Email,
                            UserName = userName,
                            Name = fullName
                        };

                        var result = await _userManager.CreateAsync(indentityuser);

                        if (result.Succeeded)
                        {
                            result = await _userManager.AddLoginAsync(indentityuser, info);

                            if (!result.Succeeded)
                            {
                                return new ApiErrorResult<TokenResult>(400, "ErrorLinkedGoogle");
                            }

                            var roles = await _userManager.AddToRoleAsync(indentityuser, "Subscriber");


                            var tokenResult = await GenerateUserTokenAsync(exist_user, null);

                            return new ApiSuccessResult<TokenResult>("LoginGoogleSuccessful", tokenResult);
                        }
                        else
                        {
                            List<IdentityError> errorList = result.Errors.ToList();
                            var errors = string.Join(", ", errorList.Select(e => e.Description));
                            return new ApiErrorResult<TokenResult>(400, "RegisterGoogleUnsuccessful " + errors);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return new ApiErrorResult<TokenResult>(500, "ErrorSystem" + e.Message);
            }
        }

        //Đăng nhập bằng Apple
        public async Task<ApiResult<TokenResult>> SignInApple(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var userToken = tokenHandler.ReadJwtToken(accessToken);

                // Get the subject to use for the Name Identifier claim
                string subject = userToken.Subject;

                return new ApiSuccessResult<TokenResult>("LoginGoogleSuccessful");

            }
            catch (Exception e)
            {
                return new ApiErrorResult<TokenResult>(500, "ErrorSystem" + e.Message);
            }
        }

            public async Task<ApiResult<ForgotPassword>> SendPasswordResetCode(string email)
        {
            //Get identity user details user manager
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new ApiErrorResult<ForgotPassword>(404, "UserNotFound");
            }

            //Generate password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //Generate OTP
            int otp = RandomNumberGeneartor.Generate(100000, 999999);

            var resetPassword = new ForgotPassword()
            {
                Email = email,
                OTP = otp.ToString(),
                Token = token,
                UserId = user.Id,
                DateTime = DateTime.Now
            };

            //save data into db with OTP
            _context.ForgotPassword.Add(resetPassword);
            await _context.SaveChangesAsync();

            //To do: Send token in email

            await EmailSender.SendEmailAsync(email, "Reset Password OTP", "Hello "
                                                                          + email +
                                                                          "<br><br>Please find the reset password token below<br><br><b>"
                                                                          + otp +
                                                                          "<b><br><br>Thanks<br>FakenewsFilter.tk");

            return new ApiSuccessResult<ForgotPassword>("TokenSendSuccess", resetPassword);
        }

        public async Task<ApiResult<ForgotPassword>> ResetPassword(string email, string otp, string newPassword)
        {
            //Get identity user details user manager
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || string.IsNullOrEmpty(newPassword))
            {
                return new ApiErrorResult<ForgotPassword>(400, "EmailAndPasswordNotNull");
            }

            //Getting token from otp
            var resetPasswordDetails = await _context.ForgotPassword
                .Where(x => x.OTP == otp && x.UserId == user.Id)
                .OrderByDescending(x => x.DateTime).FirstOrDefaultAsync();

            //Verify if token is older than 3 minutes
            var expirationDateTime = resetPasswordDetails.DateTime.AddMinutes(3);

            if (expirationDateTime < DateTime.Now)
            {
                return new ApiErrorResult<ForgotPassword>(400, "GenerateTheNewOTP");
            }

            var res = await _userManager.ResetPasswordAsync(user, resetPasswordDetails.Token, newPassword);

            if (!res.Succeeded)
            {
                return new ApiErrorResult<ForgotPassword>(400, "OTPWrong");
            }

            return new ApiSuccessResult<ForgotPassword>("ChangePasswordSuccessful");
        }

        public static class RandomNumberGeneartor
        {
            private static readonly Random _random = new Random();

            public static int Generate(int min, int max)
            {
                return _random.Next(min, max);
            }
        }

        public static class EmailSender
        {
            public static async Task SendEmailAsync(string email, string subject, string htmlMessage)
            {
                string fromMail = "thanh26092000@gmail.com";
                string fromPassword = "rwgewnngblhsttyu";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = subject;
                message.To.Add(new MailAddress(email));
                message.Body = "<html><body>" + htmlMessage + "</body></html>";
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(message);
            }
        }
    }
}
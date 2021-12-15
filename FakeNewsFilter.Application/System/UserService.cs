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
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net.Mail;
using SmtpClient = System.Net.Mail.SmtpClient;
using System.Net;

namespace FakeNewsFilter.Application.System
{
    public interface IUserService
    {
        Task<ApiResult<TokenResult>> Authencate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<List<UserViewModel>>> GetUsers();

        Task<ApiResult<bool>> Update(UserUpdateRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);

        Task<ApiResult<bool>> Delete(String UserId);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);

        Task<ApiResult<TokenResult>> SignInFacebook(string accessToken);

        Task<ApiResult<TokenResult>> SignInGoogle(string accessToken);
        Task<ApiResult<ForgotPassword>> SendPasswordResetCode(string Email);
        Task<ApiResult<ForgotPassword>> ResetPassword(string email, string opt, string newPassword);

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
        
        
        public UserService(ApplicationDBContext context, UserManager<User> userManager, SignInManager<User> signInManager, FacebookAuthService facebookAuthService, IConfiguration config, IMapper mapper, RoleManager<Role> roleManager, FileStorageService storageService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _facebookAuthService = facebookAuthService;
            _config = config;
            _mapper = mapper;
            _roleManager = roleManager;
            FileStorageService.USER_CONTENT_FOLDER_NAME= "images/avatars";
            _storageService = storageService;
        }

        //Tạo Token
        private async Task<TokenResult> GenerateUserTokenAsync(User user, string avatar)
        {

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[] {
                new Claim(ClaimTypes.Uri, avatar ?? "default.png"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Role, roles==null ? "Subscriber" :  string.Join(";", roles)),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMonths(1);

            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds);

            return new TokenResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
                Email = user.Email,
                Expires = expires,
            };

        }

        //Đăng nhập
        public async Task<ApiResult<TokenResult>> Authencate(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName) ?? await _userManager.FindByEmailAsync(request.UserName);

                if (user == null) return new ApiErrorResult<TokenResult>("AccountDoesNotExist");

                var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

                if (!result.Succeeded)
                {
                    return new ApiErrorResult<TokenResult>("LoginUnsuccessful");
                }

                
                var avatar = _context.Media.Where(m => m.MediaId == user.AvatarId).Select(m => m.PathMedia).FirstOrDefault();

                var tokenResult = await GenerateUserTokenAsync(user, avatar);

                return new ApiSuccessResult<TokenResult>("LoginSuccessful", tokenResult);
            }

            catch (FakeNewsException e)
            {
                return new ApiErrorResult<TokenResult>("ErrorSystem: " + e.Message);
            }
        }

        //Đăng ký người dùng mới
        public async Task<ApiResult<bool>>  Register(RegisterRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if(user != null)
                {
                    return new ApiErrorResult<bool>("UsernameIsvAvailable");
                }

                user = await _userManager.FindByEmailAsync(request.Email);

                if ( user != null)
                {
                    return new ApiErrorResult<bool>("EmailIsAvailable");
                }    

                user = new User()
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber,
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
                    return new ApiErrorResult<bool>("RegisterUnsuccessful " + errors);
                }
            }

            catch (FakeNewsException e)
            {
                return new ApiErrorResult<bool>("ErrorSystem: " + e.Message);
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
                                          PhoneNumber = x.PhoneNumber,
                                          Avatar = _context.Media.Where(m => m.MediaId == x.AvatarId).Select(m => m.PathMedia).FirstOrDefault(),
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
                        PhoneNumber = p.PhoneNumber,
                        Avatar = p.Avatar,
                        Roles = p.RoleNames
                    }
                   ).ToListAsync();


                return new ApiSuccessResult<List<UserViewModel>>("LoadingListUsersSuccessful", userList);
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<List<UserViewModel>>("ErrorSystem: " + e.Message);
            }
        }

        //Cập nhật người dùng
        public async Task<ApiResult<bool>> Update(UserUpdateRequest request)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
                //Kiểm tra trùng Email
                var email = await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id == request.UserId);
                if (email == true)
                {
                    return new ApiErrorResult<bool>("EmailIsAvailable");
                }

                user.Email = request.Email ?? user.Email;
                user.Name = request.Name ?? user.Name;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;

                //Lưu Avatar vào Host
                if (request.MediaFile != null)
                {
                    
                    var thumb = _context.Media.FirstOrDefault(i => i.MediaId == user.AvatarId);

                    //Thêm mới Avatar nếu Tài khoản chưa có
                    if(thumb == null)
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
                return new ApiErrorResult<bool>("UpdateUnsuccessful");
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<bool>("ErrorSystem: " + e.Message);
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
                    return new ApiErrorResult<UserViewModel>("AccountDoesNotExist");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var avatar = _context.Media.Where(m => m.MediaId == user.AvatarId).Select(m => m.PathMedia).FirstOrDefault();

                var userVm = _mapper.Map<UserViewModel>(user);

                userVm.Roles = roles;
                userVm.Avatar = avatar;

                return new ApiSuccessResult<UserViewModel>("GetInfoUserSuccessful",userVm);
            }

            catch (FakeNewsException e)
            {
                return new ApiErrorResult<UserViewModel>("ErrorSystem: " + e.Message);
            }

        }

        //Xoá người dùng
        public async Task<ApiResult<bool>> Delete(string UserId)
        {
            try {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null)
                {
                    return new ApiErrorResult<bool>("AccountDoesNotExist");
                }

                //Xoá Avatar ra khỏi Source
                var avatar = _context.Media.SingleOrDefault(x => x.MediaId == user.AvatarId);

                if (avatar != null )
                {
                    if (avatar.PathMedia != null)
                        await _storageService.DeleteFileAsync(avatar.PathMedia);
                        _context.Media.Remove(avatar);
                }

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return new ApiSuccessResult<bool>("DeleteSuccessfull", false);

                return new ApiErrorResult<bool>("DeleteUnsuccessfull");
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<bool>("ErrorSystem: " + e.Message);
            }
        }

        //Gán quyền người dùng
        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("AccountDoesNotExist");
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

        public async Task<ApiResult<TokenResult>> SignInFacebook(string accessToken)
        {
            try
            {

                var validatedTokenResult = await _facebookAuthService.ValidationAcessTokenAsync(accessToken);

                if (!validatedTokenResult.Data.IsValid)
                {
                    return new ApiErrorResult<TokenResult>("InvalidFacebookToken");
                }

                //Lấy thông tin User Facebook từ AccessToken
                var userInfo = await _facebookAuthService.GetUsersInfoAsync(accessToken);

                //Cắt Email thành UsernName
                string userName = (userInfo.Email).Split('@')[0];

                //Tạo FullName
                string fullName = (userInfo.LastName + userInfo.FirstName).ToString();

                //Kiểm tra user đã liên kết với Facebook chưa
                var checkLinked = await _signInManager.ExternalLoginSignInAsync("Facebook", userInfo.Id, false);

                //Nếu đã có liên kết trước đó thì tiến hành đăng nhập luôn
                if (checkLinked.Succeeded)
                {
                    var exist_user = await _userManager.FindByNameAsync(userName);

                    var avatar = _context.Media.Where(m => m.MediaId == exist_user.AvatarId).Select(m => m.PathMedia).FirstOrDefault();

                    var tokenResult = await GenerateUserTokenAsync(exist_user, avatar);

                    return new ApiSuccessResult<TokenResult>("LoginFacebookSuccessful", tokenResult);
                    
                }
                else  //Chưa được liên kết với Facebook
                {
                    var info = new ExternalLoginInfo(ClaimsPrincipal.Current, "Facebook", userInfo.Id, null);

                    //Kiểm tra tồn tại Email và Username
                    var exist_user = await _userManager.FindByNameAsync(userName);

                    if (exist_user != null)
                    {
                        //Gán tài khoản Facebook vào tài khoản đã có sẵn
                        var result = await _userManager.AddLoginAsync(exist_user, info);
                        if (result.Succeeded)
                        {
                            var avatar = _context.Media.Where(m => m.MediaId == exist_user.AvatarId).Select(m => m.PathMedia).FirstOrDefault();

                            var tokenResult = await GenerateUserTokenAsync(exist_user, avatar);

                            return new ApiSuccessResult<TokenResult>("LinkedFacebookSuccessful", tokenResult);
                        }
                        else
                        {
                            return new ApiErrorResult<TokenResult>("ErrorLinkedFacebook");
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
                                return new ApiErrorResult<TokenResult>("ErrorLinkedFacebook");
                            }

                            var roles = await _userManager.AddToRoleAsync(indentityuser, "Subscriber");


                            var tokenResult = await GenerateUserTokenAsync(exist_user, null);

                            return new ApiSuccessResult<TokenResult>("LoginFacebookSuccessful", tokenResult);

                        }
                        else
                        {
                            List<IdentityError> errorList = result.Errors.ToList();
                            var errors = string.Join(", ", errorList.Select(e => e.Description));
                            return new ApiErrorResult<TokenResult>("RegisterFacebookUnsuccessful " + errors);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                return new ApiErrorResult<TokenResult>("LoginUnsuccessful" + e.Message);
            }
        }

        public async Task<ApiResult<TokenResult>> SignInGoogle(string accessToken)
        {
            try
            {
                Payload payload = await ValidateAsync(accessToken);


                if (payload == null)
                {
                    return new ApiErrorResult<TokenResult>("InvalidGoogleToken");
                }

               
                //Kiểm tra user đã liên kết với Google chưa
                var checkLinked = await _signInManager.ExternalLoginSignInAsync("Google", payload.Subject, false);

                //Cắt Email thành UsernName
                string userName = (payload.Email).Split('@')[0];

                //Tạo FullName
                string fullName = (payload.FamilyName + payload.GivenName).ToString();

                //Nếu đã có liên kết trước đó thì tiến hành đăng nhập luôn
                if (checkLinked.Succeeded)
                {
                    var exist_user = await _userManager.FindByNameAsync(userName);

                    var avatar = _context.Media.Where(m => m.MediaId == exist_user.AvatarId).Select(m => m.PathMedia).FirstOrDefault();

                    var tokenResult = await GenerateUserTokenAsync(exist_user, avatar);

                    return new ApiSuccessResult<TokenResult>("LoginGoogleSuccessful", tokenResult);

                }
                else  //Chưa được liên kết với Facebook
                {
                    var info = new ExternalLoginInfo(ClaimsPrincipal.Current, "Google", payload.Subject, null);

                    //Kiểm tra tồn tại Email và Username
                    var exist_user = await _userManager.FindByNameAsync(userName);

                    if (exist_user != null)
                    {
                        //Gán tài khoản Facebook vào tài khoản đã có sẵn
                        var result = await _userManager.AddLoginAsync(exist_user, info);

                        if (result.Succeeded)
                        {
                            var avatar = _context.Media.Where(m => m.MediaId == exist_user.AvatarId).Select(m => m.PathMedia).FirstOrDefault();

                            var tokenResult = await GenerateUserTokenAsync(exist_user, avatar);

                            return new ApiSuccessResult<TokenResult>("LinkedGoogleSuccessful", tokenResult);
                        }
                        else
                        {
                            return new ApiErrorResult<TokenResult>("ErrorLinkedGoogle");
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
                                return new ApiErrorResult<TokenResult>("ErrorLinkedGoogle");
                            }

                            var roles = await _userManager.AddToRoleAsync(indentityuser, "Subscriber");


                            var tokenResult = await GenerateUserTokenAsync(exist_user, null);

                            return new ApiSuccessResult<TokenResult>("LoginGoogleSuccessful", tokenResult);

                        }
                        else
                        {
                            List<IdentityError> errorList = result.Errors.ToList();
                            var errors = string.Join(", ", errorList.Select(e => e.Description));
                            return new ApiErrorResult<TokenResult>("RegisterGoogleUnsuccessful " + errors);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                return new ApiErrorResult<TokenResult>("LoginUnsuccessful" + e.Message);
            }

        }

        public async Task<ApiResult<ForgotPassword>> SendPasswordResetCode(string email)
        {

            //Get identity user details user manager
            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null)
            {
                return new ApiErrorResult<ForgotPassword>("UserNotFound");
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

            await EmailSender.SendEmailAsync(email, "Reset Password OTP", "Hello"
                + email + "<br><br>Please find the reset password token below<br><br><b>" 
                + otp + "<b><br><br>Thanks<br>FakenewsFilter.com");

            return new ApiSuccessResult<ForgotPassword>("TokenSendSuccess", resetPassword);
        }

        public async Task<ApiResult<ForgotPassword>> ResetPassword(string email, string otp, string newPassword)
        {
            //Get identity user details user manager
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || string.IsNullOrEmpty(newPassword))
            {
                return new ApiErrorResult<ForgotPassword>("EmailAndPasswordNotNull");
            }
            //Getting token from otp
            var resetPasswordDetails = await _context.ForgotPassword
                .Where(x => x.OTP == otp && x.UserId == user.Id)
                .OrderByDescending(x => x.DateTime).FirstOrDefaultAsync();

            //Verify if token is older than 15 minutes
            var expirationDateTime = resetPasswordDetails.DateTime.AddMinutes(3);

            if(expirationDateTime < DateTime.Now)
            {
                return new ApiErrorResult<ForgotPassword>("GenerateTheNewOTP");
            }

            var res = await _userManager.ResetPasswordAsync(user, resetPasswordDetails.Token, newPassword);

            if(!res.Succeeded)
            {
                return new ApiErrorResult<ForgotPassword>("OTPWrong");
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
                string fromPassword = "ldhopwrtqzfypdkq";

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
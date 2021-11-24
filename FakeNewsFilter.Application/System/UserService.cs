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
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IConfiguration _config;

        private readonly IMapper _mapper;

        private readonly RoleManager<Role> _roleManager;

        private FileStorageService _storageService;
        
        
        public UserService(ApplicationDBContext context, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IMapper mapper, RoleManager<Role> roleManager, FileStorageService storageService)
        {

            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _mapper = mapper;
            _roleManager = roleManager;
            FileStorageService.USER_CONTENT_FOLDER_NAME= "images/avatars";
            _storageService = storageService;
        }
              
        //Đăng nhập
        public async Task<ApiResult<TokenResult>> Authencate(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null) return new ApiErrorResult<TokenResult>("AccountDoesNotExist");

                var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

                if (!result.Succeeded)
                {
                    return new ApiErrorResult<TokenResult>("LoginUnsuccessful");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var avatar = _context.Media.Where(m => m.MediaId == user.AvatarId).Select(m => m.PathMedia).FirstOrDefault();

                var claims = new[]
                {
                    new Claim(ClaimTypes.Uri, avatar ?? "default.png"),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.GivenName,user.Name),
                    new Claim(ClaimTypes.Role, string.Join(";",roles)),
                    new Claim(ClaimTypes.Name, request.UserName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: creds);

                var returnResult = new TokenResult{Token = new JwtSecurityTokenHandler().WriteToken(token), UserId = user.Id};

                return new ApiSuccessResult<TokenResult>("LoginSuccessful", returnResult);
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

                if (await _userManager.FindByEmailAsync(request.Email) != null)
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
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());

                //Kiểm tra trùng Email
                var email = await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != request.UserId);
                if (email == false)
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
    }
}

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

namespace FakeNewsFilter.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IConfiguration _config;

        private readonly IMapper _mapper;

        private readonly RoleManager<Role> _roleManager;


        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IMapper mapper, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        
        //Đăng nhập
        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null) return new ApiErrorResult<string>("Account does not exist");

                var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

                if (!result.Succeeded)
                {
                    return new ApiErrorResult<string>("Login Unsuccessful. Please Check Username or Password!");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var claims = new[]
                {
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

                return new ApiSuccessResult<string>("Login successful!", new JwtSecurityTokenHandler().WriteToken(token));
            }

            catch (FakeNewsException e)
            {
                return new ApiErrorResult<string>("Error System: " + e.Message);
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
                    return new ApiErrorResult<bool>("Username is available");
                }

                if (await _userManager.FindByEmailAsync(request.Email) != null)
                {
                    return new ApiErrorResult<bool>("Email is available");
                }    

                user = new User()
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber,
                    Name = request.Name,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if(result.Succeeded)
                {
                    return new ApiSuccessResult<bool>();
                }
                return new ApiErrorResult<bool>("Register Unsuccessful.");
            }

            catch (FakeNewsException e)
            {
                return new ApiErrorResult<bool>("Error System: " + e.Message);
            }
        }

        //Lấy danh sách người dùng (phân trang)
        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
        {
            try
            {
                var query = _userManager.Users;

                if (!string.IsNullOrEmpty(request.keyWord))
                {
                    query = query.Where(x => x.UserName.Contains(request.keyWord)
                     || x.Email.Contains(request.keyWord));
                }

                //3. Paging
                int totalRow = await query.CountAsync();

                var data = await query.Skip((request.pageIndex - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(x => _mapper.Map<UserViewModel>(x)
                ).ToListAsync();

                //4. Select and projection
                var pagedResult = new PagedResult<UserViewModel>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.pageIndex,
                    PageSize = request.pageSize,
                    Items = data
                };

                return new ApiSuccessResult<PagedResult<UserViewModel>>("Loading List Users Successful!", pagedResult);
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<PagedResult<UserViewModel>>("Error System: " + e.Message);
            }
        }

        //Cập nhật người dùng
        public async Task<ApiResult<bool>> Update(Guid UserId, UserUpdateRequest request)
        {
            try
            {
                if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != UserId))
                {
                    return new ApiErrorResult<bool>("Email is available!");
                }

                var user = await _userManager.FindByIdAsync(UserId.ToString());
           
                user.Email = request.Email;
                user.Name = request.Name;
                user.PhoneNumber = request.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new ApiSuccessResult<bool>();
                }
                return new ApiErrorResult<bool>("Update Unsuccessful.");
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<bool>("Error System: " + e.Message);
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
                    return new ApiErrorResult<UserViewModel>("User is not exist!");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var userVm = _mapper.Map<UserViewModel>(user);

                userVm.Roles = roles;

                return new ApiSuccessResult<UserViewModel>("Get Info User Successful!",userVm);
            }

            catch (FakeNewsException e)
            {
                return new ApiErrorResult<UserViewModel>("Error System: " + e.Message);
            }

        }

        //Xoá người dùng
        public async Task<ApiResult<bool>> Delete(String UserId)
        {
            try {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null)
                {
                    return new ApiErrorResult<bool>("User does not exists!");
                }
                var reult = await _userManager.DeleteAsync(user);
                if (reult.Succeeded)
                    return new ApiSuccessResult<bool>("Delete Successfull!",true);

                return new ApiErrorResult<bool>("Delete Unsuccessfull!");
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<bool>("Error System: " + e.Message);
            }
        }

        //Gán quyền người dùng
        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
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

            return new ApiSuccessResult<bool>();
        }
    }
}

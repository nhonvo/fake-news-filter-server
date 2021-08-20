﻿using System;
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

namespace FakeNewsFilter.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly RoleManager<Role> _roleManager;

        private readonly IConfiguration _config;

        private readonly IMapper _mapper;


        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IConfiguration config, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _mapper = mapper;
        }
        

        public async Task<string> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

            if (!result.Succeeded)
            {
                return null;
            }

            var roles = _userManager.GetRolesAsync(user);

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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<bool> Register(RegisterRequest request)
        {
            var user = new User()
            {
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if(result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)
                 || x.Email.Contains(request.Keyword));
            }
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.pageIndex - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(x => new UserViewModel()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FullName = x.Name,
                    UserId = x.Id,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };

            return pagedResult;
        }
    }
}

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using FakeNewsFilter.ClientServices;
using Microsoft.Extensions.Configuration;
using FakeNewsFilter.ViewModel.System.Users;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace FakeNewsFilter.UserApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApi _userApi;
        private readonly IConfiguration _configuration;

        public UserController(IUserApi userApi, IConfiguration configuration)
        {
            _userApi = userApi;
            _configuration = configuration;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ModelState = ModelState;
                    return View();
                }

                var result = await _userApi.Authenticate(request);

                if (string.IsNullOrEmpty(result.ResultObj?.Token))
                {
                    ViewBag.Error = result.Message;
                    return View();
                }

                var userPrincipal = this.ValidateToken(result.ResultObj.Token);

                // Gets list of claims.
                IEnumerable<Claim> claim = userPrincipal.Claims;

                var role = claim
                    .Where(x => x.Type == ClaimTypes.Role)
                    .FirstOrDefault();

                if (role.Value.Contains("Subscriber"))
                {

                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        IsPersistent = false
                    };

                    HttpContext.Session.SetString("Token", result.ResultObj.Token);

                    await HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                userPrincipal,
                                authProperties);

                    return RedirectToAction("Index", "HomePage");
                }
                else
                {
                    ViewBag.Error = "You don't have role to access ";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return View();
            }
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "HomePage");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ModelState = ModelState;
                    return View();
                }

                var result = await _userApi.RegisterUser(request);

                return RedirectToAction("Login", "User");
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return View();
            }

        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}

    using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.System.Users;
using FakeNewsFilter.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.AdminApp.Controllers
{

    public class LoginController : Controller
    {
        private readonly IUserApi _userApi;
        private readonly IConfiguration _configuration;

        public LoginController(IUserApi userApi, IConfiguration configuration)
        {
            _userApi = userApi;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
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

                if (role.Value.Contains("Admin"))
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

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "You don't have role to access ";
                    return View();
                }
            }catch(Exception ex)
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

            return RedirectToAction("Index", "Login");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Application.System;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.System.LoginSocial;
using FakeNewsFilter.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IStringLocalizer<UsersController> _localizer;
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;


        public UsersController(IUserService userService, IStringLocalizer<UsersController> localizer, ILogger<UsersController> logger)
        {
            _userService = userService;
            _localizer = localizer;
            _logger = logger;
        }

        //Phương thức thêm Token vào Cookie
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var response = await _userService.RefreshTokenAsync(refreshToken);

            if (string.IsNullOrEmpty(response.ResultObj?.Token))
            {
                _logger.LogError(response.Message);
                return BadRequest(response);
            }
            //Cập nhật lại Cookies
            SetRefreshTokenInCookie(response.ResultObj.Token);
            
            return Ok(response);
        } 

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]LoginRequest request)
        {
            try
            {
                LoginRequestUserValidator validator = new LoginRequestUserValidator(_localizer);

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(400, errors);

                    return BadRequest(result);
                }

                var resultToken = await _userService.Authencate(request);

                resultToken.Message = _localizer[resultToken.Message].Value;

                if (string.IsNullOrEmpty(resultToken.ResultObj?.Token))
                {
                    _logger.LogError(resultToken.Message);
                    return BadRequest(resultToken);
                }

                //Lưu Token vào Cookie
                SetRefreshTokenInCookie(resultToken.ResultObj.Token);

                _logger.LogInformation(resultToken.Message);
                return Ok(resultToken);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                RegisterRequestUserValidator validator = new RegisterRequestUserValidator(_localizer);

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(400, errors);

                    return BadRequest(result);
                }

                var resultToken = await _userService.Register(request);

                resultToken.Message = _localizer[resultToken.Message].Value;

                if (resultToken.StatusCode != 200)
                {
                    _logger.LogError(resultToken.Message);
                    return BadRequest(resultToken);

                }
                _logger.LogInformation(resultToken.Message);
                return Ok(resultToken);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }
 
        [HttpGet("List")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPaging()
        {
            var users = await _userService.GetUsers();

            users.Message = _localizer[users.Message].Value;

            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);

            user.Message = _localizer[user.Message].Value;

            return Ok(user);
        }

        

        //PUT: http://localhost/api/users/id
        [HttpPut()]
        public async Task<IActionResult> Update([FromForm] UserUpdateRequest request)
        {
            try
            {
                UpdateRequestUserValidator validator = new UpdateRequestUserValidator(_localizer);

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(400, errors);

                    return BadRequest(result);
                }

                var resultToken = await _userService.Update(request);

                resultToken.Message = _localizer[resultToken.Message].Value;

                if (resultToken.StatusCode != 200)
                {
                    _logger.LogError(resultToken.Message);
                    return BadRequest(resultToken);
                }
                _logger.LogInformation(resultToken.Message);
                return Ok(resultToken);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Delete(String id)
        {
            try
            {
                var result = await _userService.Delete(id);

                result.Message = _localizer[result.Message].Value;

                if (result.StatusCode != 200)
                {
                    _logger.LogError(result.Message);
                    return BadRequest(result);
                }
                _logger.LogInformation(result.Message);
                return Ok(result);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _userService.RoleAssign(id, request);

                result.Message = _localizer[result.Message].Value;

                if (result.StatusCode != 200)
                {
                    _logger.LogError(result.Message);
                    return BadRequest(result);
                }
                _logger.LogInformation(result.Message);
                return Ok(result);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpPost("SignInFacebook")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInFacebook([FromBody] LoginSocialRequest request)
        {
            try
            {
                var authReponse = await _userService.SignInFacebook(request.AccessToken);

                authReponse.Message = _localizer[authReponse.Message].Value;

                if (string.IsNullOrEmpty(authReponse.ResultObj?.Token))
                {
                    _logger.LogError(authReponse.Message);
                    return BadRequest(authReponse);
                }
                _logger.LogInformation(authReponse.Message);
                return Ok(authReponse);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }


        }

        [HttpPost("SignInGoogle")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInGoogle([FromBody] LoginSocialRequest request)
        {
            try
            {
                var authReponse = await _userService.SignInGoogle(request.AccessToken);

                authReponse.Message = _localizer[authReponse.Message].Value;

                if (string.IsNullOrEmpty(authReponse.ResultObj?.Token))
                {
                    _logger.LogError(authReponse.Message);
                    return BadRequest(authReponse);
                }

                _logger.LogInformation(authReponse.Message);
                return Ok(authReponse);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }


        }

        [HttpPost("SendPasswordResetCode")]
        [AllowAnonymous]
        public async Task<IActionResult> SendPasswordResetCode(string email)
        {
            var result = await _userService.SendPasswordResetCode(email);

            result.Message = _localizer[result.Message].Value;

            if (string.IsNullOrEmpty(result.ResultObj?.Token))
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string email, string otp, string newPassword)
        {
            try
            {
                var result = await _userService.ResetPassword(email, otp, newPassword);

                result.Message = _localizer[result.Message].Value;

                if (string.IsNullOrEmpty(result.ResultObj?.Token))
                {
                    _logger.LogError(result.Message);
                    return BadRequest(result);
                }
                _logger.LogInformation(result.Message);
                return Ok(result);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }


        }
    }
}

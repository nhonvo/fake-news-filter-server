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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IStringLocalizer<UsersController> _localizer;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, IStringLocalizer<UsersController> localizer)
        {
            _userService = userService;
            _localizer = localizer;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]LoginRequest request)
        {
            LoginRequestUserValidator validator = new LoginRequestUserValidator();

            List<string> ValidationMessages = new List<string>();

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                var result = new ApiErrorResult<bool>(errors);

                return BadRequest(result);
            }

            var resultToken = await _userService.Authencate(request);

                resultToken.Message = _localizer[resultToken.Message].Value;

                if (string.IsNullOrEmpty(resultToken.ResultObj?.Token))
                    {
                        return BadRequest(resultToken);
                    }

                return Ok(resultToken);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            RegisterRequestUserValidator validator = new RegisterRequestUserValidator();

            List<string> ValidationMessages = new List<string>();

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                var result = new ApiErrorResult<bool>(errors);

                return BadRequest(result);
            }

            var resultToken = await _userService.Register(request);

            resultToken.Message = _localizer[resultToken.Message].Value;

            if (!resultToken.IsSuccessed)
            {
                return BadRequest(resultToken);

            }
            return Ok(resultToken);
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
            UpdateRequestUserValidator validator = new UpdateRequestUserValidator();

            List<string> ValidationMessages = new List<string>();

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                var result = new ApiErrorResult<bool>(errors);

                return BadRequest(result);
            }

            var resultToken = await _userService.Update(request);

            resultToken.Message = _localizer[resultToken.Message].Value;

            if (!resultToken.IsSuccessed)
            {
                return BadRequest(resultToken);
            }
            return Ok(resultToken);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Delete(String id)
        {
            var result = await _userService.Delete(id);

            result.Message = _localizer[result.Message].Value;

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RoleAssign(id, request);

            result.Message = _localizer[result.Message].Value;

            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SignInFacebook")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInFacebook([FromBody]LoginFacebookRequest request)
        {
            var authReponse = await _userService.SignInFacebook(request.AccessToken);

            authReponse.Message = _localizer[authReponse.Message].Value;

            if (string.IsNullOrEmpty(authReponse.ResultObj?.Token))
            {
                return BadRequest(authReponse);
            }

            return Ok(authReponse);

        }

    }
}

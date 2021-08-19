using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Application.System.Users;
using FakeNewsFilter.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromForm]LoginRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultToken = await _userService.Authencate(request);

            if(string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Username and Password is incorrect.");

            }
            return Ok(new { token = resultToken });
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm]RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Register(request);

            if (!result)
            {
                return BadRequest("Register unsuccessful.");

            }
            return Ok();
        }
    }
}

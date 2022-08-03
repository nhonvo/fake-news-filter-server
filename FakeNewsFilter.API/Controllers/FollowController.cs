using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Follows;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    [Authorize]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _IFollowService;
        private readonly IStringLocalizer<FollowController> _localizer;
        private readonly ILogger<FollowController> _logger;
        public FollowController(IFollowService IFollowService, IStringLocalizer<FollowController> localizer, ILogger<FollowController> logger)
        {
            _IFollowService = IFollowService;
            _localizer = localizer;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FollowCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _IFollowService.Create(request);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFollowTopicByUser(Guid userId)
        {
            var user = await _IFollowService.GetFollowTopicByUser(userId);

            user.Message = _localizer[user.Message].Value;

            return Ok(user);
        }
    }
}
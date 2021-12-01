using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.Follows;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
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

        public FollowController(IFollowService IFollowService, IStringLocalizer<FollowController> localizer)
        {
            _IFollowService = IFollowService;
            _localizer = localizer;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FollowCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _IFollowService.Create(request);

            result.Message = _localizer[result.Message].Value;

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FollowUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _IFollowService.Update(request);

            result.Message = _localizer[result.Message].Value;

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
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
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.Story;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.Validator.Story;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _IStoryService;
        private readonly IStringLocalizer<StoryController> _localizer;

        public StoryController(IStoryService IStoryService, IStringLocalizer<StoryController> localizer)
        {
            _IStoryService = IStoryService;
            _localizer = localizer;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] StoryCreateRequest request)
        {
            CreateRequestStoryValidator validator = new CreateRequestStoryValidator();

            List<string> ValidationMessages = new List<string>();

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                var result = new ApiErrorResult<bool>(errors);

                return BadRequest(result);
            }

            var resultToken = await _IStoryService.Create(request);

            resultToken.Message = _localizer[resultToken.Message].Value;

            if (resultToken.IsSuccessed == false)
            {
                return BadRequest(resultToken);
            }
            return Ok(resultToken);

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromForm] StoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _IStoryService.Update(request);

            result.Message = _localizer[result.Message].Value;

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{StoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int storyId)
        {
            var result = await _IStoryService.Delete(storyId);

            result.Message = _localizer[result.Message].Value;

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{StoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int storyId)
        {
            var topic = await _IStoryService.GetOneStory(storyId);

            topic.Message = _localizer[topic.Message].Value;

            return Ok(topic);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string languageId)
        {
            var topics = await _IStoryService.GetAllStory(languageId);

            topics.Message = _localizer[topics.Message].Value;

            return Ok(topics);
        }
    }
}

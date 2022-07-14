using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Story;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.Validator.Story;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<StoryController> _logger;
        public StoryController(IStoryService IStoryService, IStringLocalizer<StoryController> localizer, ILogger<StoryController> logger)
        {
            _IStoryService = IStoryService;
            _localizer = localizer;
           _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] StoryCreateRequest request)
        {
            try
            {
                CreateRequestStoryValidator validator = new CreateRequestStoryValidator(_localizer);

                List<string> ValidationMessages = new List<string>();

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(errors);

                    return BadRequest(result);
                }

                var resultToken = await _IStoryService.Create(request);

                resultToken.Message = _localizer[resultToken.Message].Value + resultToken.ResultObj;

                if (resultToken.IsSuccessed == false)
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

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromForm] StoryUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _IStoryService.Update(request);

                result.Message = _localizer[result.Message].Value + result.ResultObj; 

                if (result.ResultObj != null)
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

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Archive([FromForm] StoryUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _IStoryService.Archive(request);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                if (result.ResultObj != null)
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

        [HttpDelete("{StoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int storyId)
        {
            try
            {
                var result = await _IStoryService.Delete(storyId);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                if (result.IsSuccessed == false)
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

        [HttpGet("{StoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int storyId)
        {
            var story = await _IStoryService.GetOneStory(storyId);

            story.Message = _localizer[story.Message].Value;

            return Ok(story);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string languageId)
        {
            var story = await _IStoryService.GetAllStory(languageId);

            story.Message = _localizer[story.Message].Value;

            return Ok(story);
        }
    }
}

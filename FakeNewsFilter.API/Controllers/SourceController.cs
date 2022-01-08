using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.SourceStory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SourceController : ControllerBase
    {
        private readonly IScourceService _IScourceStoryService;
        private readonly IStringLocalizer<SourceController> _localizer;
        private readonly ILogger<SourceController> _logger;
        public SourceController(IScourceService IScourceStoryService, IStringLocalizer<SourceController> localizer, ILogger<SourceController> logger)
        {
            _IScourceStoryService = IScourceStoryService;
            _localizer = localizer;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] SourceCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _IScourceStoryService.Create(request);

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

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] SourceUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _IScourceStoryService.Update(request);

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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string languageId)
        {
            var topics = await _IScourceStoryService.GetAll(languageId);

            topics.Message = _localizer[topics.Message].Value;

            return Ok(topics);
        }

        [HttpDelete("{SourceId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int sourceid)
        {
            try
            {
                var result = await _IScourceStoryService.Delete(sourceid);

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

        [HttpGet("{SourceId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int sourceid)
        {
            var news = await _IScourceStoryService.GetoneStory(sourceid);

            news.Message = _localizer[news.Message].Value;

            if (news == null)
            {
                return NotFound(news);
            }
            return Ok(news);
        }
    }
}

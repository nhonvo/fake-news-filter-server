using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.SourceStory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
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

        public SourceController(IScourceService IScourceStoryService, IStringLocalizer<SourceController> localizer)
        {
            _IScourceStoryService = IScourceStoryService;
            _localizer = localizer;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] SourceCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _IScourceStoryService.Create(request);

            result.Message = _localizer[result.Message].Value;

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] SourceUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _IScourceStoryService.Update(request);

            result.Message = _localizer[result.Message].Value;

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
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
            var result = await _IScourceStoryService.Delete(sourceid);

            result.Message = _localizer[result.Message].Value;

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
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

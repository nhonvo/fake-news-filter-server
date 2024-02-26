using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.SourceStory;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SourceController : ReturnStatus
    {
        private readonly ISourceService _IScourceStoryService;
        private readonly IStringLocalizer<SourceController> _localizer;
        private readonly ILogger<SourceController> _logger;
        public SourceController(ISourceService IScourceStoryService, IStringLocalizer<SourceController> localizer, ILogger<SourceController> logger) : base(logger)
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

                return ReturnWithModel(result);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpPut("Update")]
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

                return ReturnWithModel(result);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Archive([FromBody] SourceUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _IScourceStoryService.Archive(request);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                return ReturnWithModel(result);
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
            var result = await _IScourceStoryService.GetAll(languageId);

            result.Message = _localizer[result.Message].Value;

            return ReturnWithListModel(result);
        }

        [HttpDelete("{SourceId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int sourceid)
        {
            try
            {
                var result = await _IScourceStoryService.Delete(sourceid);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                return ReturnWithModel(result);
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
            var result = await _IScourceStoryService.GetAStory(sourceid);

            result.Message = _localizer[result.Message].Value;

            return ReturnWithModel(result);
        }
    }
}

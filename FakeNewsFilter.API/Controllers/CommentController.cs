using FakeNewsFilter.API.Contract;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.Comment;
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
    public class CommentController : Controller
    {
        private readonly ICommentService _ICommentService;
        private readonly IStringLocalizer<CommentController> _localizer;
        private readonly ILoggerService _loggerService;
        public CommentController(ICommentService ICommentService, IStringLocalizer<CommentController> localizer, ILoggerService loggerService)
        {
            _ICommentService = ICommentService;
            _localizer = localizer;
            _loggerService = loggerService;
        }

        

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Create([FromBody] CommentCreateRequest request)
        {
            _loggerService.LogInfo("Test logging");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _ICommentService.Create(request);

            result.Message = _localizer[result.Message].Value;

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{newsId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCommentByNewsId(int newsId)
        {
            _loggerService.LogDebug("Test logging");

            var comments = await _ICommentService.GetCommentByNewsId(newsId);

            comments.Message = _localizer[comments.Message].Value;
            
            return Ok(comments);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int commentId, Guid userId)
        {
            var result = await _ICommentService.Delete(commentId, userId);

            result.Message = _localizer[result.Message].Value;

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CommentUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _ICommentService.Update(request);

            result.Message = _localizer[result.Message].Value;

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}

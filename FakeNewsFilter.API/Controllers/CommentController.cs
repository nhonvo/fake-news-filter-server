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
        public CommentController(ICommentService ICommentService, IStringLocalizer<CommentController> localizer)
        {
            _ICommentService = ICommentService;
            _localizer = localizer;
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Create([FromBody] CommentCreateRequest request)
        {
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
            var news = await _ICommentService.GetCommentByNewsId(newsId);

            news.Message = _localizer[news.Message].Value;

            if (news == null)
            {
                return NotFound(news);
            }
            return Ok(news);
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

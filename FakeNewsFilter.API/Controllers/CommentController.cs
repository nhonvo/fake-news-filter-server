using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Comment;
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
    public class CommentController : Controller
    {
        private readonly ICommentService _ICommentService;
        private readonly IStringLocalizer<CommentController> _localizer;
        private readonly ILogger<CommentController> _logger;
        public CommentController(ICommentService ICommentService, IStringLocalizer<CommentController> localizer, ILogger<CommentController> logger)
        {
            _ICommentService = ICommentService;
            _localizer = localizer;
            _logger = logger;
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CommentCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _ICommentService.Create(request);

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

        [HttpGet("{newsId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCommentByNewsId(int newsId)
        {

            var comments = await _ICommentService.GetCommentByNewsId(newsId);

            comments.Message = _localizer[comments.Message].Value;
            
            return Ok(comments);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int commentId, Guid userId)
        {
            try
            {
                var result = await _ICommentService.Delete(commentId, userId);

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
        public async Task<IActionResult> Update([FromBody] CommentUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _ICommentService.Update(request);

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
        public async Task<IActionResult> Archive([FromBody] CommentUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _ICommentService.Archive(request);

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
    }
}

using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Comment;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ReturnStatus
    {
        private readonly ICommentService _ICommentService;
        private readonly IStringLocalizer<CommentController> _localizer;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentService ICommentService, IStringLocalizer<CommentController> localizer,
            ILogger<CommentController> logger) : base(logger)
        {
            _ICommentService = ICommentService;
            _localizer = localizer;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _ICommentService.Create(request);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                return ReturnWithModel(result);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("news/{newsId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCommentByNewsId(int newsId)
        {
            var comments = await _ICommentService.GetCommentByNewsId(newsId);

            comments.Message = _localizer[comments.Message].Value;

            return ReturnWithListModel(comments);
        }

        [HttpGet("{cmtId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCommentById(int cmtId)
        {
            var comment = await _ICommentService.GetCommentById(cmtId);

            comment.Message = _localizer[comment.Message].Value;

            return ReturnWithModel(comment);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int commentId, Guid userId)
        {
            try
            {
                var result = await _ICommentService.Delete(commentId, userId);

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

                return ReturnWithModel(result);
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

                return ReturnWithModel(result);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
using FakeNewsFilter.API.Validator.NewsCommunity;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.NewsCommunity;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NewsCommunityController : ReturnStatus
    {
        private readonly INewsCommunityService _NewsCommunityService;
        private readonly IStringLocalizer<NewsCommunityController> _localizer;
        private readonly IFollowService _followService;
        private readonly ILogger<NewsCommunityController> _logger;
        public NewsCommunityController(INewsCommunityService NewsCommunityService, IFollowService followService, IStringLocalizer<NewsCommunityController> localizer, ILogger<NewsCommunityController> logger) : base(logger)
        {
            _NewsCommunityService = NewsCommunityService;
            _followService = followService;
            _localizer = localizer;
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NewsCommunityCreateRequest request)
        {
            try
            {
                CreateRequestNewsCommunityValidator validator = new CreateRequestNewsCommunityValidator(_localizer);

                List<string> ValidationMessages = new List<string>();

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(400, errors);

                    return BadRequest(result);
                }

                var createNews = await _NewsCommunityService.Create(request);

                createNews.Message = _localizer[createNews.Message].Value + createNews.ResultObj;

                return ReturnWithModel(createNews);

                var getNews = await _NewsCommunityService.GetById(Int32.Parse(createNews.ResultObj.ToString()));

                getNews.Message = _localizer[getNews.Message].Value;

                _logger.LogInformation(createNews.Message);
                return CreatedAtAction(nameof(GetById), new { NewsCommunityId = createNews }, getNews);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{newsCommunityId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int newsCommunityId)
        {
            var news = await _NewsCommunityService.GetById(newsCommunityId);

            news.Message = _localizer[news.Message].Value;

            if (news == null)
            {
                return NotFound(news);
            }
            return Ok(news);
        }

        [HttpDelete("{newsCommunityId}")]
        public async Task<IActionResult> Delete(int newsCommunityId)
        {
            try
            {
                var result = await _NewsCommunityService.Delete(newsCommunityId);

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
        public async Task<IActionResult> Update([FromForm] NewsCommunityUpdateRequest request)
        {
            try
            {
                UpdateRequestNewsCommunityValidator validator = new UpdateRequestNewsCommunityValidator(_localizer);

                List<string> ValidationMessages = new List<string>();

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(400, errors);

                    return BadRequest(result);
                }

                var resultToken = await _NewsCommunityService.Update(request);

                resultToken.Message = _localizer[resultToken.Message].Value + resultToken.ResultObj;

                return ReturnWithModel(resultToken);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpPatch("link/{newsCommunityId}/{newLink}")]
        public async Task<IActionResult> UpdateLink(int newsCommunityId, string newLink)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _NewsCommunityService.UpdateLink(newsCommunityId, newLink);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                return ReturnWithModel(result);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        // GET: api/news
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetNews(string languageId)
        {
            var topics = await _NewsCommunityService.GetAll(languageId);

            topics.Message = _localizer[topics.Message].Value;

            return ReturnWithListModel(topics); 
        }

        [HttpGet("User")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewsInTopic([FromQuery] Guid userId)
        {
            var newsintopics = await _NewsCommunityService.GetNewsByUserId(userId);

            newsintopics.Message = _localizer[newsintopics.Message].Value;

            return ReturnWithListModel(newsintopics);
        }

        
        [HttpPut]
        public async Task<IActionResult> Archive(int id)
        {
            try
            {
                var resultToken = await _NewsCommunityService.Archive(id);

                resultToken.Message = _localizer[resultToken.Message].Value;

                return ReturnWithModel(resultToken);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }
    }
}

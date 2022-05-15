using System;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.Validator.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FakeNewsFilter.Utilities.Exceptions;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IStringLocalizer<NewsController> _localizer;
        private readonly IFollowService _followService;
        private readonly ILogger<NewsController> _logger;
        public NewsController(INewsService newsService, IFollowService followService, IStringLocalizer<NewsController> localizer, ILogger<NewsController> logger)
        {
            _newsService = newsService;
            _followService = followService;
            _localizer = localizer;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] NewsCreateRequest request)
        {
            try
            {
                CreateRequestNewsValidator validator = new CreateRequestNewsValidator(_localizer);

                List<string> ValidationMessages = new List<string>();

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(errors);

                    return BadRequest(result);
                }

                var createNews = await _newsService.Create(request);

                createNews.Message = _localizer[createNews.Message].Value + createNews.ResultObj;

                if (createNews.IsSuccessed == false)
                {
                    _logger.LogError(createNews.Message);
                    return BadRequest(createNews);
                }
                
                var getNews = await _newsService.GetById(Int32.Parse(createNews.ResultObj));

                getNews.Message = _localizer[getNews.Message].Value;

                _logger.LogInformation(createNews.Message);
                return CreatedAtAction(nameof(GetById), new { newsId = createNews }, getNews);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{newsId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int newsId)
        {
            try
            {
                var result = await _newsService.Delete(newsId);

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

        // GET: api/news
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetNews(string languageId, string filter)
        {
            var topics = await _newsService.GetAll(languageId, filter);

            topics.Message = _localizer[topics.Message].Value;

            return Ok(topics);
        }

        [HttpGet("{newsId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int newsId)
        {
            var news = await _newsService.GetById(newsId);

            news.Message = _localizer[news.Message].Value;

            if (news == null)
            {
                return NotFound(news);
            }
            return Ok(news);
        }

        [HttpGet("Content")]
        [AllowAnonymous]
        public async Task<IActionResult> GetContent(int newsId)
        {
            var content = await _newsService.GetContent(newsId);

            // news.Message = _localizer[news.Message].Value;

            // if (news == null)
            // {
            //     return NotFound(news);
            // }
            return Ok(content);
        }
        
        // GET: api/news/topic
        [HttpGet("Topic")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewsInTopic([FromQuery] int topicId)
        {
            var newsintopics = await _newsService.GetNewsInTopic(topicId);

            newsintopics.Message = _localizer[newsintopics.Message].Value;

            return Ok(newsintopics);
        }

        [HttpGet("FollowedTopic")]
        public async Task<IActionResult> GetNewsByFollowedTopic(Guid userId)
        {
            var topics = await _followService.GetFollowTopicByUser(userId);

            if (!topics.IsSuccessed)
            {
                return NotFound(topics);
            }

            var newsList = await _newsService.GetNewsByFollowedTopic(topics.ResultObj, userId);

            newsList.Message = _localizer[newsList.Message].Value;

            return Ok(newsList);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromForm] NewsUpdateRequest request)
        {
            try
            {
                UpdateRequestNewsValidator validator = new UpdateRequestNewsValidator(_localizer);

                List<string> ValidationMessages = new List<string>();

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(errors);

                    return BadRequest(result);
                }

                var resultToken = await _newsService.Update(request);

                resultToken.Message = _localizer[resultToken.Message].Value + resultToken.ResultObj;

                if (resultToken.ResultObj != null)
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

        [HttpPatch("link/{newsId}/{newLink}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateLink(int newsId, string newLink)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _newsService.UpdateLink(newsId, newLink);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                if (result.ResultObj == null)
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

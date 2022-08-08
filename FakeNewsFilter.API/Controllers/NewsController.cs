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
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Quartz;
using StackExchange.Redis;
using FakeNewsFilter.API.Validator.News;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NewsController : ControllerBase, IJob
    {
        private readonly IDistributedCache _distributedCache;
        private readonly INewsService _newsService;
        private readonly IStringLocalizer<NewsController> _localizer;
        private readonly IFollowService _followService;
        private readonly ILogger<NewsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IConnectionMultiplexer _redis;

        public NewsController(IDistributedCache distributedCache, INewsService newsService,
            IFollowService followService, IStringLocalizer<NewsController> localizer, ILogger<NewsController> logger,
            IConnectionMultiplexer redis, IConfiguration configuration)
        {
            _distributedCache = distributedCache;
            _newsService = newsService;
            _followService = followService;
            _localizer = localizer;
            _logger = logger;
            _configuration = configuration;
            _redis = redis;
        }

        [HttpPost("CreateBySystem")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBySytem([FromForm] NewsSystemCreateRequest request)
        {
            try
            {
                CreateSystemNewsValidator validator = new CreateSystemNewsValidator(_localizer);

                List<string> ValidationMessages = new List<string>();

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(400, errors);

                    return BadRequest(result);
                }

                var createNews = await _newsService.CreateBySystem(request);

                createNews.Message = _localizer[createNews.Message].Value + createNews.ResultObj;

                if (createNews.StatusCode != 200)
                {
                    _logger.LogError(createNews.Message);
                    return BadRequest(createNews);
                }

                var getNews = await _newsService.GetById(Int32.Parse(createNews.ResultObj));

                getNews.Message = _localizer[getNews.Message].Value;

                _logger.LogInformation(createNews.Message);
                return CreatedAtAction(nameof(GetById), new {newsId = createNews}, getNews);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreateByOutSource")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateByOutSource([FromForm] NewsOutSourceCreateRequest request)
        {
            try
            {
                CreateOutSourceNewsValidator validator = new CreateOutSourceNewsValidator(_localizer);

                List<string> ValidationMessages = new List<string>();

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(400, errors);

                    return BadRequest(result);
                }

                var createNews = await _newsService.CreateByOther(request);

                createNews.Message = _localizer[createNews.Message].Value + createNews.ResultObj;

                if (createNews.StatusCode != 200)
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
            try
            {
                var news = await _newsService.GetAll(languageId, filter);

                news.Message = _localizer[news.Message].Value;

                if (news.StatusCode == 200)
                {
                    return Ok(news);
                }
                return BadRequest(news);

            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            
        }

        //Lấy các tin tức dựa trên nguồn tạo (từ hệ thống/ nguồn bên ngoài)
        [HttpGet("Source")]
        public async Task<IActionResult> GetNewsBySource(string source)
        {
            var topics = await _newsService.GetBySouce(source);

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

        [HttpGet("views/{newsId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetViewCount(int newsId)
        {
            var news = await _newsService.GetViewCount(newsId);

            if (news == null)
            {
                return NotFound(news);
            }

            // news.Message = _localizer[news.Message].Value;
            var cacheKey = "view_count_news_" + newsId;
            var newsCached = _distributedCache.GetString(cacheKey);
            if (newsCached == null)
            {
                await _distributedCache.SetStringAsync(cacheKey, "1");
                news.ResultObj = 1;
            }
            else
            {
                var viewCount = Int32.Parse(newsCached);
                viewCount++;
                _distributedCache.SetString(cacheKey, viewCount.ToString());
                news.ResultObj = viewCount;
            }

            return Ok(news);
        }

        [HttpGet("content/{newsId}")]
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

            if (topics.StatusCode != 200)
            {
                return NotFound(topics);
            }

            var newsList = await _newsService.GetNewsByFollowedTopic(topics.ResultObj, userId);

            newsList.Message = _localizer[newsList.Message].Value;

            return Ok(newsList);
        }

        [HttpPut("UpdateBySystem")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBySystem([FromForm] NewsSystemUpdateRequest request)
        {
            try
            {
                UpdateSystemNewsValidator validator = new UpdateSystemNewsValidator(_localizer);

                List<string> ValidationMessages = new List<string>();

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(400, errors);

                    return BadRequest(result);
                }

                var resultToken = await _newsService.UpdateBySystem(request);

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

        [HttpPut("UpdateByOutSource")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateByOutSource([FromForm] NewsOutSourceUpdateRequest request)
        {
            try
            {
                UpdateOutSourceNewsValidator validator = new UpdateOutSourceNewsValidator(_localizer);

                List<string> ValidationMessages = new List<string>();

                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                    var result = new ApiErrorResult<bool>(400, errors);

                    return BadRequest(result);
                }

                var resultToken = await _newsService.UpdateByOutSource(request);

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

        public Dictionary<int, int> GetKeysAndValuesInRedisCache()
        {
            var dict = new Dictionary<int, int>();
            string connectionString = _configuration["Redis:ConnectionString"];
            var redisServer = _redis.GetServer(connectionString);
            var keys = redisServer.Keys();

            foreach (var key in keys)
            {
                //get newsId from key
                var keySplit = key.ToString().Split('_');
                var newsId = Int32.Parse(keySplit[^1]);

                var value = _distributedCache.GetString(key);
                dict.Add(newsId, Int32.Parse(value));
            }

            return dict;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var newsViewCountDict = GetKeysAndValuesInRedisCache();
            await _newsService.UpdateViewCount(newsViewCountDict);
        }

        [HttpPut("Archive")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Archive(int request)
        {
            try
            {
                var resultToken = await _newsService.Archive(request);

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
    }
}
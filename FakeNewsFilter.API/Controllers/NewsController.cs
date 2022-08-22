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
using Microsoft.AspNetCore.Http;
using Google.Apis.Http;
using System.Configuration;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NewsController : ReturnStatus, IJob
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
            IConnectionMultiplexer redis, IConfiguration configuration ) : base(logger)
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

                createNews.Message = _localizer[createNews.Message].Value;

                _logger.LogInformation(createNews.Message);

                return ReturnWithModel(createNews);
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

                createNews.Message = _localizer[createNews.Message].Value;


                _logger.LogInformation(createNews.Message);

                return ReturnWithModel(createNews);
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

                return ReturnWithListModel(news);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaging(GetManageNewsRequest request)
        {
            try
            {
                var news = await _newsService.GetNewsPaging(request);

                news.Message = _localizer[news.Message].Value;

                if (news.StatusCode != 200)
                {
                    return BadRequest(news);
                }

                return Ok(news);
            }
            catch (FakeNewsException e)
            {
                return BadRequest(e.Message);
            }
        }


        //Lấy các tin tức dựa trên nguồn tạo (từ hệ thống/ nguồn bên ngoài)
        [HttpGet("Source")]
        public async Task<IActionResult> GetNewsBySource(string source)
        {
            var news = await _newsService.GetBySouce(source);

            news.Message = _localizer[news.Message].Value;

            return ReturnWithListModel(news);
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
            var newsApiResult = await _newsService.GetById(newsId);

            newsApiResult.Message = _localizer[newsApiResult.Message].Value;

            if (newsApiResult.StatusCode != 200)
            {
                return BadRequest(newsApiResult);
            }

            var news = newsApiResult.ResultObj;

            var cacheKey = "viewcountnews_" + newsId;
            var newsCached = await _distributedCache.GetStringAsync(cacheKey);
            if (newsCached == null)
            {
                if (news.ViewCount > 0)
                {
                    var newViewCount = news.ViewCount + 1;
                    await _distributedCache.SetStringAsync(cacheKey, newViewCount.ToString());
                    news.ViewCount = newViewCount;
                }
                else
                {
                    await _distributedCache.SetStringAsync(cacheKey, "1");
                    news.ViewCount = 1;
                }
            }
            else
            {
                var viewCount = Int32.Parse(newsCached);
                viewCount++;
                _distributedCache.SetString(cacheKey, viewCount.ToString());
                news.ViewCount = viewCount;
            }

            return Ok(newsApiResult);
        }

        [HttpGet("content/{newsId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetContent(int newsId)
        {
            var content = await _newsService.GetContent(newsId);

            content.Message = _localizer[content.Message].Value;

            if (content.StatusCode != 200)
            {
                return NotFound(content);
            }

            return Ok(content);
        }

        // GET: api/news/topic
        [HttpGet("Topic")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewsInTopic([FromQuery] int topicId)
        {
            var newsintopics = await _newsService.GetNewsInTopic(topicId);

            newsintopics.Message = _localizer[newsintopics.Message].Value;

            return ReturnWithListModel(newsintopics);
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

            return ReturnWithListModel(newsList);
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

                resultToken.Message = _localizer[resultToken.Message].Value;

                return ReturnWithModel(resultToken);
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

                resultToken.Message = _localizer[resultToken.Message].Value;

                return ReturnWithModel(resultToken);
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

                result.Message = _localizer[result.Message].Value;

                return ReturnWithModel(result);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        public Dictionary<int, int> GetKeysAndValuesInRedisCache(string preKey)
        {
            var dict = new Dictionary<int, int>();
            string connectionString = _configuration["Redis:ConnectionString"];
            var redisServer = _redis.GetServer(connectionString);
            var keys = redisServer.Keys();

            foreach (var key in keys)
            {
                var keySplit = key.ToString().Split('_');

                if (keySplit[0].Equals(preKey))
                {
                    var newsId = Int32.Parse(keySplit[^1]);

                    var value = _distributedCache.GetString(key);
                    dict.Add(newsId, Int32.Parse(value));
                }  
            }

            return dict;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var newsViewCountDict = GetKeysAndValuesInRedisCache("viewcountnews");
            await _newsService.UpdateViewCount(newsViewCountDict);
        }

        [HttpPut("Archive/{newsId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Archive(int newsId)
        {
            try
            {
                var resultToken = await _newsService.Archive(newsId);

                resultToken.Message = _localizer[resultToken.Message].Value;

                return ReturnWithModel(resultToken);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        //[HttpDelete("{newsId}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Delete(int newsId)
        //{
        //    try
        //    {
        //        var result = await _newsService.Delete(newsId);

        //        result.Message = _localizer[result.Message].Value + result.ResultObj;

        //        return ResultStatusString(result);
        //    }
        //    catch (FakeNewsException e)
        //    {
        //        _logger.LogError(e.Message);
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
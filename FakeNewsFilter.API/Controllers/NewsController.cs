using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IStringLocalizer<NewsController> _localizer;

        public NewsController(INewsService newsService, IStringLocalizer<NewsController> localizer)
        {
            _newsService = newsService;
            _localizer = localizer;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] NewsCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createNews = await _newsService.Create(request);

            createNews.Message = _localizer[createNews.Message].Value;

            if (createNews.IsSuccessed == false)
            {
                return BadRequest(createNews);
            }
            
            var getNews = await _newsService.GetById(createNews.ResultObj);

            getNews.Message = _localizer[getNews.Message].Value;
            
            return CreatedAtAction(nameof(GetById), new { newsId = createNews }, getNews);
        }

        [HttpDelete("{newsId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int newsId)
        {
            var result = await _newsService.Delete(newsId);

            result.Message = _localizer[result.Message].Value;

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET: api/news
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetNews(string languageId)
        {
            var topics = await _newsService.GetAll(languageId);

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

        // GET: api/news/topic
        [HttpGet("Topic")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewsInTopic([FromQuery] int topicId)
        {
            var newsintopics = await _newsService.GetNewsInTopic(topicId);

            newsintopics.Message = _localizer[newsintopics.Message].Value;

            return Ok(newsintopics);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromForm] NewsUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _newsService.Update(request);

            result.Message = _localizer[result.Message].Value;

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("link/{newsId}/{newLink}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateLink(int newsId, string newLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _newsService.UpdateLink(newsId, newLink);

            result.Message = _localizer[result.Message].Value;

            if (result.ResultObj == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
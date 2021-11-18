using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] NewsCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsId = await _newsService.Create(request);

            if (newsId.ResultObj == 0)
            {
                return BadRequest("Cannot create news");
            }

            var news = await _newsService.GetById(newsId.ResultObj);

            return CreatedAtAction(nameof(GetById), new { newsId = newsId }, news);
        }

        [HttpDelete("{newsId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int newsId)
        {
            var result = await _newsService.Delete(newsId);

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

            return Ok(topics);
        }

        [HttpGet("{newsId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int newsId)
        {
            var news = await _newsService.GetById(newsId);

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

            if (result.ResultObj == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
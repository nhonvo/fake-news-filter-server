using FakeNewsFilter.Application.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpPost]
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
        public async Task<IActionResult> GetNews(string languageCode)
        {
            var topics = await _newsService.GetAll(languageCode);

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
        public async Task<IActionResult> GetNewsInTopic([FromQuery] GetPublicNewsRequest request)
        {
            var newsintopics = await _newsService.GetNewsInTopic(request);

            if (newsintopics.ResultObj.Count == 0)
            {
                return NotFound(newsintopics);
            }
            return Ok(newsintopics);
        }

        [HttpPut]
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
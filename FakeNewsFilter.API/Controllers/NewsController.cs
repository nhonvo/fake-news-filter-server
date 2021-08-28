using FakeNewsFilter.Application.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        public async Task<IActionResult> Create([FromForm] NewsCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsId = await _newsService.Create(request);

            if (newsId == 0)
            {
                return BadRequest("Cannot create news");
            }

            var news = await _newsService.GetById(newsId);

            return CreatedAtAction(nameof(GetById), new { newsId = newsId }, news);
        }

        [HttpDelete("{newsId}")]
        public async Task<IActionResult> Delete(int newsId)
        {
            var result = await _newsService.Delete(newsId);

            if (result == 0)
            {
                return BadRequest("Cannot delete this news");
            }
            return Ok("Deleted successfully");
        }

        // GET: api/news
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var topics = await _newsService.GetAll();

            return Ok(topics);
        }

        [HttpGet("{newsId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int newsId)
        {
            var news = await _newsService.GetById(newsId);

            if (news == null)
            {
                return NotFound("Cannot find product");
            }
            return Ok(news);
        }

        // GET: api/news/topic
        [HttpGet("topic")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewsInTopic([FromQuery] GetPublicNewsRequest request)
        {
            var newsintopics = await _newsService.GetNewsInTopic(request);

            if (newsintopics.Count == 0)
            {
                return NotFound($"Cannot find product in topic {request.TopicId}");
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

            if (result == 0)
            {
                return BadRequest("Cannot update this news");
            }
            return Ok("Updated successfully");
        }

        [HttpPatch("link/{newsId}/{newLink}")]
        public async Task<IActionResult> UpdateLink(int newsId, string newLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _newsService.UpdateLink(newsId, newLink);

            if (result == false)
            {
                return BadRequest("Cannot update link this news");
            }
            return Ok("Updated link successfully");
        }
    }
}
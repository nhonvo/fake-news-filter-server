using FakeNewsFilter.Application.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IManageNewsService _manageNewsService;
        private readonly IPublicNewsService _publicNewsService;

        public NewsController(IPublicNewsService publicTopicNewsService, IManageNewsService manageTopicNewsService)
        {
            _publicNewsService = publicTopicNewsService;
            _manageNewsService = manageTopicNewsService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NewsCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsId = await _manageNewsService.Create(request);

            if (newsId == 0)
            {
                return BadRequest("Cannot create news");
            }

            var news = await _manageNewsService.GetById(newsId);

            return CreatedAtAction(nameof(GetById), new { newsId = newsId }, news);
        }

        [HttpDelete("{newsId}")]
        public async Task<IActionResult> Delete(int newsId)
        {
            var result = await _manageNewsService.Delete(newsId);

            if (result == 0)
            {
                return BadRequest("Cannot delete this news");
            }
            return Ok("Deleted successfully");
        }

        // GET: api/news
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var topics = await _publicNewsService.GetAll();

            return Ok(topics);
        }

        [HttpGet("{newsId}")]
        public async Task<IActionResult> GetById(int newsId)
        {
            var news = await _manageNewsService.GetById(newsId);

            if (news == null)
            {
                return NotFound("Cannot find product");
            }
            return Ok(news);
        }

        // GET: api/news/topic
        [HttpGet("topic")]
        public async Task<IActionResult> GetNewsInTopic([FromQuery] GetPublicNewsRequest request)
        {
            var newsintopics = await _publicNewsService.GetNewsInTopic(request);

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

            var result = await _manageNewsService.Update(request);

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
            var result = await _manageNewsService.UpdateLink(newsId, newLink);

            if (result == false)
            {
                return BadRequest("Cannot update link this news");
            }
            return Ok("Updated link successfully");
        }
    }
}
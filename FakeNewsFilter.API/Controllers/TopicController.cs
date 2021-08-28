using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TopicController : ControllerBase
    {
        private readonly TopicService _topicNewsService;

        public TopicController(TopicService topicNewsService)
        {
            _topicNewsService = topicNewsService;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TopicNewsCreateRequest request)
        {
            var result = await _topicNewsService.Create(request);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{topicId}")]
        public async Task<IActionResult> Delete(int topicId)
        {
            var result = await _topicNewsService.Delete(topicId);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET: api/topic
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var topics = await _topicNewsService.GetTopicHotNews();

            if(topics.IsSuccessed == false)
            {
                return BadRequest(topics);
            }
            return Ok(topics);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] TopicNewsUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _topicNewsService.Update(request);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
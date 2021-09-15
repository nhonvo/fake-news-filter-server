using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class TopicController : ControllerBase
    {
        private readonly TopicService _topicService;

        public TopicController(TopicService topicService)
        {
            _topicService = topicService;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]TopicCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _topicService.Create(request);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        
        // GET: api/topic
        [HttpGet("List")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var topics = await _topicService.GetTopicHotNews();

            if(topics.IsSuccessed == false)
            {
                return BadRequest(topics);
            }
            return Ok(topics);
        }

       
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var topic = await _topicService.GetTopicById(Id);
            return Ok(topic);
        }

        [HttpPut("{topicId}")]
        public async Task<IActionResult> Update([FromRoute]int topicId, [FromForm] TopicUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.TopicId = topicId;

            var result = await _topicService.Update(request);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{topicId}")]
        public async Task<IActionResult> Delete(int topicId)
        {
            var result = await _topicService.Delete(topicId);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
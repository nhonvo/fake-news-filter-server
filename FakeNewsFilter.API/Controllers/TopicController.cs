using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.Validator.Topic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TopicController : ControllerBase
    {
        private readonly IStringLocalizer<TopicController> _localizer;
        private readonly TopicService _topicService;

        public TopicController(TopicService topicService, IStringLocalizer<TopicController> localizer)
        {
            _topicService = topicService;
            _localizer = localizer;
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm]TopicCreateRequest request)
        {
            CreateRequestTopicValidator validator = new CreateRequestTopicValidator(_localizer);

            List<string> ValidationMessages = new List<string>();

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                var result = new ApiErrorResult<bool>(errors);

                return BadRequest(result);
            }

            var resultToken = await _topicService.Create(request);

            resultToken.Message = _localizer[resultToken.Message].Value;

            if (resultToken.IsSuccessed == false)
            {
                return BadRequest(resultToken);
            }

            return Ok(resultToken);
        }

        
        // GET: api/topic
        [HttpGet("List")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string languageId)
        {
            var topics = await _topicService.GetTopicHotNews(languageId);

            topics.Message = _localizer[topics.Message].Value;

            if (topics.IsSuccessed == false)
            {
                return BadRequest(topics);
            }
            
            return Ok(topics);

        }


        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int Id)
        {
            var topic = await _topicService.GetTopicById(Id);

            topic.Message = _localizer[topic.Message].Value;

            return Ok(topic);
        }

        [HttpPut("{topicId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute]int topicId, [FromForm] TopicUpdateRequest request)
        {
            UpdateRequestTopicValidator validator = new UpdateRequestTopicValidator(_localizer);

            List<string> ValidationMessages = new List<string>();

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                var resultupdate = new ApiErrorResult<bool>(errors);

                return BadRequest(resultupdate);
            }

            request.TopicId = topicId;

            var result = await _topicService.Update(request);

            result.Message = _localizer[result.Message].Value;

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{topicId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int topicId)
        {
            var result = await _topicService.Delete(topicId);

            result.Message = _localizer[result.Message].Value;

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
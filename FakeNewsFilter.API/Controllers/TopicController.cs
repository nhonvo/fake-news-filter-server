using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.Validator.Topic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TopicController : ControllerBase
    {
        private readonly IStringLocalizer<TopicController> _localizer;
        private readonly TopicService _topicService;
        private readonly ILogger<TopicController> _logger;
        private readonly ApplicationDBContext _context;

        public TopicController(ApplicationDBContext context, TopicService topicService, IStringLocalizer<TopicController> localizer, ILogger<TopicController> logger)
        {
            _context = context;
            _topicService = topicService;
            _localizer = localizer;
            _logger = logger;
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm]TopicCreateRequest request)
        {
            
                try
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

                    resultToken.Message = _localizer[resultToken.Message].Value + resultToken.ResultObj;

                    if (resultToken.IsSuccessed == false)
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

        
        // GET: api/topic
        [HttpGet("List")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string languageId)
        {
            try
            {
                var topics = await _topicService.GetTopicHotNews(languageId);

                topics.Message = _localizer[topics.Message].Value;

                if (topics.IsSuccessed == false)
                {
                    return BadRequest(topics);
                }
                return Ok(topics);
            }
            catch (FakeNewsException e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var topic = await _topicService.GetTopicById(Id);

                topic.Message = _localizer[topic.Message].Value;

                return Ok(topic);
            }
            catch (FakeNewsException e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{topicId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute]int topicId, [FromForm] TopicUpdateRequest request)
        {
            try
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

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                if (result.IsSuccessed == false)
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

        [HttpDelete("{topicId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int topicId)
        {
            try
            {
                var result = await _topicService.Delete(topicId);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                if (result.IsSuccessed == false)
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

    }
}
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;


namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TopicController : ReturnStatus
    {
        private readonly IStringLocalizer<TopicController> _localizer;
        private readonly ITopicService _topicService;
        private readonly ILogger<TopicController> _logger;
        private readonly ApplicationDBContext _context;

        public TopicController(ApplicationDBContext context,
            ITopicService topicService,
            IStringLocalizer<TopicController> localizer,
            ILogger<TopicController> logger)
            : base(logger)
        {
            _context = context;
            _topicService = topicService;
            _localizer = localizer;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] TopicCreateRequest request)
        {
                try
                {
                    CreateRequestTopicValidator validator = new CreateRequestTopicValidator(_localizer);

                    List<string> ValidationMessages = new List<string>();

                    var validationResult = validator.Validate(request);

                    if (!validationResult.IsValid)
                    {
                        string errors = string.Join(" ", validationResult.Errors.Select(x => x.ToString()).ToArray());

                        var result = new ApiErrorResult<bool>(400, errors);

                        return BadRequest(result);
                    }

                    var resultToken = await _topicService.Create(request);

                    resultToken.Message = _localizer[resultToken.Message].Value + resultToken.ResultObj;

                return ReturnWithModel(resultToken);

                }
                catch (FakeNewsException e)
                {
                    _logger.LogError(e.Message);
                    return BadRequest(e.Message);
                }
            
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(string LanguageId)
        {
            try
            {
                var topics = await _topicService.GetAllTopic(LanguageId);

                topics.Message = _localizer[topics.Message].Value;

                return ReturnWithListModel(topics);
            }
            catch (FakeNewsException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/topic
        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaging(GetTopicNewsRequest request)
        {
            try
            {
                var topics = await _topicService.GetTopicPaging(request);

                topics.Message = _localizer[topics.Message].Value;

                return ReturnWithModel(topics);
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

                return ReturnWithModel(topic);
            }
            catch (FakeNewsException e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("Update/{topicId}")]
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

                    var resultupdate = new ApiErrorResult<bool>(400, errors);

                    return BadRequest(resultupdate);
                }

                request.TopicId = topicId;

                var result = await _topicService.Update(request);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                return ReturnWithModel(result);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpPut("Archive/{topicId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Archive(int topicId)
        {
            try
            {
                var result = await _topicService.Archive(topicId);

                result.Message = _localizer[result.Message].Value;

                return ReturnWithModel(result);
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

                return ReturnWithModel(result);
            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

    }
}
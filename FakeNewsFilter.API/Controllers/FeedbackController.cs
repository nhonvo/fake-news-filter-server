using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Feedback;
using FakeNewsFilter.ViewModel.Catalog.Vote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IStringLocalizer<VoteController> _localizer;
        private readonly ILogger<VoteController> _logger;

        public FeedbackController(IFeedbackService feedbackService, IStringLocalizer<VoteController> localizer, ILogger<VoteController> logger)
        {
            _feedbackService = feedbackService;
            _localizer = localizer;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateFeedbackRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _feedbackService.Create(request);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                if (result.StatusCode != 200)
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

        [HttpPost("ReportLinkNews")]
        public async Task<IActionResult> ReportLinkNews([FromBody] CreateFeedbackRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _feedbackService.ReportLinkNews(request);

                result.Message = _localizer[result.Message].Value + result.ResultObj;

                if (result.StatusCode != 200)
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


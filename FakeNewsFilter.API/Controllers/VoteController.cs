using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Vote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class VoteController : ControllerBase, IJob
    {
        private readonly IVoteService _voteService;
        private readonly IStringLocalizer<VoteController> _localizer;
        private readonly ILogger<VoteController> _logger;
        public VoteController(IVoteService voteService, IStringLocalizer<VoteController> localizer, ILogger<VoteController> logger)
        {
            _voteService = voteService;
            _localizer = localizer;
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VoteCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _voteService.Create(request);

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
        
        public async Task Execute(IJobExecutionContext context)
        {
            await _voteService.UpdateRatingVote();
        }
    }
}

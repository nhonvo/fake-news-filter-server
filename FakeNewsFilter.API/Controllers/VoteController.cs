using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.Vote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;
        private readonly IStringLocalizer<VoteController> _localizer;
        public VoteController(IVoteService voteService, IStringLocalizer<VoteController> localizer)
        {
            _voteService = voteService;
            _localizer = localizer;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VoteCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _voteService.Create(request);

            result.Message = _localizer[result.Message].Value;

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

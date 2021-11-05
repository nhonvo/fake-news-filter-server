using System;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.Vote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Subscriber")]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VoteCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _voteService.Create(request);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

         [HttpPut]
        public async Task<IActionResult> Update([FromBody] VoteUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _voteService.Update(request);

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}

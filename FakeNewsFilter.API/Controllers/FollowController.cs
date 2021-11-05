using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.Follows;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _IFollowService;

        public FollowController(IFollowService IFollowService)
        {
            _IFollowService = IFollowService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FollowCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _IFollowService.Create(request);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FollowUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _IFollowService.Update(request);

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
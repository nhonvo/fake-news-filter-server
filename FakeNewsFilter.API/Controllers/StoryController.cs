using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.Story;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _IStoryService;

        public StoryController(IStoryService IStoryService)
        {
            _IStoryService = IStoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] StoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _IStoryService.Create(request);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromForm] StoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _IStoryService.Update(request);

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{StoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int storyId)
        {
            var result = await _IStoryService.Delete(storyId);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{StoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int storyId)
        {
            var topic = await _IStoryService.GetOneStory(storyId);
            return Ok(topic);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string languageId)
        {
            var topics = await _IStoryService.GetAllStory(languageId);

            return Ok(topics);
        }
    }
}

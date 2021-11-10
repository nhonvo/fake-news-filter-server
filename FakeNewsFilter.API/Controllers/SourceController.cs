using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.SourceStory;
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
    public class SourceController : ControllerBase
    {
        private readonly IScourceService _IScourceStoryService;

        public SourceController(IScourceService IScourceStoryService)
        {
            _IScourceStoryService = IScourceStoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SourceCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _IScourceStoryService.Create(request);

            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SourceUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _IScourceStoryService.Update(request);

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string languageId)
        {
            var topics = await _IScourceStoryService.GetAll(languageId);

            return Ok(topics);
        }

        [HttpDelete("{SourceId}")]
        public async Task<IActionResult> Delete(int sourceid)
        {
            var result = await _IScourceStoryService.Delete(sourceid);

            if (result.ResultObj != false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{SourceId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int sourceid)
        {
            var news = await _IScourceStoryService.GetoneStory(sourceid);

            if (news == null)
            {
                return NotFound(news);
            }
            return Ok(news);
        }
    }
}

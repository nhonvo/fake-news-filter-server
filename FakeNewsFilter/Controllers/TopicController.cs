using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog.Topic;
using FakeNewsFilter.Application.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    public class TopicController : Controller
    {
        private readonly IManageTopicNewsService _manageTopicNewsService;
        private readonly IPublicTopicNewsService _publicTopicNewsService;

        public TopicController(IPublicTopicNewsService publicTopicNewsService, IManageTopicNewsService manageTopicNewsService)
        {
            _publicTopicNewsService = publicTopicNewsService;
            _manageTopicNewsService = manageTopicNewsService;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TopicNewsCreateRequest request)
        {
            var result = await _manageTopicNewsService.Create(request);

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{topicId}")]
        public async Task<IActionResult> Delete(int topicId)
        {
            var result = await _manageTopicNewsService.Delete(topicId);

            if (result == 0)
            {
                return BadRequest("Cannot delete this topic");
            }
            return Ok("Deleted successfully");
        }

        // GET: api/topic
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var topics = await _publicTopicNewsService.GetTopicHotNews();

            return Ok(topics);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] TopicNewsUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _manageTopicNewsService.Update(request);

            if (result == 0)
            {
                return BadRequest("Cannot update this topic");
            }
            return Ok("Updated successfully");
        }
    }
}
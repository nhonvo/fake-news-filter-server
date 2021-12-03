using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.ExtraFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    [Authorize]
    public class ExtraFeaturesController : Controller
    {
        private readonly IExtraFeaturesService _featuresService;

        public ExtraFeaturesController(IExtraFeaturesService featuresService)
        {
            _featuresService = featuresService;
        }
        // GET: api/values
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromBody]SeachContentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _featuresService.SearchContent(request);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}


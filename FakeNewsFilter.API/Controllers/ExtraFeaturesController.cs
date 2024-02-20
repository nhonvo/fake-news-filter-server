using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ViewModel.Catalog.ExtraFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    [Authorize]
    public class ExtraFeaturesController : ControllerBase
    {
        private readonly IExtraFeaturesService _featuresService;
        private readonly IStringLocalizer<ExtraFeaturesController> _localizer;
        public ExtraFeaturesController(IExtraFeaturesService featuresService, IStringLocalizer<ExtraFeaturesController> localizer)
        {
            _featuresService = featuresService;
            _localizer = localizer;
        }
        // GET: api/values
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery]SeachContentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _featuresService.SearchContent(request);
            if (result.StatusCode != 200)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocialMediaUser(Guid id)
        {
            var user = await _featuresService.GetSocialMediaUser(id);

            user.Message = _localizer[user.Message].Value;

            return Ok(user);
        }
    }
}


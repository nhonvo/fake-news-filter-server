using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Version;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class VersionController : ControllerBase
    {
        private readonly IStringLocalizer<VersionController> _localizer;
        private readonly VersionService _versionService;
        private readonly ILogger<VersionController> _logger;
        private readonly ApplicationDBContext _context;

        public VersionController(ApplicationDBContext context, VersionService versionService, IStringLocalizer<VersionController> localizer, ILogger<VersionController> logger)
        {
            _context = context;
            _versionService = versionService;
            _localizer = localizer;
            _logger = logger;
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] VersionCreateRequest request)
        {
            try
            {

                var resultToken = await _versionService.Create(request);

                resultToken.Message = _localizer[resultToken.Message].Value + resultToken.ResultObj;

                if (resultToken.StatusCode != 200)
                {
                    _logger.LogError(resultToken.Message);
                    return BadRequest(resultToken);
                }
                _logger.LogInformation(resultToken.Message);
                return Ok(resultToken);

            }
            catch (FakeNewsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpGet("list-platform")]
        public async Task<IActionResult> GetVersionListPlatform(string platform)
        {
            try
            {
                var list_version = await _versionService.GetVerionPlatform(platform);

                list_version.Message = _localizer[list_version.Message].Value;

                if (list_version.StatusCode != 200)
                {
                    return BadRequest(list_version);
                }
                return Ok(list_version);
            }
            catch (FakeNewsException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("checklastest")]
        public async Task<IActionResult> GetLastestVersion(string platform, float version_current)
        {
            try
            {
                var last_version = await _versionService.CheckNewVersion(version_current, platform);

                last_version.Message = _localizer[last_version.Message].Value;

                if (last_version.StatusCode != 200)
                {
                    return BadRequest(last_version);
                }
                return Ok(last_version);
            }
            catch (FakeNewsException e)
            {
                return BadRequest(e.Message);
            }
        }
    }

}


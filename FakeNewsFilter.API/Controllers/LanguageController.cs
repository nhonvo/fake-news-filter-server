using System;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Utilities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;
        private readonly IStringLocalizer<LanguageController> _localizer;

        public LanguageController(ILanguageService languageService, IStringLocalizer<LanguageController> localizer)
        {
            _languageService = languageService;
            _localizer = localizer;
        }

        [HttpGet("List")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var languages = await _languageService.GetAllLanguage();

                languages.Message = _localizer[languages.Message].Value;

                if (languages.StatusCode != 200)
                {
                    throw new FakeNewsException(languages.Message);
                }
                return Ok(languages);

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }      
        }
    }
}

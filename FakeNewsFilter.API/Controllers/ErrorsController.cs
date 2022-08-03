
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.API.Controllers
{
    public class ErrorsController : Controller
    {
        private readonly IStringLocalizer<ErrorsController> _localizer;

        public ErrorsController(IStringLocalizer<ErrorsController> localizer)
        {
            _localizer = localizer;
        }

        [Route("errors/{statusCode}")]
        public IActionResult CustomError(int statusCode)
        {
                switch (statusCode)
                {
                    case 400:
                    {
                        return new JsonResult(new ApiErrorResult<string>(400, _localizer["BadRequest"].Value));
                    }
                    case 401:
                    {
                        return new JsonResult(new ApiErrorResult<string>(401, _localizer["Unauthorized"].Value));
                    }
                    case 403:
                    {
                        return new JsonResult(new ApiErrorResult<string>(403, _localizer["Forbidden"].Value));
                    }
                    case 404:
                    {
                        return new JsonResult(new ApiErrorResult<string>(404, _localizer["NotFound"].Value));
                    }
                    case 405:
                    {
                        return new JsonResult(new ApiErrorResult<string>(405, _localizer["MethodNotAllowed"].Value));
                    }
                    default:
                    {
                        return new JsonResult(new ApiErrorResult<string>(500, _localizer["InternalServerError"].Value));
                    }
                }
           
        }
    }
}





using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FakeNewsFilter.ViewModel.Common
{
    public class ReturnStatus : ControllerBase
    {
        private readonly ILogger _logger;

        protected ReturnStatus(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult ReturnWithModel<T>(ApiResult<T> resultToken)
        {
            switch (resultToken.StatusCode)
            {
                case 200:
                    {
                        _logger.LogInformation(resultToken.Message);
                        return Ok(resultToken);
                    }
                case 400:
                    {
                        _logger.LogError(resultToken.Message);
                        return BadRequest(resultToken);
                    }
                case 404:
                    {
                        _logger.LogError(resultToken.Message);
                        return NotFound(resultToken);
                    }
                default:
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, resultToken);
                    }
            }
        }

        protected IActionResult ReturnWithPagedModel<T>(ApiResult<PagedResult<T>> resultToken)
        {
            switch (resultToken.StatusCode)
            {
                case 200:
                    {
                        _logger.LogInformation(resultToken.Message);
                        return Ok(resultToken);
                    }
                case 400:
                    {
                        _logger.LogError(resultToken.Message);
                        return BadRequest(resultToken);
                    }
                case 404:
                    {
                        _logger.LogError(resultToken.Message);
                        return NotFound(resultToken);
                    }
                default:
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, resultToken);
                    }
            }
        }

        protected IActionResult ReturnWithListModel<T>(ApiResult<List<T>> resultToken)
        {
            switch (resultToken.StatusCode)
            {
                case 200:
                    {
                        _logger.LogInformation(resultToken.Message);
                        return Ok(resultToken);
                    }
                case 400:
                    {
                        _logger.LogError(resultToken.Message);
                        return BadRequest(resultToken);
                    }
                case 404:
                    {
                        _logger.LogError(resultToken.Message);
                        return NotFound(resultToken);
                    }
                default:
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, resultToken);
                    }
            }
        }
    }
}


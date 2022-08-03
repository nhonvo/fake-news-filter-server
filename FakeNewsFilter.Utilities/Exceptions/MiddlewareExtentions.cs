using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.Utilities.Exceptions
{
    public class MiddlewareExtentions
    {
        private readonly RequestDelegate _next;

        public MiddlewareExtentions(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                
                switch (error)
                {
                    case FakeNewsException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var responseModel = new ApiErrorResult<string>(response.StatusCode, error.Message);

                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}


using System;
namespace FakeNewsFilter.ViewModel.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {

        public ApiErrorResult()
        {

        }

        public ApiErrorResult(int statuscode, string message)
        {
            StatusCode = statuscode;
            Message = message;
        }

        public ApiErrorResult(int statuscode, string message, T detail)
        {
            StatusCode = statuscode;
            Message = message;
            ResultObj = detail;
        }

    }
}

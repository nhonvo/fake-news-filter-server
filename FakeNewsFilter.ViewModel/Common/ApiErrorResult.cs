using System;
namespace FakeNewsFilter.ViewModel.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {

        public ApiErrorResult()
        {

        }

        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }
    }
}

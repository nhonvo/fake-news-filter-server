using System;

namespace FakeNewsFilter.ViewModel.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {

        public ApiSuccessResult()
        {
            StatusCode = 200;
        }

        public ApiSuccessResult(string v)
        {
            StatusCode = 200;
            Message = v;
        }

        public ApiSuccessResult(String message, T resultObj)
        {
            StatusCode = 200;
            Message = message;
            ResultObj = resultObj;
        }
    }
}

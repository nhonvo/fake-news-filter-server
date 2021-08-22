using System;
using System.Collections.Generic;
using System.Text;

namespace FakeNewsFilter.ViewModel.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }

        public ApiSuccessResult(String message, T resultObj)
        {
            IsSuccessed = true;
            Message = message;
            ResultObj = resultObj;
        }
    }
}

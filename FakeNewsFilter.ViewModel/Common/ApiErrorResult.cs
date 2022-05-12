using System;
namespace FakeNewsFilter.ViewModel.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        private int storyId;

        public ApiErrorResult()
        {

        }

        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ApiErrorResult(string message, T detail)
        {
            IsSuccessed = false;
            Message = message;
            ResultObj = detail;
        }

        public ApiErrorResult(string message, int storyId) : this(message)
        {
            this.storyId = storyId;
        }
    }
}

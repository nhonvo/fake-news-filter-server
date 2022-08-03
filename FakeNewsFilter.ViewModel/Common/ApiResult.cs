namespace FakeNewsFilter.ViewModel.Common
{
    public class ApiResult<T>
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public T ResultObj { get; set; }
    }
}

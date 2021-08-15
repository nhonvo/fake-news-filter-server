using System;
namespace FakeNewsFilter.ViewModel.Common
{
    public class PagingRequestBase
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}

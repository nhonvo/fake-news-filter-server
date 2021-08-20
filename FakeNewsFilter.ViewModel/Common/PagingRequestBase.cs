using System;
namespace FakeNewsFilter.ViewModel.Common
{
    public class PagingRequestBase : RequestBase
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}

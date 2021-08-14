using System;
namespace FakeNewsFilter.Application.DTOs
{
    public class PagingRequestBase
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}

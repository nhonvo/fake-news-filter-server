using System;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.ViewModel.Catalog.News.Public
{
    public class GetNewsPagingRequest : PagingRequestBase
    {
        public int? TopicId { get; set; }
    }
}

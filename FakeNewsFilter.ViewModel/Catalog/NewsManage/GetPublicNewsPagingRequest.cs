using System;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class GetPublicNewsPagingRequest : PagingRequestBase
    {
        public int? TopicId { get; set; }
    }
}

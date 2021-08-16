using System;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class GetPublicNewsRequest : PagingRequestBase
    {
        public int? TopicId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.ViewModel.Catalog.News.Manage
{
    public class GetNewsPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public List<int> TopicIds { get; set; }
    }
}

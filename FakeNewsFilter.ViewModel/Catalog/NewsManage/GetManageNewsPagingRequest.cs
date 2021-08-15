using FakeNewsFilter.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class GetManageNewsPagingRequest : PagingRequestBase
    {
        public string Keyword {get; set; }
        public List<int> TopicIds { get; set; }
    }
}

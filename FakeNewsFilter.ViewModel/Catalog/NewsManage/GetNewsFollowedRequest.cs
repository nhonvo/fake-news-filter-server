using System;
using System.Collections.Generic;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class GetNewsFollowedRequest : PagingBase
    {
        public List<int> topicList { get; set; }

        public Guid userId { get; set; }
    }
}


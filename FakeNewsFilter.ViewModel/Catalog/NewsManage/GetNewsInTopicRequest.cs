using System;
using FakeNewsFilter.ViewModel.Common;
using System.Collections.Generic;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class GetNewsInTopicRequest : PagingBase
    {
        public int topicId { get; set; }
       
    }
}


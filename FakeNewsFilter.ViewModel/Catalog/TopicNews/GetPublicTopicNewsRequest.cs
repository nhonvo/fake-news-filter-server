using System;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.ViewModel.Catalog.TopicNews
{
    public class GetTopicNewsRequest : PagingBase
    {
        public string Keyword { get; set; }

        public string LanguageId { get; set; }
    }
}

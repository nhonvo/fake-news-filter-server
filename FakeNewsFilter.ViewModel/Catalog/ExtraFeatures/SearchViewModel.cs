using System;
using System.Collections.Generic;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;

namespace FakeNewsFilter.ViewModel.Catalog.ExtraFeatures
{
    public class SearchViewModel
    {
        public int CountTopic { get; set; }

        public List<TopicInfoVM> TopicNews { get; set; }

        public int CountNews { get; set; }

        public List<NewsViewModel> News { get; set; }
    }
}


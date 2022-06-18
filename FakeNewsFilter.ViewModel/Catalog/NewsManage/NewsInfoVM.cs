using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class NewsInfoVM
    {
        public int NewsId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string OfficialRating { get; set; }

        public string Content { get; set; }
        public string Source { get; set; }
        public string ImageLink { get; set; }

        public MediaType? Type { get; set; }

        public String? thumbNews { get; set; }

        public string LanguageId { get; set; }

        public string Publisher { get; set; }

        public DateTime? DatePublished { get; set; }

        public List<int> TopicId { get; set; }

        public List<TopicInfo> TopicInfo { get; set; }
    }
}
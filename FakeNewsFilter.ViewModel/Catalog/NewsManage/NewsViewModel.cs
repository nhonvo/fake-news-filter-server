﻿using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OfficialRating { get; set; }

        public string Publisher { get; set; }

        public string Content { get; set; }

        public String? ThumbNews { get; set; }

        public Status Status { get; set; }

        public DateTime Timestamp { get; set; }

        public string LanguageId { get; set; }

        public List<TopicInfo> TopicInfo { get; set; }

    }
}

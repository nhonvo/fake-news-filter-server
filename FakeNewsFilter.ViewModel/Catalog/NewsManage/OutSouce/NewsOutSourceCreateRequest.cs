﻿using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class NewsOutSourceCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string OfficialRating { get; set; }

        public string UrlNews { get; set; }

        public string ImageLink { get; set; }

        public MediaType? Type { get; set; }

        public string LanguageId { get; set; }

        public string Publisher { get; set; }

        public bool isVote { get; set; }

        public DateTime? DatePublished { get; set; }

        public List<int> TopicId { get; set; }

        public string SourceCreate { get; set; }
    }
}

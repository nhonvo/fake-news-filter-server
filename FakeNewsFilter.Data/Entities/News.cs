﻿using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FakeNewsFilter.Data.Entities
{
    public class News
    {
        public int NewsId { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public LabelNews OfficialRating { get; set; }

        public string ImageLink { get; set; }

        public string UrlNews { get; set; }

        public string Publisher { get; set; }

        public double SocialBeliefs { get; set; }

        public bool? IsVote { get; set; }

        public int ViewCount { get; set; }

        public SourceCreate SourceCreate { get; set; }

        public DateTime DatePublished { get; set; }

        public DateTime Timestamp { get; set; }

        public Status Status { get; set; }

        public string LanguageId { set; get; }

        public Language Language { get; set; }

        public DetailNews DetailNews { get; set; }

        public List<NewsInTopics> NewsInTopics { get; set; }

        public List<Vote> Vote { get; set; }

        public List<Comment> Comment { get; set; }

    }
}
using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.Data.Entities
{
    public class News
    {
        public int NewsId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OfficialRating { get; set; }

        public string Content { get; set; }

        public string Source { get; set; }
        public string Publisher { get; set; }

        public DateTime DatePublished { get; set; }

        public DateTime Timestamp { get; set; }

        public int? ThumbNews { get; set; }

        public Media Media { get; set; }

        public Status Status { get; set; }

        public string LanguageId { set; get; }

        public Language Language { get; set; }

        public List<NewsInTopics> NewsInTopics { get; set; }

        public List<Vote> Vote { get; set; }

        public List<Comment> Comment { get; set; }
    }
}
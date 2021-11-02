using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.Data.Entities
{
    public class TopicNews
    {
        public int TopicId { get; set; }

        public string Label { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public DateTime Timestamp { get; set; }

        public Status Status { get; set; }

        public int? ThumbTopic { get; set; }

        public Media Media { get; set; }

        public string LanguageId { set; get; }

        public Language Language { get; set; }

        public List<NewsInTopics> NewsInTopics { get; set; }

        public List<Follow> Follows { get; set; }

    }
}
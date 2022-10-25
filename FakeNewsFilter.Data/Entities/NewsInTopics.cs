using System;
using System.Collections.Generic;

namespace FakeNewsFilter.Data.Entities
{
    public class NewsInTopics
    {
        public int NewsId { get; set; }

        public News News { get; set; }

        public int TopicId { get; set; }

        public TopicNews TopicNews { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
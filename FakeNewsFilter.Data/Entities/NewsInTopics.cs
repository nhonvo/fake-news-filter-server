using System;
using System.Collections.Generic;

namespace FakeNewsFilter.Data.Entities
{
    public class NewsInTopics
    {
        public News News { get; set; }
        public int NewsId { get; set; }
        public int TopicId { get; set; }

        public TopicNews TopicNews { get; set; }
    }
}
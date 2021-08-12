using System;
using System.Collections.Generic;

namespace FakeNewsFilter.Data.Entities
{
    public class TopicNews
    {
        public int TopicId { get; set; }
        
        public string TopicName { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public DateTime Timestamp { get; set; }

        public List<NewsInTopics> NewsInTopics { get; set; }

        public List<Follow> Follows { get; set; }
    }
}

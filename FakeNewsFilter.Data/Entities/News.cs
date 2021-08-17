using System;
using System.Collections.Generic;

namespace FakeNewsFilter.Data.Entities
{
    public class News
    {
        public string Description { get; set; }
        public Media Media { get; set; }
        public string Name { get; set; }
        public int NewsId { get; set; }
        public List<NewsInTopics> NewsInTopics { get; set; }
        public string OfficialRating { get; set; }

        public double SocialBeliefs { get; set; }

        public string SourceLink { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
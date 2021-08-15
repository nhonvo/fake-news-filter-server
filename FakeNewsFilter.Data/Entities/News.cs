using System;
using System.Collections.Generic;

namespace FakeNewsFilter.Data.Entities
{
    public class News
    {
        public int NewsId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OfficialRating { get; set; }

        public double SocialBeliefs { get; set; }

        public string SourceLink { get; set; }

        public DateTime Timestamp { get; set; }

        public List<NewsInTopics> NewsInTopics { get; set; }

        public int? MediaId { get; set; }

        public Media Media { get; set; }
    } 
}

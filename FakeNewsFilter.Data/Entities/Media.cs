using System;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.Data.Entities
{
    public class Media
    {
        public string Caption { get; set; }
        public DateTime DateCreated { get; set; }
        public int Duration { get; set; }
        public long FileSize { get; set; }
        public int MediaId { get; set; }

        public News News { get; set; }
        public int? NewsId { get; set; }
        public string PathMedia { get; set; }
        public int SortOrder { get; set; }
        public int? TopicId { get; set; }
        public TopicNews TopicNews { get; set; }
        public MediaType Type { get; set; }
        public string Url { get; set; }
    }
}
using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.Data.Entities
{
    public class Media
    {
        public int MediaId { get; set; }

        public MediaType Type { get; set; }

        public string PathMedia { get; set; }

        public string Caption { get; set; }

        public int SortOrder { get; set; }

        public int Duration { get; set; }

        public long FileSize { get; set; }

        public DateTime DateCreated { get; set; }

        public DetailNews DetailNews { get; set; }

        public NewsCommunity NewsCommunity { get; set; }

        public Story Story { get; set; }
       
        public TopicNews TopicNews { get; set; }

        public User User { get; set; }
    }
}
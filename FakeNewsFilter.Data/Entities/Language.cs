using System;
using System.Collections.Generic;

namespace FakeNewsFilter.Data.Entities
{
    public class Language
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public List<TopicNews> TopicNews { get; set; }

        public List<News> News { get; set; }
    }
}


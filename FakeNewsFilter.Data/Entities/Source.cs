using System;
using System.Collections.Generic;
using System.Text;

namespace FakeNewsFilter.Data.Entities
{
    public class Source
    {
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public string LanguageId { set; get; }
        public Language Language { get; set; }
        public List<Story> Story { get; set; }

    }
}

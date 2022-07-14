using FakeNewsFilter.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeNewsFilter.Data.Entities
{
    public class Story
    {
        public int StoryId { get; set; }
        public int? Thumbstory { get; set; }
        public DateTime Timestamp { get; set; }
        public string Link { get; set; }
        public Media Media { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
        public string LanguageId { set; get; }
        public Language Language { get; set; }
        public Status Status { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FakeNewsFilter.ViewModel.Catalog.Story
{
    public class StoryViewModel
    {
        public int? ThumbNews { get; set; }
        public int StoryId { get; set; }
        public TimeSpan SyncTime { get; set; }
        public string Link { get; set; }
        public int SourceId { get; set; }
    }
}

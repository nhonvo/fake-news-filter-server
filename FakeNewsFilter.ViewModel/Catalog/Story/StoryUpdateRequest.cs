using System;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.Catalog.Story
{
    public class StoryUpdateRequest
    {
        public int StoryId { get; set; }
        public IFormFile ThumbStory { get; set; }
        public string LanguageId { set; get; }
        public DateTime Timestamp { get; set; }
        public string Link { get; set; }
        public int SourceId { get; set; }
    }
}

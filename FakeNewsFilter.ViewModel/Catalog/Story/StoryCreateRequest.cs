using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.Catalog.Story
{
    public class StoryCreateRequest
    {
        public int? ThumbNews { get; set; }
        public string LanguageId { set; get; }
        public IFormFile ThumbStory { get; set; }
        public DateTime Timestamp { get; set; }
        public string Link { get; set; }
        public int SourceId { get; set; }
    }
}

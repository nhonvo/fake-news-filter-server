using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class NewsSystemCreateRequest
    {
        public string Title { get; set; }

        public string OfficialRating { get; set; }

        public string Content { get; set; }

        public string Alias { get; set; }

        public MediaType? Type { get; set; }

        public IFormFile ThumbNews { get; set; }

        public string LanguageId { get; set; }

        public string Publisher { get; set; }

        public bool isVote { get; set; }

        public DateTime? DatePublished { get; set; }

        public List<int> TopicId { get; set; }

        public SourceCreate SourceCreate { get; set; }
    }
}


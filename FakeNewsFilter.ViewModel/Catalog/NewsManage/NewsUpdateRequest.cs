using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class NewsUpdateRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public MediaType Type { get; set; }

        public IFormFile ThumbNews { get; set; }

        public string LanguageId { get; set; }

        public string Publisher { get; set; }

        public DateTime? DatePublished { get; set; }

        public List<int> TopicId { get; set; }

    }
}

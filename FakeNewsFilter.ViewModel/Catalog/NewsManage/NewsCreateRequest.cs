using System;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class NewsCreateRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string OfficialRating { get; set; }

        public double? SocialBeliefs { get; set; }

        public string PostURL { get; set; }

        public MediaType? Type { get; set; }

        public IFormFile ThumbNews{ get; set;}

        public string LanguageCode { get; set; }

        public string Publisher { get; set; }

        public DateTime? DatePublished { get; set; }

        public int TopicId { get; set; }
    }
}

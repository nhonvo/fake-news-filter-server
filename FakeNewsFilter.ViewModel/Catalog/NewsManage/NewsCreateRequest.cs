using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class NewsCreateRequest
    {
        public int NewsId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OfficialRating { get; set; }

        public double SocialBeliefs { get; set; }

        public string SourceLink { get; set; }

        public string MediaLink { get; set; }


        public Media Media { get; set; }

        public MediaType Type { get; set; }

        public IFormFile ThumbnailImage { get; set;}
    }
}

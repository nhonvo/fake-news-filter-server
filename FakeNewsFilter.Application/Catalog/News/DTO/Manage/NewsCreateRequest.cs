using System;
using System.Collections.Generic;
using FakeNewsFilter.Data.Entities;

namespace FakeNewsFilter.Application.Catalog.News.DTO.Manage
{
    public class NewsCreateRequest
    {
        public int NewsId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OfficialRating { get; set; }

        public double SocialBeliefs { get; set; }

        public string SourceLink { get; set; }

        public Media Media { get; set; }
       
    }
}

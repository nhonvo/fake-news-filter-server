﻿using System;
namespace FakeNewsFilter.Data.Entities
{
    public class News
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

﻿using FakeNewsFilter.Data.Entities;
using System;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OfficialRating { get; set; }

        public double SocialBeliefs { get; set; }

        public string SourceLink { get; set; }

        public Media Media { get; set; }

        public DateTime Timestamp { get; set; }

        public int TopicId { get; set; }

        public string LabelTopic { get; set; }

    }
}

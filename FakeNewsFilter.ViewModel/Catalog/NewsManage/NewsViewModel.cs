using System;
using FakeNewsFilter.ViewModel.Catalog.Media;

namespace FakeNewsFilter.ViewModel.Catalog.NewsManage
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OfficialRating { get; set; }

        public double SocialBeliefs { get; set; }

        public string PostURL { get; set; }

        public MediaViewModel Media { get; set; }

        public DateTime Timestamp { get; set; }

        public int TopicId { get; set; }

        public string LabelTopic { get; set; }

    }
}

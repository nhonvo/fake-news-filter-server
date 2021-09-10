using System;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Media;

namespace FakeNewsFilter.ViewModel.Catalog.TopicNews
{
    public class TopicViewModel
    {
        public int TopicId { get; set; }

        public string Label { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public DateTime Timestamp { get; set; }

        public MediaViewModel Media { get; set; }

        public News NewsInTopic { get; set; }

    }
}

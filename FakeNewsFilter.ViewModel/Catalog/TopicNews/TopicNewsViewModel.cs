using System;
namespace FakeNewsFilter.ViewModel.Catalog.TopicNews
{
    public class TopicNewsViewModel
    {
        public int TopicId { get; set; }

        public string Label { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public DateTime Timestamp { get; set; }

        public int? MediaId { get; set; }

    }
}

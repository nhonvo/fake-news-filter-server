using System;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.ViewModel.Catalog.TopicNews
{
    public class TopicInfo
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
    }

    public class TopicInfoVM
    {
        public int TopicId { get; set; }

        public string Label { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public string ThumbImage { get; set; }

        public Status Status { get; set; }

        public int NONews { get; set; }

        public DateTime? RealTime { get; set; }
    }
}

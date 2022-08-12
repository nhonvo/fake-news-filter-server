using System;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.Data.Entities
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        public int? NewsId { get; set; }
        public News News { get; set; }

        public Guid? UserId { get; set; }
        public User User { get; set; }

        public string Content { get; set; }

        public int? Screenshoot { get; set; }

        public Media Media { get; set; }

        public Status Status { get; set; }

        public DateTime Timestamp { get; set; }
    }
}


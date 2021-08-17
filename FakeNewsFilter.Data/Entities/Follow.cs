using System;

namespace FakeNewsFilter.Data.Entities
{
    public class Follow
    {
        public int TopicId { get; set; }

        public TopicNews TopicNews { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
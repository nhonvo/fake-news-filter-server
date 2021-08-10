using System;

namespace FakeNewsFilter.Data.Entities
{
    public class Follow
    {
        public int FollowId { get; set; }

        public TopicNews TopicId { get; set; }

        public User userId { get; set; }
    }
}

using System;
namespace FakeNewsFilter.Data.Entities
{
    public class Vote
    {
        public int NewsId { get; set; }
        public News News { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public bool isReal { get; set; }

        public DateTime Timestamp { get; set; }

    }
}
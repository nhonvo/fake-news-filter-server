using System;
namespace FakeNewsFilter.ViewModel.Catalog.Vote
{
    public class VoteCreateRequest
    {
        public int NewsId { get; set; }

        public Guid UserId { get; set; }

        public bool isReal { get; set; }

        public DateTime Timestamp { get; set; }
    }
}

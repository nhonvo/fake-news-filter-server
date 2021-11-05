using System;
using System.Collections.Generic;
using System.Text;

namespace FakeNewsFilter.ViewModel.Catalog.Follows
{
    public class FollowUpdateRequest
    {
        public List<int> TopicId { get; set; }
        public Guid UserId { get; set; }
    }
}

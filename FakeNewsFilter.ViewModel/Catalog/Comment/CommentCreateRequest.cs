using FakeNewsFilter.Data.Entities;
using System;

namespace FakeNewsFilter.ViewModel.Catalog.Comment
{
    public class CommentCreateRequest
    {
        public int CommentId { get; set; }
        public int ParentId { get; set; }
        public string Content { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

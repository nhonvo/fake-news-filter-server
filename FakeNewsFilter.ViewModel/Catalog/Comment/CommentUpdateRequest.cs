using System;

namespace FakeNewsFilter.ViewModel.Catalog.Comment
{
    public class CommentUpdateRequest
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

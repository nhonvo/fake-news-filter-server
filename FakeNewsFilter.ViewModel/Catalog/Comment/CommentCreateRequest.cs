using FakeNewsFilter.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.ViewModel.Catalog.Comment
{
    public class CommentCreateRequest
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

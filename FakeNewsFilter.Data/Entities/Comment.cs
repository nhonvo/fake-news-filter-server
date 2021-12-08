using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.Data.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public int ParentId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

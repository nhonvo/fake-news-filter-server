using FakeNewsFilter.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int? ParentId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Status Status { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual Comment Parent {get; set;}
        
        [NotMapped]
        public List<Comment> Child { get; set; }

    }
}

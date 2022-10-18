using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.System.Users;

namespace FakeNewsFilter.ViewModel.Catalog.Comment
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public int ParentId { get; set; }
        public string Content { get; set; }
        public int NewsId { get; set; }
        public UserViewModel User { get; set; }
        public Guid UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public List<CommentViewModel> Child { get; set; }
    }
}
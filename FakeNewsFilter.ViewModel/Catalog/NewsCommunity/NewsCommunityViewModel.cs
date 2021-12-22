using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.System.Users;

namespace FakeNewsFilter.ViewModel.Catalog.NewsCommunity
{
    public class NewsCommunityViewModel
    {
        public int NewsCommunityId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPopular { get; set; }
        public UserViewModel Publisher { get; set; }
        public DateTime DatePublished { get; set; }
        public string? ThumbNews { get; set; }
        public string LanguageId { set; get; }

    }
}

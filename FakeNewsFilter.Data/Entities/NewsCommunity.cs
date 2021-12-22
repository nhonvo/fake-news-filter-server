using FakeNewsFilter.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.Data.Entities
{
    public class NewsCommunity
    {
        public int NewsCommunityId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        public string Source { get; set; }
        public bool IsPopular { get; set; }
        public Guid UserId { get; set; }
        public User Publisher { get; set; }

        public DateTime DatePublished { get; set; }
        public int? ThumbNews { get; set; }

        public Media Media { get; set; }

        public Status Status { get; set; }

        public string LanguageId { set; get; }

        public Language Language { get; set; }
    }
}

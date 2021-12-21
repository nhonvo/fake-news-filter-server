using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.ViewModel.Catalog.NewsCommunity
{
    public class NewsCommunityUpdateRequest
    {
        public int NewsCommunityId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public DateTime DatePublished { get; set; }
        public MediaType Type { get; set; }
        public IFormFile ThumbNews { get; set; }
        public string LanguageId { set; get; }
    }
}

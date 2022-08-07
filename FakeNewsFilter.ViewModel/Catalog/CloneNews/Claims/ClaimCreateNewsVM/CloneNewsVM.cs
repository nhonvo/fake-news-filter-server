using System;
using System.Collections.Generic;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using NewsAPI.Models;

namespace FakeNewsFilter.ViewModel.Catalog.Claims.ClaimCreateNewsVM
{
    public class CloneNewsVM
    {
        public NewsCreateRequest NewsCreateRequest { get; set; }
        public List<ClaimViewModel> ClaimViewModel { get; set; }
        public List<Article> ArticlesResults { get; set; }
    }
}

using System;
using System.Collections.Generic;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using NewsAPI.Models;

namespace FakeNewsFilter.ViewModel.Catalog.Claims.ClaimCreateNewsVM
{
    public class CloneNewsVM
    {
        public NewsOutSourceCreateRequest NewsCreateRequest { get; set; }
        public List<ClaimViewModel> ClaimViewModel { get; set; }
        public List<Article> ArticlesResults { get; set; }
        public OigetitNewsViewModel OigetitNewsViewModel { get; set; }
    }
}
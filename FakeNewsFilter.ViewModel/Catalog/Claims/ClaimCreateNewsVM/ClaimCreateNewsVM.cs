using System;
using System.Collections.Generic;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;

namespace FakeNewsFilter.ViewModel.Catalog.Claims.ClaimCreateNewsVM
{
    public class ClaimCreateNewsVM
    {
        public NewsCreateRequest NewsCreateRequest { get; set; }

        public List<ClaimViewModel>? ClaimViewModel { get; set; }
    }
}

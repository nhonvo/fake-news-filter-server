using System;
using System.Collections.Generic;

namespace FakeNewsFilter.ViewModel.Catalog.Claims
{
    public class ClaimViewModel
    {
        public string Text{ get; set; }

        public string Claimant{ get; set; }

        public DateTime ClaimDate { get; set; }

        public List<ClaimReviewViewModel> ClaimReview{ get; set; }
    }
}
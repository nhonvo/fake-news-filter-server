using System;
using System.Collections.Generic;

namespace FakeNewsFilter.ViewModel.Catalog.Claims
{
    public class GetClaimsRequest
    {
        public List<ClaimViewModel> Claims { get; set; }

        public string NextPageToken { get; set; }

        public string Message { get; set; }

        public GetClaimsRequest(string message) {
            Message = message;
        }
    }
}

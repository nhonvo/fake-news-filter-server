using System;
using FakeNewsFilter.ViewModel.Common;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase 
    {
        public string Keyword { get; set; }
    }
}

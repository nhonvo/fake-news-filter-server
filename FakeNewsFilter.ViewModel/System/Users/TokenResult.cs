using System;
using Newtonsoft.Json;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class TokenResult
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }

}

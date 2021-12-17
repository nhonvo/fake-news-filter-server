using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.ViewModel.System.LoginSocial
{
    public class GetUserLoginSocialRequest
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public Guid UserId { get; set; }
        public string ProviderDisplayName { get; set; }
    }
}

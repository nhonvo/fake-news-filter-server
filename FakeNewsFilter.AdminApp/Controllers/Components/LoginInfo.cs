using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.System.Users;
using FakeNewsFilter.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeNewsFilter.AdminApp.Controllers.Components
{
    public class LoginInfo : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {

            // Cast to ClaimsIdentity.
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;

            // Gets name from claims. Generally it's an email address.
            var name = claim
                .Where(x => x.Type == ClaimTypes.GivenName)
                .FirstOrDefault();

            var avatar = claim
               .Where(x => x.Type == ClaimTypes.Uri)
               .FirstOrDefault();

            var role = claim
               .Where(x => x.Type == ClaimTypes.Role)
               .FirstOrDefault();

            var userinfo = new UserViewModel
            {
                FullName = name.Value,
                Avatar = avatar.Value,
                Role = role.Value,
            };
            return Task.FromResult((IViewComponentResult)View("Default", userinfo));
        }
    }
}

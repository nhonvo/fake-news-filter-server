using System;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Mvc;

namespace FakeNewsFilter.AdminApp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}

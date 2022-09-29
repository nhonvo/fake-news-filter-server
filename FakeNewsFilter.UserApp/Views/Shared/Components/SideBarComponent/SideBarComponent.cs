using Microsoft.AspNetCore.Mvc;

namespace FakeNewsFilter.UserApp.Views.Shared.Components.SideBar
{
    public class SideBarComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace FakeNewsFilter.UserApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

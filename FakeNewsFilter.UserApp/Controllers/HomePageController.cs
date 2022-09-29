using Microsoft.AspNetCore.Mvc;

namespace FakeNewsFilter.UserApp.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}

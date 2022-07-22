using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FakeNewsFilter.UserApp.Models;
using System.Diagnostics;

namespace FakeNewsFilter.UserApp.Controllers
{
    public class PolicyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/policy")]
        public IActionResult Policy()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

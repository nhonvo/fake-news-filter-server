using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FakeNewsFilter.UserApp.Models;
using System.Diagnostics;


namespace FakeNewsFilter.UserApp.Controllers
{
    public class IntroController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IntroController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [HttpGet("/intro")]
        public IActionResult Index()
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

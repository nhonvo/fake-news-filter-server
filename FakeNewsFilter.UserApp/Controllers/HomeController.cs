using System.Diagnostics;
using FakeNewsFilter.AdminApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FakeNewsFilter.UserApp.Models;

namespace FakeNewsFilter.UserApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewsApi _newsApi;

        public HomeController(ILogger<HomeController> logger, NewsApi newsApi)
        {
            _logger = logger;
            _newsApi = newsApi;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("privacy-policy")]
        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Dashboard()
        {
            // var newsList = _newsApi.GetAll();
            
            // return View("Dashboard", newsList.Result?.ResultObj);
            return View();
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}


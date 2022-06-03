using System.Diagnostics;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FakeNewsFilter.UserApp.Models;
using FakeNewsFilter.UserApp.Services;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;

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
        public IActionResult Dashboard()
        {
            var newsList = _newsApi.GetAll();
            
            return View("Dashboard", newsList.Result?.ResultObj);
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


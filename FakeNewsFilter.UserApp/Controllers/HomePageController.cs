using FakeNewsFilter.AdminApp.Services;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.UserApp.Services;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakeNewsFilter.UserApp.Controllers
{
    public class HomePageController : Controller
    {
        private readonly NewsApiDeprecated _newsApiDeprecated;

        public HomePageController(NewsApiDeprecated newsApiDeprecated)
        {
            _newsApiDeprecated = newsApiDeprecated;
        }
        // GET: api/news
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(GetManageNewsRequest request)
        {
            // research code api get all new for user
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

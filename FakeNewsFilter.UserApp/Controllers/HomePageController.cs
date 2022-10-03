using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ClientService;
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
       

        public HomePageController()
        {
        }
        
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

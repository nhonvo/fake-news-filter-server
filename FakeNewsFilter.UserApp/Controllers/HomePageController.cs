using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ClientService;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using Google.Apis.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

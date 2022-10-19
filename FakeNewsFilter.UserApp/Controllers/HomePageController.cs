using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.ClientService;
using FakeNewsFilter.UserApp.Services;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using Google.Apis.Util;
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
        private readonly NewsApi _newsApi;

        public HomePageController(NewsApi newsAPI)
        {
            _newsApi = newsAPI;
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
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetNewsByTopic(int topicId)
        {

            var data = await _newsApi.GetNewsByTopic(topicId);

            return View("NewsByTopic", data.ResultObj == null ? data.ResultObj : data.ResultObj.Items);
        }
    }
}

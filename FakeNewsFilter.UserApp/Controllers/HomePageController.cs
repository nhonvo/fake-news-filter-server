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
        [HttpGet]
        public async Task<IActionResult> Search(String name)
        {
            var listNews = await _newsApi.GetAll();
            var data = listNews.ResultObj.Items.Where(x => x.Title.Contains(name)).ToList();
            return View("NewsByTopic", data);
        }
        public async Task<IActionResult> TestAsync(int id)
        {
            var data = await _newsApi.GetById(id);

            return View(data.ResultObj);
        }
    }
}

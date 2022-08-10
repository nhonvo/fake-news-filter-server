using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Services;
using FakeNewsFilter.ViewModel.Catalog.Claims;
using FakeNewsFilter.ViewModel.Catalog.Claims.ClaimCreateNewsVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using SmartBreadcrumbs.Attributes;

namespace FakeNewsFilter.AdminApp.Controllers
{
    public class CloneNewsController : BaseController
    {
        private readonly FactCheckApi _factCheckApi;
        private readonly TopicApi _topicApi;
        private readonly LanguageApi _languageApi;
        private readonly IConfiguration _configuration;


        public CloneNewsController(FactCheckApi factCheckApi, TopicApi topicApi, LanguageApi languageApi,
            IConfiguration configuration)
        {
            _factCheckApi = factCheckApi;
            _topicApi = topicApi;
            _languageApi = languageApi;
            _configuration = configuration;
        }

        // [Breadcrumb("Clone News")]
        public async Task<IActionResult> GoogleFactCheckIndex()
        {
            var topicData = await _topicApi.GetAllTopic();

            var languageData = await _languageApi.GetLanguageInfo();

            var model = new CloneNewsVM();

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }

            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            ViewBag.ListTopic = new SelectList(topicData.ResultObj, "TopicId", "Tag");
            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");

            return View(model);
        }

        public async Task<IActionResult> NewsApiIndex()
        {
            //
            var topicData = await _topicApi.GetAllTopic();
            
            var languageData = await _languageApi.GetLanguageInfo();
            
            var model = new CloneNewsVM();
            
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }
            
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            
            ViewBag.ListTopic = new SelectList(topicData.ResultObj, "TopicId", "Tag");
            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");
            return View(model);
        }

        public async Task<IActionResult> NewsApiSearch(string query, int page = 1)
        {
            if (string.IsNullOrEmpty(query))
            {
                return new PartialViewResult
                {
                    ViewName = "ViewNewsApiCloneNews"
                };
            }

            var newsApiClient = new NewsApiClient(_configuration["NewsAPIKey"]);

            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = query,
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = new DateTime(2022, 7, 7),
                Page = page
            });

            if (articlesResponse.Status == Statuses.Error)
            {
                TempData["Error"] = articlesResponse.Error.Message;
                return RedirectToAction("NewsApiIndex");
            }


            var topicData = await _topicApi.GetAllTopic();

            var languageData = await _languageApi.GetLanguageInfo();

            var model = new CloneNewsVM();

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }

            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            return new PartialViewResult
            {
                ViewName = "ViewNewsApiCloneNews",
                ViewData = new ViewDataDictionary<List<Article>>(ViewData, articlesResponse.Articles)
            };
        }

        public async Task<IActionResult> FactCheckSearch(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return new PartialViewResult
                {
                    ViewName = "ViewFactcheckCloneNews"
                };
            }

            var data = await _factCheckApi.Search(query);

            var topicData = await _topicApi.GetAllTopic();

            var languageData = await _languageApi.GetLanguageInfo();

            var model = new CloneNewsVM();

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }

            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            ViewBag.NextPageToken = data.NextPageToken;

            model.ClaimViewModel = data.Claims;

            return new PartialViewResult
            {
                ViewName = "ViewFactcheckCloneNews",
                ViewData = new ViewDataDictionary<List<ClaimViewModel>>(ViewData, data.Claims)
            };
        }

        public async Task<IActionResult> LoadMore(string nextPageToken, string query)
        {
            if (string.IsNullOrEmpty(nextPageToken) || string.IsNullOrEmpty(query))
            {
                return new PartialViewResult
                {
                    ViewName = "ViewFactcheckCloneNews"
                };
            }

            var data = await _factCheckApi.LoadMore(nextPageToken, query);

            var topicData = await _topicApi.GetAllTopic();
            var languageData = await _languageApi.GetLanguageInfo();

            var model = new CloneNewsVM();

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }

            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            ViewBag.ListTopic = new SelectList(topicData.ResultObj, "TopicId", "Tag");
            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");

            ViewBag.NextPageToken = data.NextPageToken;

            model.ClaimViewModel = data.Claims;

            return new PartialViewResult
            {
                ViewName = "ViewFactcheckCloneNews",
                ViewData = new ViewDataDictionary<List<ClaimViewModel>>(ViewData, data.Claims)
            };
        }
    }
}
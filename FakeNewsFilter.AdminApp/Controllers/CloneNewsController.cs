using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Services;
using FakeNewsFilter.ViewModel.Catalog;
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
        private readonly CloneNewsApi _cloneNewsApi;
        private readonly TopicApi _topicApi;
        private readonly LanguageApi _languageApi;
        private readonly IConfiguration _configuration;


        public CloneNewsController(CloneNewsApi cloneNewsApi, TopicApi topicApi, LanguageApi languageApi,
            IConfiguration configuration)
        {
            _cloneNewsApi = cloneNewsApi;
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

        public async Task<IActionResult> FactCheckSearch(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return new PartialViewResult
                {
                    ViewName = "ViewFactcheckCloneNews"
                };
            }

            var data = await _cloneNewsApi.Search(query);

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
                From = DateTime.Now.AddDays(-20),
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

        public async Task<IActionResult> OigetitIndex()
        {
            var topicData = await _topicApi.GetAllTopic();

            var languageData = await _languageApi.GetLanguageInfo();

            var oigetitData = await _cloneNewsApi.GetBreakingOigetitNews("VI");
            
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

            model.OigetitNewsViewModel = new OigetitNewsViewModel() {result = oigetitData};

            return View(model);
        }

        public async Task<IActionResult> OigetitSearch(string query, int page = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                return new PartialViewResult
                {
                    ViewName = "ViewOigetitCloneNews"
                };
            }

            var data = await _cloneNewsApi.SearchOigetitNews(query, page);

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
                ViewName = "ViewOigetitCloneNews",
                ViewData = new ViewDataDictionary<OigetitNewsViewModel>(ViewData, data)
            };
        }

        public async Task<IActionResult> GetOigetitCategory(int categoryId)
        {
            var data = await _cloneNewsApi.GetOigetitCategoryNews(categoryId);

            var topicData = await _topicApi.GetAllTopic();

            var languageData = await _languageApi.GetLanguageInfo();

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }

            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            var model = new OigetitNewsViewModel();
            model.result = data;

            return new PartialViewResult
            {
                ViewName = "ViewOigetitCloneNews",
                ViewData = new ViewDataDictionary<OigetitNewsViewModel>(ViewData, model)
            };
        }

        public async Task<IActionResult> GetOigetitNewsDescription(string newsId)
        {
            var data = await _cloneNewsApi.GetOigetitNewsDesc(newsId);

            var topicData = await _topicApi.GetAllTopic();

            var languageData = await _languageApi.GetLanguageInfo();

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }

            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            return Json(data);
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

            var data = await _cloneNewsApi.LoadMore(nextPageToken, query);

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
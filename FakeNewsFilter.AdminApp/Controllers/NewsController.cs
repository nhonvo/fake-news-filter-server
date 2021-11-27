﻿
using System;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Services;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartBreadcrumbs.Attributes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.AdminApp.Controllers
{
    [Authorize]
    public class NewsController : BaseController
    {

        private readonly NewsApi _newsApi;
        private readonly TopicApi _topicApi;
        private readonly LanguageApi _languageApi;

        public NewsController(NewsApi newsApi, TopicApi topicApi, LanguageApi languageApi)
        {
            _newsApi = newsApi;
            _topicApi = topicApi;
            _languageApi = languageApi;
        }


        [Breadcrumb("News Manager")]
        public async Task<IActionResult> Index()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            var topicData = await _topicApi.GetTopicInfo();
            var languageData = await _languageApi.GetLanguageInfo();

            ViewBag.ListTopic = new SelectList(topicData.ResultObj, "TopicId", "Tag");
            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            var data = await _newsApi.GetNewsInfo();

            return Json(new
            {
                data = data.ResultObj
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsById(int Id)
        {
            var data = await _newsApi.GetById(Id);

            return Json(new
            {
                data = data.ResultObj
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsCreateRequest request)
        {
            if (request.TopicId == null || request.Content == null || request.Description == null || request.Name == null || request.LanguageId == null)
            {
                throw new Exception("Cannot create news");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return RedirectToAction("Index");
            }
            
            var result = await _newsApi.CreateNews(request);

            if (result.IsSuccessed)
            {
                TempData["Result"] = $"Create News Successful!";

                return RedirectToAction("Index");
            }

            TempData["Error"] = result.Message;

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Breadcrumb("Edit News", FromController = typeof(NewsController), FromPage = typeof(Index))]
        public async Task<IActionResult> Edit(int Id)
        {

            var result = await _newsApi.GetById(Id);

            var topicData = await _topicApi.GetTopicInfo();
            var languageData = await _languageApi.GetLanguageInfo();

            ViewBag.ListTopic = new SelectList(topicData.ResultObj, "TopicId", "Tag");
            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");

            if (result.IsSuccessed)
            {
                return View(result.ResultObj);
            }

            return View("Error", "Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewsUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _newsApi.UpdateNews(request);

            if (result.IsSuccessed)
            {
                TempData["result"] = $"Update news {request.Id} successful!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int newsId)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _newsApi.Delete(newsId);

            if (result.IsSuccessed)
            {
                TempData["result"] = $"Delete news successful!";
                return Json("done");
            }

            ModelState.AddModelError("", result.Message);
            return Json("error");
        }
    }
}

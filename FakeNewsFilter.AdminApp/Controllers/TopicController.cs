﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Services;
using FakeNewsFilter.Utilities.Constants;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartBreadcrumbs.Attributes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.AdminApp.Controllers
{
    [Authorize]
    public class TopicController : BaseController
    {
        private readonly TopicApi _topicApi;
        private readonly LanguageApi _languageApi;

        // GET: /<controller>/

        public TopicController(TopicApi topicApi, LanguageApi languageApi)
        {
            _topicApi = topicApi;
            _languageApi = languageApi;

        }

        [Breadcrumb("Topic Manager")]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetTopicNewsRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId
            };

            var data = await _topicApi.GetTopicPaging(request);

            var languageData = await _languageApi.GetLanguageInfo();

           
            ViewBag.ListTopic = new SelectList(data.ResultObj.Items.GroupBy(x => x.Label).Select(y => y.First()).Distinct(), "Label", "Label");
            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopicCreateRequest request)
        {
            //true if any property is null
            bool allPropertiesNull = request.GetType()
                 .GetProperties() //get all properties on object
                 .Select(pi => pi.GetValue(request)) //get value for the property
                 .Any(value => value == null);

            if (allPropertiesNull)
            {
                throw new Exception("Cannot create new topic");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return RedirectToAction("Index");
            }

            var result = await _topicApi.CreateTopic(request);

            if (result.IsSuccessed)
            {
                TempData["Result"] = $"Create User {request.Tag} successful!";

                return RedirectToAction("Index");
            }

            TempData["Error"] = result.Message;

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Breadcrumb("Edit Topic", FromController = typeof(TopicController), FromPage = typeof(Index))]
        public async Task<IActionResult> Edit(int Id)
        {
     
            var result = await _topicApi.GetById(Id);
            var languageData = await _languageApi.GetLanguageInfo();

            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");

            if (result.IsSuccessed)
            {
                return View(result.ResultObj);
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TopicUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _topicApi.UpdateTopic(request);

            if (result.IsSuccessed)
            {
                TempData["result"] = $"Update Topic {request.Tag} successful!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int topicId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return RedirectToAction("Index");
            }

            var result = await _topicApi.Delete(topicId);
            if (result.IsSuccessed)
            {
                TempData["result"] = $"Delete Topic successful!";
                return Json("done");
            }

            ModelState.AddModelError("", result.Message);
            return Json("error");
        }
    }
}

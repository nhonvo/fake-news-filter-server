using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Services;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.AdminApp.Controllers
{
    public class TopicController : BaseController
    {
        private readonly TopicApiClient _topicApiClient;
        // GET: /<controller>/

        public TopicController(TopicApiClient topicApiClient)
        {
            _topicApiClient = topicApiClient;
        }

        [Breadcrumb("Topic Manager")]
        public IActionResult Index()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTopics()
        {
            var data = await _topicApiClient.GetTopicInfo();

            return Json(new
            {
                data = data.ResultObj
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopicCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return RedirectToAction("Index");
            }

            var result = await _topicApiClient.CreateTopic(request);

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
     
            var result = await _topicApiClient.GetById(Id);

            if(result.IsSuccessed)
            {
                return View(result.ResultObj);
            }

            return View("Error", "Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TopicUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _topicApiClient.UpdateTopic(request);

            if (result.IsSuccessed)
            {
                TempData["result"] = $"Update Topic {request.Tag} successful!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int topicId)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _topicApiClient.Delete(topicId);
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

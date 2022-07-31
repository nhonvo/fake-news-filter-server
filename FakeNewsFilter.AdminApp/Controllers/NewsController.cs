using System;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Services;
using FakeNewsFilter.Utilities.Constants;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            // if (TempData["result"] != null)
            // {
            //     ViewBag.SuccessMsg = TempData["Result"];
            // }
            //
            // if (TempData["Error"] != null)
            // {
            //     ViewBag.Error = TempData["Error"];
            // }
            //
            // var topicData = await _topicApi.GetAllTopic();
            // var languageData = await _languageApi.GetLanguageInfo();
            //
            // ViewBag.ListTopic = new SelectList(topicData.ResultObj, "TopicId", "Tag");
            // ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");
            //
            // return View();
            
            
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var data = _newsApi.GetNewsInfo();

            var topicData = await _topicApi.GetAllTopic();

            var languageData = await _languageApi.GetLanguageInfo();
          
            ViewBag.ListLabel = new SelectList(topicData.ResultObj.GroupBy(x => x.Label).Select(y => y.First()).Distinct(), "Label", "Label");

            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");
            
            ViewBag.ListTopic = new SelectList(topicData.ResultObj, "TopicId", "Tag");

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            return View(data.Result);
            
            
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
            var topicData = await _topicApi.GetAllTopic();

            var model = await _newsApi.GetById(Id);

            var languageData = await _languageApi.GetLanguageInfo();

            ViewBag.ListTopic = new SelectList(topicData.ResultObj, "TopicId", "Tag");

            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");

            return PartialView("Edit", model.ResultObj);
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsCreateRequest request)
        {
            if (request.TopicId == null)
            {
                TempData["Error"] = "Please select topic";
                return RedirectToAction("Index");
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

                return Json(new
                {
                   result.ResultObj
                });
            }
            else
            {
                TempData["Error"] = $"Create News Failed!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Breadcrumb("Edit News", FromController = typeof(NewsController), FromPage = typeof(Index))]
        public async Task<IActionResult> Edit(int Id)
        {
            var result = await _newsApi.GetById(Id);

            var topicData = await _topicApi.GetAllTopic();
            var languageData = await _languageApi.GetLanguageInfo();

            ViewBag.ListTopic = new SelectList(topicData.ResultObj, "TopicId", "Tag");
            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");

            if (result.IsSuccessed)
            {
                return View(result.ResultObj);
            }

            return View("Edit");
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

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int newsId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return RedirectToAction("Index");
            }

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
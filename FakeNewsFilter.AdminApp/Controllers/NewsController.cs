using System;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.ClientService;
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
        public async Task<IActionResult> Index(string source)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var data = _newsApi.GetNewsBySouce(source);
            
            var topicData = await _topicApi.GetAllTopic();

            var languageData = await _languageApi.GetLanguageInfo();

            ViewBag.ListLabel =
                new SelectList(topicData.ResultObj.GroupBy(x => x.Label).Select(y => y.First()).Distinct(), "Label",
                    "Label");

            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");

            ViewBag.ListTopic = new SelectList(topicData.ResultObj, "TopicId", "Tag");

            ViewBag.Source = source;

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }

            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            return View(data.Result.ResultObj.Items);
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsById(int Id, string source)
        {
            var topicData = await _topicApi.GetAllTopic();

            var model = await _newsApi.GetById(Id);

            var languageData = await _languageApi.GetLanguageInfo();

            ViewBag.ListTopic = new SelectList(topicData.ResultObj, "TopicId", "Tag");

            ViewBag.ListLanguage = new SelectList(languageData.ResultObj, "Id", "Name");

            ViewBag.ListNewsInTopicId = model.ResultObj.TopicInfo.Select(x => x.TopicId).ToList();

            if (source == "system")
            {
                //map NewsInfoVM to NewsSystemUpdateRequest
                var modelUpdate = new NewsSystemUpdateRequest
                {
                    Id = model.ResultObj.NewsId,
                    Title = model.ResultObj.Title,
                    Description = model.ResultObj.Description,
                    Content = model.ResultObj.Content,
                    LanguageId = model.ResultObj.LanguageId,
                    Publisher = model.ResultObj.Publisher,
                    TopicId = model.ResultObj.TopicId,
                    DatePublished = model.ResultObj.DatePublished,
                };
                return PartialView("EditNewsSystem", modelUpdate);
            }
            else
            {
                //map NewsInfoVM to NewsOutSourceUpdateRequest
                var modelUpdate = new NewsOutSourceUpdateRequest
                {
                    Id = model.ResultObj.NewsId,
                    Title = model.ResultObj.Title,
                    Description = model.ResultObj.Description,
                    LanguageId = model.ResultObj.LanguageId,
                    Publisher = model.ResultObj.Publisher,
                    TopicId = model.ResultObj.TopicId,
                    ImageLink = model.ResultObj.ImageLink,
                    UrlNews = model.ResultObj.UrlNews,
                    DatePublished = model.ResultObj.DatePublished,
                };

                return PartialView("EditNewsOutsource", modelUpdate);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewsByOther(NewsOutSourceCreateRequest request)
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

            var result = await _newsApi.CreateByOther(request);

            if (result.StatusCode == 200)
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

        [HttpPost]
        public async Task<IActionResult> CreateNewsBySystem(NewsSystemCreateRequest request)
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

            var result = await _newsApi.CreateBySystem(request);

            if (result.StatusCode == 200)
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

        [HttpPost]
        public async Task<IActionResult> UpdateNewsBySystem(NewsSystemUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", new {source = "system"});

            var result = await _newsApi.UpdateBySystem(request);

            if (result.StatusCode == 200)
            {
                TempData["result"] = $"Update news {request.Id} successful!";
                return RedirectToAction("Index", new {source = "system"});
            }

            ModelState.AddModelError("", result.Message);

            return RedirectToAction("Index", new {source = "system"});
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNewsByOutSource(NewsOutSourceUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", new {source = "outsource"});

            var result = await _newsApi.UpdateByOutSource(request);

            if (result.StatusCode == 200)
            {
                TempData["result"] = $"Update news {request.Id} successful!";
                return RedirectToAction("Index", new {source = "outsource"});
            }

            ModelState.AddModelError("", result.Message);

            return RedirectToAction("Index", new {source = "outsource"});
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

            if (result.StatusCode == 200)
            {
                TempData["result"] = $"Delete news successful!";
                return Json("done");
            }

            ModelState.AddModelError("", result.Message);
            return Json("error");
        }
        
        [HttpPost]
        public async Task<IActionResult> Archive(int newsId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return RedirectToAction("Index");
            }

            var result = await _newsApi.Archive(newsId);

            if (result.StatusCode == 200)
            {
                TempData["result"] = $"Archive news successful!";
                return Json("done");
            }

            ModelState.AddModelError("", result.Message);
            return Json("error");
        }
    }
}
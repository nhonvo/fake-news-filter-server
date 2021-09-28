
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

        public NewsController(NewsApi newsApi, TopicApi topicApi)
        {
            _newsApi = newsApi;
            _topicApi = topicApi;
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
            var data = await _topicApi.GetTopicInfo();

            ViewBag.ListTopic = new SelectList(data.ResultObj, "TopicId", "Tag");

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

        [HttpPost]
        public async Task<IActionResult> Create(NewsCreateRequest request)
        {
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

    }
}


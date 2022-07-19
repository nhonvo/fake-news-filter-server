using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Services;
using FakeNewsFilter.ViewModel.Catalog.Claims;
using FakeNewsFilter.ViewModel.Catalog.Claims.ClaimCreateNewsVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SmartBreadcrumbs.Attributes;

namespace FakeNewsFilter.AdminApp.Controllers
{
    public class CloneNewsController : BaseController
    {
        private readonly FactCheckApi _factCheckApi;
        private readonly TopicApi _topicApi;
        private readonly LanguageApi _languageApi;

        public CloneNewsController(FactCheckApi factCheckApi, TopicApi topicApi, LanguageApi languageApi)
        {
            _factCheckApi = factCheckApi;
            _topicApi = topicApi;
            _languageApi = languageApi;
        }

        [Breadcrumb("Clone News")]
        public async Task<IActionResult> Index()
        {

            var topicData = await _topicApi.GetAllTopic();

            var languageData = await _languageApi.GetLanguageInfo();

            var model = new ClaimCreateNewsVM();

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

        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return new PartialViewResult
                {
                    ViewName = "CloneNews"
                };
            }

            var data = await _factCheckApi.Search(query);

            var topicData = await _topicApi.GetAllTopic();

            var languageData = await _languageApi.GetLanguageInfo();

            var model = new ClaimCreateNewsVM();

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
                ViewName = "CloneNews",
                ViewData = new ViewDataDictionary<List<ClaimViewModel>>(ViewData, data.Claims)
            };
        }

        public async Task<IActionResult> LoadMore(string nextPageToken, string query)
        {
            if(string.IsNullOrEmpty(nextPageToken) || string.IsNullOrEmpty(query))
            {
                return new PartialViewResult
                {
                    ViewName = "CloneNews"
                };
            }

            var data = await _factCheckApi.LoadMore(nextPageToken, query);

            var topicData = await _topicApi.GetAllTopic();
            var languageData = await _languageApi.GetLanguageInfo();

            var model = new ClaimCreateNewsVM();

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
                ViewName = "CloneNews",
                ViewData = new ViewDataDictionary<List<ClaimViewModel>>(ViewData, data.Claims)
            };
        }
    }
}

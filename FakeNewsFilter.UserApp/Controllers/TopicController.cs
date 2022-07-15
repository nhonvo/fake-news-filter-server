using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace FakeNewsFilter.UserApp.Controllers;

public class TopicViewComponent : ViewComponent
{
    private readonly TopicApi _topicApi;
    private readonly LanguageApi _languageApi;

    public TopicViewComponent(TopicApi topicApi, LanguageApi languageApi)
    {
        _topicApi = topicApi;
        _languageApi = languageApi;

    }
    // GET
    public async Task<IViewComponentResult> InvokeAsync()
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
        return View(data.ResultObj);
    }
}
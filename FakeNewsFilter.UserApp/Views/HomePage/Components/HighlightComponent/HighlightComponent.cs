using FakeNewsFilter.ClientService;
using FakeNewsFilter.UserApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FakeNewsFilter.UserApp.Views.Home.Components.HighlightComponent
{
    public class HighlightComponent : ViewComponent
    {

        private readonly NewsApi _newsApi;

        public HighlightComponent(NewsApi newsApi)
        {
            _newsApi = newsApi;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var objNews = await _newsApi.GetAll();
            return View(objNews.ResultObj.Items);
        }
    }
}

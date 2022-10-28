using FakeNewsFilter.ClientService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FakeNewsFilter.UserApp.Views.Home.Components.HighlightComponent
{
    public class PopsUpNewComponent : ViewComponent
    {

        private readonly NewsApi _newsApi;

        public PopsUpNewComponent(NewsApi newsApi)
        {
            _newsApi = newsApi;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var objNews = await _newsApi.GetById(id);
            return View(objNews.ResultObj);
        }
    }
}

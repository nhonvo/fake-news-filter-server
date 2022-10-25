using FakeNewsFilter.ClientService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FakeNewsFilter.UserApp.Views.News.Components.RecentPostComponent
{
    public class RelatedPostComponent : ViewComponent
    {

        private readonly NewsApi _newsApi;

        public RelatedPostComponent(NewsApi newsApi)
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

using FakeNewsFilter.ClientService;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
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
        public async Task<IViewComponentResult> InvokeAsync(string lang, int index, int size)
        {

            var objNews = await _newsApi.GetNewsPaging(lang, index, size);
            return View(objNews.ResultObj.Items);
        }
    }
}

using FakeNewsFilter.ClientService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FakeNewsFilter.UserApp.Views.Shared.Components.SideBar
{
    public class SideBarComponent : ViewComponent
    {
        private readonly TopicApi _topicApi;

        public SideBarComponent(TopicApi topicApi)
        {
            _topicApi = topicApi;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var topicObject = await _topicApi.GetAllTopic();
            return View(topicObject.ResultObj);
        }
    }
}

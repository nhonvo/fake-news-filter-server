﻿using FakeNewsFilter.ClientService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FakeNewsFilter.UserApp.Views.Home.Components.HighlightComponent
{
    public class ThreeNewsComponent : ViewComponent
    {

        private readonly NewsApi _newsApi;

        public ThreeNewsComponent(NewsApi newsApi)
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

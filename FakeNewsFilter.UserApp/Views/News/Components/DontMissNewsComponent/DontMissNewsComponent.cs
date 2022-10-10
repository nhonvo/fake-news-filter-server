﻿using FakeNewsFilter.ClientService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FakeNewsFilter.UserApp.Views.News.Components.DontMissComponent
{
    public class DontMissNewsComponent : ViewComponent
    {

        private readonly NewsApi _newsApi;

        public DontMissNewsComponent(NewsApi newsApi)
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

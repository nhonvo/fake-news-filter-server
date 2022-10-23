using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.ClientService;
using FakeNewsFilter.UserApp.Services;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace FakeNewsFilter.UserApp.Controllers;

public class NewsController : Controller
{
    private readonly NewsApi _newsApi;
    public NewsController(NewsApi newsApi)
    {
        _newsApi = newsApi;
    }

    //[Route("news/{alias}-{Id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetNewsById(int Id)
    {

        var data = await _newsApi.GetContent(Id);

        return View("Details", data.ResultObj);
    }
  

}
using System;
using System.Threading.Tasks;
using FakeNewsFilter.ClientService;
using FakeNewsFilter.UserApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetNewsById(int Id, string alias)
    {

        var data = await _newsApi.GetById(Id);

        return View("Details", data.ResultObj);
    }

   
}
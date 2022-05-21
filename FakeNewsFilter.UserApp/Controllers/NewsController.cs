using System;
using System.Threading.Tasks;
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
    
    [Route("news/{alias}-{Id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetNewsById(int Id)
    {
        var data = await _newsApi.GetById(Id);

        var getContent = await _newsApi.GetContent(data.Url);
        
        return View("Details", data.ResultObj);
    }
}
using System;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Services;
using FakeNewsFilter.UserApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeNewsFilter.UserApp.Controllers;

public class NewsController : Controller
{
    private readonly NewsApiDeprecated _newsApi;
    private readonly NewsApi _newsApiAdmin;

    public NewsController(NewsApiDeprecated newsApi, NewsApi newsApiAdmin)
    {
        _newsApi = newsApi;
        _newsApiAdmin = newsApiAdmin;
    }
    
    [Route("news/{alias}-{Id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetNewsById(int Id)
    {
    
        var data = await _newsApi.GetContent(Id);
        
        return View("Details", data.ResultObj);
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetNewsByTopic(int topicId)
    {
    
        var data = await _newsApiAdmin.GetNewsByTopic(topicId);
        
        return View("NewsByTopic", data.ResultObj);
    }
    
}
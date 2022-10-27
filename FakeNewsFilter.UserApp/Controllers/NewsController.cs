using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.ClientService;
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
    [AllowAnonymous]
    /*[Route("news/{alias}-{Id:int}")]*/
    public async Task<IActionResult> GetNewsById(int newId)
    {
        var data = await _newsApi.GetContent(newId);
        return View("Details", data.ResultObj);
    }
    [HttpGet]
    public async Task<IActionResult> GetNewsByTopic(int topicId)
    {

        var data = await _newsApi.GetNewsByTopic(topicId);

        return View("NewsByTopic", data.ResultObj == null ? data.ResultObj : data.ResultObj.Items);
    }
    [HttpGet]
    public async Task<IActionResult> Search(String searchName)
    {
        var listNews = await _newsApi.GetAll();
        var data = listNews.ResultObj.Items.Where(x => x.Title.Contains(searchName)).ToList();
        return View("NewsByTopic", data);
    }
    public async Task<IActionResult> TestAsync(int id)
    {
        var data = await _newsApi.GetById(id);

        return View(data.ResultObj);
    }

}
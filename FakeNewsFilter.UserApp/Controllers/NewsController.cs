using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.ClientService;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Catalog.Vote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace FakeNewsFilter.UserApp.Controllers;

public class NewsController : Controller
{
    private readonly NewsApi _newsApi;
    private readonly VoteApi _voteApi;

    public NewsController(NewsApi newsApi, VoteApi voteApi)
    {
        _newsApi = newsApi;
        _voteApi = voteApi;
    }
    [AllowAnonymous]
    public async Task<IActionResult> GetNewsById(int newId)
    {
        var data = await _newsApi.GetNewsById(newId);
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
    [HttpGet]
    public IActionResult AddComponentPopsUp(int newsId)
    {
        return ViewComponent("PopsUpNewComponent", new { id = newsId });
    }
    [HttpPost]
    public async Task<IActionResult> VoteNews([FromForm]VoteCreateRequest request)
    {
        await _voteApi.CreateVote(request);
        return RedirectToAction("Index","HomePage");
    }
}
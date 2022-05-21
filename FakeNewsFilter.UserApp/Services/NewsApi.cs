#nullable enable
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FakeNewsFilter.UserApp.Services;

public interface INewsApi
{
    Task<ApiResult<NewsViewModel>?> GetById(int id);
    Task<ApiResult<NewsSystemViewModel>?> GetContent(int id);
}

public class NewsApi : INewsApi
{
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly IConfiguration _configuration;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public NewsApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<ApiResult<NewsViewModel>?> GetById(int id)
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_configuration["BaseAddress"]);

        var sessions = _httpContextAccessor.HttpContext?.Session.GetString("Token");
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

        var response = await client.GetAsync($"/api/News/{id}");
        var body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonConvert.DeserializeObject<ApiSuccessResult<NewsViewModel>>(body);

        return JsonConvert.DeserializeObject<ApiErrorResult<NewsViewModel>>(body);
    }
    
    public async Task<ApiResult<NewsSystemViewModel>?> GetContent(int id)
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_configuration["BaseAddress"]);

        var sessions = _httpContextAccessor.HttpContext?.Session.GetString("Token");
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

        var response = await client.GetAsync($"/api/News/Content/{id}");
        var body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonConvert.DeserializeObject<ApiSuccessResult<NewsSystemViewModel>>(body);

        return JsonConvert.DeserializeObject<ApiErrorResult<NewsSystemViewModel>>(body);
    }
}
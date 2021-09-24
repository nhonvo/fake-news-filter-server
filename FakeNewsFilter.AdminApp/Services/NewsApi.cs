using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FakeNewsFilter.AdminApp.Services
{
    public interface INewsApi
    {
        Task<ApiResult<List<NewsViewModel>>> GetNewsInfo();

        Task<ApiResult<bool>> CreateNews(NewsCreateRequest request);
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


        //Lấy danh sách tin tức
        public async Task<ApiResult<List<NewsViewModel>>> GetNewsInfo()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                client.BaseAddress = new Uri(_configuration["BaseAddress"]);

                var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

                var respone = await client.GetAsync($"/api/news");


                var body = await respone.Content.ReadAsStringAsync();

                if (respone.IsSuccessStatusCode)
                {

                    return JsonConvert.DeserializeObject<ApiSuccessResult<List<NewsViewModel>>>(body);
                }
                return JsonConvert.DeserializeObject<ApiErrorResult<List<NewsViewModel>>>(body);
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<List<NewsViewModel>>("Error System: " + e.Message);
            }
        }


        public Task<ApiResult<bool>> CreateNews(NewsCreateRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}


using System;
using System.Collections.Generic;
using System.IO;
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


        public async Task<ApiResult<bool>> CreateNews(NewsCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbNews != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbNews.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbNews.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbNews", request.ThumbNews.FileName);
            }

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "Name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.OfficialRating) ? "" : request.OfficialRating.ToString()), "OfficialRating");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "Description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.PostURL) ? "" : request.PostURL.ToString()), "PostURL");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.LanguageCode) ? "" : request.LanguageCode.ToString()), "LanguageCode");

            foreach(int topicId in request.TopicId)
            {
                requestContent.Add(new StringContent(string.IsNullOrEmpty(topicId.ToString()) ? "" : topicId.ToString()), "TopicId");
            }

            var response = await client.PostAsync($"/api/news/", requestContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}


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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FakeNewsFilter.ClientService
{
    public interface INewsApi
    {
        Task<ApiResult<NewsPagingResponse>?> GetAll();
        Task<ApiResult<NewsPagingResponse>> GetNewsBySouce(string source_name);
        Task<ApiResult<List<NewsViewModel>>> GetNewsByTopic(int topicId);

        Task<ApiResult<NewsViewModel>> CreateBySystem(NewsSystemCreateRequest request);
        Task<ApiResult<NewsViewModel>> CreateByOther(NewsOutSourceCreateRequest request);


        Task<ApiResult<string>> UpdateBySystem(NewsSystemUpdateRequest request);
        Task<ApiResult<string>> UpdateByOutSource(NewsOutSourceUpdateRequest request);


        Task<ApiResult<NewsInfoVM>> GetById(int Id);

        Task<ApiResult<string>> Delete(int newsId);
        Task<ApiResult<string>> Archive(int newsId);
    }

    public class NewsApi : BaseApiClient, INewsApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public NewsApi(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<NewsPagingResponse>?> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/News");
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<NewsPagingResponse>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<NewsPagingResponse>>(body);
        }
        //Lấy danh sách tin tức
        public async Task<ApiResult<NewsPagingResponse>> GetNewsBySouce(string source_name)
        {
            var data = await GetAsync<ApiResult<NewsPagingResponse>>($"/api/news/source?keyword={source_name}");

            return data;
        }

        public async Task<ApiResult<List<NewsViewModel>>> GetNewsByTopic(int topicId)
        {
            var data = await GetAsync<ApiResult<List<NewsViewModel>>>($"/api/News/Topic?TopicId={topicId}");

            return data;
        }

        public async Task<ApiResult<NewsViewModel>> CreateBySystem(NewsSystemCreateRequest request)
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
                    data = br.ReadBytes((int) request.ThumbNews.OpenReadStream().Length);
                }

                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbNews", request.ThumbNews.FileName);
            }

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Title) ? "" : request.Title.ToString()),
                "Title");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.OfficialRating)
                    ? ""
                    : request.OfficialRating.ToString()), "OfficialRating");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.Content) ? "" : request.Content.ToString()), "Content");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "Description");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.LanguageId) ? "" : request.LanguageId.ToString()),
                "LanguageId");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.Publisher) ? "" : request.Publisher.ToString()),
                "Publisher");
            requestContent.Add(
                new StringContent((string.IsNullOrEmpty(request.DatePublished.ToString())
                    ? ""
                    : request.DatePublished.ToString()) ?? string.Empty), "DatePublished");

            foreach (int topicId in request.TopicId)
            {
                requestContent.Add(
                    new StringContent(string.IsNullOrEmpty(topicId.ToString()) ? "" : topicId.ToString()), "TopicId");
            }

            var response = await client.PostAsync($"/api/news/CreateBySystem", requestContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return JsonConvert.DeserializeObject<ApiSuccessResult<NewsViewModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<NewsViewModel>>(result);
        }

        public async Task<ApiResult<NewsViewModel>> CreateByOther(NewsOutSourceCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Title) ? "" : request.Title.ToString()),
                "Title");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()),
                "Description");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.OfficialRating)
                    ? ""
                    : request.OfficialRating.ToString()), "OfficialRating");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.SourceCreate) ? "" : request.SourceCreate.ToString()),
                "SourceCreate");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.ImageLink) ? "" : request.ImageLink.ToString()),
                "ImageLink");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.UrlNews) ? "" : request.UrlNews.ToString()), "UrlNews");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.LanguageId) ? "" : request.LanguageId.ToString()),
                "LanguageId");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.Publisher) ? "" : request.Publisher.ToString()),
                "Publisher");
            requestContent.Add(
                new StringContent((string.IsNullOrEmpty(request.DatePublished.ToString())
                    ? ""
                    : request.DatePublished.ToString()) ?? string.Empty), "DatePublished");

            foreach (int topicId in request.TopicId)
            {
                requestContent.Add(
                    new StringContent(string.IsNullOrEmpty(topicId.ToString()) ? "" : topicId.ToString()), "TopicId");
            }

            var response = await client.PostAsync($"/api/news/CreateByOutSource", requestContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return JsonConvert.DeserializeObject<ApiSuccessResult<NewsViewModel>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<NewsViewModel>>(result);
        }

        public async Task<ApiResult<NewsInfoVM>> GetById(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/News/{Id}");
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<NewsInfoVM>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<NewsInfoVM>>(body);
        }

        /////////////update news
        public async Task<ApiResult<string>> UpdateBySystem(NewsSystemUpdateRequest request)
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
                    data = br.ReadBytes((int) request.ThumbNews.OpenReadStream().Length);
                }

                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbNews", request.ThumbNews.FileName);
            }

            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.Id.ToString()) ? "" : request.Id.ToString()), "Id");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Title) ? "" : request.Title.ToString()),
                "Title");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()),
                "Description");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.OfficialRating)
                    ? ""
                    : request.OfficialRating.ToString()), "OfficialRating");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.Content) ? "" : request.Content.ToString()), "Content");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.Publisher) ? "" : request.Publisher.ToString()),
                "Publisher");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.LanguageId) ? "" : request.LanguageId.ToString()),
                "LanguageId");

            foreach (int topicId in request.TopicId)
            {
                requestContent.Add(
                    new StringContent(string.IsNullOrEmpty(topicId.ToString()) ? "" : topicId.ToString()), "TopicId");
            }

            var response = await client.PutAsync($"/api/News/UpdateBySystem", requestContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return new ApiSuccessResult<string>("Update News Successfully");

            return new ApiErrorResult<string>(400, "Update News Unsuccessfully");
        }

        public async Task<ApiResult<string>> UpdateByOutSource(NewsOutSourceUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.Id.ToString()) ? "" : request.Id.ToString()), "Id");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Title) ? "" : request.Title.ToString()),
                "Title");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()),
                "Description");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.OfficialRating)
                    ? ""
                    : request.OfficialRating.ToString()), "OfficialRating");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.Publisher) ? "" : request.Publisher.ToString()),
                "Publisher");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.LanguageId) ? "" : request.LanguageId.ToString()),
                "LanguageId");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.UrlNews) ? "" : request.UrlNews.ToString()),
                "UrlNews");
            requestContent.Add(
                new StringContent(string.IsNullOrEmpty(request.ImageLink) ? "" : request.ImageLink.ToString()),
                "ImageLink");

            foreach (int topicId in request.TopicId)
            {
                requestContent.Add(
                    new StringContent(string.IsNullOrEmpty(topicId.ToString()) ? "" : topicId.ToString()), "TopicId");
            }

            var response = await client.PutAsync($"/api/News/UpdateByOutSource", requestContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return new ApiSuccessResult<string>("Update News Successfully");

            return new ApiErrorResult<string>(400, "Update News Unsuccessfully");
        }


        public async Task<ApiResult<string>> Delete(int newsId)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/News/" + newsId);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)

                return new ApiSuccessResult<string>("Delete News Successfully");

            return new ApiErrorResult<string>(400, "Delete News Unsuccessfully");
        }

        public async Task<ApiResult<string>> Archive(int newsId)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.PutAsync($"/api/News/Archive/" + newsId, null);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)

                return new ApiSuccessResult<string>("Archive News Successfully");

            return new ApiErrorResult<string>(400, "Archive News Unsuccessfully");
        }
    }
}

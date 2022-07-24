using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FakeNewsFilter.AdminApp.Services
{
    public interface ITopicApi
    {
        Task<ApiResult<List<TopicInfoVM>>> GetAllTopic();

        Task<ApiResult<PagedResult<TopicInfoVM>>> GetTopicPaging(GetTopicNewsRequest request);

        Task<ApiResult<string>> CreateTopic(TopicCreateRequest request);

        Task<ApiResult<TopicInfoVM>> GetById(int Id);

        Task<ApiResult<string>> UpdateTopic(TopicUpdateRequest request);

        Task<ApiResult<string>> Delete(int topicId);
    }

    public class TopicApi : BaseApiClient, ITopicApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TopicApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        //Lấy toàn bộ chủ đề tin tức
        public async Task<ApiResult<List<TopicInfoVM>>> GetAllTopic()
        {
                var data = await GetAsync<ApiResult<List<TopicInfoVM>>>($"/api/topic/list");

                return data;
        }

        //Lấy danh sách chủ đề (có phân trang)
        public async Task<ApiResult<PagedResult<TopicInfoVM>>> GetTopicPaging(GetTopicNewsRequest request)
        {
            try
            {
                var data = await GetAsync<ApiResult<PagedResult<TopicInfoVM>>>(
                $"/api/topic/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}&languageId={request.LanguageId}");

                return data;
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<PagedResult<TopicInfoVM>>("Error System: " + e.Message);
            }
        }


        public async Task<ApiResult<string>> CreateTopic(TopicCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbTopic != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbTopic.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbTopic.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbTopic", request.ThumbTopic.FileName);
            }

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Tag) ? "" : request.Tag.ToString()), "Tag");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Label) ? "" : request.Label.ToString()), "Label");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.LanguageId) ? "" : request.LanguageId.ToString()), "LanguageId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "Description");


            var response = await client.PostAsync($"/api/topic/", requestContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return new ApiSuccessResult<string>("Create Topic Successfully");

            return  new ApiErrorResult<string>("Create Topic Unsuccessfully");
        }

        public async Task<ApiResult<TopicInfoVM>> GetById(int Id)
        {
            var data = await GetAsync<ApiResult<TopicInfoVM>>($"/api/topic/{Id}");

            return data;
        }

        public async Task<ApiResult<string>> UpdateTopic(TopicUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbImage", request.ThumbImage.FileName);
            }

            
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Label) ? "" : request.Label.ToString()), "Label");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Tag) ? "" : request.Tag.ToString()), "Tag");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "Description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.LanguageId) ? "" : request.LanguageId.ToString()), "LanguageId");


            var response = await client.PutAsync($"/api/topic/Update/" + request.TopicId, requestContent);

            var result = await response.Content.ReadAsStringAsync();

            
            if (response.IsSuccessStatusCode)

                return new ApiSuccessResult<string>("Update Topic Successfully");

            return  new ApiErrorResult<string>("Update Topic Unsuccessfully");
        }

        public async Task<ApiResult<string>> Delete(int topicId)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/topic/"+topicId);
            var body = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)

                return new ApiSuccessResult<string>("Delete Topic Successfully");

            return  new ApiErrorResult<string>("Delete Topic Unsuccessfully");
        }
    }
}

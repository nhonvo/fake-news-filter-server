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
        Task<ApiResult<List<TopicInfoVM>>> GetTopicInfo();

        Task<ApiResult<bool>> CreateTopic(TopicCreateRequest request);
    }

    public class TopicApi : ITopicApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TopicApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        //Lấy danh sách Topic 
        public async Task<ApiResult<List<TopicInfoVM>>> GetTopicInfo()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                client.BaseAddress = new Uri(_configuration["BaseAddress"]);

                var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

                var respone = await client.GetAsync($"/api/topic/list");

                
                var body = await respone.Content.ReadAsStringAsync();

                if (respone.IsSuccessStatusCode)
                {

                    return JsonConvert.DeserializeObject<ApiSuccessResult<List<TopicInfoVM>>>(body);
                }
                return JsonConvert.DeserializeObject<ApiErrorResult<List<TopicInfoVM>>>(body);
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<List<TopicInfoVM>>("Error System: " + e.Message);
            }
        }

        //Tạo mới Topic
        public async Task<ApiResult<bool>> CreateTopic(TopicCreateRequest request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "Description");


            var response = await client.PostAsync($"/api/topic/", requestContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        //Lấy thông tin topic (dựa vào Id)
        public async Task<ApiResult<TopicInfoVM>> GetById(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/topic/{Id}");
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<TopicInfoVM>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<TopicInfoVM>>(body);
        }

        //Cập nhật tài khoản
        public async Task<ApiResult<bool>> UpdateTopic(TopicUpdateRequest request)
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


            var response = await client.PutAsync($"/api/topic/" + request.TopicId, requestContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        //Xoá người dùng
        public async Task<ApiResult<bool>> Delete(int topicId)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/topic/"+topicId);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }
    }
}

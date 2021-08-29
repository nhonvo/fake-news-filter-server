using System;
using System.Collections.Generic;
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
    public class TopicApiClient : ITopicApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TopicApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


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
    }
}

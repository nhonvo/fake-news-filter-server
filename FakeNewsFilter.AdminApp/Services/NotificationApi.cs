using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.Notification;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FakeNewsFilter.AdminApp.Services
{
    public interface INotificationApi
    {
        Task<GetViewNotifications> GetNotifications();
    }

    public class NotificationApi : INotificationApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<GetViewNotifications> GetNotifications()
        {
            var client = _httpClientFactory.CreateClient("Notification");
            client.BaseAddress = new Uri(_configuration["OneSignalBaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{_configuration["OneSignalRestApiKey"]}");
            var response = await client.GetAsync($"/api/v1/notifications?app_id={_configuration["OneSignalAppId"]}");
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetViewNotifications>(body);
            }
            return new GetViewNotifications("Error System: " + response.StatusCode);
        }
    }
}


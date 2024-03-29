using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.Notification;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FakeNewsFilter.ClientService
{
    public interface INotificationApi
    {
        Task<GetViewNotifications> GetNotifications();
        Task<ApiResult<CreateNotification>> CreateNotification(CreateNotificationRequest createNotificationRequest);
        Task<Notification> GetNotification(string id);
    }

    public class NotificationApi : INotificationApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationApi(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetViewNotifications> GetNotifications()
        {
            var client = _httpClientFactory.CreateClient("Notification");
            client.BaseAddress = new Uri(_configuration["OneSignalBaseAddress"]);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", $"{_configuration["OneSignalRestApiKey"]}");
            var response = await client.GetAsync($"/api/v1/notifications?app_id={_configuration["OneSignalAppId"]}");
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetViewNotifications>(body);
            }

            return new GetViewNotifications("Error System: " + response.StatusCode);
        }

        public async Task<ApiResult<CreateNotification>> CreateNotification(CreateNotificationRequest request)
        {
            var client = _httpClientFactory.CreateClient("Notification");
            client.BaseAddress = new Uri(_configuration["OneSignalBaseAddress"]);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", $"{_configuration["OneSignalRestApiKey"]}");

            var notification = new CreateNotification
            {
                app_id = _configuration["OneSignalAppId"],
                headings = request.headings,
                contents = request.contents,
                name = request.name,
                //if has request.filter is true, it will send to all user

                filters = !String.IsNullOrEmpty(request.filter)
                    ? new List<Filter>
                        {new Filter {field = "tag", key = "language_content", relation = "=", value = request.filter}}
                    : null,
                url = !String.IsNullOrEmpty(request.url) ? request.url : null,
                included_segments = "Khanh test",
            };

            var jsonToString = JsonConvert.SerializeObject(notification);

            var response = await client.PostAsync($"/api/v1/notifications",
                new StringContent(jsonToString, Encoding.UTF8, "application/json"));

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return JsonConvert.DeserializeObject<ApiSuccessResult<CreateNotification>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<CreateNotification>>(result);
        }

        public async Task<Notification> GetNotification(string id)
        {
            var client = _httpClientFactory.CreateClient("Notification");
            client.BaseAddress = new Uri(_configuration["OneSignalBaseAddress"]);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", $"{_configuration["OneSignalRestApiKey"]}");

            var response = await client.GetAsync($"/api/v1/notifications/{id}?app_id={_configuration["OneSignalAppId"]}");

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return JsonConvert.DeserializeObject<Notification>(result);

            return JsonConvert.DeserializeObject<Notification>(result);
        }
    }
}

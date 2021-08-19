﻿using FakeNewsFilter.ViewModel.System.Users;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.WebApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            client.BaseAddress = new Uri("http://localhost:5001");

            var respone = await client.PostAsync("/api/Users/Authenticate", httpContent);

            var token = await respone.Content.ReadAsStringAsync();

            return token;
        }
    }
}

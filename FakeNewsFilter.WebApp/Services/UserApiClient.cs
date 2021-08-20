﻿using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.System.Users;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.WebApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var respone = await client.PostAsync("/api/Users/Authenticate", httpContent);

            var token = await respone.Content.ReadAsStringAsync();

            return token;
        }

        public async Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request)
        {
           
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", request.BearerToken);

            var respone = await client.GetAsync($"/api/users/paging?pageIndex={request.pageIndex}&pageSize={request.pageSize}&keyWork={request.Keyword}");

            var body = await respone.Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<PagedResult<UserViewModel>>(body);

            return users;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.System.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FakeNewsFilter.AdminApp.Services
{
    public class RoleApiClient : IRoleApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/roles");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<RoleViewModel> myDeserializedObjList = (List<RoleViewModel>)JsonConvert.DeserializeObject(body, typeof(List<RoleViewModel>));
                return new ApiSuccessResult<List<RoleViewModel>>("Get All Roles Successful!", myDeserializedObjList);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<RoleViewModel>>>(body);
        }
    }
}

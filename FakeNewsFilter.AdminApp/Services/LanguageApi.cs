using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Language;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FakeNewsFilter.AdminApp.Services
{
    public interface ILanguageApi
    {
        Task<ApiResult<List<GetLanguageRequest>>> GetLanguageInfo();
    }

    public class LanguageApi : ILanguageApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LanguageApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<List<GetLanguageRequest>>> GetLanguageInfo()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                client.BaseAddress = new Uri(_configuration["BaseAddress"]);

                var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

                var respone = await client.GetAsync($"/api/Language/List");


                var body = await respone.Content.ReadAsStringAsync();

                if (respone.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiSuccessResult<List<GetLanguageRequest>>>(body);
                }
                return JsonConvert.DeserializeObject<ApiErrorResult<List<GetLanguageRequest>>>(body);
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<List<GetLanguageRequest>>(500, "Error System: " + e.Message);
            }
        }
    }
}

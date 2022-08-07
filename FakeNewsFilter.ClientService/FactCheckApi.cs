using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Claims;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FakeNewsFilter.AdminApp.Services
{
    public interface IFactCheckApi
    {
        Task<GetClaimsRequest> Search(string query);
        Task<GetClaimsRequest> LoadMore(string nextPageToken, string query);
    }

    public class FactCheckApi : IFactCheckApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public FactCheckApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetClaimsRequest> LoadMore(string nextPageToken, string query)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();

                request.RequestUri = new Uri("https://factchecktools.googleapis.com/v1alpha1/claims:search?key=" + _configuration["GoogleFactCheckAPIKey"] + "&languageCode=en-US&maxAgeDays=200&offset=0&pageSize=20&query=" + query + "&pageToken=" + nextPageToken);
                                
                request.Method = HttpMethod.Get;

                HttpResponseMessage response = await httpClient.SendAsync(request);


                var body = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;


                if (statusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<GetClaimsRequest>(body);
                }
                return JsonConvert.DeserializeObject<GetClaimsRequest>(body);
            }
            catch (FakeNewsException e)
            {
                return new GetClaimsRequest("Error System: " + e.Message);
            }
        }

        public async Task<GetClaimsRequest> Search(string query)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();

                request.RequestUri = new Uri("https://factchecktools.googleapis.com/v1alpha1/claims:search?key=AIzaSyBLYy6lGSiXQd38r-ba0jxsBm56lmXjaXI&languageCode=en-US&maxAgeDays=200&offset=0&pageSize=20&query=" + query);

                request.Method = HttpMethod.Get;

                HttpResponseMessage response = await httpClient.SendAsync(request);


                var body = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;


                if (statusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<GetClaimsRequest>(body);
                }
                return JsonConvert.DeserializeObject<GetClaimsRequest>(body);
            }
            catch (FakeNewsException e)
            {
                return new GetClaimsRequest("Error System: " + e.Message);
            }
        }
    }
}

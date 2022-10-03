using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog;
using FakeNewsFilter.ViewModel.Catalog.Claims;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FakeNewsFilter.ClientService
{
    public interface ICloneNewsApi
    {
        Task<GetClaimsRequest> Search(string query);
        Task<GetClaimsRequest> LoadMore(string nextPageToken, string query);
        Task<List<OigetitNews>> GetBreakingOigetitNews(string languages);
        Task<OigetitNewsViewModel> SearchOigetitNews(string query, int page);
        Task<List<OigetitNews>> GetOigetitCategoryNews(int categoryId);
        Task<string> GetOigetitNewsDesc(string newsId);
    }

    public class CloneNewsApi : ICloneNewsApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CloneNewsApi(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
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

                request.RequestUri = new Uri("https://factchecktools.googleapis.com/v1alpha1/claims:search?key=" +
                                             _configuration["GoogleFactCheckAPIKey"] +
                                             "&languageCode=en-US&maxAgeDays=200&offset=0&pageSize=20&query=" + query +
                                             "&pageToken=" + nextPageToken);

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

                request.RequestUri =
                    new Uri(
                        "https://factchecktools.googleapis.com/v1alpha1/claims:search?key=AIzaSyBLYy6lGSiXQd38r-ba0jxsBm56lmXjaXI&languageCode=en-US&maxAgeDays=200&offset=0&pageSize=20&query=" +
                        query);

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

        public async Task<OigetitNewsViewModel> SearchOigetitNews(string query, int page)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();

                request.RequestUri = new Uri("https://api.oigetit.com:8081/V3/GetArticlesFromSearch?search=" + query +
                                             "&page=" + page + "&size=100&sort=1");

                request.Method = HttpMethod.Get;

                HttpResponseMessage response = await httpClient.SendAsync(request);


                var body = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;


                if (statusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<OigetitNewsViewModel>(body);
                }

                return JsonConvert.DeserializeObject<OigetitNewsViewModel>(body);
            }
            catch (FakeNewsException e)
            {
                return new OigetitNewsViewModel("Error System: " + e.Message);
            }
        }
        public async Task<List<OigetitNews>> GetBreakingOigetitNews(string language)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();

                request.RequestUri = new Uri("https://api.oigetit.com:8081/V2/GetBreakingNews/" + language);

                request.Method = HttpMethod.Get;

                HttpResponseMessage response = await httpClient.SendAsync(request);

                var body = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;


                if (statusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<List<OigetitNews>>(body);
                }

                return JsonConvert.DeserializeObject<List<OigetitNews>>(body);
            }
            catch (FakeNewsException e)
            {
                return new List<OigetitNews>(new OigetitNews[]
                    {new OigetitNews {Title = "Error System: " + e.Message}});
            }
        }
        public async Task<List<OigetitNews>> GetOigetitCategoryNews(int categoryId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();

                request.RequestUri = new Uri("https://api.oigetit.com:8081/V2/GetCategoryNews/EN/" + categoryId);

                request.Method = HttpMethod.Get;

                HttpResponseMessage response = await httpClient.SendAsync(request);

                var body = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;


                if (statusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<List<OigetitNews>>(body);
                }

                return JsonConvert.DeserializeObject<List<OigetitNews>>(body);
            }
            catch (FakeNewsException e)
            {
                return new List<OigetitNews>(new OigetitNews[]
                    {new OigetitNews {Title = "Error System: " + e.Message}});
            }
        }

        public async Task<string> GetOigetitNewsDesc(string newsId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();

                request.RequestUri = new Uri("https://api.oigetit.com:8081/V2/GetArticle/" + newsId);
                request.Method = HttpMethod.Get;

                HttpResponseMessage response = await httpClient.SendAsync(request);

                var body = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;

                JObject result = JObject.Parse(body);

                if (statusCode == HttpStatusCode.OK)
                {
                    return result["Description"]!.ToString();
                }

                return "An error has occurred while trying to get the news description";
            }
            catch (FakeNewsException e)
            {
                return "An error has occurred while trying to get the news description";
            }
        }
    }
}

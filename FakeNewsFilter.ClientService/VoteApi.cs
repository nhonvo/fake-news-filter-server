using System.Net.Http.Headers;
using System.Text;
using FakeNewsFilter.ViewModel.Catalog.Vote;
using FakeNewsFilter.ViewModel.Common;
using Newtonsoft.Json;

namespace FakeNewsFilter.ClientService
{
    public interface IVoteApi
    {
        Task<ApiResult<string>> CreateVote(VoteCreateRequest request);
    }
    public class VoteApi : BaseApiClient, IVoteApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public VoteApi(IHttpClientFactory httpClientFactory,
                       IConfiguration configuration,
                       IHttpContextAccessor httpContextAccessor)
                : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        // create vote 
        public async Task<ApiResult<string>> CreateVote(VoteCreateRequest request)
        {
            var json = JsonConvert.SerializeObject(request);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            
            // [sessions] can't get token it's null
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.PostAsync($"/api/Vote", httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(result)!;

            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(result)!;
        }
    }
}

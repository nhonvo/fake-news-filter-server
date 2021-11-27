using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using FakeNewsFilter.Utilities.Constants;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.System.LoginSocial;
using FakeNewsFilter.ViewModel.System.Users;
using Newtonsoft.Json;

namespace FakeNewsFilter.Application.System
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenValidationResult> ValidationAcessTokenAsync(string accessToken);

        Task<FacebookUserInfoResult> GetUsersInfoAsync(string accessToken);

    }

    public class FacebookAuthService : IFacebookAuthService
    {
        private const string TokenValidationUrl = "";

        private const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";

        private readonly IHttpClientFactory _httpClientFactory;


        public FacebookAuthService(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }

        public async Task<FacebookUserInfoResult> GetUsersInfoAsync(string accessToken)
        {
            var formattedUrl = string.Format(UserInfoUrl, accessToken);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString);
        }


        public async Task<FacebookTokenValidationResult> ValidationAcessTokenAsync(string accessToken)
        {
            string AppId = "263493625606585";
            string AppSecret = "c7e450a259a3a5ec1892d0f99e28b917";

            string formattedUrl = $"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={AppId}|{AppSecret}";

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
        }

        
    }
}


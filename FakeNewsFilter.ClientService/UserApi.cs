using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.ClientServices
{
    public interface IUserApi
    {
        Task<ApiResult<TokenResult>> Authenticate(LoginRequest request);

        Task<ApiResult<List<UserViewModel>>> GetUsers();

        Task<ApiResult<bool>> RegisterUser(RegisterRequest request);

        Task<ApiResult<bool>> UpdateUser(UserUpdateRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);

        Task<ApiResult<bool>> Delete(String UserId);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }

    public class UserApi : IUserApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        
        //Đăng nhập 
        public async Task<ApiResult<TokenResult>> Authenticate(LoginRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");


                var client = _httpClientFactory.CreateClient();

                client.BaseAddress = new Uri(_configuration["BaseAddress"]);

                var respone = await client.PostAsync("/api/Users/Authenticate", httpContent);

                var content = await respone.Content.ReadAsStringAsync();

                if (respone.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiSuccessResult<TokenResult>>(content);
                }

                return JsonConvert.DeserializeObject<ApiErrorResult<TokenResult>>(content);
            }
            catch(FakeNewsException e)
            {
                return new ApiErrorResult<TokenResult>(500,"Error System: " + e.Message);
            }
            
        }

        //Lấy danh sách người dùng (phân trang)
        public async Task<ApiResult<List<UserViewModel>>> GetUsers()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                client.BaseAddress = new Uri(_configuration["BaseAddress"]);

                var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

                var respone = await client.GetAsync($"/api/users/list");

                var body = await respone.Content.ReadAsStringAsync();
                if (respone.IsSuccessStatusCode) {

                    return JsonConvert.DeserializeObject<ApiSuccessResult<List<UserViewModel>>>(body);
                }
                return JsonConvert.DeserializeObject<ApiErrorResult<List<UserViewModel>>>(body);
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<List<UserViewModel>>(500, "Error System: " + e.Message);
            }
        }

        //Tạo tài khoản mới
        public async Task<ApiResult<bool>> RegisterUser(RegisterRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var client = _httpClientFactory.CreateClient();

                client.BaseAddress = new Uri(_configuration["BaseAddress"]);

                var response = await client.PostAsync("/api/Users/Register", httpContent);

                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

                return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
            }
            catch (FakeNewsException e)
            {
                return new ApiErrorResult<bool>(500, "Error System: " + e.Message);
            }

        }

        //Cập nhật tài khoản
        public async Task<ApiResult<bool>> UpdateUser(UserUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.MediaFile != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.MediaFile.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.MediaFile.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "MediaFile", request.MediaFile.FileName);
            }

            requestContent.Add(new StringContent(request.UserId.ToString()), "UserId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.UserName) ? "" : request.UserName.ToString()), "UserName");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "Name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Email) ? "" : request.Email.ToString()), "Email");

           
            var response = await client.PutAsync($"/api/users/", requestContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)

                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        //Lấy thông tin người dùng (dựa vào Id)
        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/users/{id}");
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<UserViewModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<UserViewModel>>(body);
        }

        //Xoá người dùng
        public async Task<ApiResult<bool>> Delete(String UserId)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/users/{UserId}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }

        //Gán quyền người dùng
        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/users/{id}/roles", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}

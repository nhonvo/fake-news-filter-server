using System;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.System.Users;

namespace FakeNewsFilter.WebApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);

        Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request);
    }
}

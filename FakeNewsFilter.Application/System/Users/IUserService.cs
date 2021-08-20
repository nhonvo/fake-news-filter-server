using System;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.System.Users;

namespace FakeNewsFilter.Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest request);

        Task<bool> Register(RegisterRequest request);

        Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request);
    }
}

using System;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.System.Users;

namespace FakeNewsFilter.WebApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);

    }
}

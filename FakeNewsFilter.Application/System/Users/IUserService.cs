using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.System.Users;

namespace FakeNewsFilter.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<List<UserViewModel>>> GetUsers();

        Task<ApiResult<bool>> Update(Guid UserId, UserUpdateRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);

        Task<ApiResult<bool>> Delete(String UserId);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}

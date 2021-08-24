using System.Collections.Generic;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.System.Roles;

namespace FakeNewsFilter.Application.System.Roles
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetAll();
    }
}

using System;
using FakeNewsFilter.ViewModel.System.Users;

namespace FakeNewsFilter.ViewModel.System.Roles
{
    public class UserRoleViewModel
    {
        public UserUpdateRequest EditUser { get; set; }

        public RoleAssignRequest EditRole { get; set; }
    }
}

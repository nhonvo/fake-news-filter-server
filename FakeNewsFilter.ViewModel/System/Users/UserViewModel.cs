using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public Status Status { get; set; }

        public IList<string> Roles { get; set; }
    }
}

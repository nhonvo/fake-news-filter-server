using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FakeNewsFilter.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }
        
        public string Email { get; set; }

        public Status Status { get; set; }

        public string Avatar { get; set; }

        public int? noNewsVoted { get; set; }

        public int? noNewsContributed { get; set; }

        public IList<string> Roles { get; set; }
    }
}

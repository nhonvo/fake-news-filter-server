using System;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class UserUpdateRequest
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Avatar { get; set; }

        public IFormFile MediaFile { get; set; }

    }
}

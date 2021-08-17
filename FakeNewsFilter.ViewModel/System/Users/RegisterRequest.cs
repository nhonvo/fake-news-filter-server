﻿using System;
namespace FakeNewsFilter.ViewModel.System.Users
{
    public class RegisterRequest
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}

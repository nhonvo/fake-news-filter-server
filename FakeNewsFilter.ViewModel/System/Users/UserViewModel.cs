using System;
using System.ComponentModel.DataAnnotations;
using FakeNewsFilter.Data.Enums;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }

        [Display(Name = "Họ và Tên")]
        public string FullName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public Status Status { get; set; }
    }
}

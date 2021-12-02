using System;
using System.Collections.Generic;
using System.Text;

namespace FakeNewsFilter.Data.Entities
{
    public class ForgotPassword
    {
        public int IdForgotPassword { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string OTP { get; set; }
        public DateTime DateTime { get; set; }
    }
}

using System;
using FluentValidation;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class LoginRequestUserValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(6).WithMessage("Password is at least 6 characters.");
        }
    }
}

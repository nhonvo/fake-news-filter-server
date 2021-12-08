using System;
using FakeNewsFilter.API.Controllers;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class LoginRequestUserValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestUserValidator(IStringLocalizer<UsersController> localizer)
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(x=>localizer["UsernameIsRequired"]);
            RuleFor(x => x.Password).NotEmpty().WithMessage(x => localizer["PasswordIsRequired"]).MinimumLength(6).WithMessage(x => localizer["PasswordAtLeast6Characters"]);
        }
    }
}

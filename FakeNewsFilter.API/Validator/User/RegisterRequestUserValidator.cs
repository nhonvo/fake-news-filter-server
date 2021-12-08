using System;
using FakeNewsFilter.API.Controllers;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class RegisterRequestUserValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestUserValidator(IStringLocalizer<UsersController> localizer)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(x => localizer["NameIsRequired"]).MaximumLength(100).WithMessage(x => localizer["NameMaximum100Characters"]);
            RuleFor(x => x.Email).NotEmpty().WithMessage(x => localizer["EmailIsRequired"]).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage(x => localizer["EmailWrongFormat"]);
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(x => localizer["PhoneIsRequired"]);//.Matches(@"/^\+?(\d.*){10,}$/").WithMessage(x => localizer["PhoneWrongFormat"]);
            RuleFor(x => x.UserName).NotEmpty().WithMessage(x => localizer["UsernameIsRequired"]);
            RuleFor(x => x.Password).NotEmpty().WithMessage(x => localizer["PasswordIsRequired"]).MinimumLength(6).WithMessage(x => localizer["PasswordAtLeast6Characters"]);

            RuleFor(x => x).Custom((request, context) =>
            {
                if(request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Password", localizer["ConformPasswordNotMatch"]);
                }
            });

        }
    }
}

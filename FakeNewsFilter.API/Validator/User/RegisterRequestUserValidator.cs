using System;
using FluentValidation;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class RegisterRequestUserValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").MaximumLength(100).WithMessage("Name cannot over 100 characters.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email format not match.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");//.Matches(@"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/").WithMessage("Format not match.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(6).WithMessage("Password is at least 6 characters.");

            RuleFor(x => x).Custom((request, context) =>
            {
                if(request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm password not match.");
                }
            });

        }
    }
}

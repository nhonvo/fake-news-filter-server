using FakeNewsFilter.API.Controllers;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class UpdateRequestUserValidator : AbstractValidator<UserUpdateRequest>
    {
        public UpdateRequestUserValidator(IStringLocalizer<UsersController> localizer)
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(x => localizer["UserIsRequired"]);
            RuleFor(x => x.Email).NotEmpty().WithMessage(x => localizer["EmailIsRequired"]).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage(x => localizer["EmailWrongFormat"]);
        }
    }
}

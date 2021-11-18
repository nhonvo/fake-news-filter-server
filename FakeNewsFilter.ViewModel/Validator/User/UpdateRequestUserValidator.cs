using FluentValidation;

namespace FakeNewsFilter.ViewModel.System.Users
{
    public class UpdateRequestUserValidator : AbstractValidator<UserUpdateRequest>
    {
        public UpdateRequestUserValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email format not match.");
        }
    }
}

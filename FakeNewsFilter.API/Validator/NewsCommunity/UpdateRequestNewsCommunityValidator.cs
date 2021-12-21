using FakeNewsFilter.API.Controllers;
using FakeNewsFilter.ViewModel.Catalog.NewsCommunity;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.API.Validator.NewsCommunity
{
    public class UpdateRequestNewsCommunityValidator : AbstractValidator<NewsCommunityUpdateRequest>
    {
        public UpdateRequestNewsCommunityValidator(IStringLocalizer<NewsCommunityController> localizer)
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(x => localizer["TitleIsRequired"]);
            RuleFor(x => x.Content).NotEmpty().WithMessage(x => localizer["ContentIsRequired"]);
        }
    }
}

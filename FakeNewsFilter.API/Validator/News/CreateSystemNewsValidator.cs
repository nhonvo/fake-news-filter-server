using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FluentValidation;
using FakeNewsFilter.API.Controllers;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.API.Validator.News
{
    public class CreateSystemNewsValidator : AbstractValidator<NewsSystemCreateRequest>
    {
        public CreateSystemNewsValidator(IStringLocalizer<NewsController> localizer)
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(x => localizer["NameIsRequired"]);
            RuleFor(x => x.Content).NotEmpty().WithMessage(x => localizer["ContentIsRequired"]);
            RuleFor(x => x.LanguageId).NotEmpty().WithMessage(x => localizer["LanguageIsRequired"]);
        }
    }
}

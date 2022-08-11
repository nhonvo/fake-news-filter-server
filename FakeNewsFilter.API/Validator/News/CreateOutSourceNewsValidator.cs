using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FluentValidation;
using FakeNewsFilter.API.Controllers;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.API.Validator.News
{
    public class CreateOutSourceNewsValidator : AbstractValidator<NewsOutSourceCreateRequest>
    {
        public CreateOutSourceNewsValidator(IStringLocalizer<NewsController> localizer)
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(x => localizer["TitleIsRequired"]);
            RuleFor(x => x.UrlNews).NotEmpty().WithMessage(x => localizer["UrlNewsIdRequired"]);
            RuleFor(x => x.LanguageId).NotEmpty().WithMessage(x => localizer["LanguageIdRequired"]);
        }
    }
}


using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FluentValidation;
using FakeNewsFilter.API.Controllers;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.ViewModel.Validator.News
{
    public class UpdateOutSourceNewsValidator : AbstractValidator<NewsOutSourceUpdateRequest>
    {
        public UpdateOutSourceNewsValidator(IStringLocalizer<NewsController> localizer)
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(x => localizer["NameIsRequired"]);
        }
    }
}

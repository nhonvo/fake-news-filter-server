using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FluentValidation;
using FakeNewsFilter.API.Controllers;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.ViewModel.Validator.News
{
    public class UpdateSystemNewsValidator : AbstractValidator<NewsSystemUpdateRequest>
    {
        public UpdateSystemNewsValidator(IStringLocalizer<NewsController> localizer)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(x => localizer["NewsIdIsRequired"]);
        }
    }
}

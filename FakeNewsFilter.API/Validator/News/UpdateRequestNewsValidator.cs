using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using FakeNewsFilter.API.Controllers;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.ViewModel.Validator.News
{
    public class UpdateRequestNewsValidator : AbstractValidator<NewsUpdateRequest>
    {
        public UpdateRequestNewsValidator(IStringLocalizer<NewsController> localizer)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(x => localizer["NameIsRequired"]);
            RuleFor(x => x.Description).NotEmpty().WithMessage(x => localizer["DescriptionIsRequired"]);
            RuleFor(x => x.Content).NotEmpty().WithMessage(x => localizer["ContentIsRequired"]);
        }
    }
}

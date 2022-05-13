﻿using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using FakeNewsFilter.API.Controllers;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.ViewModel.Validator.News
{
    public class CreateRequestNewsValidator : AbstractValidator<NewsCreateRequest>
    {
        public CreateRequestNewsValidator(IStringLocalizer<NewsController> localizer)
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(x => localizer["NameIsRequired"]);
        }
    }
}

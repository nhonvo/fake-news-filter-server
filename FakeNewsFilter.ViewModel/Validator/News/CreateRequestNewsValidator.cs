﻿using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeNewsFilter.ViewModel.Validator.News
{
    public class CreateRequestNewsValidator : AbstractValidator<NewsCreateRequest>
    {
        public CreateRequestNewsValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
        }
    }
}

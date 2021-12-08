using FakeNewsFilter.ViewModel.Catalog.Story;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using FakeNewsFilter.API.Controllers;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.ViewModel.Validator.Story
{
    public class CreateRequestStoryValidator : AbstractValidator<StoryCreateRequest>
    {
        public CreateRequestStoryValidator(IStringLocalizer<StoryController> localizer)
        {
            RuleFor(x => x.Link).NotEmpty().WithMessage(x => localizer["LinkIsRequired"]);
        }
    }
}

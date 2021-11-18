using FakeNewsFilter.ViewModel.Catalog.Story;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeNewsFilter.ViewModel.Validator.Story
{
    public class CreateRequestStoryValidator : AbstractValidator<StoryCreateRequest>
    {
        public CreateRequestStoryValidator()
        {
            RuleFor(x => x.Link).NotEmpty().WithMessage("Link is required.");
        }
    }
}

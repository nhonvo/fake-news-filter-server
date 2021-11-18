using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeNewsFilter.ViewModel.Validator.Topic
{
    public class CreateRequestTopicValidator : AbstractValidator<TopicCreateRequest>
    {
        public CreateRequestTopicValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Tag).NotEmpty().WithMessage("Tag is required.");
            RuleFor(f => f.ThumbTopic).NotNull().WithMessage("Please Attach your photo");
        }
    }
}

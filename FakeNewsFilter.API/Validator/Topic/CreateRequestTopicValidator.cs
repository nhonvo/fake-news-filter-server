using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.ViewModel.Validator.Topic
{
    public class CreateRequestTopicValidator : AbstractValidator<TopicCreateRequest>
    {
        public CreateRequestTopicValidator()
        {
            //RuleFor(x => x.Description).MinimumLength(3).WithMessage(
            //"Name must be at least 3 characters long".Localize()
            //.In("vi", "Das name must be at least 3 characters long"));
            RuleFor(x => x.Description).NotNull().WithMessage("DescriptionIsRequired");
            RuleFor(x => x.Tag).NotEmpty().WithMessage("Tag is required.");
            RuleFor(f => f.ThumbTopic).NotNull().WithMessage("Please Attach your photo");
        }
    }
}

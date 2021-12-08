using FakeNewsFilter.API.Controllers;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.ViewModel.Validator.Topic
{
    public class CreateRequestTopicValidator : AbstractValidator<TopicCreateRequest>
    {
        public CreateRequestTopicValidator(IStringLocalizer<TopicController> localizer)
        {
            //RuleFor(x => x.Description).MinimumLength(3).WithMessage(
            //"Name must be at least 3 characters long".Localize()
            //.In("vi", "Das name must be at least 3 characters long"));
            RuleFor(x => x.Description).NotNull().WithMessage(x => localizer["DescriptionIsRequired"]);
            RuleFor(x => x.Tag).NotEmpty().WithMessage(x => localizer["TagIsRequired"]);
            RuleFor(f => f.ThumbTopic).NotNull().WithMessage(x => localizer["PhotoIsRequired"]);
        }
    }
}

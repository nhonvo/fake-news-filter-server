using FakeNewsFilter.API.Controllers;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FakeNewsFilter.ViewModel.Validator.Topic
{
    public class UpdateRequestTopicValidator : AbstractValidator<TopicUpdateRequest>
    {
        public UpdateRequestTopicValidator(IStringLocalizer<TopicController> localizer)
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage(x => localizer["DescriptionIsRequired"]);
            RuleFor(x => x.Tag).NotEmpty().WithMessage(x => localizer["TagIsRequired"]);
        }
    }
}

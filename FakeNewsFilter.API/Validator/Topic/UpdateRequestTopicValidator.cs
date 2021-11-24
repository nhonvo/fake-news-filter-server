using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FluentValidation;

namespace FakeNewsFilter.ViewModel.Validator.Topic
{
    public class UpdateRequestTopicValidator : AbstractValidator<TopicUpdateRequest>
    {
        public UpdateRequestTopicValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Tag).NotEmpty().WithMessage("Tag is required.");
        }
    }
}

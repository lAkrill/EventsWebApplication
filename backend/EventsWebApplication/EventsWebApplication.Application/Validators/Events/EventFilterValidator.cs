using FluentValidation;

namespace EventsWebApplication.Application.Validators.Events
{
    public class EventFilterValidator : AbstractValidator<EventFilterDto>
    {
        public EventFilterValidator()
        {
            RuleFor(e => e.Location)
                .EventLocation()
                .When(e => !string.IsNullOrEmpty(e.Location));

            RuleFor(e => e.SearchQuery)
                .MaximumLength(50).WithMessage("Search query can't be greater than 50")
                .When(e => !string.IsNullOrEmpty(e.SearchQuery));
        }
    }
}

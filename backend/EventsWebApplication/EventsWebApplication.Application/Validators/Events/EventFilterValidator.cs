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
                .When(e => !string.IsNullOrEmpty(e.SearchQuery)); ;

            RuleFor(e => e.Page)
                .Must(page => page > 0).WithMessage("Page can't be less than 1");

            RuleFor(e => e.PageSize)
                .Must(pageSize => pageSize > 0).WithMessage("Page size can't be less than 1");
        }
    }
}

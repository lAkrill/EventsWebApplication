using EventsWebApplication.Application.Dtos.EventDtos;
using FluentValidation;

namespace EventsWebApplication.Application.Validators.Events
{
    public class PageValidator : AbstractValidator<PageDto>
    {
        public PageValidator()
        {
            RuleFor(e => e.Page)
                .Must(page => page > 0).WithMessage("Page number can't be less than 1");

            RuleFor(e => e.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
        }
    }
}

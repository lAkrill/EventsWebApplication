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
                .Must(pageSize => pageSize > 0).WithMessage("Page size can't be less than 1");
        }
    }
}

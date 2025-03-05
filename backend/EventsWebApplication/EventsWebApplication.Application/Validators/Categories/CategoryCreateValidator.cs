using FluentValidation;

namespace EventsWebApplication.Application.Validators.Categories
{
    public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateValidator()
        {
            RuleFor(category => category.Title)
                .TitleOrName();
        }
    }
}

using FluentValidation;

namespace EventsWebApplication.Application.Validators.Categories
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(category => category.Title)
                .TitleOrName();
        }
    }
}

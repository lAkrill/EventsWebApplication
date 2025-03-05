using FluentValidation;

namespace EventsWebApplication.Application.Validators.Users
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
        {
            RuleFor(user => user.FirstName)
                .UserName();

            RuleFor(user => user.LastName)
                .UserName();

            RuleFor(user => user.Email)
                .UserEmail();

            RuleFor(user => user.Password)
                .PasswordCreating();

            RuleFor(user => user.Birthday)
                .Must(BeAValidDate).WithMessage("Birthday must be a valid date");
        }

        private bool BeAValidDate(DateOnly date)
        {
            return date <= DateOnly.FromDateTime(DateTime.UtcNow);
        }
    }
}

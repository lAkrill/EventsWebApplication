using FluentValidation;

namespace EventsWebApplication.Application.Validators.Users
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(user => user.Email)
                .UserEmail();

            RuleFor(user => user.Password)
                .Password();
        }
    }
}

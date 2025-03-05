using FluentValidation;

namespace EventsWebApplication.Application.Validators.Users
{
    public class UserRoleValidator : AbstractValidator<UserRoleDto>
    {
        public UserRoleValidator()
        {
            RuleFor(user => user.Role)
                .IsInEnum().WithMessage("Invalid user role");
        }
    }
}

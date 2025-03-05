using FluentValidation;

namespace EventsWebApplication.Application.Validators
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Password should not be null")
                .NotEmpty().WithMessage("Password should not be empty");
        }
        public static IRuleBuilderOptions<T, string> PasswordCreating<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Password()
                .Length(8, 20).WithMessage("Password should have length between 8 and 20")
                .Matches("[A-Z]").WithMessage("Password should contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password should contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password should contain at least one digit")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password should contain at least one special character.");
        }

        public static IRuleBuilderOptions<T, string?> TitleOrName<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("{PropertyName} should not be null")
                .NotEmpty().WithMessage("{PropertyName} should not be empty")
                .Length(3, 50).WithMessage("{PropertyName} should have length between 3 and 50");
        }
        public static IRuleBuilderOptions<T, string?> EventDescription<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(500).WithMessage("Event description should not exceed 500 characters");
        }

        public static IRuleBuilderOptions<T, string?> EventLocation<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(200).WithMessage("Event location should not exceed 500 characters");
        }

        public static IRuleBuilderOptions<T, string?> UserName<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {

            return ruleBuilder
                .NotNull().WithMessage("{PropertyName} should not be null")
                .NotEmpty().WithMessage("{PropertyName} should not be empty")
                .Must(username => username != null && (username.Length >= 3 && username.Length <= 50))
                .WithMessage("{PropertyName} should have length between 3 and 20");
        }

        public static IRuleBuilderOptions<T, string> UserEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Email should not be null")
                .NotEmpty().WithMessage("Email should not be empty")
                .EmailAddress().WithMessage("Email doesn't match pattern");
        }

        public static IRuleBuilderOptions<T, int> EventMaxParticipants<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThan(0).WithMessage("Quantity should be greater than 0");
        }
    }
}

using CSharpFunctionalExtensions;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace EventsWebApplication.Core.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; } = UserRole.RegularUser;

        private User()
        {
            
        }


        private User(Guid id, string firstName, string lastName, string emailAddress, string passwordHash)
        {
            Id = id;
            
            Email = emailAddress;
            PasswordHash = passwordHash;
        }

        public static Result<User> Create(string firstName, string lastName, string email, string passwordHash)
        {

            if (string.IsNullOrWhiteSpace(passwordHash))
                return Result.Failure<User>("Пароль не может быть пустым.");

            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<User>("Имя не должно быть пустым.");

            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<User>("Фамилия не должна быть пустой.");


            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrWhiteSpace(email) ||
                !Regex.IsMatch(email, emailRegex))
                return Result.Failure<User>("Email имеет неверный формат.");

            return Result.Success(new User(Guid.NewGuid(), firstName, lastName, email, passwordHash));
        }

        public Result ChangeRole(UserRole role)
        {
            if (!Enum.IsDefined(typeof(UserRole), role))
                return Result.Failure("Недопустимая роль.");
            Role = role;
            return Result.Success();
        }

        public Result Update(string firstName, string lastName, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                return Result.Failure<User>("Пароль не может быть пустым.");

            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<User>("Имя не должно быть пустым.");

            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<User>("Фамилия не должна быть пустой.");


            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrWhiteSpace(email) ||
                !Regex.IsMatch(email, emailRegex))
                return Result.Failure<User>("Email имеет неверный формат.");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;

            return Result.Success();
        }
    }
}

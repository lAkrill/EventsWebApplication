public record UserRegisterDto(
    string FirstName,
    string LastName,
    string Email,
    DateOnly Birthday,
    string Password);

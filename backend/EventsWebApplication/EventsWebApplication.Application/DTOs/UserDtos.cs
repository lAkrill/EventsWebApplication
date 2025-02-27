public record UserRegisterDto(
    string FirstName,
    string LastName,
    string Email,
    string Password);

public record UserUpdateDto(
    string FirstName,
    string LastName,
    string Email,
    string Password);

public record UserResponseDto(
    Guid Id,
    string FullName,
    string Email,
    UserRole Role);

public record UserLoginDto(string Email, string Password);
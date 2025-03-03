public record UserReadDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateOnly Birthday,
    UserRole Role);

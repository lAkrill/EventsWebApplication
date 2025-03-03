using EventsWebApplication.Core.Models;

public record UserReadDetailDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateOnly Birthday,
    UserRole Role,
    List<Participant> Participants);
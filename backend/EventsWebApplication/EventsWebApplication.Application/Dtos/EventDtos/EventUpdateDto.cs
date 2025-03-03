using EventsWebApplication.Core.Models;
using Microsoft.AspNetCore.Http;

public record EventUpdateDto(
    string Title,
    string Description,
    DateTime Date,
    string Location,
    Guid CategoryId,
    int MaxParticipants,
    IFormFile? Image);

using EventsWebApplication.Core.Models;
using Microsoft.AspNetCore.Http;

public record EventCreateDto(
    string Title,
    string Description,
    DateTime Date,
    string Location,
    Guid CategoryId,
    int MaxParticipants,
    IFormFile? Image);

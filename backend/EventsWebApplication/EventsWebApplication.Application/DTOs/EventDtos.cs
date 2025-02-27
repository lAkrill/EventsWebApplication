using Microsoft.AspNetCore.Http;

public record EventCreateDto(
    string Title,
    string Description,
    DateTime Date,
    string Location,
    Guid CategoryId,
    int MaxParticipants,
    IFormFile? Image);

public record EventUpdateDto(
    string Title,
    string Description,
    DateTime Date,
    string Location,
    Guid CategoryId,
    int MaxParticipants,
    IFormFile? Image);

public record EventResponseDto(
    Guid Id,
    string Title,
    string Description,
    DateTime Date,
    string Location,
    string CategoryName,
    int CurrentParticipants,
    int MaxParticipants,
    string? ImageUrl);

public record EventFilterDto(
    DateTime? Date,
    string? Location,
    Guid? CategoryId,
    string? SearchQuery);
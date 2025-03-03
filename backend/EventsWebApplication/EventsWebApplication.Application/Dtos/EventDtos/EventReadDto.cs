public record EventReadDto(
    Guid Id,
    string Title,
    string Description,
    DateTime Date,
    string Location,
    CategoryReadDto CategoryReadDto,
    int CurrentParticipants,
    int MaxParticipants,
    string? ImageUrl);

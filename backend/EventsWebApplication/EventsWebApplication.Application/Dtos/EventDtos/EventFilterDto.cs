public record EventFilterDto(
    DateTime? Date,
    string? Location,
    Guid? CategoryId,
    string? SearchQuery);
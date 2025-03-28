using EventsWebApplication.Application.Dtos.EventDtos;

public record EventFilterDto : PageDto
{
    public DateTime? Date { get; set; }
    public string? Location { get; set; }
    public Guid? CategoryId { get; set; }
    public string? SearchQuery { get; set; }
}
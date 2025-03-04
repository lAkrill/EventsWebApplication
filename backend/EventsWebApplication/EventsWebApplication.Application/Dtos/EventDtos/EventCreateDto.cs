using Microsoft.AspNetCore.Http;

public record EventCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public Guid? CategoryId { get; set; }
    public int MaxParticipants { get; set; }
    public IFormFile? Image { get; set; }
}

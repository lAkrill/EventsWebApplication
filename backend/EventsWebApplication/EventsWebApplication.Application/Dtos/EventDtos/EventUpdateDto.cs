using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

public record EventUpdateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public Guid? CategoryId { get; set; }
    public int MaxParticipants { get; set; }
    public IFormFile? Image { get; set; }
    public bool RemoveImage { get; set; }
}

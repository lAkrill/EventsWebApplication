using System.Text.Json.Serialization;

public record CategoryUpdateDto
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Title { get; set; }
}

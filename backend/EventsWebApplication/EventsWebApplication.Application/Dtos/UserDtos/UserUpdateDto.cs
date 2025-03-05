using System.Text.Json.Serialization;

public record UserUpdateDto
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly Birthday { get; set; }
    public string Password { get; set; }
}

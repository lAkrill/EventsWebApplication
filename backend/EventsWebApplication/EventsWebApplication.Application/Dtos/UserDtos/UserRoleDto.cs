using System.Text.Json.Serialization;

public record UserRoleDto
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public UserRole Role { get; set; }
}
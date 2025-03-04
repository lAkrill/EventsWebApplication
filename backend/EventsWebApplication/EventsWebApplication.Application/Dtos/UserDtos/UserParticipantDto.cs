public record UserParticipantDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly Birthday { get; set; }
    public UserRole Role { get; set; }
    public List<ParticipantUserDto> ParticipantUserDtos { get; set; }
}
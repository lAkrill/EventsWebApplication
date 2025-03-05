public record UserRegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly Birthday { get; set; }
    public string Password { get; set; }
}

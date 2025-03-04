public record EventReadDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public CategoryReadDto? CategoryReadDto { get; set; }
    public int CurrentParticipants { get; set; }
    public int MaxParticipants { get; set; }
    public string? ImagePath { get; set; }
}

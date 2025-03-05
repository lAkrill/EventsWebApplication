public record EventFilterDto
{
    public DateTime? Date { get; set; }
    public string? Location { get; set; }
    public Guid? CategoryId { get; set; }
    public string? SearchQuery { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
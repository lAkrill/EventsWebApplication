namespace EventsWebApplication.Application.Dtos.EventDtos
{
    public record PageDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}

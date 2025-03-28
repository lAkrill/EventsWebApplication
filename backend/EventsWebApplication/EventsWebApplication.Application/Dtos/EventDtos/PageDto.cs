namespace EventsWebApplication.Application.Dtos.EventDtos
{
    public record PageDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

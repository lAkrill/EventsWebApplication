namespace EventsWebApplication.Core.Models
{
    public class Category 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Event> Events { get; set; }
    }
}

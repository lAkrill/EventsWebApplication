namespace EventsWebApplication.Core.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int MaxParticipants { get; set; }
        public string? ImagePath { get; set; }
        public List<Participant>? Participants { get; set; }
        public List<User>? Users { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}

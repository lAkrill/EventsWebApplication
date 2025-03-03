using CSharpFunctionalExtensions;

namespace EventsWebApplication.Core.Models
{
    public class Participant
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}

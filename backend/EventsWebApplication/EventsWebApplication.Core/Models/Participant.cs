using CSharpFunctionalExtensions;

namespace EventsWebApplication.Core.Models
{
    public class Participant
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid EventId { get; private set; }
        public DateTime RegistrationDate { get; private set; }

        private Participant()
        {
            
        }

        private Participant(Guid id, Guid userId, Guid eventId, DateTime registrationDate)
        {
            Id = id;
            UserId = userId;
            EventId = eventId;
            RegistrationDate = registrationDate;
        }

        internal static Result<Participant> Create(Guid userId, Guid eventId)
        {
            return Result.Success(new Participant(Guid.NewGuid(), userId, eventId, DateTime.UtcNow));
        }
    }
}

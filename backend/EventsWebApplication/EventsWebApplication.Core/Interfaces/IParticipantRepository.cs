using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Core.Interfaces
{
    public interface IParticipantRepository
    {
        Task<Participant?> GetParticipantByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Participant>> GetParticipantsByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
        Task AddParticipantAsync(Participant participant, CancellationToken cancellationToken = default);
        Task RemoveParticipantAsync(Participant participant, CancellationToken cancellationToken = default);
    }
}

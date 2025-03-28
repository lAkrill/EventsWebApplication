using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Core.Interfaces
{
    public interface IParticipantRepository
    {
        Task<Participant?> GetParticipantAsync(Guid userId, Guid eventId, CancellationToken ct = default);
        Task AddParticipantAsync(Participant participant, CancellationToken ct = default);
        Task DeleteParticipantAsync(Participant participant, CancellationToken ct = default);
    }
}
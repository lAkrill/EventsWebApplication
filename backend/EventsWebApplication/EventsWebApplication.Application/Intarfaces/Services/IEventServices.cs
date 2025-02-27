using CSharpFunctionalExtensions;
using EventsWebApplication.Core.Models;
namespace EventsWebApplication.Application.Intarfaces.Services
{
    public interface IEventService
    {
        Task<Result<Event>> CreateEventAsync(
            string title,
            string description,
            DateTime date,
            string location,
            Guid categoryId,
            Guid userId,
            int maxParticipants,
            CancellationToken cancellationToken = default);

        Task<Result<Event>> UpdateEventAsync(
            Guid eventId,
            string title,
            string description,
            DateTime date,
            string location,
            Guid categoryId,
            int maxParticipants,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteEventAsync(Guid eventId, CancellationToken cancellationToken = default);
        Task<Result<Event>> GetEventByIdAsync(Guid eventId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<Event>>> GetEventsByCriteriaAsync(
            DateTime? date,
            string location,
            Guid? categoryId,
            CancellationToken cancellationToken = default);
    }
}

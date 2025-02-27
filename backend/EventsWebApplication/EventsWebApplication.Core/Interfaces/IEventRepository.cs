using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Core.Interfaces
{
    public interface IEventRepository
    {
        Task<Event?> GetEventByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Event?> GetEventByTitleAsync(string title, CancellationToken cancellationToken = default);
        Task<IEnumerable<Event>> GetAllEventsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Event>> GetEventsByCriteriaAsync(
            DateTime? date,
            string? location,
            Guid? categoryId,
            CancellationToken cancellationToken = default
        );
        Task AddEventAsync(Event newEvent, CancellationToken cancellationToken = default);
        Task UpdateEventAsync(Event updatedEvent, CancellationToken cancellationToken = default);
        Task DeleteEventAsync(Event deletedEvent, CancellationToken cancellationToken = default);
    }
}

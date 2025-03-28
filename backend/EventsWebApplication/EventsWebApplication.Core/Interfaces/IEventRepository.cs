using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Core.Interfaces
{
    public interface IEventRepository
    {
        Task AddEventAsync(Event newEvent, CancellationToken ct = default);
        Task DeleteEventAsync(Event deletedEvent, CancellationToken ct = default);
        Task<List<Event>> GetEventsByPageAsync(int page = 1, int pageSize = 20, CancellationToken ct = default);
        Task<Event?> GetEventByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Event>> GetEventsByTitleAsync(string title, int page = 1, int pageSize = 20, CancellationToken ct = default);
        Task<Event?> GetEventByTitleAsync(string title, CancellationToken ct = default);
        Task<List<Event>> GetEventsByCriteriaAsync(DateTime? date, string? location, Guid? categoryId, string? search, int page = 1, int pageSize = 20, CancellationToken ct = default); 
        Task UpdateEventAsync(Event updatedEvent, CancellationToken ct = default);
    }
}
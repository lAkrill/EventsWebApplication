﻿using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Application.Interfaces
{
    public interface IEventRepository
    {
        Task AddEventAsync(Event newEvent, CancellationToken ct = default);
        Task DeleteEventAsync(Guid id, CancellationToken ct = default);
        Task<List<Event>> GetAllEventsAsync(CancellationToken ct = default);
        Task<Event?> GetEventByIdAsync(Guid id, CancellationToken ct = default);
        Task<Event?> GetEventByTitleAsync(string title, CancellationToken ct = default);
        Task<List<Event>> GetEventsByCriteriaAsync(DateTime? date, string? location, Guid? categoryId, CancellationToken ct = default);
        Task UpdateEventAsync(Event updatedEvent, CancellationToken ct = default);
    }
}
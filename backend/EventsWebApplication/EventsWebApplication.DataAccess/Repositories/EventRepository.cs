using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.DataAccess.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Event?> GetEventByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Events
                .AsNoTracking()
                .Include(e => e.Users)
                .FirstOrDefaultAsync(e => e.Id == id, ct);
        }

        public async Task<Event?> GetEventByTitleAsync(string title, CancellationToken ct = default)
        {
            return await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Title == title, ct);
        }

        public async Task<List<Event>> GetAllEventsAsync(CancellationToken ct = default)
        {
            return await _context.Events
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<List<Event>> GetEventsByCriteriaAsync(
            DateTime? date,
            string? location,
            Guid? categoryId,
            CancellationToken ct = default
        )
        {
            var query = _context.Events.AsQueryable();

            if (date.HasValue)
                query = query.Where(e => e.Date.Date == date.Value.Date);

            if (!string.IsNullOrEmpty(location))
                query = query.Where(e => e.Location.Contains(location));

            if (categoryId.HasValue)
                query = query.Where(e => e.CategoryId == categoryId.Value);

            return await query.ToListAsync(ct);
        }

        public async Task AddEventAsync(Event newEvent, CancellationToken ct = default)
        {
            await _context.Events.AddAsync(newEvent, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateEventAsync(Event updatedEvent, CancellationToken ct = default)
        {
            _context.Events.Update(updatedEvent);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteEventAsync(Guid id, CancellationToken ct = default)
        {
            var deletedEvent = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == id);
            if (deletedEvent != null)
            {
                _context.Events.Remove(deletedEvent);
                await _context.SaveChangesAsync(ct);
            }
        }
    }
}

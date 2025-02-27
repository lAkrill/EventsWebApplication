using EventsWebApplication.Core.Interfaces;
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

        public async Task<Event?> GetEventByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Events
                .Include(e => e.Participants)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<Event?> GetEventByTitleAsync(string title, CancellationToken cancellationToken = default)
        {
            return await _context.Events
                .FirstOrDefaultAsync(e => e.Title == title, cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Events
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetEventsByCriteriaAsync(
            DateTime? date,
            string? location,
            Guid? categoryId,
            CancellationToken cancellationToken = default
        )
        {
            var query = _context.Events.AsQueryable();

            if (date.HasValue)
                query = query.Where(e => e.Date.Date == date.Value.Date);

            if (!string.IsNullOrEmpty(location))
                query = query.Where(e => e.Location.Contains(location));

            if (categoryId.HasValue)
                query = query.Where(e => e.CategoryId == categoryId.Value);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task AddEventAsync(Event newEvent, CancellationToken cancellationToken = default)
        {
            await _context.Events.AddAsync(newEvent, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateEventAsync(Event updatedEvent, CancellationToken cancellationToken = default)
        {
            _context.Events.Update(updatedEvent);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteEventAsync(Event deletedEvent, CancellationToken cancellationToken = default)
        {
            _context.Events.Remove(deletedEvent);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

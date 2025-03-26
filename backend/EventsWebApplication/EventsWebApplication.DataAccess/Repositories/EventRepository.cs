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

        public async Task<Event?> GetEventByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Events
                .AsNoTracking()
                .Include(e => e.Category)
                .Include(e => e.Participants)
                    .ThenInclude(p=>p.User)
                .FirstOrDefaultAsync(e => e.Id == id, ct);
        }

        public async Task<Event?> GetEventByTitleAsync(string title, CancellationToken ct = default)
        {
            return await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Title == title);
        }

        public async Task<List<Event>> GetEventsByTitleAsync(string title, int page = 1, int pageSize = 20, CancellationToken ct = default)
        {
            return await _context.Events
                .AsNoTracking()
                .Include(e => e.Category)
                .Where(e => e.Title.Contains(title))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task<List<Event>> GetEventsByPageAsync(int page = 1, int pageSize = 20, CancellationToken ct = default)
        {
            return await _context.Events
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(e => e.Category)
                .ToListAsync(ct);
        }

        public async Task<List<Event>> GetEventsByCriteriaAsync(
            DateTime? date,
            string? location,
            Guid? categoryId,
            string? search,
            int page = 1, 
            int pageSize = 20,
            CancellationToken ct = default)
        {
            var query = _context.Events.AsQueryable();

            if (date.HasValue)
                query = query.Where(e => e.Date.Date == date.Value.Date);

            if (!string.IsNullOrEmpty(location))
                query = query.Where(e => e.Location.Contains(location));

            if (categoryId.HasValue)
                query = query.Where(e => e.CategoryId == categoryId.Value);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(e => e.Title.Contains(search));

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
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

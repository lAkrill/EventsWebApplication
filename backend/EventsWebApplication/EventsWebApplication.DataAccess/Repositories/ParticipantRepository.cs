using EventsWebApplication.Core.Interfaces;
using EventsWebApplication.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.DataAccess.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly AppDbContext _context;

        public ParticipantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Participant?> GetParticipantAsync(Guid userId, Guid eventId, CancellationToken ct = default)
        {
            return await _context.Participants
                .AsNoTracking()
                .Include(p => p.Event)
                .FirstOrDefaultAsync(p => p.EventId == eventId && p.UserId == userId, ct);
        }

        public async Task AddParticipantAsync(Participant participant, CancellationToken ct = default)
        {
            await _context.Participants.AddAsync(participant, ct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteParticipantAsync(Participant participant, CancellationToken ct = default)
        {
            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync(ct);
        }
    }
}

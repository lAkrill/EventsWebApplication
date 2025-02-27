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

        public async Task<Participant?> GetParticipantByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Participants
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Participant>> GetParticipantsByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
        {
            return await _context.Participants
                .Where(p => p.EventId == eventId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddParticipantAsync(Participant participant, CancellationToken cancellationToken = default)
        {
            await _context.Participants.AddAsync(participant, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveParticipantAsync(Participant participant, CancellationToken cancellationToken = default)
        {
            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

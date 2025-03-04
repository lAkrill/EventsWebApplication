using EventsWebApplication.Application.Interfaces;
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

        public async Task AddParticipantAsync(Guid userId, Guid eventId, CancellationToken ct = default)
        {
            var participant = new Participant()
            {
                EventId = eventId,
                UserId = userId,
                RegistrationDate = DateTime.Now
            };
            await _context.Participants.AddAsync(participant, ct);
        }

        public async Task DeleteParticipantAsync(Guid userId, Guid eventId, CancellationToken ct = default)
        {
            var participant = await _context.Participants
                .FirstOrDefaultAsync(p => p.EventId == eventId && p.UserId == userId, ct);
            if (participant != null)
            {
                _context.Participants.Remove(participant);
                await _context.SaveChangesAsync(ct);
            }
        }
    }
}

using AutoMapper;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Application.Services
{
    public class ParticipantService 
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;

        public ParticipantService(IParticipantRepository participantRepository, IUserRepository userRepository, IEventRepository eventRepository)
        {
            _participantRepository = participantRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
        }

        public async Task DeleteParticipantAsync(Guid userId, Guid eventId, CancellationToken ct)
        {
            var deletedParticipant = await _participantRepository.GetParticipantAsync(userId, eventId, ct);
            if (deletedParticipant == null)
            {
                throw new NotFoundException($"Participant not found with user id: {userId} and event id: {eventId}");
            }

            await _participantRepository.DeleteParticipantAsync(userId, eventId, ct);
        }

        public async Task AddParticipantAsync(Guid userId, Guid eventId, CancellationToken ct)
        {
            var existingParticipant = await _participantRepository.GetParticipantAsync(userId, eventId, ct);
            if (existingParticipant != null)
            {
                throw new NotUniqueException($"Participant already exists with user id: {userId} and event id: {eventId}");
            }

            var existingUser = await _userRepository.GetUserByIdAsync(userId, ct);
            if (existingUser == null)
            {
                throw new NotFoundException($"User not found with id {userId}");
            }

            var existingEvent = await _eventRepository.GetEventByIdAsync(eventId, ct);
            if (existingEvent == null)
            {
                throw new NotFoundException($"Event not found with id {eventId}");
            }

            if(existingEvent.MaxParticipants <= existingEvent.Participants.Count)
            {
                throw new InvalidPropertyAdditionException("Can't add more participants to this event");
            }

            await _participantRepository.AddParticipantAsync(userId, eventId, ct);
        }
    }
}

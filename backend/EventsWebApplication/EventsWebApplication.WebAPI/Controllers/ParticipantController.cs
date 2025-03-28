﻿namespace EventsWebApplication.WebAPI.Controllers
{
    [Route("api/participant")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly ParticipantService _participantService;

        public ParticipantController(ParticipantService participantService)
        {
            _participantService = participantService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser(Guid userId, Guid eventId, CancellationToken ct = default)
        {
            await _participantService.AddParticipantAsync(userId, eventId, ct);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{userId}&{eventId}")]
        public async Task<IActionResult> DeleteUser(Guid userId, Guid eventId, CancellationToken ct = default)
        {
            await _participantService.DeleteParticipantAsync(userId, eventId, ct);
            return NoContent();
        }
    }
}

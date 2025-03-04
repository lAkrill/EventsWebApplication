using EventsWebApplication.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApplication.WebAPI.Controllers
{
    [Route("api/participant")]
    [ApiController]
    public class ParticipantController : Controller
    {
        private readonly ParticipantService _participantService;

        public ParticipantController(ParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(Guid userId, Guid eventId, CancellationToken ct = default)
        {
            await _participantService.AddParticipantAsync(userId, eventId, ct);
            return Ok();
        }

        [HttpDelete("{userId}&{eventId}")]
        public async Task<IActionResult> DeleteUser(Guid userId, Guid eventId, CancellationToken ct = default)
        {
            await _participantService.DeleteParticipantAsync(userId, eventId, ct);
            return Ok();
        }
    }
}

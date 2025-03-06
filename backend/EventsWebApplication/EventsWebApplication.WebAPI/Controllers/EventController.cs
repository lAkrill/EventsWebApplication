namespace EventsWebApplication.WebAPI.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [Authorize]
        [HttpGet("{pageNumber}&{pageSize}")]
        public async Task<IActionResult> GetAllUsers(int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var events = await _eventService.GetAllEventsAsync(pageNumber, pageSize, ct);
            return Ok(events);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(Guid id, CancellationToken ct = default)
        {
            var @event = await _eventService.GetEventByIdAsync(id, ct);
            return Ok(@event);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}/participants")]
        public async Task<IActionResult> GetParticipantsByEventId(Guid id, CancellationToken ct = default)
        {
            var @event = await _eventService.GetEventParticipantsByIdAsync(id, ct);
            return Ok(@event);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetEventsByfilter([FromQuery] EventFilterDto eventFilterDto, CancellationToken ct = default)
        {
            var events = await _eventService.GetEventsByCriteriaAsync(eventFilterDto, ct);
            return Ok(events);
        }

        [Authorize]
        [HttpGet("title/{title}&{pageNumber}&{pageSize}")]
        public async Task<IActionResult> GetEventsByTitle(string title,int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var events = await _eventService.GetEventsByTitleAsync(title, pageNumber, pageSize, ct);
            return Ok(events);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventCreateDto eventCreateDto, CancellationToken ct = default)
        {
            var @event = await _eventService.CreateEventAsync(eventCreateDto, ct);
            return Ok(@event);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        public async Task<IActionResult> UpdateEvent(EventUpdateDto eventUpdateDto, CancellationToken ct = default)
        {
            var @event = await _eventService.UpdateEventAsync(eventUpdateDto, ct);
            return Ok(@event);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id, CancellationToken ct = default)
        {
            await _eventService.DeleteEventByIdAsync(id, ct);
            return Ok();
        }
    }
}

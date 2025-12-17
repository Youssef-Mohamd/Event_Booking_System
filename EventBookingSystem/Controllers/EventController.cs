using EventBooking.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET /api/events
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        // GET /api/events/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var e = await _eventService.GetEventByIdAsync(id);
            if (e == null) return NotFound();
            return Ok(e);
        }

        // GET /api/events/{id}/tickets
        [HttpGet("{id}/tickets")]
        public async Task<IActionResult> GetEventTickets(int id)
        {
            var tickets = await _eventService.GetEventTicketsAsync(id);
            if (tickets == null) return NotFound();
            return Ok(tickets);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBooking.Application.DTOs.Responses;
using EventBooking.Application.Interfaces;
using EventBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Application.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventResponseDTO>> GetAllEventsAsync()
        {
            return await _context.Events
                .Select(e => new EventResponseDTO
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Price = e.Price,
                    Location = e.Location,
                    Status = e.Status,
                    Category = e.Category,
                    ImageUrl = e.ImageUrl
                }).ToListAsync();
        }

        public async Task<EventResponseDTO> GetEventByIdAsync(int eventId)
        {
            var e = await _context.Events.FindAsync(eventId);
            if (e == null) return null;

            return new EventResponseDTO
            {
                EventId = e.EventId,
                Title = e.Title,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Price = e.Price,
                Location = e.Location,
                Status = e.Status,
                Category = e.Category,
                ImageUrl = e.ImageUrl
            };
        }

        public async Task<IEnumerable<TicketResponseDTO>> GetEventTicketsAsync(int eventId)
        {
            var tickets = await _context.TicketTypes
                .Where(t => t.EventId == eventId)
                .Select(t => new TicketResponseDTO
                {
                    TicketTypeId = t.TicketTypeId,
                    Name = t.Name,
                    Price = t.Price,
                    QuantityAvailable = t.QuantityAvailable
                }).ToListAsync();

            return tickets;
        }
    }
}

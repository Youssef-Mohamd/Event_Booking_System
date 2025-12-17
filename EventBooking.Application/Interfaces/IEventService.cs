using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBooking.Application.DTOs.Responses;

namespace EventBooking.Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventResponseDTO>> GetAllEventsAsync();
        Task<EventResponseDTO> GetEventByIdAsync(int eventId);
        Task<IEnumerable<TicketResponseDTO>> GetEventTicketsAsync(int eventId);
    }
}

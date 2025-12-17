using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBooking.Domain.Enums;

namespace EventBooking.Application.DTOs.Responses
{
    public class EventResponseDTO
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public EventStatus Status { get; set; }
        public EventCategory Category { get; set; }
        public string? ImageUrl { get; set; }
    }
}

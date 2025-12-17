using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.DTOs.Responses
{
    public class BookingSummaryDto
    {
        public int BookingId { get; set; }
        public string EventTitle { get; set; }
        public string TicketType { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

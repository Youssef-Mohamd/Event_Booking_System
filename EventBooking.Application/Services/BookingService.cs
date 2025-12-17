using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBooking.Application.DTOs.Requests;
using EventBooking.Application.DTOs.Responses;
using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using EventBooking.Domain.Enums;
using EventBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Application.Services
{
    public class BookingService : IBookingService
    {

        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BookingSummaryDto> CreateBookingAsync(int userId, CreateBookingRequest request)
        {
            var ticketType = await _context.TicketTypes
              .Include(t => t.Event)
              .FirstOrDefaultAsync(t => t.TicketTypeId == request.TicketTypeId
                                        && t.EventId == request.EventId);

            if (ticketType == null)
                throw new Exception("Invalid ticket type");

            if (ticketType.QuantityAvailable < request.Quantity)
                throw new Exception("Not enough tickets available");

            var totalPrice = ticketType.Price * request.Quantity;

            var booking = new Booking
            {
                UserId = userId,
                EventId = request.EventId,
                TicketTypeId = request.TicketTypeId,
                Quantity = request.Quantity,
                TotalPrice = totalPrice,
                Status = BookingStatus.Confirmed
            };

            ticketType.QuantityAvailable -= request.Quantity;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return new BookingSummaryDto
            {
                BookingId = booking.BookingId,
                EventTitle = ticketType.Event.Title,
                TicketType = ticketType.Name,
                Quantity = booking.Quantity,
                TotalPrice = booking.TotalPrice,
                Status = booking.Status.ToString(),
                CreatedAt = booking.CreatedAt
            };
        }

        public async Task<List<BookingSummaryDto>> GetMyBookingsAsync(int userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Event)
                .Include(b => b.TicketType)
                .Select(b => new BookingSummaryDto 
                {
                    BookingId = b.BookingId,
                    EventTitle = b.Event.Title,
                    TicketType = b.TicketType.Name,
                    Quantity = b.Quantity,
                    TotalPrice = b.TotalPrice,
                    Status = b.Status.ToString(),
                    CreatedAt = b.CreatedAt
                })
                .ToListAsync();
        }
    }
}

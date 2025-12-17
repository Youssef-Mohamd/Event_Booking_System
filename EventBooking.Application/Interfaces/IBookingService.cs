using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBooking.Application.DTOs.Requests;
using EventBooking.Application.DTOs.Responses;

namespace EventBooking.Application.Interfaces
{
    public interface IBookingService
    {

        Task<BookingSummaryDto> CreateBookingAsync(int userId, CreateBookingRequest request);
        Task<List<BookingSummaryDto>> GetMyBookingsAsync(int userId);

    }
}

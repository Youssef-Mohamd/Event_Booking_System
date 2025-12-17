using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBooking.Application.DTOs.Requests;
using EventBooking.Application.DTOs.Responses;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Interfaces
{
    public interface IUserService
    {
        // Profile Management
        Task<UserProfileDto>GetCurrentUserAsync(int userId);
        Task<UserProfileDto> UpdateProfileAsync(int userId, UpdateUserProfileRequest request);

        // Wishlist Management
        Task<IEnumerable<WishlistDto>> GetWishlistAsync(int userId);
        Task AddToWishlistAsync(int userId, int eventId);
        Task RemoveFromWishlistAsync(int userId, int eventId);

        // Get Booking  

        Task<IEnumerable<BookingSummaryDto>> GetMyBookingsAsync(int userId);
    }
}

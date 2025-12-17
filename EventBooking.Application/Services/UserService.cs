using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBooking.Application.DTOs.Requests;
using EventBooking.Application.DTOs.Responses;
using EventBooking.Application.Interfaces;
using EventBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddToWishlistAsync(int userId, int eventId)
        {
            if (await _context.Wishlists.AnyAsync(w => w.UserId == userId && w.EventId == eventId))
                return; // Already in wishlist
            var wishlistItem = new Domain.Entities.Wishlist
            {
                UserId = userId,
                EventId = eventId
            };
            _context.Wishlists.Add(wishlistItem);
        }

        public async Task<UserProfileDto> GetCurrentUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId)
             ?? throw new Exception("User not found");
            return new UserProfileDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                 CreatedAt = user.CreatedAt
            };
        }

        public async Task<IEnumerable<BookingSummaryDto>> GetMyBookingsAsync(int userId)
        {
            return await _context.Bookings
                 .Where(b => b.UserId == userId)
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

        public async Task<IEnumerable<WishlistDto>> GetWishlistAsync(int userId)
        {
            return await _context.Wishlists
                .Where(w => w.UserId==userId)
                .Select(w => new WishlistDto
                {
                    EventId = w.EventId,
                    EventTitle = w.Event.Title,
                    EventDate = w.Event.StartDate
                })
                .ToListAsync();

        }

        public async Task RemoveFromWishlistAsync(int userId, int eventId)
        {
            var wishlist = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.EventId == eventId);

            if (wishlist == null) return;

            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task<UserProfileDto> UpdateProfileAsync(int userId, UpdateUserProfileRequest request)
        {
            var user = await _context.Users.FindAsync(userId)
                     ?? throw new Exception("User not found");

            user.FullName = request.FullName;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetCurrentUserAsync(userId);
        }
    }
}

using System.Security.Claims;
using EventBooking.Application.DTOs.Requests;
using EventBooking.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userservice)
        {
            _userService = userservice;
        }

        private int CurrentUserId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        [HttpGet("me")]
        public async Task<IActionResult> Me()
               => Ok(await _userService.GetCurrentUserAsync(CurrentUserId));


        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile(UpdateUserProfileRequest request)
            => Ok(await _userService.UpdateProfileAsync(CurrentUserId, request));

        [HttpGet("me/wishlist")]
        public async Task<IActionResult> Wishlist()
            => Ok(await _userService.GetWishlistAsync(CurrentUserId));

        [HttpPost("me/wishlist/{eventId}")]
        public async Task<IActionResult> AddToWishlist(int eventId)
        {
            await _userService.AddToWishlistAsync(CurrentUserId, eventId);
            return Ok();
        }

        [HttpDelete("me/wishlist/{eventId}")]
        public async Task<IActionResult> RemoveFromWishlist(int eventId)
        {
            await _userService.RemoveFromWishlistAsync(CurrentUserId, eventId);
            return NoContent();
        }

        [HttpGet("me/bookings")]
        public async Task<IActionResult> MyBookings()
            => Ok(await _userService.GetMyBookingsAsync(CurrentUserId));
    }
}

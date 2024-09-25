using Microsoft.AspNetCore.Mvc;
using VenueBookingAPI.Data;
using VenueBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BookingController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings()
    {
        var bookings = await _context.Bookings
            .Include(b => b.Venue)
            .Include(b => b.User)
            .ToListAsync();
        return Ok(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
    {
        var venue = await _context.Venues.FindAsync(booking.VenueId);
        if (venue == null)
        {
            return NotFound("Venue not found.");
        }

        // Check if the venue is available on the booking date
        bool isAvailable = !await _context.Bookings
            .AnyAsync(b => b.VenueId == booking.VenueId && b.BookingDate.Date == booking.BookingDate.Date);

        if (!isAvailable)
        {
            return BadRequest("Venue is not available on the selected date.");
        }

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        // Send booking confirmation notification
        await SendNotification(booking.UserId ?? 0, "Booking Confirmed", "Your booking has been confirmed."); // Fix for nullable int issue

        return Ok("Booking created successfully.");
    }

    private async Task SendNotification(int userId, string title, string message)
    {
        // Logic to send notification
        var notification = new Notification
        {
            UserId = userId,
            Message = message,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
    }
}

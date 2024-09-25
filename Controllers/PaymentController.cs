using Microsoft.AspNetCore.Mvc;
using VenueBookingAPI.Data;
using VenueBookingAPI.Models;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PaymentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest paymentRequest)
    {
        if (paymentRequest.Amount <= 0)
        {
            return BadRequest("Invalid payment amount.");
        }

        // Validate that the BookingId exists
        var booking = await _context.Bookings.FindAsync(paymentRequest.BookingId);
        if (booking == null)
        {
            return NotFound("Booking not found.");
        }

        // Create the Payment object from the PaymentRequest
        var payment = new Payment
        {
            BookingId = paymentRequest.BookingId,
            Amount = paymentRequest.Amount,
            PaymentMethod = paymentRequest.PaymentMethod
        };

        // Add the Payment object to the database
        _context.Payments.Add(paymentRequest);
        await _context.SaveChangesAsync();

        return Ok("Payment processed successfully.");
    }
}

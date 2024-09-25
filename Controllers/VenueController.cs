using Microsoft.AspNetCore.Mvc;
using VenueBookingAPI.Data;
using VenueBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class VenueController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VenueController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetVenues()
    {
        var venues = await _context.Venues
            .Include(v => v.Images)
            .Include(v => v.Ratings)
            .ToListAsync();
        return Ok(venues);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVenueDetail(int id)
    {
        var venue = await _context.Venues
            .Include(v => v.Images)
            .Include(v => v.Ratings)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (venue == null)
        {
            return NotFound("Venue not found.");
        }

        return Ok(venue);
    }

    [HttpPost]
    public async Task<IActionResult> AddVenue([FromBody] Venue venue)
    {
        _context.Venues.Add(venue);
        await _context.SaveChangesAsync();
        return Ok("Venue added successfully.");
    }

    [HttpPost("images")]
    public async Task<IActionResult> AddVenueImages([FromBody] List<VenueImage> images)
    {
        foreach (var image in images)
        {
            var venue = await _context.Venues.FindAsync(image.VenueId);
            if (venue == null)
            {
                return NotFound($"Venue with ID {image.VenueId} not found.");
            }

            _context.VenueImages.Add(image);
        }

        await _context.SaveChangesAsync();
        return Ok("Images added successfully.");
    }

    [HttpPost("ratings")]
    public async Task<IActionResult> AddVenueRating([FromBody] VenueRating rating)
    {
        var venue = await _context.Venues.FindAsync(rating.VenueId);
        if (venue == null)
        {
            return NotFound("Venue not found.");
        }

        _context.VenueRatings.Add(rating);
        await _context.SaveChangesAsync();

        venue.AverageRating = venue.Ratings.Average(r => r.RatingValue);
        _context.Venues.Update(venue);
        await _context.SaveChangesAsync();

        return Ok("Rating added successfully.");
    }
}

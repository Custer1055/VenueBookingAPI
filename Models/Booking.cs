namespace VenueBookingAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public Venue? Venue { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
    }
}

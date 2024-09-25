namespace VenueBookingAPI.Models
{
    public class VenueImage
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public string ImageUrl { get; set; } // URL to the image
        public string Description { get; set; }
    }
}

namespace VenueBookingAPI.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } // Price of the venue
        public double AverageRating { get; set; } // Average rating of the venue
        public List<VenueImage> Images { get; set; } // Images of the venue
        public List<VenueRating> Ratings { get; set; } // List of ratings
        public int OwnerId { get; set; }
        public User Owner { get; set; }
    }
}

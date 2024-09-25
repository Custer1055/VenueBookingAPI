namespace VenueBookingAPI.Models
{
    public class VenueRating
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public double RatingValue { get; set; } // Value of the rating (1-5)
        public string Comment { get; set; } // Optional comment from the user
        public DateTime RatingDate { get; set; } // Date of the rating
    }
}

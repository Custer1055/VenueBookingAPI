namespace VenueBookingAPI.Models
{
    public class PaymentRequest
    {
        public int Id { get; set; } // Define this as the primary key
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal
    }

}

using Microsoft.EntityFrameworkCore;
using VenueBookingAPI.Models;

namespace VenueBookingAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<PaymentRequest> Payments { get; set; }
        public DbSet<VenueImage> VenueImages { get; set; }
        public DbSet<VenueRating> VenueRatings { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaymentRequest>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Venue>()
                .Property(v => v.Price)
                .HasColumnType("decimal(18, 2)");
        }
    }
}

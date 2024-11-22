using Hotels.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Data
{
    public class HotelsContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // add classes derived from abstract room
            builder.Entity<SingleRoom>();
            builder.Entity<DoubleRoom>();
            builder.Entity<DeluxeRoom>();

            base.OnModelCreating(builder);
        }
    }
}

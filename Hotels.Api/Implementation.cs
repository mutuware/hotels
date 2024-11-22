using Hotels.Data.Entities;
using Hotels.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Api
{
    public static class Implementation
    {
        public static void AddApplicationApi(this WebApplication app)
        {
            var api = app.MapGroup("/api");

            api.MapGet("/hotels", GetHotels);
            api.MapPost("/db/seed", Seed);
            api.MapPost("/db/reset", Reset);
            api.MapGet("/bookings/{reference}", (HotelsContext context, string reference) => GetBookingReference(context, reference));
        }

        private static async Task<Booking?> GetBookingReference(HotelsContext context, string reference)
        {
            return await context.Bookings.Include(x => x.Hotel).Include(x => x.Room).SingleOrDefaultAsync(x => x.Reference == reference);
        }

        private static async Task<string> Seed(HotelsContext context)
        {
            await Reset(context);

            var room1 = new DeluxeRoom();
            var hotel1 = new Hotel
            {
                Name = "Rafles",
                Rooms = [
                    room1, new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom()
                ]
            };

            await context.Hotels.AddRangeAsync(
                hotel1,
                new Hotel
                {
                    Name = "Pullman",
                    Rooms = [
                        new SingleRoom(), new DoubleRoom(), new DoubleRoom(), new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom()
                    ]
                },
                new Hotel
                {
                    Name = "Novotel",
                    Rooms = [
                    new SingleRoom(), new DoubleRoom(), new DoubleRoom(), new DoubleRoom(), new DoubleRoom(), new DoubleRoom()
                ]
                },
                new Hotel
                {
                    Name = "Ibis",
                    Rooms = [
                    new SingleRoom(), new SingleRoom(), new DoubleRoom(), new DoubleRoom(),new DoubleRoom(), new DoubleRoom()
                ]
                });


            await context.Bookings.AddRangeAsync(
                new Booking { Hotel = hotel1, Room = room1, StartDate = new DateTime(2024, 12, 1), EndDate = new DateTime(2024, 12, 7) });

            await context.SaveChangesAsync();

            return "Seeded";
        }

        private static async Task<string> Reset(HotelsContext context)
        {
            // must be ordered correctly to ensure referential integrity is respected
            await context.Bookings.ExecuteDeleteAsync();
            await context.Rooms.ExecuteDeleteAsync();
            await context.Hotels.ExecuteDeleteAsync();
            return "Reset";
        }

        private static async Task<List<Hotel>> GetHotels(HotelsContext context)
        {
            return await context.Hotels.Include(x => x.Rooms).ToListAsync();
        }
    }
}

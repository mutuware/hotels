using Hotels.Api.Models;
using Hotels.Data;
using Hotels.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Api
{
    public static class Implementation
    {
        public static void AddApplicationApi(this WebApplication app)
        {
            app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription(); // redirect root to API docs

            var api = app.MapGroup("/api");

            api.MapGet("/hotels", (HotelsContext context, string? name) => GetHotels(context, name));
            api.MapGet("/hotels/{hotelId}", (HotelsContext context, int hotelId) => GetHotel(context, hotelId));
            api.MapGet("/hotels/{hotelId}/rooms/{roomId}", (HotelsContext context, int hotelId, int roomId) => GetRoom(context, hotelId, roomId));
            api.MapGet("/bookings", (HotelsContext context, string? reference) => GetBookingReference(context, reference)).WithName("GetBookingByReference");
            api.MapPost("/bookings", (HotelsContext context, CreateBooking booking) => PostBookingReference(context, booking));
            api.MapGet("/availability", (HotelsContext context, DateTime from, DateTime to, int people) => GetAvailability(context, from, to, people));
            api.MapPost("/db/seed", Seed).WithTags("Hotels.Api");
            api.MapPost("/db/reset", Reset).WithTags("Hotels.Api");
        }

        private static async Task<Results<Ok<List<Hotel>>, NotFound>> GetHotels(HotelsContext context, string? name)
        {
            var query = context.Hotels.Include(x => x.Rooms).ThenInclude(x => x.Bookings).AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name == name);
            }

            var hotels = await query.ToListAsync();
            return TypedResults.Ok(hotels);
        }

        private static Results<Ok<Hotel>, NotFound> GetHotel(HotelsContext context, int hotelId)
        {
            var hotel = context.Hotels.Include(x => x.Rooms).Single(x => x.Id == hotelId);
            return hotel != null
                ? TypedResults.Ok(hotel)
                : TypedResults.NotFound();
        }

        private static Results<Ok<Room>, NotFound> GetRoom(HotelsContext context, int hotelId, int roomId)
        {
            var room = context.Hotels.Include(x => x.Rooms).ThenInclude(x => x.Bookings).Single(x => x.Id == hotelId)?.Rooms.Single(y => y.Id == roomId);
            return room != null
                ? TypedResults.Ok(room)
                : TypedResults.NotFound();
        }

        private static async Task<Results<CreatedAtRoute<Booking>, BadRequest, InternalServerError>> PostBookingReference(HotelsContext context, CreateBooking booking)
        {
            try
            {
                // validate that the incoming hotelId & roomId are valid
                var hotel = context.Find<Hotel>(booking.HotelId);
                if (hotel == null)
                {
                    return TypedResults.BadRequest();
                }

                var room = context.Rooms.SingleOrDefault(x => x.Id == booking.RoomId);
                if (room == null)
                {
                    return TypedResults.BadRequest();
                }

                var newBooking = new Booking(hotel, room, booking.StartDate, booking.EndDate);

                context.Bookings.Add(newBooking);
                await context.SaveChangesAsync();

                var newReference = newBooking.Reference;
                return TypedResults.CreatedAtRoute(newBooking, "GetBookingByReference", new { reference = newReference });
            }
            catch
            {
                return TypedResults.InternalServerError();
            }
        }

        private static async Task<Results<Ok<List<Booking>>, NotFound>> GetBookingReference(HotelsContext context, string? reference)
        {
            var query = context.Bookings.AsQueryable();

            if (!string.IsNullOrWhiteSpace(reference))
            {
                query = query.Where(x => x.Reference == reference);
            }

            var results = await query.ToListAsync();
            return results != null
                ? TypedResults.Ok(results)
                : TypedResults.NotFound();
        }

        private static Ok<List<Room>> GetAvailability(HotelsContext context, DateTime from, DateTime to, int people)
        {
            var allRooms = context.Rooms.Include(x => x.Bookings).AsEnumerable(); // seems like EF & SQLite doesn't handle date queries, so need to do in-memory :(

            // excluded rooms with any booking that overlap requested dates
            var freeRooms = allRooms.Where(x => !x.Bookings.Exists(y => from >= y.StartDate && from <= y.EndDate || to >= y.StartDate && to <= y.EndDate));
            var capacityRooms = freeRooms.Where(x => x.Capacity >= people).ToList();

            // returns empty list if no availablity found rather than 404
            return TypedResults.Ok(capacityRooms);
        }

        private static async Task<Ok<string>> Seed(HotelsContext context)
        {
            await Reset(context);

            await context.Hotels.AddRangeAsync(
                    new Hotel("Raffles",
                        [
                        new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom()
                    ]),
                    new Hotel("Pullman",
                        [
                            new SingleRoom(), new DoubleRoom(), new DoubleRoom(), new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom()
                    ]),
                    new Hotel("Novotel",
                        [
                        new SingleRoom(), new DoubleRoom(), new DoubleRoom(), new DoubleRoom(), new DoubleRoom(), new DoubleRoom()
                    ])
                    ,
                    new Hotel("Ibis",
                        [
                        new SingleRoom(), new SingleRoom(), new DoubleRoom(), new DoubleRoom(),new DoubleRoom(), new DoubleRoom()
                    ]));

            await context.SaveChangesAsync();

            var hotel = context.Hotels.Single(x => x.Name == "Raffles");
            var room = hotel.Rooms.First();
            var booking = new Booking(hotel, room, new DateTime(2024, 12, 1), new DateTime(2024, 12, 31));

            context.Bookings.Add(booking);

            await context.SaveChangesAsync();

            return TypedResults.Ok("Seeded");
        }

        private static async Task<Ok<string>> Reset(HotelsContext context)
        {
            // must be ordered correctly to ensure referential integrity is respected
            await context.Bookings.ExecuteDeleteAsync();
            await context.Rooms.ExecuteDeleteAsync();
            await context.Hotels.ExecuteDeleteAsync();
            return TypedResults.Ok("Reset");
        }
    }
}

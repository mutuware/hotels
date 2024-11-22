using Hotels.Data;
using Hotels.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<HotelsContext>(options =>
    options.UseSqlite("Data Source=hotels.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

var api = app.MapGroup("/api");
api.MapGet("/hotels", GetHotels).WithName("GetHotels");

api.MapPost("/db/seed", Seed);
api.MapPost("/db/reset", Reset);

app.Run();

async Task<string> Seed(HotelsContext context)
{
    await Reset(context);
    await context.Hotels.AddRangeAsync(
        new Hotel
        {
            Name = "Rafles",
            Rooms = [
            new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom(), new DeluxeRoom()
        ]
        },
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
        }
        );

    await context.SaveChangesAsync();

    return "Seeded";
}

async Task<string> Reset(HotelsContext context)
{
    // must be ordered correctly to ensure referential integrity is respected
    await context.Rooms.ExecuteDeleteAsync();
    await context.Hotels.ExecuteDeleteAsync();
    return "Reset";
}

async Task<List<Hotel>> GetHotels(HotelsContext context)
{
    return await context.Hotels.Include(x => x.Rooms).ToListAsync();
}


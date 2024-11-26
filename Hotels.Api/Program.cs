using Hotels.Api;
using Hotels.Data;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<HotelsContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("AppDatabase")));

// TODO: Return DTOs not Entities from API and this can be removed.
builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(x => x.AddServer(new ScalarServer(builder.Configuration["server"]!, "Hotel API")));
}

app.UseHttpsRedirection();

app.AddApplicationApi();

app.Run();

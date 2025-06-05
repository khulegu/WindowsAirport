using AirportServer.Data;
using AirportServer.Models;
using AirportServer.Services; // Custom Socket Server Service
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<CheckInService>();
builder.Services.AddScoped<FlightOperationsService>();
builder.Services.AddScoped<PassengerService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowSpecificOrigin",
        builder =>
            builder
                .WithOrigins(
                    "https://localhost:7025",
                    "http://localhost:50866",
                    "http://localhost:5188"
                ) // Add all origins where your Blazor client might run
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
    ); // Essential for SignalR
});

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" }
    );
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AirportForms API", Version = "v1" }
    );
});

var app = builder.Build();

// Database-ийг автоматаар үүсгэх/шинэчлэх болон суудал нэмэх
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        await dbContext.Database.MigrateAsync(); // Migration-уудыг хийнэ

        // Нислэгүүдэд суудал үүсгэх (хэрэв байхгүй бол)
        var flightsNeedingSeats = await dbContext
            .Flights.Include(f => f.Seats)
            .Where(f => !f.Seats.Any())
            .ToListAsync();

        foreach (var flight in flightsNeedingSeats)
        {
            if (flight.TotalSeats > 0)
            {
                logger.LogInformation(
                    "Generating seats for flight {FlightNumber} (ID: {FlightId})",
                    flight.FlightNumber,
                    flight.Id
                );
                for (
                    int i = 1;
                    i <= flight.TotalSeats / 6 + (flight.TotalSeats % 6 == 0 ? 0 : 1);
                    i++
                ) // Мөрийн тоо
                {
                    foreach (char col in new[] { 'A', 'B', 'C', 'D', 'E', 'F' })
                    {
                        if (flight.Seats.Count < flight.TotalSeats)
                        {
                            var seat = new Seat { FlightId = flight.Id, SeatNumber = $"{i}{col}" };
                            if (
                                !dbContext.Seats.Any(s =>
                                    s.FlightId == flight.Id && s.SeatNumber == seat.SeatNumber
                                )
                            )
                            {
                                dbContext.Seats.Add(seat);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (flight.Seats.Count >= flight.TotalSeats)
                        break;
                }
                await dbContext.SaveChangesAsync();
                logger.LogInformation(
                    "Generated {SeatCount} seats for flight {FlightNumber}",
                    flight.Seats.Count,
                    flight.FlightNumber
                );
            }
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred during database migration or seeding.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers(); // REST API Controller-уудыг холбох
app.MapHub<FlightHub>("/flighthub");
app.Run();

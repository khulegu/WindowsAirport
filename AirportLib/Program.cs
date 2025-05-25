using AirlineServer.Api.Services;
using AirportLib.Data;
using AirportLib.Hubs;
using AirportLib.Models;
using AirportLib.Services; // Custom Socket Server Service
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// 1. Service-үүдийг бүртгэх
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<CheckInService>();
builder.Services.AddScoped<FlightOperationsService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Airport API", Version = "v1" }
    );
});

builder.Services.AddSignalR();

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(
//         "AllowSpecificOrigins",
//         policy =>
//         {
//             policy
//                 .WithOrigins("http://localhost:xxxx", "https://localhost:yyyy") // WinForms, Blazor app-ийн хаяг
//                 .AllowAnyHeader()
//                 .AllowAnyMethod()
//                 .AllowCredentials(); // SignalR-д чухал
//         }
//     );
// });

// Custom Socket Server-ийг Background Service хэлбэрээр бүртгэх
builder.Services.AddHostedService<CustomSocketServer>();

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

// 2. HTTP request pipeline тохируулах
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins"); // CORS policy-г идэвхжүүлэх
app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // REST API Controller-уудыг холбох

// SignalR Hub-уудыг холбох
app.MapHub<FlightInfoHub>("/flightInfoHub");

// app.MapHub<CheckInHub>("/checkInHub"); // Суудал оноолтын мэдээллийг агент хооронд дамжуулах Hub (шаардлагатай бол)

app.Run();

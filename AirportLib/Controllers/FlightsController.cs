// AirlineServer.Api/Controllers/FlightsController.cs
using AirportLib.Data; // AppDbContext-ийг ашиглах
using AirportLib.Models;
using AirportLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AirportLib.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly FlightOperationsService _flightOpsService;
    private readonly CheckInService _checkInService; // Суудлын мэдээлэл авахад
    private readonly AppDbContext _context; // Шууд хандалт (хялбар байдлаар)

    public FlightsController(
        FlightOperationsService flightOpsService,
        CheckInService checkInService,
        AppDbContext context
    )
    {
        _flightOpsService = flightOpsService;
        _checkInService = checkInService;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFlights()
    {
        var flights = await _context
            .Flights.Select(f => new
            {
                f.Id,
                f.FlightNumber,
                f.DepartureCity,
                f.ArrivalCity,
                f.DepartureTime,
                f.ArrivalTime,
                f.Status,
                f.TotalSeats,
            })
            .ToListAsync();
        return Ok(flights);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFlightDetails(int id)
    {
        var flight = await _checkInService.GetFlightWithSeatsAsync(id); // Суудал, зорчигчийн мэдээллийг хамт авах
        if (flight == null)
            return NotFound();

        // DTO ашиглан зөвхөн шаардлагатай мэдээллийг буцаах нь зүйтэй
        var flightDetailsDto = new
        {
            flight.Id,
            flight.FlightNumber,
            flight.DepartureCity,
            flight.ArrivalCity,
            flight.DepartureTime,
            flight.ArrivalTime,
            flight.Status,
            flight.TotalSeats,
            Seats = flight
                .Seats.Select(s => new
                {
                    s.SeatNumber,
                    s.IsOccupied,
                    PassengerName = s.AssignedPassenger != null
                        ? $"{s.AssignedPassenger.FirstName} {s.AssignedPassenger.LastName}"
                        : null,
                    PassportNumber = s.AssignedPassenger?.PassportNumber,
                })
                .ToList(),
        };
        return Ok(flightDetailsDto);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateFlightStatus(
        int id,
        [FromBody] UpdateFlightStatusRequest request
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (success, message) = await _flightOpsService.UpdateFlightStatusAsync(
            id,
            request.NewStatus
        );
        if (!success)
            return BadRequest(new { Message = message });

        return Ok(new { Message = message });
    }
}

public class UpdateFlightStatusRequest
{
    public FlightStatus NewStatus { get; set; }
}

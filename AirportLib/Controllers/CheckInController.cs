using AirportLib.Data;
using AirportLib.Hubs;
using AirportLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AirportLib.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckInController(
    CheckInService checkInService,
    IHubContext<FlightInfoHub, IFlightInfoClient> flightInfoHubContext,
    AppDbContext dbContext
) : ControllerBase
{
    private readonly CheckInService _checkInService = checkInService;
    private readonly IHubContext<FlightInfoHub, IFlightInfoClient> _flightInfoHubContext =
        flightInfoHubContext;
    private readonly AppDbContext _dbContext = dbContext;

    /// <summary>
    /// Тийзний мэдээллийг авах
    /// </summary>
    /// <param name="passportNumber">Зорчигчийн паспортын дугаар</param>
    /// <returns>Тийзний мэдээлэл</returns>
    [HttpGet("booking")]
    public async Task<IActionResult> GetBooking([FromQuery] string passportNumber)
    {
        if (string.IsNullOrWhiteSpace(passportNumber))
            return BadRequest("Паспортын дугаарын оруулна уу.");

        var booking = await _checkInService.FindBookingByPassportAsync(passportNumber);
        if (booking == null)
            return NotFound("Захиалга олдсонгүй эсвэл зорчигч бүртгүүлсэн байна.");

        // Зорчигчийн нэмэлт мэдээллийг авах
        var passenger = await _dbContext.Passengers.FirstOrDefaultAsync(p =>
            p.PassportNumber == passportNumber
        );

        return Ok(
            new
            {
                booking.Id,
                booking.FlightId,
                booking.Flight?.FlightNumber,
                booking.PassportNumber,
                booking.PassengerName,
                PassengerDetails = passenger != null
                    ? new { passenger.FirstName, passenger.LastName }
                    : null,
                booking.IsCheckedIn,
                booking.AssignedSeatNumber,
            }
        );
    }

    [HttpGet("seats")]
    public async Task<IActionResult> GetSeatMap([FromQuery] int flightId)
    {
        var flight = await _checkInService.GetFlightWithSeatsAsync(flightId);
        if (flight == null || flight.Seats == null)
            return NotFound("Flight or seats not found.");

        var seatMap = flight.Seats.Select(s => new
        {
            s.SeatNumber,
            s.IsOccupied,
            PassengerName = s.AssignedPassenger != null
                ? $"{s.AssignedPassenger.FirstName} {s.AssignedPassenger.LastName}"
                : null,
        });

        return Ok(seatMap);
    }

    [HttpPost("assignseat")]
    public async Task<IActionResult> AssignSeat([FromBody] AssignSeatRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (success, message, boardingPass) = await _checkInService.AssignSeatAsync(
            request.FlightId,
            request.PassportNumber,
            request.SeatNumber
        );

        string? passengerName = null;
        if (success && boardingPass != null)
        {
            passengerName = boardingPass.PassengerName;
        }
        else if (!success)
        {
            var booking = await _checkInService.FindBookingByPassportAsync(request.PassportNumber);
            if (booking != null)
                passengerName = booking.PassengerName;
        }

        // SignalR-аар бусад клиентүүдэд мэдээлэх
        await _flightInfoHubContext
            .Clients.Group($"flight-agent-{request.FlightId}")
            .BroadcastSeatUpdate(
                request.FlightId,
                request.SeatNumber,
                request.PassportNumber,
                passengerName,
                success,
                message
            );

        // Мэдээллийн дэлгэцүүдэд суудлын газрын зургийг шинэчлэх (амжилттай бол)
        if (success)
        {
            var flight = await _checkInService.GetFlightWithSeatsAsync(request.FlightId);
            if (flight?.Seats != null)
            {
                var seatMapDto = flight.Seats.Select(s => new
                {
                    s.SeatNumber,
                    s.IsOccupied,
                    PassengerName = s.AssignedPassenger != null
                        ? $"{s.AssignedPassenger.FirstName} {s.AssignedPassenger.LastName}"
                        : null,
                });
                await _flightInfoHubContext
                    .Clients.Group($"flight-display-{request.FlightId}")
                    .ReceiveSeatMapUpdate(request.FlightId, seatMapDto);
            }
        }

        if (!success)
            return BadRequest(new { Message = message });
        return Ok(new { Message = message, BoardingPass = boardingPass });
    }
}

public class AssignSeatRequest
{
    public int FlightId { get; set; }
    public string PassportNumber { get; set; } = string.Empty;
    public string SeatNumber { get; set; } = string.Empty;
}

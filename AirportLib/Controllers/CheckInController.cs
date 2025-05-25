using AirportLib.Data;
using AirportLib.Hubs;
using AirportLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CheckInController : ControllerBase
{
    private readonly CheckInService _checkInService;
    private readonly IHubContext<FlightInfoHub, IFlightInfoClient> _flightInfoHubContext;
    private readonly AppDbContext _dbContext;

    public CheckInController(
        CheckInService checkInService,
        IHubContext<FlightInfoHub, IFlightInfoClient> flightInfoHubContext,
        AppDbContext dbContext
    )
    {
        _checkInService = checkInService;
        _flightInfoHubContext = flightInfoHubContext;
        _dbContext = dbContext;
    }

    [HttpGet("booking")]
    public async Task<IActionResult> GetBooking(
        [FromQuery] int flightId,
        [FromQuery] string passportNumber
    )
    {
        if (string.IsNullOrWhiteSpace(passportNumber))
            return BadRequest("Passport number is required.");

        var booking = await _checkInService.FindBookingByPassportAsync(flightId, passportNumber);
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
                booking.PassengerName, // Энэ нь захиалга дээрх нэр
                PassengerDetails = passenger != null
                    ? new { passenger.FirstName, passenger.LastName }
                    : null, // Паспорт дээрх нэр
                booking.IsCheckedIn,
                booking.AssignedSeatNumber,
            }
        );
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
            var booking = await _checkInService.FindBookingByPassportAsync(
                request.FlightId,
                request.PassportNumber
            );
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

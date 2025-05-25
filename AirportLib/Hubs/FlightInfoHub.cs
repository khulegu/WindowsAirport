using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AirportLib.Models;
using AirportLib.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace AirportLib.Hubs
{
    public class FlightInfoHub : Hub<IFlightInfoClient>
    {
        private readonly CheckInService _checkInService;
        private readonly ILogger<FlightInfoHub> _logger;

        public FlightInfoHub(CheckInService checkInService, ILogger<FlightInfoHub> logger)
        {
            _checkInService = checkInService;
            _logger = logger;
        }

        // Нислэгийн төлөв өөрчлөгдөхөд дуудагдана (FlightOperationsService-ээс)
        public async Task BroadcastFlightStatusUpdate(int flightId, FlightStatus status)
        {
            _logger.LogInformation(
                "Broadcasting status update for flight {FlightId}: {Status}",
                flightId,
                status
            );
            await Clients.Group($"flight-{flightId}").FlightStatusChanged(flightId, status);
            await Clients.Group("all-flight-displays").FlightStatusChanged(flightId, status);
        }

        // Суудал оноогдсон/амжилтгүй болсон үед дуудна (CheckInService эсвэл Controller-оос)
        public async Task BroadcastSeatUpdate(
            int flightId,
            string seatNumber,
            string passportNumber,
            string? passengerName,
            bool success,
            string message
        )
        {
            _logger.LogInformation(
                "Broadcasting seat update for flight {FlightId}, Seat {SeatNumber}, Success: {Success}",
                flightId,
                seatNumber,
                success
            );
            if (success)
            {
                await Clients
                    .Group($"flight-agent-{flightId}")
                    .SeatAssigned(flightId, seatNumber, passportNumber, passengerName);
                // Мэдээллийн дэлгэцүүдэд суудлын газрын зургийг шинэчлэх
                var flight = await _checkInService.GetFlightWithSeatsAsync(flightId);
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
                    await Clients
                        .Group($"flight-display-{flightId}")
                        .ReceiveSeatMapUpdate(flightId, seatMapDto);
                }
            }
            else
            {
                // Зөвхөн тухайн үйлдлийг хийсэн агентын групп эсвэл бүх агентуудад
                await Clients
                    .Group($"flight-agent-{flightId}")
                    .SeatAssignmentFailed(flightId, seatNumber, message);
            }
        }

        public async Task JoinFlightGroup(string flightIdString, bool isDisplay = false)
        {
            if (int.TryParse(flightIdString, out int flightId))
            {
                string groupName = isDisplay
                    ? $"flight-display-{flightId}"
                    : $"flight-agent-{flightId}";
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                _logger.LogInformation(
                    "Client {ConnectionId} joined group {GroupName}",
                    Context.ConnectionId,
                    groupName
                );

                // Хэрэв дэлгэц холбогдож байвал анхны суудлын мэдээллийг илгээнэ
                if (isDisplay)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "all-flight-displays"); // For global status changes

                    var flight = await _checkInService.GetFlightWithSeatsAsync(flightId);
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
                        await Clients
                            .Client(Context.ConnectionId)
                            .InitialSeatMap(flightId, seatMapDto);
                    }
                }
                else
                {
                    // Агент холбогдвол анхны суудлын мэдээлэл + нислэгийн төлөв
                    var flight = await _checkInService.GetFlightWithSeatsAsync(flightId);
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
                        await Clients
                            .Client(Context.ConnectionId)
                            .InitialSeatMap(flightId, seatMapDto);
                        await Clients
                            .Client(Context.ConnectionId)
                            .FlightStatusChanged(flightId, flight.Status);
                    }
                }
            }
            else
            {
                _logger.LogWarning(
                    "Failed to parse flightId: {FlightIdString} for JoinFlightGroup",
                    flightIdString
                );
            }
        }

        public async Task LeaveFlightGroup(string flightIdString, bool isDisplay = false)
        {
            if (int.TryParse(flightIdString, out int flightId))
            {
                string groupName = isDisplay
                    ? $"flight-display-{flightId}"
                    : $"flight-agent-{flightId}";
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
                _logger.LogInformation(
                    "Client {ConnectionId} left group {GroupName}",
                    Context.ConnectionId,
                    groupName
                );
                if (isDisplay)
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "all-flight-displays");
            }
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
            // Хэрэв бүх нислэгийн мэдээллийг харуулдаг дэлгэц байвал түүнийг тусгай группт нэмж болно.
            // await Groups.AddToGroupAsync(Context.ConnectionId, "flight-information-displays");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
            // Группээс автоматаар хасагдах тул энд нэмэлт үйлдэл хийх шаардлагагүй байж болно.
            await base.OnDisconnectedAsync(exception);
        }
    }
}

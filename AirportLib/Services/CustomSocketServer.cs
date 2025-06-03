// AirlineServer.Api/Services/CustomSocketServer.cs
using System.Net;
using System.Net.Sockets;
using System.Text;
using AirportLib.Data;
using AirportLib.Hubs;
using AirportLib.Models;
using AirportLib.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json; // JSON боловсруулахад

namespace AirlineServer.Api.Services;

public class CustomSocketServer : BackgroundService
{
    private readonly ILogger<CustomSocketServer> _logger;
    private readonly IServiceScopeFactory _scopeFactory; // DI scope үүсгэхэд
    private TcpListener? _tcpListener;
    private const int Port = 11001; // Socket серверийн порт

    public CustomSocketServer(ILogger<CustomSocketServer> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _tcpListener = new TcpListener(IPAddress.Any, Port);
            _tcpListener.Start();
            _logger.LogInformation("Custom Socket Server starting on port {Port}...", Port);

            stoppingToken.Register(() =>
            {
                _logger.LogInformation("Custom Socket Server is stopping via cancellation token.");
                _tcpListener?.Stop();
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Waiting for a client connection on socket server...");
                TcpClient client = await _tcpListener.AcceptTcpClientAsync(stoppingToken);
                _logger.LogInformation(
                    "Socket client connected: {RemoteEndPoint}",
                    client.Client.RemoteEndPoint
                );

                // Клиент бүрийг тусдаа Task-д боловсруулах
                _ = HandleClientAsync(client, stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Custom Socket Server execution was canceled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in custom socket server listener loop.");
        }
        finally
        {
            _tcpListener?.Stop();
            _logger.LogInformation("Custom Socket Server stopped listening.");
        }
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken stoppingToken)
    {
        var remoteEndPoint = client.Client.RemoteEndPoint?.ToString() ?? "Unknown client";
        _logger.LogInformation("Handling socket client {ClientEndpoint}", remoteEndPoint);

        using var stream = client.GetStream();
        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true); // StreamReader ашиглавал мессеж хиллэхэд хялбар
        using var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true)
        {
            AutoFlush = true,
        };

        try
        {
            // Service-үүдийг авахын тулд scope үүсгэх
            using (var scope = _scopeFactory.CreateScope())
            {
                var checkInService = scope.ServiceProvider.GetRequiredService<CheckInService>();
                var flightOpsService =
                    scope.ServiceProvider.GetRequiredService<FlightOperationsService>();
                var flightInfoHubContext = scope.ServiceProvider.GetRequiredService<
                    IHubContext<FlightInfoHub, IFlightInfoClient>
                >();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // Шууд хандалт (шаардлагатай бол)

                string? line;
                while ((line = await reader.ReadLineAsync(stoppingToken)) != null) // CancellationToken-г дэмждэг хувилбар
                {
                    _logger.LogInformation(
                        "Received from socket {ClientEndpoint}: {Request}",
                        remoteEndPoint,
                        line
                    );
                    SocketMessage? request = null;
                    try
                    {
                        request = JsonConvert.DeserializeObject<SocketMessage>(line);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError(
                            jsonEx,
                            "Failed to deserialize request from socket client {ClientEndpoint}",
                            remoteEndPoint
                        );
                        var errorResponse = new SocketResponse
                        {
                            Success = false,
                            Message = "Invalid request format.",
                        };
                        await writer.WriteLineAsync(
                            JsonConvert.SerializeObject(errorResponse).AsMemory(),
                            stoppingToken
                        );
                        continue;
                    }

                    if (request == null)
                        continue;

                    SocketResponse response = new SocketResponse
                    {
                        Success = false,
                        Message = "Unknown action.",
                    };

                    switch (request.Action?.ToUpper())
                    {
                        case "ASSIGN_SEAT":
                            AssignSeatRequest? assignPayload =
                                request.Payload?.ToObject<AssignSeatRequest>();
                            if (assignPayload != null)
                            {
                                var (success, message, boardingPass) =
                                    await checkInService.AssignSeatAsync(
                                        assignPayload.FlightId,
                                        assignPayload.PassportNumber,
                                        assignPayload.SeatNumber
                                    );

                                response.Success = success;
                                response.Message = message;
                                response.Data = boardingPass;

                                string? passengerName = boardingPass?.PassengerName;
                                if (!success && string.IsNullOrEmpty(passengerName))
                                {
                                    var booking = await dbContext.Bookings.FirstOrDefaultAsync(
                                        b =>
                                            b.FlightId == assignPayload.FlightId
                                            && b.PassportNumber == assignPayload.PassportNumber,
                                        stoppingToken
                                    );
                                    passengerName = booking?.PassengerName;
                                }

                                // SignalR-аар бусад клиентүүдэд мэдээлэх
                                await flightInfoHubContext
                                    .Clients.Group($"flight-agent-{assignPayload.FlightId}")
                                    .BroadcastSeatUpdate(
                                        assignPayload.FlightId,
                                        assignPayload.SeatNumber,
                                        assignPayload.PassportNumber,
                                        passengerName,
                                        success,
                                        message
                                    );

                                if (success)
                                {
                                    var flight = await checkInService.GetFlightWithSeatsAsync(
                                        assignPayload.FlightId
                                    );
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
                                        await flightInfoHubContext
                                            .Clients.Group(
                                                $"flight-display-{assignPayload.FlightId}"
                                            )
                                            .ReceiveSeatMapUpdate(
                                                assignPayload.FlightId,
                                                seatMapDto
                                            );
                                    }
                                }
                            }
                            else
                            {
                                response.Message = "Invalid payload for ASSIGN_SEAT.";
                            }
                            break;

                        case "GET_BOOKING":
                            GetBookingRequest? bookingPayload =
                                request.Payload?.ToObject<GetBookingRequest>();
                            if (bookingPayload != null)
                            {
                                var booking = await checkInService.FindBookingByPassportAsync(
                                    bookingPayload.PassportNumber
                                );
                                if (booking != null)
                                {
                                    var passenger = await dbContext.Passengers.FirstOrDefaultAsync(
                                        p => p.PassportNumber == bookingPayload.PassportNumber,
                                        stoppingToken
                                    );
                                    response.Success = true;
                                    response.Message = "Booking found.";
                                    response.Data = new
                                    {
                                        booking.Id,
                                        booking.FlightId,
                                        FlightNumber = booking.Flight?.FlightNumber,
                                        booking.PassportNumber,
                                        booking.PassengerName,
                                        PassengerDetails = passenger != null
                                            ? new { passenger.FirstName, passenger.LastName }
                                            : null,
                                        booking.IsCheckedIn,
                                        booking.AssignedSeatNumber,
                                    };
                                }
                                else
                                {
                                    response.Message = "Booking not found or already checked in.";
                                }
                            }
                            else
                            {
                                response.Message = "Invalid payload for GET_BOOKING.";
                            }
                            break;

                        case "GET_FLIGHT_DETAILS":
                            GetFlightDetailsRequest? flightDetailsPayload =
                                request.Payload?.ToObject<GetFlightDetailsRequest>();
                            if (flightDetailsPayload != null)
                            {
                                var flight = await checkInService.GetFlightWithSeatsAsync(
                                    flightDetailsPayload.FlightId
                                );
                                if (flight != null)
                                {
                                    response.Success = true;
                                    response.Message = "Flight details retrieved.";
                                    response.Data = new
                                    { // DTO
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
                                }
                                else
                                {
                                    response.Message = "Flight not found.";
                                }
                            }
                            else
                            {
                                response.Message = "Invalid payload for GET_FLIGHT_DETAILS.";
                            }
                            break;

                        case "UPDATE_FLIGHT_STATUS":
                            UpdateFlightStatusSocketRequest? statusPayload =
                                request.Payload?.ToObject<UpdateFlightStatusSocketRequest>();
                            if (statusPayload != null)
                            {
                                var (success, message) =
                                    await flightOpsService.UpdateFlightStatusAsync(
                                        statusPayload.FlightId,
                                        statusPayload.NewStatus
                                    );
                                response.Success = success;
                                response.Message = message;
                                // SignalR notification is handled by FlightOperationsService
                            }
                            else
                            {
                                response.Message = "Invalid payload for UPDATE_FLIGHT_STATUS.";
                            }
                            break;
                        // Add more actions as needed
                    }
                    await writer.WriteLineAsync(
                        JsonConvert.SerializeObject(response).AsMemory(),
                        stoppingToken
                    );
                }
            } // DI scope энд дуусна
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation(
                "Socket client handler for {ClientEndpoint} was canceled.",
                remoteEndPoint
            );
        }
        catch (IOException ioEx) when (ioEx.InnerException is SocketException se)
        {
            _logger.LogWarning(
                ioEx,
                "Socket client {ClientEndpoint} disconnected (SocketException: {SocketErrorCode}).",
                remoteEndPoint,
                se.SocketErrorCode
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling socket client {ClientEndpoint}", remoteEndPoint);
            if (client.Connected && stream.CanWrite)
            { // Хэрэв холболт идэвхтэй байвал алдааны мэдээлэл илгээх оролдлого хийх
                try
                {
                    var errorResponse = new SocketResponse
                    {
                        Success = false,
                        Message = "An unexpected server error occurred.",
                    };
                    await writer.WriteLineAsync(
                        JsonConvert.SerializeObject(errorResponse).AsMemory(),
                        CancellationToken.None
                    ); // stoppingToken энд хүчингүй байж магадгүй
                }
                catch (Exception writeEx)
                {
                    _logger.LogError(
                        writeEx,
                        "Failed to send error response to socket client {ClientEndpoint}",
                        remoteEndPoint
                    );
                }
            }
        }
        finally
        {
            if (client.Connected)
                client.Close();
            _logger.LogInformation(
                "Socket client {ClientEndpoint} disconnected and resources released.",
                remoteEndPoint
            );
        }
    }

    // Socket-оор дамжуулах мессежний загварууд
    public class SocketMessage
    {
        public string? Action { get; set; }
        public Newtonsoft.Json.Linq.JToken? Payload { get; set; } // Уян хатан payload
    }

    public class SocketResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }

    // Payload-д ашиглагдах загварууд (AssignSeatRequest-ийг давхар ашиглаж болно)
    public class GetBookingRequest
    {
        public int FlightId { get; set; }
        public string PassportNumber { get; set; } = string.Empty;
    }

    public class GetFlightDetailsRequest
    {
        public int FlightId { get; set; }
    }

    public class UpdateFlightStatusSocketRequest
    {
        public int FlightId { get; set; }
        public FlightStatus NewStatus { get; set; }
    }
}

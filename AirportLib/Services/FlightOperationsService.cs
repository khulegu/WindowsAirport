using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportLib.Data;
using AirportLib.Hubs;
using AirportLib.Models;
using Microsoft.AspNetCore.SignalR;

namespace AirportLib.Services
{
    public class FlightOperationsService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<FlightInfoHub, IFlightInfoClient> _flightInfoHubContext;

        public FlightOperationsService(
            AppDbContext context,
            IHubContext<FlightInfoHub, IFlightInfoClient> flightInfoHubContext
        )
        {
            _context = context;
            _flightInfoHubContext = flightInfoHubContext;
        }

        public async Task<(bool Success, string Message)> UpdateFlightStatusAsync(
            int flightId,
            FlightStatus newStatus
        )
        {
            var flight = await _context.Flights.FindAsync(flightId);
            if (flight == null)
            {
                return (false, "Нислэг олдсонгүй.");
            }

            flight.Status = newStatus;
            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();

            // Бүх холбогдсон клиентүүдэд мэдээлэх (SignalR)
            await _flightInfoHubContext
                .Clients.Group($"flight-{flightId}")
                .FlightStatusChanged(flightId, newStatus);
            await _flightInfoHubContext
                .Clients.Group("all-flight-displays")
                .FlightStatusChanged(flightId, newStatus);

            return (true, "Нислэгийн төлөв амжилттай шинэчлэгдлээ.");
        }
    }
}

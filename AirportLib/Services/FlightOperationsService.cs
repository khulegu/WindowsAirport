using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportLib.Data;
using AirportLib.Models;
using Microsoft.AspNetCore.SignalR;

namespace AirportLib.Services
{
    public class FlightOperationsService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

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
            return (true, "Нислэгийн төлөв амжилттай шинэчлэгдлээ.");
        }
    }
}

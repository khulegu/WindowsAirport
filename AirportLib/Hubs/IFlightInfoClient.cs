using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportLib.Models;

namespace AirportLib.Hubs
{
    public interface IFlightInfoClient
    {
        Task BroadcastSeatUpdate(
            int flightId,
            string seatNumber,
            string passportNumber,
            string? passengerName,
            bool success,
            string message
        );
        Task BroadcastFlightStatusUpdate(int flightId, FlightStatus newStatus);
        Task FlightStatusChanged(int flightId, FlightStatus newStatus);
        Task SeatAssigned(
            int flightId,
            string seatNumber,
            string passportNumber,
            string? passengerName
        );
        Task SeatAssignmentFailed(int flightId, string seatNumber, string message);
        Task ReceiveSeatMapUpdate(int flightId, IEnumerable<object> seatMap); // object-ийг SeatDto болгох
        Task InitialSeatMap(int flightId, IEnumerable<object> seatMap);
    }
}

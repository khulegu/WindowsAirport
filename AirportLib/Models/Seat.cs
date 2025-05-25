using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportLib.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; } = string.Empty; // "1A", "12F"
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }
        public int? PassengerId { get; set; } // Эзэнгүй суудал байж болно
        public Passenger? AssignedPassenger { get; set; }
        public bool IsOccupied => PassengerId.HasValue;
    }
}

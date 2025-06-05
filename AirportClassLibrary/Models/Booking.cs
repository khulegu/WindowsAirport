using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportServer.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string PassportNumber { get; set; } = string.Empty; // Зорчигчийг олох түлхүүр
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }
        public string PassengerName { get; set; } = string.Empty; // Шууд хайхад хялбар байх үүднээс
        public bool IsCheckedIn { get; set; } = false;
        public string? AssignedSeatNumber { get; set; }
    }
}

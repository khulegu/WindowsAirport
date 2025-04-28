namespace AirportLib
{
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public double Price { get; set; }
        public Flight(string flightNumber, string departureAirport, string arrivalAirport, DateTime departureTime, DateTime arrivalTime, double price)
        {
            FlightNumber = flightNumber;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Price = price;
        }
    }
}

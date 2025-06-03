namespace FlightStatusWeb.Components.Models
{
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string Destination { get; set; }
        public DateTime Time { get; set; }
        public string Gate { get; set; }
        public string Status { get; set; }
    }

}

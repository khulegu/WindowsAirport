using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AirportLib.Models;

public class CheckInApiClient
{
    private readonly HttpClient _httpClient;

    public CheckInApiClient(string baseAddress)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
    }

    public async Task<BookingResponse?> GetBookingAsync(string passportNumber)
    {
        var response = await _httpClient.GetAsync($"api/checkin/booking?passportNumber={passportNumber}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<BookingResponse>();
    }

    public async Task<(bool success, string message)> AssignSeatAsync(AssignSeatRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/checkin/assignseat", request);
        var body = await response.Content.ReadAsStringAsync();

        return (response.IsSuccessStatusCode, body);
    }
    public async Task<List<SeatInfo>?> GetSeatsAsync(int flightId)
    {
        var response = await _httpClient.GetAsync($"api/checkin/seats?flightId={flightId}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<List<SeatInfo>>();
    }

    public async Task<FlightDetails?> GetFlightDetailsAsync(int flightId)
    {
        var response = await _httpClient.GetAsync($"api/flights/{flightId}");
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        var root = JsonDocument.Parse(json).RootElement;

        return new FlightDetails
        {
            Id = root.GetProperty("id").GetInt32(),
            FlightNumber = root.GetProperty("flightNumber").GetString(),
            DepartureCity = root.GetProperty("departureCity").GetString(),
            ArrivalCity = root.GetProperty("arrivalCity").GetString(),
            DepartureTime = root.GetProperty("departureTime").GetDateTime(),
            ArrivalTime = root.GetProperty("arrivalTime").GetDateTime(),
        };
    }


}

// These classes should match the JSON structure
public class BookingResponse
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public string FlightNumber { get; set; } = "";
    public string PassportNumber { get; set; } = "";
    public string PassengerName { get; set; } = "";
    public bool IsCheckedIn { get; set; }
    public string AssignedSeatNumber { get; set; } = "";
    public PassengerDetails? PassengerDetails { get; set; }
}

public class PassengerDetails
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}

public class AssignSeatRequest
{
    public int FlightId { get; set; }
    public string PassportNumber { get; set; } = "";
    public string SeatNumber { get; set; } = "";
}
public class SeatInfo
{
    public string SeatNumber { get; set; } = "";
    public bool IsOccupied { get; set; }
}
public class FlightDetails
{
    public int Id { get; set; }
    public string FlightNumber { get; set; }
    public string DepartureCity { get; set; }
    public string ArrivalCity { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
}

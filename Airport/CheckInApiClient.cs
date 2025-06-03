using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
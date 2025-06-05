using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AirportServer.Models;


public class FlightApiClient
{
    private readonly HttpClient _http;

    public FlightApiClient(HttpClient httpClient)
    {
        _http = httpClient;
    }

    public async Task<List<Flight>> GetAllFlightsAsync()
    {
        return await _http.GetFromJsonAsync<List<Flight>>("api/flights");
    }

    public async Task<bool> UpdateFlightStatusAsync(int flightId, FlightStatus newStatus)
    {
        var request = new UpdateFlightStatusRequest { NewStatus = newStatus };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PutAsync($"api/flights/{flightId}/status", content);
        return response.IsSuccessStatusCode;
    }

    private class UpdateFlightStatusRequest
    {
        public FlightStatus NewStatus { get; set; }
    }
}


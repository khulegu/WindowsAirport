using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;

namespace AirportServer
{
    public class SeatHub:Hub
    {
        // Clients call this to start receiving updates for a specific flight
        public async Task JoinFlightGroup(string flightId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"flight-{flightId}");
        }

        // Clients call this to stop receiving updates for a specific flight
        public async Task LeaveFlightGroup(string flightId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"flight-{flightId}");
        }
    }
}

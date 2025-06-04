using AirportLib.Data;
using AirportLib.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class FlightHub : Hub
{
    private readonly AppDbContext _context;

    public FlightHub(AppDbContext context)
    {
        _context = context;
    }

    public override async Task OnConnectedAsync()
    {
        var flights = await _context.Flights.ToListAsync();
    }

    public async Task GetAllFlights()
    {
        var flights = await _context.Flights.ToListAsync();
        await Clients.Caller.SendAsync("ReceiveAllFlights", flights);
    }

    public async Task RegisterDisplay()
    {
        Console.WriteLine($"Registering display {Context.ConnectionId}");
        await Groups.AddToGroupAsync(Context.ConnectionId, "Display");
    }

    public async Task UnregisterDisplay()
    {
        Console.WriteLine($"Unregistering display {Context.ConnectionId}");
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Display");
    }
}

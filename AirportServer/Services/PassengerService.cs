using AirportServer.Data;
using AirportServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportServer.Services
{
    public class PassengerService
    {
        private readonly AppDbContext _context;

        public PassengerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Passenger>> GetAllAsync()
        {
            return await _context.Passengers.ToListAsync();
        }

        public async Task<Passenger?> GetByIdAsync(int id)
        {
            return await _context.Passengers.FindAsync(id);
        }

        public async Task<Passenger?> GetByPassportNumberAsync(string passportNumber)
        {
            return await _context.Passengers.FirstOrDefaultAsync(p =>
                p.PassportNumber == passportNumber
            );
        }

        public async Task<Passenger> CreateAsync(Passenger passenger)
        {
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();
            return passenger;
        }

        public async Task<bool> UpdateAsync(Passenger passenger)
        {
            var existing = await _context.Passengers.FindAsync(passenger.Id);
            if (existing == null)
                return false;
            existing.FirstName = passenger.FirstName;
            existing.LastName = passenger.LastName;
            existing.PassportNumber = passenger.PassportNumber;
            _context.Passengers.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger == null)
                return false;
            _context.Passengers.Remove(passenger);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

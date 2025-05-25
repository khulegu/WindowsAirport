// AirlineServer.Data/AppDbContext.cs
using System.Reflection.Emit;
using AirportLib.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportLib.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Flight> Flights => Set<Flight>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<Booking> Bookings => Set<Booking>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Flight>()
                .HasMany(f => f.Seats)
                .WithOne(s => s.Flight)
                .HasForeignKey(s => s.FlightId);

            modelBuilder
                .Entity<Seat>()
                .HasIndex(s => new { s.FlightId, s.SeatNumber }) // Нэг нислэгт суудал давхцахгүй
                .IsUnique();

            modelBuilder
                .Entity<Seat>()
                .HasOne(s => s.AssignedPassenger)
                .WithMany()
                .HasForeignKey(s => s.PassengerId)
                .IsRequired(false); // PassengerId нь заавал байх албагүй

            modelBuilder
                .Entity<Booking>()
                .HasIndex(b => new { b.FlightId, b.PassportNumber }) // Нэг зорчигч нэг нислэгт нэг л захиалгатай
                .IsUnique();

            // Анхдагч өгөгдөл (Seed data)
            modelBuilder
                .Entity<Passenger>()
                .HasData(
                    new Passenger
                    {
                        Id = 1,
                        PassportNumber = "E1234567",
                        FirstName = "Bold",
                        LastName = "Dorj",
                    },
                    new Passenger
                    {
                        Id = 2,
                        PassportNumber = "E7654321",
                        FirstName = "Saruul",
                        LastName = "Bat",
                    },
                    new Passenger
                    {
                        Id = 3,
                        PassportNumber = "P9876543",
                        FirstName = "Tuya",
                        LastName = "Chimed",
                    }
                );

            modelBuilder
                .Entity<Flight>()
                .HasData(
                    new Flight
                    {
                        Id = 1,
                        FlightNumber = "OM297",
                        DepartureCity = "ULN",
                        ArrivalCity = "ICN",
                        DepartureTime = new DateTime(2024, 5, 1, 10, 0, 0, DateTimeKind.Utc),
                        ArrivalTime = new DateTime(2024, 5, 1, 14, 0, 0, DateTimeKind.Utc),
                        Status = FlightStatus.CheckingIn,
                        TotalSeats = 20,
                    },
                    new Flight
                    {
                        Id = 2,
                        FlightNumber = "KE868",
                        DepartureCity = "ULN",
                        ArrivalCity = "ICN",
                        DepartureTime = new DateTime(2024, 5, 1, 12, 0, 0, DateTimeKind.Utc),
                        ArrivalTime = new DateTime(2024, 5, 1, 16, 0, 0, DateTimeKind.Utc),
                        Status = FlightStatus.CheckingIn,
                        TotalSeats = 30,
                    }
                );

            modelBuilder
                .Entity<Booking>()
                .HasData(
                    new Booking
                    {
                        Id = 1,
                        FlightId = 1,
                        PassportNumber = "E1234567",
                        PassengerName = "Bold Dorj",
                        IsCheckedIn = false,
                    },
                    new Booking
                    {
                        Id = 2,
                        FlightId = 1,
                        PassportNumber = "E7654321",
                        PassengerName = "Saruul Bat",
                        IsCheckedIn = false,
                    },
                    new Booking
                    {
                        Id = 3,
                        FlightId = 2,
                        PassportNumber = "P9876543",
                        PassengerName = "Tuya Chimed",
                        IsCheckedIn = false,
                    }
                );
        }
    }
}

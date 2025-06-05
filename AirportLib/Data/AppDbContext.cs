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
              modelBuilder.Entity<Passenger>().HasData(
                new Passenger { Id = 1, PassportNumber = "E1234567", FirstName = "Bold", LastName = "Dorj" },
                new Passenger { Id = 2, PassportNumber = "E7654321", FirstName = "Saruul", LastName = "Bat" },
                new Passenger { Id = 3, PassportNumber = "P9876543", FirstName = "Tuya", LastName = "Chimed" },
                new Passenger { Id = 4, PassportNumber = "E0000001", FirstName = "Bataa", LastName = "Tseren" },
                new Passenger { Id = 5, PassportNumber = "E0000002", FirstName = "Erdene", LastName = "Gerel" },
                new Passenger { Id = 6, PassportNumber = "E0000003", FirstName = "Munkh", LastName = "Erkhem" },
                new Passenger { Id = 7, PassportNumber = "E0000004", FirstName = "Solongo", LastName = "Naran" },
                new Passenger { Id = 8, PassportNumber = "E0000005", FirstName = "Enkh", LastName = "Batbold" },
                new Passenger { Id = 9, PassportNumber = "E0000006", FirstName = "Nomin", LastName = "Soyol" },
                new Passenger { Id = 10, PassportNumber = "E0000007", FirstName = "Temuulen", LastName = "Dash" }
                );
            modelBuilder.Entity<Flight>().HasData(
                    new Flight
                    {
                        Id = 1,
                        FlightNumber = "OM297",
                        DepartureCity = "ULN",
                        ArrivalCity = "ICN",
                        DepartureTime = new DateTime(2024, 5, 1, 10, 0, 0, DateTimeKind.Utc),
                        ArrivalTime = new DateTime(2024, 5, 1, 14, 0, 0, DateTimeKind.Utc),
                        Status = FlightStatus.CheckingIn,
                        TotalSeats = 20
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
                        TotalSeats = 30
                    },
                    new Flight
                    {
                        Id = 3,
                        FlightNumber = "SU123",
                        DepartureCity = "ULN",
                        ArrivalCity = "SVO",
                        DepartureTime = new DateTime(2024, 5, 2, 9, 30, 0, DateTimeKind.Utc),
                        ArrivalTime = new DateTime(2024, 5, 2, 13, 30, 0, DateTimeKind.Utc),
                        Status = FlightStatus.CheckingIn,
                        TotalSeats = 25
                    },
                    new Flight
                    {
                        Id = 4,
                        FlightNumber = "CA902",
                        DepartureCity = "ULN",
                        ArrivalCity = "PEK",
                        DepartureTime = new DateTime(2024, 5, 3, 7, 45, 0, DateTimeKind.Utc),
                        ArrivalTime = new DateTime(2024, 5, 3, 10, 45, 0, DateTimeKind.Utc),
                        Status = FlightStatus.CheckingIn,
                        TotalSeats = 35
                    },
                    new Flight
                    {
                        Id = 5,
                        FlightNumber = "JL508",
                        DepartureCity = "ULN",
                        ArrivalCity = "NRT",
                        DepartureTime = new DateTime(2024, 5, 4, 11, 15, 0, DateTimeKind.Utc),
                        ArrivalTime = new DateTime(2024, 5, 4, 15, 15, 0, DateTimeKind.Utc),
                        Status = FlightStatus.CheckingIn,
                        TotalSeats = 40
                    }
                );
            modelBuilder.Entity<Booking>().HasData(
                new Booking { Id = 1, FlightId = 1, PassportNumber = "E1234567", PassengerName = "Bold Dorj", IsCheckedIn = false },
                new Booking { Id = 2, FlightId = 1, PassportNumber = "E7654321", PassengerName = "Saruul Bat", IsCheckedIn = false },
                new Booking { Id = 3, FlightId = 2, PassportNumber = "P9876543", PassengerName = "Tuya Chimed", IsCheckedIn = false },
                new Booking { Id = 4, FlightId = 2, PassportNumber = "E0000001", PassengerName = "Bataa Tseren", IsCheckedIn = false },
                new Booking { Id = 5, FlightId = 2, PassportNumber = "E0000002", PassengerName = "Erdene Gerel", IsCheckedIn = false },
                new Booking { Id = 6, FlightId = 3, PassportNumber = "E0000003", PassengerName = "Munkh Erkhem", IsCheckedIn = false },
                new Booking { Id = 7, FlightId = 3, PassportNumber = "E0000004", PassengerName = "Solongo Naran", IsCheckedIn = false },
                new Booking { Id = 8, FlightId = 3, PassportNumber = "E0000005", PassengerName = "Enkh Batbold", IsCheckedIn = false },
                new Booking { Id = 9, FlightId = 4, PassportNumber = "E0000006", PassengerName = "Nomin Soyol", IsCheckedIn = false },
                new Booking { Id = 10, FlightId = 4, PassportNumber = "E0000007", PassengerName = "Temuulen Dash", IsCheckedIn = false },
                new Booking { Id = 11, FlightId = 4, PassportNumber = "E1234567", PassengerName = "Bold Dorj", IsCheckedIn = false },
                new Booking { Id = 12, FlightId = 5, PassportNumber = "E7654321", PassengerName = "Saruul Bat", IsCheckedIn = false },
                new Booking { Id = 13, FlightId = 5, PassportNumber = "P9876543", PassengerName = "Tuya Chimed", IsCheckedIn = false },
                new Booking { Id = 14, FlightId = 5, PassportNumber = "E0000001", PassengerName = "Bataa Tseren", IsCheckedIn = false },
                new Booking { Id = 15, FlightId = 5, PassportNumber = "E0000002", PassengerName = "Erdene Gerel", IsCheckedIn = false }
            );

        }
    }
}

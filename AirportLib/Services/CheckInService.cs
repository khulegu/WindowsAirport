using System.Collections.Concurrent; // Зэрэг ажиллагааг зохицуулах
using AirportLib.Data;
using AirportLib.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportLib.Services
{
    public class CheckInService(AppDbContext context)
    {
        // Нэг нислэг дотор суудал оноох үйлдлийг зэрэгцээ хандалтаас хамгаалах Semaphore
        private static readonly ConcurrentDictionary<int, SemaphoreSlim> _flightSeatLocks = new();

        public async Task<(
            bool Success,
            string Message,
            BoardingPassInfo? BoardingPass
        )> AssignSeatAsync(int flightId, string passportNumber, string seatNumber)
        {
            var booking = await context
                .Bookings.Include(b => b.Flight) // Нислэгийн мэдээллийг хамт авах
                .FirstOrDefaultAsync(b =>
                    b.FlightId == flightId && b.PassportNumber == passportNumber && !b.IsCheckedIn
                );

            if (booking == null || booking.Flight == null)
            {
                return (false, "Захиалга олдсонгүй эсвэл зорчигч бүртгүүлсэн байна.", null);
            }

            var passenger = await context.Passengers.FirstOrDefaultAsync(p =>
                p.PassportNumber == passportNumber
            );
            if (passenger == null)
            {
                return (false, "Зорчигчийн мэдээлэл олдсонгүй.", null);
            }

            // Нислэг бүрт тусдаа Semaphore үүсгэж ашиглана
            var flightLock = _flightSeatLocks.GetOrAdd(flightId, _ => new SemaphoreSlim(1, 1));
            await flightLock.WaitAsync(); // Lock авна
            try
            {
                var seat = await context.Seats.FirstOrDefaultAsync(s =>
                    s.FlightId == flightId && s.SeatNumber == seatNumber
                );

                if (seat == null)
                {
                    // Суудлуудыг урьдчилан үүсгэсэн байх ёстой.
                    return (false, "Ийм суудал олдсонгүй.", null);
                }

                if (seat.IsOccupied || seat.PassengerId.HasValue)
                {
                    return (false, $"'{seatNumber}' суудал аль хэдийн эзэнтэй байна.", null);
                }

                seat.PassengerId = passenger.Id;
                booking.IsCheckedIn = true;
                booking.AssignedSeatNumber = seatNumber;

                context.Seats.Update(seat);
                context.Bookings.Update(booking);

                await context.SaveChangesAsync(); // Өөрчлөлтийг хадгалах

                var boardingPass = new BoardingPassInfo
                {
                    PassengerName = $"{passenger.FirstName} {passenger.LastName}",
                    FlightNumber = booking.Flight.FlightNumber,
                    DepartureCity = booking.Flight.DepartureCity,
                    ArrivalCity = booking.Flight.ArrivalCity,
                    DepartureTime = booking.Flight.DepartureTime,
                    SeatNumber = seat.SeatNumber,
                    BoardingTime = booking.Flight.DepartureTime.AddMinutes(-45), // Жишээ
                    Gate = "A1", // Хаалганы мэдээллийг нислэгийн мэдээллээс авч болно
                };

                return (true, "Суудал амжилттай оноолоо.", boardingPass);
            }
            catch (DbUpdateConcurrencyException) // Зэрэг ажиллах үед өгөгдлийн сангийн түвшинд алдаа гарвал
            {
                // Энэ тохиолдолд хэрэглэгчид дахин оролдохыг сануулах
                return (false, "Суудал оноох явцад зөрчил үүслээ, дахин оролдоно уу.", null);
            }
            finally
            {
                flightLock.Release(); // Lock чөлөөлнө
            }
        }

        /// <summary>
        /// Паспортын дугаарын дагуу захиалгын мэдээллийг олох
        /// </summary>
        /// <param name="passportNumber">Зорчигчийн паспортын дугаар</param>
        /// <returns>Захиалгын мэдээлэл</returns>
        public async Task<Booking?> FindBookingByPassportAsync(string passportNumber)
        {
            return await context
                .Bookings.Include(b => b.Flight)
                .FirstOrDefaultAsync(b => b.PassportNumber == passportNumber && !b.IsCheckedIn);
        }

        /// <summary>
        /// Нислэгийн мэдээллийг олох
        /// </summary>
        /// <param name="flightId">Нислэгийн ID</param>
        /// <returns>Нислэгийн мэдээлэл</returns>
        public async Task<Flight?> GetFlightWithSeatsAsync(int flightId)
        {
            return await context
                .Flights.Include(f => f.Seats)
                .ThenInclude(s => s.AssignedPassenger) // Суудал эзэмшигчийн мэдээллийг хамт авах
                .FirstOrDefaultAsync(f => f.Id == flightId);
        }
    }
}

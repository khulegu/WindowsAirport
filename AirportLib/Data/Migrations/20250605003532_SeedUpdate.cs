using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirportLib.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "AssignedSeatNumber", "FlightId", "IsCheckedIn", "PassengerName", "PassportNumber" },
                values: new object[,]
                {
                    { 4, null, 2, false, "Bataa Tseren", "E0000001" },
                    { 5, null, 2, false, "Erdene Gerel", "E0000002" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "ArrivalCity", "ArrivalTime", "DepartureCity", "DepartureTime", "FlightNumber", "Status", "TotalSeats" },
                values: new object[,]
                {
                    { 3, "SVO", new DateTime(2024, 5, 2, 13, 30, 0, 0, DateTimeKind.Utc), "ULN", new DateTime(2024, 5, 2, 9, 30, 0, 0, DateTimeKind.Utc), "SU123", 0, 25 },
                    { 4, "PEK", new DateTime(2024, 5, 3, 10, 45, 0, 0, DateTimeKind.Utc), "ULN", new DateTime(2024, 5, 3, 7, 45, 0, 0, DateTimeKind.Utc), "CA902", 0, 35 },
                    { 5, "NRT", new DateTime(2024, 5, 4, 15, 15, 0, 0, DateTimeKind.Utc), "ULN", new DateTime(2024, 5, 4, 11, 15, 0, 0, DateTimeKind.Utc), "JL508", 0, 40 }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "FirstName", "LastName", "PassportNumber" },
                values: new object[,]
                {
                    { 4, "Bataa", "Tseren", "E0000001" },
                    { 5, "Erdene", "Gerel", "E0000002" },
                    { 6, "Munkh", "Erkhem", "E0000003" },
                    { 7, "Solongo", "Naran", "E0000004" },
                    { 8, "Enkh", "Batbold", "E0000005" },
                    { 9, "Nomin", "Soyol", "E0000006" },
                    { 10, "Temuulen", "Dash", "E0000007" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "AssignedSeatNumber", "FlightId", "IsCheckedIn", "PassengerName", "PassportNumber" },
                values: new object[,]
                {
                    { 6, null, 3, false, "Munkh Erkhem", "E0000003" },
                    { 7, null, 3, false, "Solongo Naran", "E0000004" },
                    { 8, null, 3, false, "Enkh Batbold", "E0000005" },
                    { 9, null, 4, false, "Nomin Soyol", "E0000006" },
                    { 10, null, 4, false, "Temuulen Dash", "E0000007" },
                    { 11, null, 4, false, "Bold Dorj", "E1234567" },
                    { 12, null, 5, false, "Saruul Bat", "E7654321" },
                    { 13, null, 5, false, "Tuya Chimed", "P9876543" },
                    { 14, null, 5, false, "Bataa Tseren", "E0000001" },
                    { 15, null, 5, false, "Erdene Gerel", "E0000002" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

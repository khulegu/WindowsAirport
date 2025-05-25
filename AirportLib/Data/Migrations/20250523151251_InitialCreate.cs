using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirportLib.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureCity = table.Column<string>(type: "TEXT", nullable: false),
                    ArrivalCity = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalSeats = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PassportNumber = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PassportNumber = table.Column<string>(type: "TEXT", nullable: false),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    PassengerName = table.Column<string>(type: "TEXT", nullable: false),
                    IsCheckedIn = table.Column<bool>(type: "INTEGER", nullable: false),
                    AssignedSeatNumber = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeatNumber = table.Column<string>(type: "TEXT", nullable: false),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    PassengerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seats_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "ArrivalCity", "ArrivalTime", "DepartureCity", "DepartureTime", "FlightNumber", "Status", "TotalSeats" },
                values: new object[,]
                {
                    { 1, "ICN", new DateTime(2024, 5, 1, 14, 0, 0, 0, DateTimeKind.Utc), "ULN", new DateTime(2024, 5, 1, 10, 0, 0, 0, DateTimeKind.Utc), "OM297", 0, 20 },
                    { 2, "ICN", new DateTime(2024, 5, 1, 16, 0, 0, 0, DateTimeKind.Utc), "ULN", new DateTime(2024, 5, 1, 12, 0, 0, 0, DateTimeKind.Utc), "KE868", 0, 30 }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "FirstName", "LastName", "PassportNumber" },
                values: new object[,]
                {
                    { 1, "Bold", "Dorj", "E1234567" },
                    { 2, "Saruul", "Bat", "E7654321" },
                    { 3, "Tuya", "Chimed", "P9876543" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "AssignedSeatNumber", "FlightId", "IsCheckedIn", "PassengerName", "PassportNumber" },
                values: new object[,]
                {
                    { 1, null, 1, false, "Bold Dorj", "E1234567" },
                    { 2, null, 1, false, "Saruul Bat", "E7654321" },
                    { 3, null, 2, false, "Tuya Chimed", "P9876543" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightId_PassportNumber",
                table: "Bookings",
                columns: new[] { "FlightId", "PassportNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_FlightId_SeatNumber",
                table: "Seats",
                columns: new[] { "FlightId", "SeatNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_PassengerId",
                table: "Seats",
                column: "PassengerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Passengers");
        }
    }
}

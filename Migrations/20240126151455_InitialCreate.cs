using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinalProject.Migrations
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
                    FID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumofSeat = table.Column<int>(type: "integer", nullable: false),
                    TicketCost = table.Column<decimal>(type: "numeric", nullable: false),
                    DeparDateandTimeOffset = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ArrDateandTimeOffset = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Destination = table.Column<string>(type: "text", nullable: false),
                    TakeOffLocation = table.Column<string>(type: "text", nullable: false),
                    FlightDuration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AirportLoc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    UID = table.Column<int>(type: "integer", nullable: false),
                    FID = table.Column<int>(type: "integer", nullable: false),
                    BookedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SeatNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => new { x.UID, x.FID });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    UserRole = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Booked = table.Column<bool>(type: "boolean", nullable: false),
                    FID = table.Column<int>(type: "integer", nullable: false),
                    FlightFID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Seats_Flights_FlightFID",
                        column: x => x.FlightFID,
                        principalTable: "Flights",
                        principalColumn: "FID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Queries",
                columns: table => new
                {
                    QID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Question = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    UID = table.Column<int>(type: "integer", nullable: false),
                    UserUID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queries", x => x.QID);
                    table.ForeignKey(
                        name: "FK_Queries_Users_UserUID",
                        column: x => x.UserUID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Queries_UserUID",
                table: "Queries",
                column: "UserUID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_FlightFID",
                table: "Seats",
                column: "FlightFID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queries");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}

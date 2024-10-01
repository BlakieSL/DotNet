using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace preparation_test_2.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoatStandards",
                columns: table => new
                {
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatStandards", x => x.IdBoatStandard);
                });

            migrationBuilder.CreateTable(
                name: "ClientCategories",
                columns: table => new
                {
                    IdClientCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscountPerc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCategories", x => x.IdClientCategory);
                });

            migrationBuilder.CreateTable(
                name: "Sailboats",
                columns: table => new
                {
                    IdSailboat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sailboats", x => x.IdSailboat);
                    table.ForeignKey(
                        name: "FK_Sailboats_BoatStandards_IdBoatStandard",
                        column: x => x.IdBoatStandard,
                        principalTable: "BoatStandards",
                        principalColumn: "IdBoatStandard",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdClientCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.IdClient);
                    table.ForeignKey(
                        name: "FK_Clients_ClientCategories_IdClientCategory",
                        column: x => x.IdClientCategory,
                        principalTable: "ClientCategories",
                        principalColumn: "IdClientCategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    NumOfBoats = table.Column<int>(type: "int", nullable: false),
                    Fulfilled = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: true),
                    CancelReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.IdReservation);
                    table.ForeignKey(
                        name: "FK_Reservations_BoatStandards_IdBoatStandard",
                        column: x => x.IdBoatStandard,
                        principalTable: "BoatStandards",
                        principalColumn: "IdBoatStandard",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SailboatReservations",
                columns: table => new
                {
                    IdSailboat = table.Column<int>(type: "int", nullable: false),
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SailboatReservations", x => new { x.IdReservation, x.IdSailboat });
                    table.ForeignKey(
                        name: "FK_SailboatReservations_Reservations_IdReservation",
                        column: x => x.IdReservation,
                        principalTable: "Reservations",
                        principalColumn: "IdReservation",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SailboatReservations_Sailboats_IdSailboat",
                        column: x => x.IdSailboat,
                        principalTable: "Sailboats",
                        principalColumn: "IdSailboat");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_IdClientCategory",
                table: "Clients",
                column: "IdClientCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdBoatStandard",
                table: "Reservations",
                column: "IdBoatStandard");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdClient",
                table: "Reservations",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_SailboatReservations_IdSailboat",
                table: "SailboatReservations",
                column: "IdSailboat");

            migrationBuilder.CreateIndex(
                name: "IX_Sailboats_IdBoatStandard",
                table: "Sailboats",
                column: "IdBoatStandard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SailboatReservations");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Sailboats");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "BoatStandards");

            migrationBuilder.DropTable(
                name: "ClientCategories");
        }
    }
}

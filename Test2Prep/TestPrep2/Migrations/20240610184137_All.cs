using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestPrep2.Migrations
{
    /// <inheritdoc />
    public partial class All : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Clients_IdClient",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_IdClient",
                table: "Reservations",
                newName: "IX_Reservations_IdClient");

            migrationBuilder.AddColumn<int>(
                name: "IdBoatStandard",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "IdReservation");

            migrationBuilder.CreateTable(
                name: "BoatStandarts",
                columns: table => new
                {
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatStandarts", x => x.IdBoatStandard);
                });

            migrationBuilder.CreateTable(
                name: "Sailboats",
                columns: table => new
                {
                    IsSailboat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    IdBoatStandartd = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sailboats", x => x.IsSailboat);
                    table.ForeignKey(
                        name: "FK_Sailboats_BoatStandarts_IdBoatStandartd",
                        column: x => x.IdBoatStandartd,
                        principalTable: "BoatStandarts",
                        principalColumn: "IdBoatStandard",
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SailboatReservations_Sailboats_IdSailboat",
                        column: x => x.IdSailboat,
                        principalTable: "Sailboats",
                        principalColumn: "IsSailboat",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdBoatStandard",
                table: "Reservations",
                column: "IdBoatStandard");

            migrationBuilder.CreateIndex(
                name: "IX_SailboatReservations_IdSailboat",
                table: "SailboatReservations",
                column: "IdSailboat");

            migrationBuilder.CreateIndex(
                name: "IX_Sailboats_IdBoatStandartd",
                table: "Sailboats",
                column: "IdBoatStandartd");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_BoatStandarts_IdBoatStandard",
                table: "Reservations",
                column: "IdBoatStandard",
                principalTable: "BoatStandarts",
                principalColumn: "IdBoatStandard",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Clients_IdClient",
                table: "Reservations",
                column: "IdClient",
                principalTable: "Clients",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_BoatStandarts_IdBoatStandard",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Clients_IdClient",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "SailboatReservations");

            migrationBuilder.DropTable(
                name: "Sailboats");

            migrationBuilder.DropTable(
                name: "BoatStandarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_IdBoatStandard",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IdBoatStandard",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_IdClient",
                table: "Reservation",
                newName: "IX_Reservation_IdClient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "IdReservation");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Clients_IdClient",
                table: "Reservation",
                column: "IdClient",
                principalTable: "Clients",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

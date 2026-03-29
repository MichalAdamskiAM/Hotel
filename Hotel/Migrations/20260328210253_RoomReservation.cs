using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Migrations
{
    /// <inheritdoc />
    public partial class RoomReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomNumber",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_RoomNumber",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Reservations");

            migrationBuilder.CreateTable(
                name: "RoomReservations",
                columns: table => new
                {
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomReservations", x => new { x.RoomNumber, x.ReservationId });
                    table.ForeignKey(
                        name: "FK_RoomReservations_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomReservations_Rooms_RoomNumber",
                        column: x => x.RoomNumber,
                        principalTable: "Rooms",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservations_ReservationId",
                table: "RoomReservations",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomReservations");

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomNumber",
                table: "Reservations",
                column: "RoomNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomNumber",
                table: "Reservations",
                column: "RoomNumber",
                principalTable: "Rooms",
                principalColumn: "Number",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

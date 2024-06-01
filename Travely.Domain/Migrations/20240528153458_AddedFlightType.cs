using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travely.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedFlightType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightType",
                table: "Flights",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightType",
                table: "Flights");
        }
    }
}

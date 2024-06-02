using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travely.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedCoordinatesSpotTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistanceFromCenter",
                table: "Spots");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Spots",
                newName: "Address");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Spots",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Spots",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Spots");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Spots");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Spots",
                newName: "Location");

            migrationBuilder.AddColumn<decimal>(
                name: "DistanceFromCenter",
                table: "Spots",
                type: "TEXT",
                nullable: true);
        }
    }
}

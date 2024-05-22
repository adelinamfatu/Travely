using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travely.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotesAndSpotForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Trips",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TripId",
                table: "Spots",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Spots_TripId",
                table: "Spots",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spots_Trips_TripId",
                table: "Spots",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spots_Trips_TripId",
                table: "Spots");

            migrationBuilder.DropIndex(
                name: "IX_Spots_TripId",
                table: "Spots");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Spots");
        }
    }
}

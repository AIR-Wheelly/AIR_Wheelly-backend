using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIR_Wheelly_DAL.Migrations
{
    /// <inheritdoc />
    public partial class locationdbupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "CarListings");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "CarListings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Adress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarListings_LocationId",
                table: "CarListings",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListings_Location_LocationId",
                table: "CarListings",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListings_Location_LocationId",
                table: "CarListings");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_CarListings_LocationId",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "CarListings");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "CarListings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

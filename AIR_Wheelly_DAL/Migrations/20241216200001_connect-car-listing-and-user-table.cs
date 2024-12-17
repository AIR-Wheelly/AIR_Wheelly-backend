using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIR_Wheelly_DAL.Migrations
{
    /// <inheritdoc />
    public partial class connectcarlistingandusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CarListings_UserId",
                table: "CarListings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListings_Users_UserId",
                table: "CarListings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListings_Users_UserId",
                table: "CarListings");

            migrationBuilder.DropIndex(
                name: "IX_CarListings_UserId",
                table: "CarListings");
        }
    }
}

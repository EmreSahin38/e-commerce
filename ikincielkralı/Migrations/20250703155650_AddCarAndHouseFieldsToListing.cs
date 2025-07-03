using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ikincielkralı.Migrations
{
    /// <inheritdoc />
    public partial class AddCarAndHouseFieldsToListing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarBrand",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarKm",
                table: "Listings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarModel",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarYear",
                table: "Listings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseCity",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseDistrict",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarBrand",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CarKm",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CarModel",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CarYear",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "HouseCity",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "HouseDistrict",
                table: "Listings");
        }
    }
}

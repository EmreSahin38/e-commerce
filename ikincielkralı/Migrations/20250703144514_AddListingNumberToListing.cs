using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ikincielkralı.Migrations
{
    /// <inheritdoc />
    public partial class AddListingNumberToListing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListingNumber",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListingNumber",
                table: "Listings");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ikincielkralı.Migrations
{
    /// <inheritdoc />
    public partial class PhotoPathsJsonSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPaths",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPathsJson",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPaths",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "PhotoPathsJson",
                table: "Listings");
        }
    }
}

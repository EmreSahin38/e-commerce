using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ikincielkralı.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtToListing2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Listings",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Listings");
        }
    }
}

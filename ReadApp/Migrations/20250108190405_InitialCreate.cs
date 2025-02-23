using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReadApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Pages = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Roman" },
                    { 2, "Hikaye" },
                    { 3, "Biyografi" },
                    { 4, "Söylev" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "IsActive", "Name", "Pages" },
                values: new object[,]
                {
                    { 1, 2, true, "Son Ayı", 250m },
                    { 2, 1, true, "Tarık Buğra'nın Roman Dünyası", 350m },
                    { 3, 1, true, "Cadı", 342m },
                    { 4, 1, true, "Ateşle Oynayan Kız", 350m },
                    { 5, 1, true, "Ejderha Dövmeli Kız", 380m },
                    { 6, 1, true, "Arı Kovanına Çomak Sokan Kız", 270m },
                    { 7, 1, true, "Kız Kulesindeki Kızılderili", 250m },
                    { 8, 1, true, "Şeker Portakalı", 269m },
                    { 9, 1, true, "Hayvan Çiftliği", 452m },
                    { 10, 1, true, "Beyaz Diş", 290m },
                    { 11, 2, true, "Küçük Prens", 370m },
                    { 12, 1, true, "Silmarillion", 700m },
                    { 13, 2, true, "La Fontaine Masalları", 150m },
                    { 14, 3, true, "Anne Frank'in Hatıra Defteri", 284m },
                    { 15, 4, true, "Nutuk", 543m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

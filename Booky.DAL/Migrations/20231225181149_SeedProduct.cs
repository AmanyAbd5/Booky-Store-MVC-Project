using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booky.DAL.Migrations
{
    public partial class SeedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ImageURL", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[] { 1, "amany", 1, "hhhhhhh", "12345678", "jjjj", 200.0, 190.0, 150.0, 180.0, "Math" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

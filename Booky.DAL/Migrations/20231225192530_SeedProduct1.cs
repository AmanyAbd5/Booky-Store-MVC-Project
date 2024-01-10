using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booky.DAL.Migrations
{
    public partial class SeedProduct1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageURL",
                value: "/images/ABriefHistoryofTime.jfif");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageURL",
                value: "/images/ABriefHistoryofTime.jfif.jfif");
        }
    }
}

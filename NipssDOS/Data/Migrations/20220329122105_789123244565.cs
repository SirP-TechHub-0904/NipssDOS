using Microsoft.EntityFrameworkCore.Migrations;

namespace NipssDOS.Data.Migrations
{
    public partial class _789123244565 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "TourSubCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "TourSubCategories");
        }
    }
}

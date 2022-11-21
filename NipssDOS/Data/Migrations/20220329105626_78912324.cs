using Microsoft.EntityFrameworkCore.Migrations;

namespace NipssDOS.Data.Migrations
{
    public partial class _78912324 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "TourCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "TourCategories");
        }
    }
}

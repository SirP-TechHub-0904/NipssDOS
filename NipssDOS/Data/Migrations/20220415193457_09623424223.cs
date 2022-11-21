using Microsoft.EntityFrameworkCore.Migrations;

namespace NipssDOS.Data.Migrations
{
    public partial class _09623424223 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "TourCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "TourCategories");
        }
    }
}

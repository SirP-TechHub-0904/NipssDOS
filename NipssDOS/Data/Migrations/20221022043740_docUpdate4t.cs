using Microsoft.EntityFrameworkCore.Migrations;

namespace NipssDOS.Data.Migrations
{
    public partial class docUpdate4t : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Show",
                table: "ParlyReportCategories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Show",
                table: "ParlyReportCategories");
        }
    }
}

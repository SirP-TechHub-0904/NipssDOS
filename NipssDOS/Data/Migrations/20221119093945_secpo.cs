using Microsoft.EntityFrameworkCore.Migrations;

namespace NipssDOS.Data.Migrations
{
    public partial class secpo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackViewImage",
                table: "secProjects");

            migrationBuilder.AddColumn<string>(
                name: "TopViewImage",
                table: "secProjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopViewImage",
                table: "secProjects");

            migrationBuilder.AddColumn<string>(
                name: "BackViewImage",
                table: "secProjects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

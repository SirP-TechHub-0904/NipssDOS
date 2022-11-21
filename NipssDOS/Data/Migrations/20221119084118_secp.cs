using Microsoft.EntityFrameworkCore.Migrations;

namespace NipssDOS.Data.Migrations
{
    public partial class secp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "secProjects");

            migrationBuilder.AddColumn<string>(
                name: "BackViewImage",
                table: "secProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "secProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrontViewImage",
                table: "secProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SideViewImage",
                table: "secProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "secProjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackViewImage",
                table: "secProjects");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "secProjects");

            migrationBuilder.DropColumn(
                name: "FrontViewImage",
                table: "secProjects");

            migrationBuilder.DropColumn(
                name: "SideViewImage",
                table: "secProjects");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "secProjects");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "secProjects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

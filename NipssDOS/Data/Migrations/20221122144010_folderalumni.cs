using Microsoft.EntityFrameworkCore.Migrations;

namespace NipssDOS.Data.Migrations
{
    public partial class folderalumni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "ParlyReportCategories",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParlyReportCategories_AlumniId",
                table: "ParlyReportCategories",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_AlumniId",
                table: "Events",
                column: "AlumniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Alumnis_AlumniId",
                table: "Events",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParlyReportCategories_Alumnis_AlumniId",
                table: "ParlyReportCategories",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Alumnis_AlumniId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_ParlyReportCategories_Alumnis_AlumniId",
                table: "ParlyReportCategories");

            migrationBuilder.DropIndex(
                name: "IX_ParlyReportCategories_AlumniId",
                table: "ParlyReportCategories");

            migrationBuilder.DropIndex(
                name: "IX_Events_AlumniId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "ParlyReportCategories");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "Events");
        }
    }
}

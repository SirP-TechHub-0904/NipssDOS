using Microsoft.EntityFrameworkCore.Migrations;

namespace NipssDOS.Data.Migrations
{
    public partial class alumniUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "ParlySubTwoCategories",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "ParlySubThreeCategories",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "ParlyReportSubCategories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParlySubTwoCategories_AlumniId",
                table: "ParlySubTwoCategories",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_ParlySubThreeCategories_AlumniId",
                table: "ParlySubThreeCategories",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_ParlyReportSubCategories_AlumniId",
                table: "ParlyReportSubCategories",
                column: "AlumniId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParlyReportSubCategories_Alumnis_AlumniId",
                table: "ParlyReportSubCategories",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParlySubThreeCategories_Alumnis_AlumniId",
                table: "ParlySubThreeCategories",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParlySubTwoCategories_Alumnis_AlumniId",
                table: "ParlySubTwoCategories",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParlyReportSubCategories_Alumnis_AlumniId",
                table: "ParlyReportSubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ParlySubThreeCategories_Alumnis_AlumniId",
                table: "ParlySubThreeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ParlySubTwoCategories_Alumnis_AlumniId",
                table: "ParlySubTwoCategories");

            migrationBuilder.DropIndex(
                name: "IX_ParlySubTwoCategories_AlumniId",
                table: "ParlySubTwoCategories");

            migrationBuilder.DropIndex(
                name: "IX_ParlySubThreeCategories_AlumniId",
                table: "ParlySubThreeCategories");

            migrationBuilder.DropIndex(
                name: "IX_ParlyReportSubCategories_AlumniId",
                table: "ParlyReportSubCategories");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "ParlySubTwoCategories");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "ParlySubThreeCategories");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "ParlyReportSubCategories");
        }
    }
}

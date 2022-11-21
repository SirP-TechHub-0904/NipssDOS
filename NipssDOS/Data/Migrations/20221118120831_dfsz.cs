using Microsoft.EntityFrameworkCore.Migrations;

namespace NipssDOS.Data.Migrations
{
    public partial class dfsz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "SliderCategories",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "Galleries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeneralTopicContinuation",
                table: "Alumnis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteFirstImage",
                table: "Alumnis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteSecondImage",
                table: "Alumnis",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubGeneralTopics",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SunTheme = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    AlumniId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGeneralTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubGeneralTopics_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SliderCategories_AlumniId",
                table: "SliderCategories",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_News_AlumniId",
                table: "News",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_AlumniId",
                table: "Galleries",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_SubGeneralTopics_AlumniId",
                table: "SubGeneralTopics",
                column: "AlumniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_Alumnis_AlumniId",
                table: "Galleries",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Alumnis_AlumniId",
                table: "News",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SliderCategories_Alumnis_AlumniId",
                table: "SliderCategories",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_Alumnis_AlumniId",
                table: "Galleries");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Alumnis_AlumniId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_SliderCategories_Alumnis_AlumniId",
                table: "SliderCategories");

            migrationBuilder.DropTable(
                name: "SubGeneralTopics");

            migrationBuilder.DropIndex(
                name: "IX_SliderCategories_AlumniId",
                table: "SliderCategories");

            migrationBuilder.DropIndex(
                name: "IX_News_AlumniId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Galleries_AlumniId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "SliderCategories");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "GeneralTopicContinuation",
                table: "Alumnis");

            migrationBuilder.DropColumn(
                name: "WebsiteFirstImage",
                table: "Alumnis");

            migrationBuilder.DropColumn(
                name: "WebsiteSecondImage",
                table: "Alumnis");
        }
    }
}

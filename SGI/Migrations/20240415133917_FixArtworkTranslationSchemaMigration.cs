using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class FixArtworkTranslationSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "artworktranslations");

            migrationBuilder.AddColumn<string>(
                name: "about",
                table: "artworktranslations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "artworktranslations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "about",
                table: "artworktranslations");

            migrationBuilder.DropColumn(
                name: "title",
                table: "artworktranslations");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "artworktranslations",
                nullable: false,
                defaultValue: "");
        }
    }
}

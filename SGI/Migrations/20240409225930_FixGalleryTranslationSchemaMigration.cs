using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class FixGalleryTranslationSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "about",
                table: "gallerytranslations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "history",
                table: "gallerytranslations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "about",
                table: "gallerytranslations");

            migrationBuilder.DropColumn(
                name: "history",
                table: "gallerytranslations");
        }
    }
}

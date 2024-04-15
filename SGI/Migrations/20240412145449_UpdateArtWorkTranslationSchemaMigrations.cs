using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class UpdateArtWorkTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "collection",
                table: "artworktranslations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "artworktranslations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "collection",
                table: "artworktranslations");

            migrationBuilder.DropColumn(
                name: "location",
                table: "artworktranslations");
        }
    }
}

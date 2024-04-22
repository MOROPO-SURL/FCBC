using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class UpdateCraftWorkTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "about",
                table: "craftworktranslations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "craftworktranslations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "about",
                table: "craftworktranslations");

            migrationBuilder.DropColumn(
                name: "title",
                table: "craftworktranslations");
        }
    }
}

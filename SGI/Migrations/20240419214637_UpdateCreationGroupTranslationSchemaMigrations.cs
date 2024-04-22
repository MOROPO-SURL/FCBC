using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class UpdateCreationGroupTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "about",
                table: "creationgrouptranslations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "brand",
                table: "creationgrouptranslations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "about",
                table: "creationgrouptranslations");

            migrationBuilder.DropColumn(
                name: "brand",
                table: "creationgrouptranslations");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class UpdateInstitutionalInformationTranslationCreationGroupTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "histor",
                table: "institutionalinformationtranslations",
                newName: "history");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "history",
                table: "institutionalinformationtranslations",
                newName: "histor");
        }
    }
}

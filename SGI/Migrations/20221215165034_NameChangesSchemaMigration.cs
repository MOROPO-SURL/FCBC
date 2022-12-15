using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class NameChangesSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "institutioninformations");

            migrationBuilder.CreateTable(
                name: "institutionalinformations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institutionalinformations", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "institutionalinformations");

            migrationBuilder.CreateTable(
                name: "institutioninformations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institutioninformations", x => x.id);
                });
        }
    }
}

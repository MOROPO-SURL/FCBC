using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class InstitutionalServiceTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "institutionalservicetranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    institutionalserviceid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institutionalservicetranslations", x => new { x.translatablelanguage, x.institutionalserviceid });
                    table.ForeignKey(
                        name: "fk_institutionalservicetranslations_institutionalservices_inst~",
                        column: x => x.institutionalserviceid,
                        principalTable: "institutionalservices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_institutionalservicetranslations_institutionalserviceid",
                table: "institutionalservicetranslations",
                column: "institutionalserviceid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "institutionalservicetranslations");
        }
    }
}

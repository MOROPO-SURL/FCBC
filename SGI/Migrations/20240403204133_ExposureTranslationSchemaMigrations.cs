using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ExposureTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exposuretranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    exposureid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exposuretranslations", x => new { x.translatablelanguage, x.exposureid });
                    table.ForeignKey(
                        name: "fk_exposuretranslations_exposures_exposureid",
                        column: x => x.exposureid,
                        principalTable: "exposures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_exposuretranslations_exposureid",
                table: "exposuretranslations",
                column: "exposureid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exposuretranslations");
        }
    }
}

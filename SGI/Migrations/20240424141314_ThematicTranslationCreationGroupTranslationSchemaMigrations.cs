using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ThematicTranslationCreationGroupTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "thematictranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    thematicid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_thematictranslations", x => new { x.translatablelanguage, x.thematicid });
                    table.ForeignKey(
                        name: "fk_thematictranslations_thematics_thematicid",
                        column: x => x.thematicid,
                        principalTable: "thematics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_thematictranslations_thematicid",
                table: "thematictranslations",
                column: "thematicid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "thematictranslations");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class CreationGroupTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "creationgrouptranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    creationgroupid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgrouptranslations", x => new { x.translatablelanguage, x.creationgroupid });
                    table.ForeignKey(
                        name: "fk_creationgrouptranslations_creationgroups_creationgroupid",
                        column: x => x.creationgroupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_creationgrouptranslations_creationgroupid",
                table: "creationgrouptranslations",
                column: "creationgroupid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "creationgrouptranslations");
        }
    }
}

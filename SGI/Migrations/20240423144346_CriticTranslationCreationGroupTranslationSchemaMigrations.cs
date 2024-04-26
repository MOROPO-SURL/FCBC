using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class CriticTranslationCreationGroupTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "critictranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    about = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_critictranslations", x => new { x.translatablelanguage, x.criticid });
                    table.ForeignKey(
                        name: "fk_critictranslations_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_critictranslations_criticid",
                table: "critictranslations",
                column: "criticid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "critictranslations");
        }
    }
}

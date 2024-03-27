using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ServiceWorkTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "serviceworktranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    serviceworkid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworktranslations", x => new { x.translatablelanguage, x.serviceworkid });
                    table.ForeignKey(
                        name: "fk_serviceworktranslations_serviceworks_serviceworkid",
                        column: x => x.serviceworkid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_serviceworktranslations_serviceworkid",
                table: "serviceworktranslations",
                column: "serviceworkid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "serviceworktranslations");
        }
    }
}

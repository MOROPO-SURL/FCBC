using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class AssociationTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "associationtranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    associationid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_associationtranslations", x => new { x.translatablelanguage, x.associationid });
                    table.ForeignKey(
                        name: "fk_associationtranslations_associations_associationid",
                        column: x => x.associationid,
                        principalTable: "associations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_associationtranslations_associationid",
                table: "associationtranslations",
                column: "associationid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "associationtranslations");
        }
    }
}

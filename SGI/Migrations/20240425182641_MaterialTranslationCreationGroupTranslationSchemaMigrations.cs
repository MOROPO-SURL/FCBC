using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class MaterialTranslationCreationGroupTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "materialtranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    materialid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_materialtranslations", x => new { x.translatablelanguage, x.materialid });
                    table.ForeignKey(
                        name: "fk_materialtranslations_materials_materialid",
                        column: x => x.materialid,
                        principalTable: "materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_materialtranslations_materialid",
                table: "materialtranslations",
                column: "materialid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "materialtranslations");
        }
    }
}

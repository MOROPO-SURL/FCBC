using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class TechniqueAndMaterialForCraftSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "craftworkmaterialbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    materialid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworkmaterialbridges", x => new { x.workid, x.materialid });
                    table.ForeignKey(
                        name: "fk_craftworkmaterialbridges_materials_materialid",
                        column: x => x.materialid,
                        principalTable: "materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworkmaterialbridges_craftworks_workid",
                        column: x => x.workid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_craftworkmaterialbridges_materialid",
                table: "craftworkmaterialbridges",
                column: "materialid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "craftworkmaterialbridges");
        }
    }
}

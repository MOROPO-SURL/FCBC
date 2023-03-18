using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class UpdateDesignWorkElaborationSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "designworkmaterialbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    materialid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworkmaterialbridges", x => new { x.workid, x.materialid });
                    table.ForeignKey(
                        name: "fk_designworkmaterialbridges_materials_materialid",
                        column: x => x.materialid,
                        principalTable: "materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworkmaterialbridges_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_designworkmaterialbridges_materialid",
                table: "designworkmaterialbridges",
                column: "materialid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "designworkmaterialbridges");
        }
    }
}

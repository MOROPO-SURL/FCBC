using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ElaboratonAndAttachmentsForServiceWorkSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "about",
                table: "serviceworkawards",
                newName: "context");

            migrationBuilder.CreateTable(
                name: "serviceworkattachments",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    ispublic = table.Column<bool>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    workid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworkattachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_serviceworkattachments_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceworkmaterialbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    materialid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworkmaterialbridges", x => new { x.workid, x.materialid });
                    table.ForeignKey(
                        name: "fk_serviceworkmaterialbridges_materials_materialid",
                        column: x => x.materialid,
                        principalTable: "materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworkmaterialbridges_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkattachments_workid",
                table: "serviceworkattachments",
                column: "workid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkmaterialbridges_materialid",
                table: "serviceworkmaterialbridges",
                column: "materialid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "serviceworkattachments");

            migrationBuilder.DropTable(
                name: "serviceworkmaterialbridges");

            migrationBuilder.RenameColumn(
                name: "context",
                table: "serviceworkawards",
                newName: "about");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class RedefineTechniquesSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "technique");

            migrationBuilder.CreateTable(
                name: "artistictechniques",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistictechniques", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "crafttechniques",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_crafttechniques", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "designtechniques",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designtechniques", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "servicetechniques",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servicetechniques", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "artworktechniquebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    techniqueid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworktechniquebridges", x => new { x.workid, x.techniqueid });
                    table.ForeignKey(
                        name: "fk_artworktechniquebridges_artistictechniques_techniqueid",
                        column: x => x.techniqueid,
                        principalTable: "artistictechniques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworktechniquebridges_artworks_workid",
                        column: x => x.workid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "craftworktechniquebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    techniqueid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworktechniquebridges", x => new { x.workid, x.techniqueid });
                    table.ForeignKey(
                        name: "fk_craftworktechniquebridges_crafttechniques_techniqueid",
                        column: x => x.techniqueid,
                        principalTable: "crafttechniques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworktechniquebridges_craftworks_workid",
                        column: x => x.workid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designworktechniquebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    techniqueid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworktechniquebridges", x => new { x.workid, x.techniqueid });
                    table.ForeignKey(
                        name: "fk_designworktechniquebridges_designtechniques_techniqueid",
                        column: x => x.techniqueid,
                        principalTable: "designtechniques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworktechniquebridges_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceworktechniquebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    techniqueid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworktechniquebridges", x => new { x.workid, x.techniqueid });
                    table.ForeignKey(
                        name: "fk_serviceworktechniquebridges_servicetechniques_techniqueid",
                        column: x => x.techniqueid,
                        principalTable: "servicetechniques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworktechniquebridges_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artworktechniquebridges_techniqueid",
                table: "artworktechniquebridges",
                column: "techniqueid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworktechniquebridges_techniqueid",
                table: "craftworktechniquebridges",
                column: "techniqueid");

            migrationBuilder.CreateIndex(
                name: "ix_designworktechniquebridges_techniqueid",
                table: "designworktechniquebridges",
                column: "techniqueid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworktechniquebridges_techniqueid",
                table: "serviceworktechniquebridges",
                column: "techniqueid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artworktechniquebridges");

            migrationBuilder.DropTable(
                name: "craftworktechniquebridges");

            migrationBuilder.DropTable(
                name: "designworktechniquebridges");

            migrationBuilder.DropTable(
                name: "serviceworktechniquebridges");

            migrationBuilder.DropTable(
                name: "artistictechniques");

            migrationBuilder.DropTable(
                name: "crafttechniques");

            migrationBuilder.DropTable(
                name: "designtechniques");

            migrationBuilder.DropTable(
                name: "servicetechniques");

            migrationBuilder.CreateTable(
                name: "technique",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_technique", x => x.id);
                });
        }
    }
}

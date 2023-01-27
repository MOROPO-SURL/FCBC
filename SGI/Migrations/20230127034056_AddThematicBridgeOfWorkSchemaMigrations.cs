using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class AddThematicBridgeOfWorkSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "artworkthematicbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    thematicid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworkthematicbridges", x => new { x.workid, x.thematicid });
                    table.ForeignKey(
                        name: "fk_artworkthematicbridges_thematics_thematicid",
                        column: x => x.thematicid,
                        principalTable: "thematics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworkthematicbridges_artworks_workid",
                        column: x => x.workid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "craftworkthematicbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    thematicid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworkthematicbridges", x => new { x.workid, x.thematicid });
                    table.ForeignKey(
                        name: "fk_craftworkthematicbridges_thematics_thematicid",
                        column: x => x.thematicid,
                        principalTable: "thematics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworkthematicbridges_craftworks_workid",
                        column: x => x.workid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designworkthematicbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    thematicid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworkthematicbridges", x => new { x.workid, x.thematicid });
                    table.ForeignKey(
                        name: "fk_designworkthematicbridges_thematics_thematicid",
                        column: x => x.thematicid,
                        principalTable: "thematics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworkthematicbridges_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceworkthematicbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    thematicid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworkthematicbridges", x => new { x.workid, x.thematicid });
                    table.ForeignKey(
                        name: "fk_serviceworkthematicbridges_thematics_thematicid",
                        column: x => x.thematicid,
                        principalTable: "thematics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworkthematicbridges_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artworkthematicbridges_thematicid",
                table: "artworkthematicbridges",
                column: "thematicid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworkthematicbridges_thematicid",
                table: "craftworkthematicbridges",
                column: "thematicid");

            migrationBuilder.CreateIndex(
                name: "ix_designworkthematicbridges_thematicid",
                table: "designworkthematicbridges",
                column: "thematicid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkthematicbridges_thematicid",
                table: "serviceworkthematicbridges",
                column: "thematicid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artworkthematicbridges");

            migrationBuilder.DropTable(
                name: "craftworkthematicbridges");

            migrationBuilder.DropTable(
                name: "designworkthematicbridges");

            migrationBuilder.DropTable(
                name: "serviceworkthematicbridges");
        }
    }
}

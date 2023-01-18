using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class AuthorForDesignWorkSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "designworkcreationgroupbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    creationgroupid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworkcreationgroupbridges", x => new { x.workid, x.creationgroupid });
                    table.ForeignKey(
                        name: "fk_designworkcreationgroupbridges_creationgroups_creationgroup~",
                        column: x => x.creationgroupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworkcreationgroupbridges_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designworkcreatorbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    creatorid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworkcreatorbridges", x => new { x.workid, x.creatorid });
                    table.ForeignKey(
                        name: "fk_designworkcreatorbridges_artists_creatorid",
                        column: x => x.creatorid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworkcreatorbridges_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_designworkcreationgroupbridges_creationgroupid",
                table: "designworkcreationgroupbridges",
                column: "creationgroupid");

            migrationBuilder.CreateIndex(
                name: "ix_designworkcreatorbridges_creatorid",
                table: "designworkcreatorbridges",
                column: "creatorid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "designworkcreationgroupbridges");

            migrationBuilder.DropTable(
                name: "designworkcreatorbridges");
        }
    }
}

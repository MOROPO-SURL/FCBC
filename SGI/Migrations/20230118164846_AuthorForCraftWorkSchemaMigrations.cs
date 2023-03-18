using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class AuthorForCraftWorkSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "craftworkcreationgroupbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    creationgroupid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworkcreationgroupbridges", x => new { x.workid, x.creationgroupid });
                    table.ForeignKey(
                        name: "fk_craftworkcreationgroupbridges_creationgroups_creationgroupid",
                        column: x => x.creationgroupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworkcreationgroupbridges_craftworks_workid",
                        column: x => x.workid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "craftworkcreatorbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    creatorid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworkcreatorbridges", x => new { x.workid, x.creatorid });
                    table.ForeignKey(
                        name: "fk_craftworkcreatorbridges_artists_creatorid",
                        column: x => x.creatorid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworkcreatorbridges_craftworks_workid",
                        column: x => x.workid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_craftworkcreationgroupbridges_creationgroupid",
                table: "craftworkcreationgroupbridges",
                column: "creationgroupid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworkcreatorbridges_creatorid",
                table: "craftworkcreatorbridges",
                column: "creatorid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "craftworkcreationgroupbridges");

            migrationBuilder.DropTable(
                name: "craftworkcreatorbridges");
        }
    }
}

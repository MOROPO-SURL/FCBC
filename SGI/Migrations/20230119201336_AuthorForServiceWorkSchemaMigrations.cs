using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class AuthorForServiceWorkSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "serviceworkcreationgroupbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    creationgroupid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworkcreationgroupbridges", x => new { x.workid, x.creationgroupid });
                    table.ForeignKey(
                        name: "fk_serviceworkcreationgroupbridges_creationgroups_creationgrou~",
                        column: x => x.creationgroupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworkcreationgroupbridges_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceworkcreatorbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    creatorid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworkcreatorbridges", x => new { x.workid, x.creatorid });
                    table.ForeignKey(
                        name: "fk_serviceworkcreatorbridges_artists_creatorid",
                        column: x => x.creatorid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworkcreatorbridges_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkcreationgroupbridges_creationgroupid",
                table: "serviceworkcreationgroupbridges",
                column: "creationgroupid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkcreatorbridges_creatorid",
                table: "serviceworkcreatorbridges",
                column: "creatorid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "serviceworkcreationgroupbridges");

            migrationBuilder.DropTable(
                name: "serviceworkcreatorbridges");
        }
    }
}

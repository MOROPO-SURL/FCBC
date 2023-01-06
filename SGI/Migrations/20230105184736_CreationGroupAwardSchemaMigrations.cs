using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class CreationGroupAwardSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "creationgroupawardbridges");

            migrationBuilder.CreateTable(
                name: "creationgroupawards",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    groupid = table.Column<Guid>(nullable: false),
                    awardid = table.Column<Guid>(nullable: false),
                    context = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupawards", x => x.id);
                    table.ForeignKey(
                        name: "fk_creationgroupawards_awards_awardid",
                        column: x => x.awardid,
                        principalTable: "awards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupawards_creationgroups_groupid",
                        column: x => x.groupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupawards_awardid",
                table: "creationgroupawards",
                column: "awardid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupawards_groupid",
                table: "creationgroupawards",
                column: "groupid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "creationgroupawards");

            migrationBuilder.CreateTable(
                name: "creationgroupawardbridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    awardid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupawardbridges", x => new { x.groupid, x.awardid });
                    table.ForeignKey(
                        name: "fk_creationgroupawardbridges_awards_awardid",
                        column: x => x.awardid,
                        principalTable: "awards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupawardbridges_creationgroups_groupid",
                        column: x => x.groupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupawardbridges_awardid",
                table: "creationgroupawardbridges",
                column: "awardid");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class CraftWorkAwardSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "craftworkaward",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    workid = table.Column<Guid>(nullable: false),
                    awardid = table.Column<Guid>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworkaward", x => x.id);
                    table.ForeignKey(
                        name: "fk_craftworkaward_awards_awardid",
                        column: x => x.awardid,
                        principalTable: "awards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworkaward_craftworks_workid",
                        column: x => x.workid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });            

            migrationBuilder.CreateIndex(
                name: "ix_craftworkaward_awardid",
                table: "craftworkaward",
                column: "awardid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworkaward_workid",
                table: "craftworkaward",
                column: "workid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "craftworkaward");

            migrationBuilder.DropIndex(
                name: "ix_creationgroupdistinctionbridges_distinctionid",
                table: "creationgroupdistinctionbridges");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupdistinctionbridges_groupid",
                table: "creationgroupdistinctionbridges",
                column: "groupid");
        }
    }
}

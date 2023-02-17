using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ServiceWorkAssessmentBridgeForWorkSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "serviceworkassessmentbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    dateofassessment = table.Column<DateTime>(nullable: false),
                    criterion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworkassessmentbridges", x => new { x.workid, x.criticid });
                    table.ForeignKey(
                        name: "fk_serviceworkassessmentbridges_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworkassessmentbridges_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkassessmentbridges_criticid",
                table: "serviceworkassessmentbridges",
                column: "criticid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "serviceworkassessmentbridges");
        }
    }
}

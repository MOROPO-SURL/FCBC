using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ServiceWorkAwardSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "serviceworkawards",
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
                    table.PrimaryKey("pk_serviceworkawards", x => x.id);
                    table.ForeignKey(
                        name: "fk_serviceworkawards_awards_awardid",
                        column: x => x.awardid,
                        principalTable: "awards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworkawards_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkawards_awardid",
                table: "serviceworkawards",
                column: "awardid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkawards_workid",
                table: "serviceworkawards",
                column: "workid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "serviceworkawards");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class UpdateAssessmentForArtistSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assessmentbridges");

            migrationBuilder.CreateTable(
                name: "artistassessmentbridges",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    dateofassessment = table.Column<DateTime>(nullable: false),
                    criterion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistassessmentbridges", x => new { x.artistid, x.criticid });
                    table.ForeignKey(
                        name: "fk_artistassessmentbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistassessmentbridges_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artistassessmentbridges_criticid",
                table: "artistassessmentbridges",
                column: "criticid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artistassessmentbridges");

            migrationBuilder.CreateTable(
                name: "assessmentbridges",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    criterion = table.Column<string>(nullable: true),
                    dateofassessment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assessmentbridges", x => new { x.artistid, x.criticid });
                    table.ForeignKey(
                        name: "fk_assessmentbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assessmentbridges_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_assessmentbridges_criticid",
                table: "assessmentbridges",
                column: "criticid");
        }
    }
}

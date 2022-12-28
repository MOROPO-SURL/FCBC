using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ArtistRecognitionSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "recognitions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "artistrecognitionbridge",
                columns: table => new
                {
                    recognitionid = table.Column<Guid>(nullable: false),
                    artistid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistrecognitionbridge", x => new { x.artistid, x.recognitionid });
                    table.ForeignKey(
                        name: "fk_artistrecognitionbridge_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistrecognitionbridge_recognitions_recognitionid",
                        column: x => x.recognitionid,
                        principalTable: "recognitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artistrecognitionbridge_recognitionid",
                table: "artistrecognitionbridge",
                column: "recognitionid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artistrecognitionbridge");

            migrationBuilder.DropColumn(
                name: "description",
                table: "recognitions");
        }
    }
}

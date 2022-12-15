using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class UpgradeArtistSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid[]>(
                name: "selectedvalues",
                table: "artists",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "artistmanifestationbridges",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    artisticmanifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistmanifestationbridges", x => new { x.artistid, x.artisticmanifestationid });
                    table.ForeignKey(
                        name: "fk_artistmanifestationbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistmanifestationbridges_artisticmanifestations_artisticm~",
                        column: x => x.artisticmanifestationid,
                        principalTable: "artisticmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artistmanifestationbridges_artisticmanifestationid",
                table: "artistmanifestationbridges",
                column: "artisticmanifestationid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artistmanifestationbridges");

            migrationBuilder.DropColumn(
                name: "selectedvalues",
                table: "artists");
        }
    }
}

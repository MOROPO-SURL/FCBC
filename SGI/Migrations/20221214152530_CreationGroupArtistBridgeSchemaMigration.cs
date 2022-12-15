using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class CreationGroupArtistBridgeSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "creationgroupartistbridges",
                columns: table => new
                {
                    creationgroupid = table.Column<Guid>(nullable: false),
                    artistid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupartistbridges", x => new { x.creationgroupid, x.artistid });
                    table.ForeignKey(
                        name: "fk_creationgroupartistbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupartistbridges_creationgroups_creationgroupid",
                        column: x => x.creationgroupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupartistbridges_artistid",
                table: "creationgroupartistbridges",
                column: "artistid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "creationgroupartistbridges");
        }
    }
}

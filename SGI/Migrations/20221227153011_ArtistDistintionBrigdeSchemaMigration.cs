using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ArtistDistintionBrigdeSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "distinctions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "artistdistinctionbridges",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    distinctionid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistdistinctionbridges", x => new { x.artistid, x.distinctionid });
                    table.ForeignKey(
                        name: "fk_artistdistinctionbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistdistinctionbridges_distinctions_distinctionid",
                        column: x => x.distinctionid,
                        principalTable: "distinctions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artistdistinctionbridges_distinctionid",
                table: "artistdistinctionbridges",
                column: "distinctionid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artistdistinctionbridges");

            migrationBuilder.DropColumn(
                name: "description",
                table: "distinctions");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ArtWorkTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "artworktranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    artworkid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworktranslations", x => new { x.translatablelanguage, x.artworkid });
                    table.ForeignKey(
                        name: "fk_artworktranslations_artworks_artworkid",
                        column: x => x.artworkid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artworktranslations_artworkid",
                table: "artworktranslations",
                column: "artworkid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artworktranslations");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ArtWorkTranslationInformationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "artworkid1",
                table: "artworktranslations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_artworktranslations_artworkid1",
                table: "artworktranslations",
                column: "artworkid1");

            migrationBuilder.AddForeignKey(
                name: "fk_artworktranslations_artworks_artworkid1",
                table: "artworktranslations",
                column: "artworkid1",
                principalTable: "artworks",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artworktranslations_artworks_artworkid1",
                table: "artworktranslations");

            migrationBuilder.DropIndex(
                name: "ix_artworktranslations_artworkid1",
                table: "artworktranslations");

            migrationBuilder.DropColumn(
                name: "artworkid1",
                table: "artworktranslations");
        }
    }
}

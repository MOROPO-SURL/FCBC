using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class CraftWorkTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "craftworktranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    craftworkid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworktranslations", x => new { x.translatablelanguage, x.craftworkid });
                    table.ForeignKey(
                        name: "fk_craftworktranslations_craftworks_craftworkid",
                        column: x => x.craftworkid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_craftworktranslations_craftworkid",
                table: "craftworktranslations",
                column: "craftworkid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "craftworktranslations");

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
    }
}

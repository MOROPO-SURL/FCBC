using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class GalleryTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gallerytranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    galleryid = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gallerytranslations", x => new { x.translatablelanguage, x.galleryid });
                    table.ForeignKey(
                        name: "fk_gallerytranslations_galleries_galleryid",
                        column: x => x.galleryid,
                        principalTable: "galleries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_gallerytranslations_galleryid",
                table: "gallerytranslations",
                column: "galleryid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gallerytranslations");
        }
    }
}

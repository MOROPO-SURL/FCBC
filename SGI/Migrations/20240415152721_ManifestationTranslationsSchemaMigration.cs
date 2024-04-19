using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ManifestationTranslationsSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "artisticmanifestationtranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artisticmanifestationtranslations", x => new { x.translatablelanguage, x.id });
                    table.ForeignKey(
                        name: "fk_artisticmanifestationtranslations_artisticmanifestations_id",
                        column: x => x.id,
                        principalTable: "artisticmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "craftmanifestationtranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftmanifestationtranslations", x => new { x.translatablelanguage, x.id });
                    table.ForeignKey(
                        name: "fk_craftmanifestationtranslations_craftmanifestations_id",
                        column: x => x.id,
                        principalTable: "craftmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "servicelinetranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servicelinetranslations", x => new { x.translatablelanguage, x.id });
                    table.ForeignKey(
                        name: "fk_servicelinetranslations_servicelines_id",
                        column: x => x.id,
                        principalTable: "servicelines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artisticmanifestationtranslations_id",
                table: "artisticmanifestationtranslations",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_craftmanifestationtranslations_id",
                table: "craftmanifestationtranslations",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_servicelinetranslations_id",
                table: "servicelinetranslations",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artisticmanifestationtranslations");

            migrationBuilder.DropTable(
                name: "craftmanifestationtranslations");

            migrationBuilder.DropTable(
                name: "servicelinetranslations");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class TechniqueTranslationSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "artistictechniquetranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    artistictechniqueid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistictechniquetranslations", x => new { x.translatablelanguage, x.artistictechniqueid });
                    table.ForeignKey(
                        name: "fk_artistictechniquetranslations_artistictechniques_artisticte~",
                        column: x => x.artistictechniqueid,
                        principalTable: "artistictechniques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "crafttechniquetranslation",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    crafttechniqueid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_crafttechniquetranslation", x => new { x.translatablelanguage, x.crafttechniqueid });
                    table.ForeignKey(
                        name: "fk_crafttechniquetranslation_crafttechniques_crafttechniqueid",
                        column: x => x.crafttechniqueid,
                        principalTable: "crafttechniques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "servicetechniquetranslation",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    servicetechniqueid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servicetechniquetranslation", x => new { x.translatablelanguage, x.servicetechniqueid });
                    table.ForeignKey(
                        name: "fk_servicetechniquetranslation_servicetechniques_servicetechni~",
                        column: x => x.servicetechniqueid,
                        principalTable: "servicetechniques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artistictechniquetranslations_artistictechniqueid",
                table: "artistictechniquetranslations",
                column: "artistictechniqueid");

            migrationBuilder.CreateIndex(
                name: "ix_crafttechniquetranslation_crafttechniqueid",
                table: "crafttechniquetranslation",
                column: "crafttechniqueid");

            migrationBuilder.CreateIndex(
                name: "ix_servicetechniquetranslation_servicetechniqueid",
                table: "servicetechniquetranslation",
                column: "servicetechniqueid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artistictechniquetranslations");

            migrationBuilder.DropTable(
                name: "crafttechniquetranslation");

            migrationBuilder.DropTable(
                name: "servicetechniquetranslation");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class InstitutionalInformationTranslationCreationGroupTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artisticmanifestationtranslations_artisticmanifestations_ar~",
                table: "artisticmanifestationtranslations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_artisticmanifestationtranslations",
                table: "artisticmanifestationtranslations");

            migrationBuilder.RenameTable(
                name: "artisticmanifestationtranslations",
                newName: "artisticmanifestationtranslation");

            migrationBuilder.RenameIndex(
                name: "ix_artisticmanifestationtranslations_artisticmanifestationid",
                table: "artisticmanifestationtranslation",
                newName: "ix_artisticmanifestationtranslation_artisticmanifestationid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_artisticmanifestationtranslation",
                table: "artisticmanifestationtranslation",
                columns: new[] { "translatablelanguage", "artisticmanifestationid" });

            migrationBuilder.CreateTable(
                name: "artistictechniquetranslation",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    artistictechniqueid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistictechniquetranslation", x => new { x.translatablelanguage, x.artistictechniqueid });
                    table.ForeignKey(
                        name: "fk_artistictechniquetranslation_artistictechniques_artistictec~",
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
                name: "institutionalinformationtranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    institutionalinformationkind = table.Column<int>(nullable: false),
                    socialobject = table.Column<string>(nullable: false),
                    mission = table.Column<string>(nullable: true),
                    vision = table.Column<string>(nullable: true),
                    histor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institutionalinformationtranslations", x => new { x.translatablelanguage, x.institutionalinformationkind });
                    table.ForeignKey(
                        name: "fk_institutionalinformationtranslations_institutionalinformati~",
                        column: x => x.institutionalinformationkind,
                        principalTable: "institutionalinformations",
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
                name: "ix_artistictechniquetranslation_artistictechniqueid",
                table: "artistictechniquetranslation",
                column: "artistictechniqueid");

            migrationBuilder.CreateIndex(
                name: "ix_crafttechniquetranslation_crafttechniqueid",
                table: "crafttechniquetranslation",
                column: "crafttechniqueid");

            migrationBuilder.CreateIndex(
                name: "ix_institutionalinformationtranslations_institutionalinformati~",
                table: "institutionalinformationtranslations",
                column: "institutionalinformationkind");

            migrationBuilder.CreateIndex(
                name: "ix_servicetechniquetranslation_servicetechniqueid",
                table: "servicetechniquetranslation",
                column: "servicetechniqueid");

            migrationBuilder.AddForeignKey(
                name: "fk_artisticmanifestationtranslation_artisticmanifestations_art~",
                table: "artisticmanifestationtranslation",
                column: "artisticmanifestationid",
                principalTable: "artisticmanifestations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artisticmanifestationtranslation_artisticmanifestations_art~",
                table: "artisticmanifestationtranslation");

            migrationBuilder.DropTable(
                name: "artistictechniquetranslation");

            migrationBuilder.DropTable(
                name: "crafttechniquetranslation");

            migrationBuilder.DropTable(
                name: "institutionalinformationtranslations");

            migrationBuilder.DropTable(
                name: "servicetechniquetranslation");

            migrationBuilder.DropPrimaryKey(
                name: "pk_artisticmanifestationtranslation",
                table: "artisticmanifestationtranslation");

            migrationBuilder.RenameTable(
                name: "artisticmanifestationtranslation",
                newName: "artisticmanifestationtranslations");

            migrationBuilder.RenameIndex(
                name: "ix_artisticmanifestationtranslation_artisticmanifestationid",
                table: "artisticmanifestationtranslations",
                newName: "ix_artisticmanifestationtranslations_artisticmanifestationid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_artisticmanifestationtranslations",
                table: "artisticmanifestationtranslations",
                columns: new[] { "translatablelanguage", "artisticmanifestationid" });

            migrationBuilder.AddForeignKey(
                name: "fk_artisticmanifestationtranslations_artisticmanifestations_ar~",
                table: "artisticmanifestationtranslations",
                column: "artisticmanifestationid",
                principalTable: "artisticmanifestations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

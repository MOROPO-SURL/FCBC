using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class TechniqueTranslationSchemaSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artisticmanifestationtranslation_artisticmanifestations_art~",
                table: "artisticmanifestationtranslation");

            migrationBuilder.DropForeignKey(
                name: "fk_artistictechniquetranslation_artistictechniques_artistictec~",
                table: "artistictechniquetranslation");

            migrationBuilder.DropPrimaryKey(
                name: "pk_artistictechniquetranslation",
                table: "artistictechniquetranslation");

            migrationBuilder.DropPrimaryKey(
                name: "pk_artisticmanifestationtranslation",
                table: "artisticmanifestationtranslation");

            migrationBuilder.RenameTable(
                name: "artistictechniquetranslation",
                newName: "artistictechniquetranslations");

            migrationBuilder.RenameTable(
                name: "artisticmanifestationtranslation",
                newName: "artisticmanifestationtranslations");

            migrationBuilder.RenameIndex(
                name: "ix_artistictechniquetranslation_artistictechniqueid",
                table: "artistictechniquetranslations",
                newName: "ix_artistictechniquetranslations_artistictechniqueid");

            migrationBuilder.RenameIndex(
                name: "ix_artisticmanifestationtranslation_artisticmanifestationid",
                table: "artisticmanifestationtranslations",
                newName: "ix_artisticmanifestationtranslations_artisticmanifestationid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_artistictechniquetranslations",
                table: "artistictechniquetranslations",
                columns: new[] { "translatablelanguage", "artistictechniqueid" });

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

            migrationBuilder.AddForeignKey(
                name: "fk_artistictechniquetranslations_artistictechniques_artisticte~",
                table: "artistictechniquetranslations",
                column: "artistictechniqueid",
                principalTable: "artistictechniques",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artisticmanifestationtranslations_artisticmanifestations_ar~",
                table: "artisticmanifestationtranslations");

            migrationBuilder.DropForeignKey(
                name: "fk_artistictechniquetranslations_artistictechniques_artisticte~",
                table: "artistictechniquetranslations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_artistictechniquetranslations",
                table: "artistictechniquetranslations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_artisticmanifestationtranslations",
                table: "artisticmanifestationtranslations");

            migrationBuilder.RenameTable(
                name: "artistictechniquetranslations",
                newName: "artistictechniquetranslation");

            migrationBuilder.RenameTable(
                name: "artisticmanifestationtranslations",
                newName: "artisticmanifestationtranslation");

            migrationBuilder.RenameIndex(
                name: "ix_artistictechniquetranslations_artistictechniqueid",
                table: "artistictechniquetranslation",
                newName: "ix_artistictechniquetranslation_artistictechniqueid");

            migrationBuilder.RenameIndex(
                name: "ix_artisticmanifestationtranslations_artisticmanifestationid",
                table: "artisticmanifestationtranslation",
                newName: "ix_artisticmanifestationtranslation_artisticmanifestationid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_artistictechniquetranslation",
                table: "artistictechniquetranslation",
                columns: new[] { "translatablelanguage", "artistictechniqueid" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_artisticmanifestationtranslation",
                table: "artisticmanifestationtranslation",
                columns: new[] { "translatablelanguage", "artisticmanifestationid" });

            migrationBuilder.AddForeignKey(
                name: "fk_artisticmanifestationtranslation_artisticmanifestations_art~",
                table: "artisticmanifestationtranslation",
                column: "artisticmanifestationid",
                principalTable: "artisticmanifestations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_artistictechniquetranslation_artistictechniques_artistictec~",
                table: "artistictechniquetranslation",
                column: "artistictechniqueid",
                principalTable: "artistictechniques",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

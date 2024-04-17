using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ReManifestationTranslationsSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artisticmanifestationtranslations_artisticmanifestations_id",
                table: "artisticmanifestationtranslations");

            migrationBuilder.DropForeignKey(
                name: "fk_craftmanifestationtranslations_craftmanifestations_id",
                table: "craftmanifestationtranslations");

            migrationBuilder.DropForeignKey(
                name: "fk_servicelinetranslations_servicelines_id",
                table: "servicelinetranslations");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "servicelinetranslations",
                newName: "servicelineid");

            migrationBuilder.RenameIndex(
                name: "ix_servicelinetranslations_id",
                table: "servicelinetranslations",
                newName: "ix_servicelinetranslations_servicelineid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "craftmanifestationtranslations",
                newName: "craftmanifestationid");

            migrationBuilder.RenameIndex(
                name: "ix_craftmanifestationtranslations_id",
                table: "craftmanifestationtranslations",
                newName: "ix_craftmanifestationtranslations_craftmanifestationid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "artisticmanifestationtranslations",
                newName: "artisticmanifestationid");

            migrationBuilder.RenameIndex(
                name: "ix_artisticmanifestationtranslations_id",
                table: "artisticmanifestationtranslations",
                newName: "ix_artisticmanifestationtranslations_artisticmanifestationid");

            migrationBuilder.AddForeignKey(
                name: "fk_artisticmanifestationtranslations_artisticmanifestations_ar~",
                table: "artisticmanifestationtranslations",
                column: "artisticmanifestationid",
                principalTable: "artisticmanifestations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_craftmanifestationtranslations_craftmanifestations_craftman~",
                table: "craftmanifestationtranslations",
                column: "craftmanifestationid",
                principalTable: "craftmanifestations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_servicelinetranslations_servicelines_servicelineid",
                table: "servicelinetranslations",
                column: "servicelineid",
                principalTable: "servicelines",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artisticmanifestationtranslations_artisticmanifestations_ar~",
                table: "artisticmanifestationtranslations");

            migrationBuilder.DropForeignKey(
                name: "fk_craftmanifestationtranslations_craftmanifestations_craftman~",
                table: "craftmanifestationtranslations");

            migrationBuilder.DropForeignKey(
                name: "fk_servicelinetranslations_servicelines_servicelineid",
                table: "servicelinetranslations");

            migrationBuilder.RenameColumn(
                name: "servicelineid",
                table: "servicelinetranslations",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "ix_servicelinetranslations_servicelineid",
                table: "servicelinetranslations",
                newName: "ix_servicelinetranslations_id");

            migrationBuilder.RenameColumn(
                name: "craftmanifestationid",
                table: "craftmanifestationtranslations",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "ix_craftmanifestationtranslations_craftmanifestationid",
                table: "craftmanifestationtranslations",
                newName: "ix_craftmanifestationtranslations_id");

            migrationBuilder.RenameColumn(
                name: "artisticmanifestationid",
                table: "artisticmanifestationtranslations",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "ix_artisticmanifestationtranslations_artisticmanifestationid",
                table: "artisticmanifestationtranslations",
                newName: "ix_artisticmanifestationtranslations_id");

            migrationBuilder.AddForeignKey(
                name: "fk_artisticmanifestationtranslations_artisticmanifestations_id",
                table: "artisticmanifestationtranslations",
                column: "id",
                principalTable: "artisticmanifestations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_craftmanifestationtranslations_craftmanifestations_id",
                table: "craftmanifestationtranslations",
                column: "id",
                principalTable: "craftmanifestations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_servicelinetranslations_servicelines_id",
                table: "servicelinetranslations",
                column: "id",
                principalTable: "servicelines",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

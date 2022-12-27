using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ArtistRecognitionBridgeSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artistrecognitionbridge_artists_artistid",
                table: "artistrecognitionbridge");

            migrationBuilder.DropForeignKey(
                name: "fk_artistrecognitionbridge_recognitions_recognitionid",
                table: "artistrecognitionbridge");

            migrationBuilder.DropPrimaryKey(
                name: "pk_artistrecognitionbridge",
                table: "artistrecognitionbridge");

            migrationBuilder.RenameTable(
                name: "artistrecognitionbridge",
                newName: "artistrecognitionbridges");

            migrationBuilder.RenameIndex(
                name: "ix_artistrecognitionbridge_recognitionid",
                table: "artistrecognitionbridges",
                newName: "ix_artistrecognitionbridges_recognitionid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_artistrecognitionbridges",
                table: "artistrecognitionbridges",
                columns: new[] { "artistid", "recognitionid" });

            migrationBuilder.AddForeignKey(
                name: "fk_artistrecognitionbridges_artists_artistid",
                table: "artistrecognitionbridges",
                column: "artistid",
                principalTable: "artists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_artistrecognitionbridges_recognitions_recognitionid",
                table: "artistrecognitionbridges",
                column: "recognitionid",
                principalTable: "recognitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artistrecognitionbridges_artists_artistid",
                table: "artistrecognitionbridges");

            migrationBuilder.DropForeignKey(
                name: "fk_artistrecognitionbridges_recognitions_recognitionid",
                table: "artistrecognitionbridges");

            migrationBuilder.DropPrimaryKey(
                name: "pk_artistrecognitionbridges",
                table: "artistrecognitionbridges");

            migrationBuilder.RenameTable(
                name: "artistrecognitionbridges",
                newName: "artistrecognitionbridge");

            migrationBuilder.RenameIndex(
                name: "ix_artistrecognitionbridges_recognitionid",
                table: "artistrecognitionbridge",
                newName: "ix_artistrecognitionbridge_recognitionid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_artistrecognitionbridge",
                table: "artistrecognitionbridge",
                columns: new[] { "artistid", "recognitionid" });

            migrationBuilder.AddForeignKey(
                name: "fk_artistrecognitionbridge_artists_artistid",
                table: "artistrecognitionbridge",
                column: "artistid",
                principalTable: "artists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_artistrecognitionbridge_recognitions_recognitionid",
                table: "artistrecognitionbridge",
                column: "recognitionid",
                principalTable: "recognitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

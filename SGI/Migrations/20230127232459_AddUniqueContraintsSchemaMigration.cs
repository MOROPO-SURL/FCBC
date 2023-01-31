using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class AddUniqueContraintsSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_creationgroups_registrationnumber",
                table: "creationgroups",
                column: "registrationnumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_artists_registrationnumber",
                table: "artists",
                column: "registrationnumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_creationgroups_registrationnumber",
                table: "creationgroups");

            migrationBuilder.DropIndex(
                name: "ix_artists_registrationnumber",
                table: "artists");
        }
    }
}

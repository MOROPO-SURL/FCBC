using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ArtisticManifestationAddParentSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "parentid",
                table: "artisticmanifestations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_artisticmanifestations_parentid",
                table: "artisticmanifestations",
                column: "parentid");

            migrationBuilder.AddForeignKey(
                name: "fk_artisticmanifestations_manifestationkinds_parentid",
                table: "artisticmanifestations",
                column: "parentid",
                principalTable: "manifestationkinds",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artisticmanifestations_manifestationkinds_parentid",
                table: "artisticmanifestations");

            migrationBuilder.DropIndex(
                name: "ix_artisticmanifestations_parentid",
                table: "artisticmanifestations");

            migrationBuilder.DropColumn(
                name: "parentid",
                table: "artisticmanifestations");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class LitteLevelUpSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artworks_unitmeasurements_heightunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropForeignKey(
                name: "fk_artworks_unitmeasurements_profunditytunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropForeignKey(
                name: "fk_artworks_unitmeasurements_widthunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropIndex(
                name: "ix_artworks_heightunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropIndex(
                name: "ix_artworks_profunditytunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "heightunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "profundityunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "profunditytunitmeasurementid",
                table: "artworks");

            migrationBuilder.RenameColumn(
                name: "widthunitmeasurementid",
                table: "artworks",
                newName: "dimensionsunitmeasurementid");

            migrationBuilder.RenameIndex(
                name: "ix_artworks_widthunitmeasurementid",
                table: "artworks",
                newName: "ix_artworks_dimensionsunitmeasurementid");

            migrationBuilder.AddColumn<DateTime>(
                name: "registrationendat",
                table: "artists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "registrationnumber",
                table: "artists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "fk_artworks_unitmeasurements_dimensionsunitmeasurementid",
                table: "artworks",
                column: "dimensionsunitmeasurementid",
                principalTable: "unitmeasurements",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artworks_unitmeasurements_dimensionsunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "registrationendat",
                table: "artists");

            migrationBuilder.DropColumn(
                name: "registrationnumber",
                table: "artists");

            migrationBuilder.RenameColumn(
                name: "dimensionsunitmeasurementid",
                table: "artworks",
                newName: "widthunitmeasurementid");

            migrationBuilder.RenameIndex(
                name: "ix_artworks_dimensionsunitmeasurementid",
                table: "artworks",
                newName: "ix_artworks_widthunitmeasurementid");

            migrationBuilder.AddColumn<Guid>(
                name: "heightunitmeasurementid",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "profundityunitmeasurementid",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "profunditytunitmeasurementid",
                table: "artworks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_artworks_heightunitmeasurementid",
                table: "artworks",
                column: "heightunitmeasurementid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_profunditytunitmeasurementid",
                table: "artworks",
                column: "profunditytunitmeasurementid");

            migrationBuilder.AddForeignKey(
                name: "fk_artworks_unitmeasurements_heightunitmeasurementid",
                table: "artworks",
                column: "heightunitmeasurementid",
                principalTable: "unitmeasurements",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_artworks_unitmeasurements_profunditytunitmeasurementid",
                table: "artworks",
                column: "profunditytunitmeasurementid",
                principalTable: "unitmeasurements",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_artworks_unitmeasurements_widthunitmeasurementid",
                table: "artworks",
                column: "widthunitmeasurementid",
                principalTable: "unitmeasurements",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

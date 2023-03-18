using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class UpdateContractorSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "brand",
                table: "contractors",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "brandimageid",
                table: "contractors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_contractors_brandimageid",
                table: "contractors",
                column: "brandimageid");

            migrationBuilder.AddForeignKey(
                name: "fk_contractors_images_brandimageid",
                table: "contractors",
                column: "brandimageid",
                principalSchema: "fcbc-sgi_sapressc_middleware_media",
                principalTable: "images",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contractors_images_brandimageid",
                table: "contractors");

            migrationBuilder.DropIndex(
                name: "ix_contractors_brandimageid",
                table: "contractors");

            migrationBuilder.DropColumn(
                name: "brand",
                table: "contractors");

            migrationBuilder.DropColumn(
                name: "brandimageid",
                table: "contractors");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class TuneUpGallerySchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "about",
                table: "galleries");

            migrationBuilder.DropColumn(
                name: "name",
                table: "galleries");

            migrationBuilder.AddColumn<string>(
                name: "history",
                table: "galleries",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "identityid",
                table: "galleries",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_galleries_identityid",
                table: "galleries",
                column: "identityid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_galleries_organizationidentities_identityid",
                table: "galleries",
                column: "identityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "organizationidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_galleries_organizationidentities_identityid",
                table: "galleries");

            migrationBuilder.DropIndex(
                name: "ix_galleries_identityid",
                table: "galleries");

            migrationBuilder.DropColumn(
                name: "history",
                table: "galleries");

            migrationBuilder.DropColumn(
                name: "identityid",
                table: "galleries");

            migrationBuilder.AddColumn<string>(
                name: "about",
                table: "galleries",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "galleries",
                nullable: false,
                defaultValue: "");
        }
    }
}

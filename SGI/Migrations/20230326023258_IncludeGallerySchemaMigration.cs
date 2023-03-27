using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class IncludeGallerySchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "latitude",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "postalcontactitems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "longitude",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "postalcontactitems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "galleryid",
                table: "exposures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "galleries",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    about = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_galleries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "galleryemployeebridges",
                columns: table => new
                {
                    galleryid = table.Column<int>(nullable: false),
                    employeeid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_galleryemployeebridges", x => new { x.galleryid, x.employeeid });
                    table.ForeignKey(
                        name: "fk_galleryemployeebridges_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_galleryemployeebridges_galleries_galleryid",
                        column: x => x.galleryid,
                        principalTable: "galleries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_exposures_galleryid",
                table: "exposures",
                column: "galleryid");

            migrationBuilder.CreateIndex(
                name: "ix_galleryemployeebridges_employeeid",
                table: "galleryemployeebridges",
                column: "employeeid");

            migrationBuilder.AddForeignKey(
                name: "fk_exposures_galleries_galleryid",
                table: "exposures",
                column: "galleryid",
                principalTable: "galleries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_exposures_galleries_galleryid",
                table: "exposures");

            migrationBuilder.DropTable(
                name: "galleryemployeebridges");

            migrationBuilder.DropTable(
                name: "galleries");

            migrationBuilder.DropIndex(
                name: "ix_exposures_galleryid",
                table: "exposures");

            migrationBuilder.DropColumn(
                name: "latitude",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "postalcontactitems");

            migrationBuilder.DropColumn(
                name: "longitude",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "postalcontactitems");

            migrationBuilder.DropColumn(
                name: "galleryid",
                table: "exposures");
        }
    }
}

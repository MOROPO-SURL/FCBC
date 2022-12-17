using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class LevelUpSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bornat",
                table: "serviceworks");

            migrationBuilder.DropColumn(
                name: "bornat",
                table: "designworks");

            migrationBuilder.DropColumn(
                name: "bornat",
                table: "craftworks");

            migrationBuilder.DropColumn(
                name: "bornat",
                table: "artworks");

            migrationBuilder.AddColumn<int>(
                name: "bornyear",
                table: "serviceworks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "featureimageid",
                table: "serviceworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "bornyear",
                table: "designworks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "featureimageid",
                table: "designworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "bornyear",
                table: "craftworks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "featureimageid",
                table: "craftworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "bornyear",
                table: "artworks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "featureimageid",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "employeeid1",
                table: "administrativerolebridges",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "artworkimagebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    imageid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworkimagebridges", x => new { x.workid, x.imageid });
                    table.ForeignKey(
                        name: "fk_artworkimagebridges_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworkimagebridges_artworks_workid",
                        column: x => x.workid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "craftworkimagebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    imageid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworkimagebridges", x => new { x.workid, x.imageid });
                    table.ForeignKey(
                        name: "fk_craftworkimagebridges_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworkimagebridges_craftworks_workid",
                        column: x => x.workid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designworkimagebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    imageid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworkimagebridges", x => new { x.workid, x.imageid });
                    table.ForeignKey(
                        name: "fk_designworkimagebridges_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworkimagebridges_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "materials",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_materials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "serviceworkimagebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    imageid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworkimagebridges", x => new { x.workid, x.imageid });
                    table.ForeignKey(
                        name: "fk_serviceworkimagebridges_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworkimagebridges_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "unitmeasurements",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unitmeasurements", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_serviceworks_featureimageid",
                table: "serviceworks",
                column: "featureimageid");

            migrationBuilder.CreateIndex(
                name: "ix_designworks_featureimageid",
                table: "designworks",
                column: "featureimageid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworks_featureimageid",
                table: "craftworks",
                column: "featureimageid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_featureimageid",
                table: "artworks",
                column: "featureimageid");

            migrationBuilder.CreateIndex(
                name: "ix_administrativerolebridges_employeeid1",
                table: "administrativerolebridges",
                column: "employeeid1");

            migrationBuilder.CreateIndex(
                name: "ix_artworkimagebridges_imageid",
                table: "artworkimagebridges",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworkimagebridges_imageid",
                table: "craftworkimagebridges",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_designworkimagebridges_imageid",
                table: "designworkimagebridges",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkimagebridges_imageid",
                table: "serviceworkimagebridges",
                column: "imageid");

            migrationBuilder.AddForeignKey(
                name: "fk_administrativerolebridges_employees_employeeid1",
                table: "administrativerolebridges",
                column: "employeeid1",
                principalTable: "employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_artworks_images_featureimageid",
                table: "artworks",
                column: "featureimageid",
                principalSchema: "fcbc-sgi_sapressc_middleware_media",
                principalTable: "images",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_craftworks_images_featureimageid",
                table: "craftworks",
                column: "featureimageid",
                principalSchema: "fcbc-sgi_sapressc_middleware_media",
                principalTable: "images",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_designworks_images_featureimageid",
                table: "designworks",
                column: "featureimageid",
                principalSchema: "fcbc-sgi_sapressc_middleware_media",
                principalTable: "images",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_serviceworks_images_featureimageid",
                table: "serviceworks",
                column: "featureimageid",
                principalSchema: "fcbc-sgi_sapressc_middleware_media",
                principalTable: "images",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_administrativerolebridges_employees_employeeid1",
                table: "administrativerolebridges");

            migrationBuilder.DropForeignKey(
                name: "fk_artworks_images_featureimageid",
                table: "artworks");

            migrationBuilder.DropForeignKey(
                name: "fk_craftworks_images_featureimageid",
                table: "craftworks");

            migrationBuilder.DropForeignKey(
                name: "fk_designworks_images_featureimageid",
                table: "designworks");

            migrationBuilder.DropForeignKey(
                name: "fk_serviceworks_images_featureimageid",
                table: "serviceworks");

            migrationBuilder.DropTable(
                name: "artworkimagebridges");

            migrationBuilder.DropTable(
                name: "craftworkimagebridges");

            migrationBuilder.DropTable(
                name: "designworkimagebridges");

            migrationBuilder.DropTable(
                name: "materials");

            migrationBuilder.DropTable(
                name: "serviceworkimagebridges");

            migrationBuilder.DropTable(
                name: "unitmeasurements");

            migrationBuilder.DropIndex(
                name: "ix_serviceworks_featureimageid",
                table: "serviceworks");

            migrationBuilder.DropIndex(
                name: "ix_designworks_featureimageid",
                table: "designworks");

            migrationBuilder.DropIndex(
                name: "ix_craftworks_featureimageid",
                table: "craftworks");

            migrationBuilder.DropIndex(
                name: "ix_artworks_featureimageid",
                table: "artworks");

            migrationBuilder.DropIndex(
                name: "ix_administrativerolebridges_employeeid1",
                table: "administrativerolebridges");

            migrationBuilder.DropColumn(
                name: "bornyear",
                table: "serviceworks");

            migrationBuilder.DropColumn(
                name: "featureimageid",
                table: "serviceworks");

            migrationBuilder.DropColumn(
                name: "bornyear",
                table: "designworks");

            migrationBuilder.DropColumn(
                name: "featureimageid",
                table: "designworks");

            migrationBuilder.DropColumn(
                name: "bornyear",
                table: "craftworks");

            migrationBuilder.DropColumn(
                name: "featureimageid",
                table: "craftworks");

            migrationBuilder.DropColumn(
                name: "bornyear",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "featureimageid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "employeeid1",
                table: "administrativerolebridges");

            migrationBuilder.AddColumn<DateTime>(
                name: "bornat",
                table: "serviceworks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "bornat",
                table: "designworks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "bornat",
                table: "craftworks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "bornat",
                table: "artworks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

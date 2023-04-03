using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class AppendExposureAndApiKeySchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "apikeys",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    token = table.Column<string>(nullable: false),
                    enabled = table.Column<bool>(nullable: false),
                    expirateat = table.Column<DateTime>(nullable: false),
                    host = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_apikeys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exposures",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    isenabled = table.Column<bool>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    startat = table.Column<DateTime>(nullable: true),
                    endat = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exposures", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "apikeypermissionbridges",
                columns: table => new
                {
                    apikeyid = table.Column<Guid>(nullable: false),
                    permissionid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_apikeypermissionbridges", x => new { x.apikeyid, x.permissionid });
                    table.ForeignKey(
                        name: "fk_apikeypermissionbridges_apikeys_apikeyid",
                        column: x => x.apikeyid,
                        principalTable: "apikeys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_apikeypermissionbridges_permissions_permissionid",
                        column: x => x.permissionid,
                        principalSchema: "fcbc-sgi_sapressc_core_security_common",
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exhibits",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    index = table.Column<int>(nullable: false),
                    exposureid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exhibits", x => x.id);
                    table.ForeignKey(
                        name: "fk_exhibits_exposures_exposureid",
                        column: x => x.exposureid,
                        principalTable: "exposures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exposureemployeecuratorshipbridges",
                columns: table => new
                {
                    exposureid = table.Column<Guid>(nullable: false),
                    employeeid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exposureemployeecuratorshipbridges", x => new { x.exposureid, x.employeeid });
                    table.ForeignKey(
                        name: "fk_exposureemployeecuratorshipbridges_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exposureemployeecuratorshipbridges_exposures_exposureid",
                        column: x => x.exposureid,
                        principalTable: "exposures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exhibitartworkbridges",
                columns: table => new
                {
                    exhibitid = table.Column<Guid>(nullable: false),
                    artworkid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exhibitartworkbridges", x => new { x.exhibitid, x.artworkid });
                    table.ForeignKey(
                        name: "fk_exhibitartworkbridges_artworks_artworkid",
                        column: x => x.artworkid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exhibitartworkbridges_exhibits_exhibitid",
                        column: x => x.exhibitid,
                        principalTable: "exhibits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exhibitcraftworkbridges",
                columns: table => new
                {
                    exhibitid = table.Column<Guid>(nullable: false),
                    craftworkid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exhibitcraftworkbridges", x => new { x.exhibitid, x.craftworkid });
                    table.ForeignKey(
                        name: "fk_exhibitcraftworkbridges_craftworks_craftworkid",
                        column: x => x.craftworkid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exhibitcraftworkbridges_exhibits_exhibitid",
                        column: x => x.exhibitid,
                        principalTable: "exhibits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exhibitdesignworkbridges",
                columns: table => new
                {
                    exhibitid = table.Column<Guid>(nullable: false),
                    designworkid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exhibitdesignworkbridges", x => new { x.exhibitid, x.designworkid });
                    table.ForeignKey(
                        name: "fk_exhibitdesignworkbridges_designworks_designworkid",
                        column: x => x.designworkid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exhibitdesignworkbridges_exhibits_exhibitid",
                        column: x => x.exhibitid,
                        principalTable: "exhibits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exhibitserviceworkbridges",
                columns: table => new
                {
                    exhibitid = table.Column<Guid>(nullable: false),
                    serviceworkid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exhibitserviceworkbridges", x => new { x.exhibitid, x.serviceworkid });
                    table.ForeignKey(
                        name: "fk_exhibitserviceworkbridges_exhibits_exhibitid",
                        column: x => x.exhibitid,
                        principalTable: "exhibits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exhibitserviceworkbridges_serviceworks_serviceworkid",
                        column: x => x.serviceworkid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_apikeypermissionbridges_permissionid",
                table: "apikeypermissionbridges",
                column: "permissionid");

            migrationBuilder.CreateIndex(
                name: "ix_exhibitartworkbridges_artworkid",
                table: "exhibitartworkbridges",
                column: "artworkid");

            migrationBuilder.CreateIndex(
                name: "ix_exhibitcraftworkbridges_craftworkid",
                table: "exhibitcraftworkbridges",
                column: "craftworkid");

            migrationBuilder.CreateIndex(
                name: "ix_exhibitdesignworkbridges_designworkid",
                table: "exhibitdesignworkbridges",
                column: "designworkid");

            migrationBuilder.CreateIndex(
                name: "ix_exhibits_exposureid",
                table: "exhibits",
                column: "exposureid");

            migrationBuilder.CreateIndex(
                name: "ix_exhibitserviceworkbridges_serviceworkid",
                table: "exhibitserviceworkbridges",
                column: "serviceworkid");

            migrationBuilder.CreateIndex(
                name: "ix_exposureemployeecuratorshipbridges_employeeid",
                table: "exposureemployeecuratorshipbridges",
                column: "employeeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apikeypermissionbridges");

            migrationBuilder.DropTable(
                name: "exhibitartworkbridges");

            migrationBuilder.DropTable(
                name: "exhibitcraftworkbridges");

            migrationBuilder.DropTable(
                name: "exhibitdesignworkbridges");

            migrationBuilder.DropTable(
                name: "exhibitserviceworkbridges");

            migrationBuilder.DropTable(
                name: "exposureemployeecuratorshipbridges");

            migrationBuilder.DropTable(
                name: "apikeys");

            migrationBuilder.DropTable(
                name: "exhibits");

            migrationBuilder.DropTable(
                name: "exposures");
        }
    }
}

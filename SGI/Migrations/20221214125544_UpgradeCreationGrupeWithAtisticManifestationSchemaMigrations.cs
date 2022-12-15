using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class UpgradeCreationGrupeWithAtisticManifestationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid[]>(
                name: "selectedvalues",
                table: "creationgroups",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "creationgroupartisticmanifestationbrigdes",
                columns: table => new
                {
                    creationgroupid = table.Column<Guid>(nullable: false),
                    artisticmanifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupartisticmanifestationbrigdes", x => new { x.creationgroupid, x.artisticmanifestationid });
                    table.ForeignKey(
                        name: "fk_creationgroupartisticmanifestationbrigdes_artisticmanifesta~",
                        column: x => x.artisticmanifestationid,
                        principalTable: "artisticmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupartisticmanifestationbrigdes_creationgroups_cr~",
                        column: x => x.creationgroupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupartisticmanifestationbrigdes_artisticmanifesta~",
                table: "creationgroupartisticmanifestationbrigdes",
                column: "artisticmanifestationid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "creationgroupartisticmanifestationbrigdes");

            migrationBuilder.DropColumn(
                name: "selectedvalues",
                table: "creationgroups");
        }
    }
}

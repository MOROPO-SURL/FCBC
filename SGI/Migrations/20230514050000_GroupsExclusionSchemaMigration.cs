using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class GroupsExclusionSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artisticmanifestations_artisticmanifestationgroups_groupid",
                table: "artisticmanifestations");

            migrationBuilder.DropForeignKey(
                name: "fk_craftmanifestations_craftmanifestationgroups_groupid",
                table: "craftmanifestations");

            migrationBuilder.DropForeignKey(
                name: "fk_designmanifestations_designmanifestationgroups_groupid",
                table: "designmanifestations");

            migrationBuilder.DropForeignKey(
                name: "fk_servicelines_servicelinegroups_groupid",
                table: "servicelines");

            migrationBuilder.DropTable(
                name: "artisticmanifestationgroups");

            migrationBuilder.DropTable(
                name: "craftmanifestationgroups");

            migrationBuilder.DropTable(
                name: "designmanifestationgroups");

            migrationBuilder.DropTable(
                name: "servicelinegroups");

            migrationBuilder.DropIndex(
                name: "ix_servicelines_groupid",
                table: "servicelines");

            migrationBuilder.DropIndex(
                name: "ix_designmanifestations_groupid",
                table: "designmanifestations");

            migrationBuilder.DropIndex(
                name: "ix_craftmanifestations_groupid",
                table: "craftmanifestations");

            migrationBuilder.DropIndex(
                name: "ix_artisticmanifestations_groupid",
                table: "artisticmanifestations");

            migrationBuilder.DropColumn(
                name: "groupid",
                table: "servicelines");

            migrationBuilder.DropColumn(
                name: "groupid",
                table: "designmanifestations");

            migrationBuilder.DropColumn(
                name: "groupid",
                table: "craftmanifestations");

            migrationBuilder.DropColumn(
                name: "groupid",
                table: "artisticmanifestations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "groupid",
                table: "servicelines",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "groupid",
                table: "designmanifestations",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "groupid",
                table: "craftmanifestations",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "groupid",
                table: "artisticmanifestations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "artisticmanifestationgroups",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    parentid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artisticmanifestationgroups", x => x.id);
                    table.ForeignKey(
                        name: "fk_artisticmanifestationgroups_artisticmanifestationgroups_par~",
                        column: x => x.parentid,
                        principalTable: "artisticmanifestationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "craftmanifestationgroups",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    parentid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftmanifestationgroups", x => x.id);
                    table.ForeignKey(
                        name: "fk_craftmanifestationgroups_craftmanifestationgroups_parentid",
                        column: x => x.parentid,
                        principalTable: "craftmanifestationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "designmanifestationgroups",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    parentid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designmanifestationgroups", x => x.id);
                    table.ForeignKey(
                        name: "fk_designmanifestationgroups_designmanifestationgroups_parentid",
                        column: x => x.parentid,
                        principalTable: "designmanifestationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "servicelinegroups",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    parentid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servicelinegroups", x => x.id);
                    table.ForeignKey(
                        name: "fk_servicelinegroups_servicelinegroups_parentid",
                        column: x => x.parentid,
                        principalTable: "servicelinegroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_servicelines_groupid",
                table: "servicelines",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_designmanifestations_groupid",
                table: "designmanifestations",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_craftmanifestations_groupid",
                table: "craftmanifestations",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_artisticmanifestations_groupid",
                table: "artisticmanifestations",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_artisticmanifestationgroups_parentid",
                table: "artisticmanifestationgroups",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_craftmanifestationgroups_parentid",
                table: "craftmanifestationgroups",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_designmanifestationgroups_parentid",
                table: "designmanifestationgroups",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_servicelinegroups_parentid",
                table: "servicelinegroups",
                column: "parentid");

            migrationBuilder.AddForeignKey(
                name: "fk_artisticmanifestations_artisticmanifestationgroups_groupid",
                table: "artisticmanifestations",
                column: "groupid",
                principalTable: "artisticmanifestationgroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_craftmanifestations_craftmanifestationgroups_groupid",
                table: "craftmanifestations",
                column: "groupid",
                principalTable: "craftmanifestationgroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_designmanifestations_designmanifestationgroups_groupid",
                table: "designmanifestations",
                column: "groupid",
                principalTable: "designmanifestationgroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_servicelines_servicelinegroups_groupid",
                table: "servicelines",
                column: "groupid",
                principalTable: "servicelinegroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

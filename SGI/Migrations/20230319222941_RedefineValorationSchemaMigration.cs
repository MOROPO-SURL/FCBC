using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class RedefineValorationSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_administrativerolebridges_employees_employeeid1",
                table: "administrativerolebridges");

            migrationBuilder.DropForeignKey(
                name: "fk_artworkvaloration_critics_criticid",
                table: "artworkvaloration");

            migrationBuilder.DropForeignKey(
                name: "fk_artworkvaloration_artworks_workid",
                table: "artworkvaloration");

            migrationBuilder.DropTable(
                name: "artistassessmentbridges");

            migrationBuilder.DropTable(
                name: "artworkassessmentbridges");

            migrationBuilder.DropTable(
                name: "craftworkassessmentbridges");

            migrationBuilder.DropTable(
                name: "designworkassessmentbridges");

            migrationBuilder.DropTable(
                name: "groupassessmentbridges");

            migrationBuilder.DropTable(
                name: "serviceworkassessmentbridges");

            migrationBuilder.DropIndex(
                name: "ix_administrativerolebridges_employeeid1",
                table: "administrativerolebridges");

            migrationBuilder.DropPrimaryKey(
                name: "pk_artworkvaloration",
                table: "artworkvaloration");

            migrationBuilder.DropColumn(
                name: "employeeid1",
                table: "administrativerolebridges");

            migrationBuilder.RenameTable(
                name: "artworkvaloration",
                newName: "artworkvalorations");

            migrationBuilder.RenameIndex(
                name: "ix_artworkvaloration_workid",
                table: "artworkvalorations",
                newName: "ix_artworkvalorations_workid");

            migrationBuilder.RenameIndex(
                name: "ix_artworkvaloration_criticid",
                table: "artworkvalorations",
                newName: "ix_artworkvalorations_criticid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_artworkvalorations",
                table: "artworkvalorations",
                column: "id");

            migrationBuilder.CreateTable(
                name: "artistvalorations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    creatorid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    declaration = table.Column<string>(nullable: false),
                    declaredat = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistvalorations", x => x.id);
                    table.ForeignKey(
                        name: "fk_artistvalorations_artists_creatorid",
                        column: x => x.creatorid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistvalorations_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "craftworkvalorations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    workid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    declaration = table.Column<string>(nullable: false),
                    declaredat = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworkvalorations", x => x.id);
                    table.ForeignKey(
                        name: "fk_craftworkvalorations_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworkvalorations_craftworks_workid",
                        column: x => x.workid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "creationgroupvalorations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    creationgroupid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    declaration = table.Column<string>(nullable: false),
                    declaredat = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupvalorations", x => x.id);
                    table.ForeignKey(
                        name: "fk_creationgroupvalorations_creationgroups_creationgroupid",
                        column: x => x.creationgroupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupvalorations_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designworkvalorations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    workid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    declaration = table.Column<string>(nullable: false),
                    declaredat = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworkvalorations", x => x.id);
                    table.ForeignKey(
                        name: "fk_designworkvalorations_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworkvalorations_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceworkvalorations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    workid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    declaration = table.Column<string>(nullable: false),
                    declaredat = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworkvalorations", x => x.id);
                    table.ForeignKey(
                        name: "fk_serviceworkvalorations_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworkvalorations_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artistvalorations_creatorid",
                table: "artistvalorations",
                column: "creatorid");

            migrationBuilder.CreateIndex(
                name: "ix_artistvalorations_criticid",
                table: "artistvalorations",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworkvalorations_criticid",
                table: "craftworkvalorations",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworkvalorations_workid",
                table: "craftworkvalorations",
                column: "workid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupvalorations_creationgroupid",
                table: "creationgroupvalorations",
                column: "creationgroupid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupvalorations_criticid",
                table: "creationgroupvalorations",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_designworkvalorations_criticid",
                table: "designworkvalorations",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_designworkvalorations_workid",
                table: "designworkvalorations",
                column: "workid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkvalorations_criticid",
                table: "serviceworkvalorations",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkvalorations_workid",
                table: "serviceworkvalorations",
                column: "workid");

            migrationBuilder.AddForeignKey(
                name: "fk_artworkvalorations_critics_criticid",
                table: "artworkvalorations",
                column: "criticid",
                principalTable: "critics",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_artworkvalorations_artworks_workid",
                table: "artworkvalorations",
                column: "workid",
                principalTable: "artworks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artworkvalorations_critics_criticid",
                table: "artworkvalorations");

            migrationBuilder.DropForeignKey(
                name: "fk_artworkvalorations_artworks_workid",
                table: "artworkvalorations");

            migrationBuilder.DropTable(
                name: "artistvalorations");

            migrationBuilder.DropTable(
                name: "craftworkvalorations");

            migrationBuilder.DropTable(
                name: "creationgroupvalorations");

            migrationBuilder.DropTable(
                name: "designworkvalorations");

            migrationBuilder.DropTable(
                name: "serviceworkvalorations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_artworkvalorations",
                table: "artworkvalorations");

            migrationBuilder.RenameTable(
                name: "artworkvalorations",
                newName: "artworkvaloration");

            migrationBuilder.RenameIndex(
                name: "ix_artworkvalorations_workid",
                table: "artworkvaloration",
                newName: "ix_artworkvaloration_workid");

            migrationBuilder.RenameIndex(
                name: "ix_artworkvalorations_criticid",
                table: "artworkvaloration",
                newName: "ix_artworkvaloration_criticid");

            migrationBuilder.AddColumn<Guid>(
                name: "employeeid1",
                table: "administrativerolebridges",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_artworkvaloration",
                table: "artworkvaloration",
                column: "id");

            migrationBuilder.CreateTable(
                name: "artistassessmentbridges",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    criterion = table.Column<string>(nullable: true),
                    dateofassessment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistassessmentbridges", x => new { x.artistid, x.criticid });
                    table.ForeignKey(
                        name: "fk_artistassessmentbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistassessmentbridges_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artworkassessmentbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    criterion = table.Column<string>(nullable: true),
                    dateofassessment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworkassessmentbridges", x => new { x.workid, x.criticid });
                    table.ForeignKey(
                        name: "fk_artworkassessmentbridges_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworkassessmentbridges_artworks_workid",
                        column: x => x.workid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "craftworkassessmentbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    criterion = table.Column<string>(nullable: true),
                    dateofassessment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworkassessmentbridges", x => new { x.workid, x.criticid });
                    table.ForeignKey(
                        name: "fk_craftworkassessmentbridges_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworkassessmentbridges_craftworks_workid",
                        column: x => x.workid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designworkassessmentbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    criterion = table.Column<string>(nullable: true),
                    dateofassessment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworkassessmentbridges", x => new { x.workid, x.criticid });
                    table.ForeignKey(
                        name: "fk_designworkassessmentbridges_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworkassessmentbridges_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "groupassessmentbridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    criterion = table.Column<string>(nullable: true),
                    dateofassessment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_groupassessmentbridges", x => new { x.groupid, x.criticid });
                    table.ForeignKey(
                        name: "fk_groupassessmentbridges_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_groupassessmentbridges_creationgroups_groupid",
                        column: x => x.groupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceworkassessmentbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    criterion = table.Column<string>(nullable: true),
                    dateofassessment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworkassessmentbridges", x => new { x.workid, x.criticid });
                    table.ForeignKey(
                        name: "fk_serviceworkassessmentbridges_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworkassessmentbridges_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_administrativerolebridges_employeeid1",
                table: "administrativerolebridges",
                column: "employeeid1");

            migrationBuilder.CreateIndex(
                name: "ix_artistassessmentbridges_criticid",
                table: "artistassessmentbridges",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkassessmentbridges_criticid",
                table: "artworkassessmentbridges",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworkassessmentbridges_criticid",
                table: "craftworkassessmentbridges",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_designworkassessmentbridges_criticid",
                table: "designworkassessmentbridges",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_groupassessmentbridges_criticid",
                table: "groupassessmentbridges",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworkassessmentbridges_criticid",
                table: "serviceworkassessmentbridges",
                column: "criticid");

            migrationBuilder.AddForeignKey(
                name: "fk_administrativerolebridges_employees_employeeid1",
                table: "administrativerolebridges",
                column: "employeeid1",
                principalTable: "employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_artworkvaloration_critics_criticid",
                table: "artworkvaloration",
                column: "criticid",
                principalTable: "critics",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_artworkvaloration_artworks_workid",
                table: "artworkvaloration",
                column: "workid",
                principalTable: "artworks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

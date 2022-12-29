using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class CreationGroupWithAwardRcognitionDistintionSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "creationgroupawardbridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    awardid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupawardbridges", x => new { x.groupid, x.awardid });
                    table.ForeignKey(
                        name: "fk_creationgroupawardbridges_awards_awardid",
                        column: x => x.awardid,
                        principalTable: "awards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupawardbridges_creationgroups_groupid",
                        column: x => x.groupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "creationgroupdistinctionbridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    distinctionid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupdistinctionbridges", x => new { x.groupid, x.distinctionid });
                    table.ForeignKey(
                        name: "fk_creationgroupdistinctionbridges_creationgroups_distinctionid",
                        column: x => x.distinctionid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupdistinctionbridges_distinctions_distinctionid",
                        column: x => x.distinctionid,
                        principalTable: "distinctions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "creationgrouprecognitionbridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    recognitionid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgrouprecognitionbridges", x => new { x.groupid, x.recognitionid });
                    table.ForeignKey(
                        name: "fk_creationgrouprecognitionbridges_creationgroups_groupid",
                        column: x => x.groupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgrouprecognitionbridges_recognitions_recognitionid",
                        column: x => x.recognitionid,
                        principalTable: "recognitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupawardbridges_awardid",
                table: "creationgroupawardbridges",
                column: "awardid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupdistinctionbridges_distinctionid",
                table: "creationgroupdistinctionbridges",
                column: "distinctionid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgrouprecognitionbridges_recognitionid",
                table: "creationgrouprecognitionbridges",
                column: "recognitionid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "creationgroupawardbridges");

            migrationBuilder.DropTable(
                name: "creationgroupdistinctionbridges");

            migrationBuilder.DropTable(
                name: "creationgrouprecognitionbridges");
        }
    }
}

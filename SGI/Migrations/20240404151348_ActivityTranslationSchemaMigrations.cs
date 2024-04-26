using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ActivityTranslationSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activitytranslations",
                columns: table => new
                {
                    translatablelanguage = table.Column<int>(nullable: false),
                    activityid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activitytranslations", x => new { x.translatablelanguage, x.activityid });
                    table.ForeignKey(
                        name: "fk_activitytranslations_activities_activityid",
                        column: x => x.activityid,
                        principalTable: "activities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_activitytranslations_activityid",
                table: "activitytranslations",
                column: "activityid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activitytranslations");
        }
    }
}

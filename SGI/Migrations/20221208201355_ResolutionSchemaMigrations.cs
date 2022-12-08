using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ResolutionSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "resolutions",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    numberresolution = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_resolutions", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resolutions");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class CriticSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "critics",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    personidentityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_critics", x => x.id);
                    table.ForeignKey(
                        name: "fk_critics_personidentities_personidentityid",
                        column: x => x.personidentityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_critics_personidentityid",
                table: "critics",
                column: "personidentityid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "critics");
        }
    }
}

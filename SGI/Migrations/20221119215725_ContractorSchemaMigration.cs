using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ContractorSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contractors",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    organizationidentityid = table.Column<Guid>(nullable: false),
                    contractorkindid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contractors", x => x.id);
                    table.ForeignKey(
                        name: "fk_contractors_contractorkinds_contractorkindid",
                        column: x => x.contractorkindid,
                        principalTable: "contractorkinds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_contractors_organizationidentities_organizationidentityid",
                        column: x => x.organizationidentityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_contractors_contractorkindid",
                table: "contractors",
                column: "contractorkindid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_contractors_organizationidentityid",
                table: "contractors",
                column: "organizationidentityid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contractors");
        }
    }
}

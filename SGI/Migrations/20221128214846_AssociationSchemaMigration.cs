using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class AssociationSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "associations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    organizationidentityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_associations", x => x.id);
                    table.ForeignKey(
                        name: "fk_associations_organizationidentities_organizationidentityid",
                        column: x => x.organizationidentityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_associations_organizationidentityid",
                table: "associations",
                column: "organizationidentityid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "associations");
        }
    }
}

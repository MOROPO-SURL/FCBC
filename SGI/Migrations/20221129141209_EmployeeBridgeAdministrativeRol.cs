using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class EmployeeBridgeAdministrativeRol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    personidentityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.id);
                    table.ForeignKey(
                        name: "fk_employees_personidentities_personidentityid",
                        column: x => x.personidentityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "administrativerolebridges",
                columns: table => new
                {
                    employeeid = table.Column<Guid>(nullable: false),
                    administrativeroleid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_administrativerolebridges", x => new { x.administrativeroleid, x.employeeid });
                    table.ForeignKey(
                        name: "fk_administrativerolebridges_administrativeroles_administrativ~",
                        column: x => x.administrativeroleid,
                        principalTable: "administrativeroles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_administrativerolebridges_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_administrativerolebridges_employeeid",
                table: "administrativerolebridges",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "ix_employees_personidentityid",
                table: "employees",
                column: "personidentityid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administrativerolebridges");

            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}

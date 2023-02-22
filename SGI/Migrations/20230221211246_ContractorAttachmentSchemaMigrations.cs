using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ContractorAttachmentSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contractorattachments",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    ispublic = table.Column<bool>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    contractorid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contractorattachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_contractorattachments_contractors_contractorid",
                        column: x => x.contractorid,
                        principalTable: "contractors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_contractorattachments_contractorid",
                table: "contractorattachments",
                column: "contractorid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contractorattachments");
        }
    }
}

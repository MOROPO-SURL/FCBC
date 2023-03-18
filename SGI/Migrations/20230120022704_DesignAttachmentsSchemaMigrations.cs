using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class DesignAttachmentsSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "designworkattachments",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    ispublic = table.Column<bool>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    workid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworkattachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_designworkattachments_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_designworkattachments_workid",
                table: "designworkattachments",
                column: "workid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "designworkattachments");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class DesignWorkAwardSchemaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_craftworkaward_awards_awardid",
                table: "craftworkaward");

            migrationBuilder.DropForeignKey(
                name: "fk_craftworkaward_craftworks_workid",
                table: "craftworkaward");

            migrationBuilder.DropPrimaryKey(
                name: "pk_craftworkaward",
                table: "craftworkaward");

            migrationBuilder.RenameTable(
                name: "craftworkaward",
                newName: "craftworkawards");

            migrationBuilder.RenameIndex(
                name: "ix_craftworkaward_workid",
                table: "craftworkawards",
                newName: "ix_craftworkawards_workid");

            migrationBuilder.RenameIndex(
                name: "ix_craftworkaward_awardid",
                table: "craftworkawards",
                newName: "ix_craftworkawards_awardid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_craftworkawards",
                table: "craftworkawards",
                column: "id");

            migrationBuilder.CreateTable(
                name: "designworkawards",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    workid = table.Column<Guid>(nullable: false),
                    awardid = table.Column<Guid>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworkawards", x => x.id);
                    table.ForeignKey(
                        name: "fk_designworkawards_awards_awardid",
                        column: x => x.awardid,
                        principalTable: "awards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworkawards_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_designworkawards_awardid",
                table: "designworkawards",
                column: "awardid");

            migrationBuilder.CreateIndex(
                name: "ix_designworkawards_workid",
                table: "designworkawards",
                column: "workid");

            migrationBuilder.AddForeignKey(
                name: "fk_craftworkawards_awards_awardid",
                table: "craftworkawards",
                column: "awardid",
                principalTable: "awards",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_craftworkawards_craftworks_workid",
                table: "craftworkawards",
                column: "workid",
                principalTable: "craftworks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_craftworkawards_awards_awardid",
                table: "craftworkawards");

            migrationBuilder.DropForeignKey(
                name: "fk_craftworkawards_craftworks_workid",
                table: "craftworkawards");

            migrationBuilder.DropTable(
                name: "designworkawards");

            migrationBuilder.DropPrimaryKey(
                name: "pk_craftworkawards",
                table: "craftworkawards");

            migrationBuilder.RenameTable(
                name: "craftworkawards",
                newName: "craftworkaward");

            migrationBuilder.RenameIndex(
                name: "ix_craftworkawards_workid",
                table: "craftworkaward",
                newName: "ix_craftworkaward_workid");

            migrationBuilder.RenameIndex(
                name: "ix_craftworkawards_awardid",
                table: "craftworkaward",
                newName: "ix_craftworkaward_awardid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_craftworkaward",
                table: "craftworkaward",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_craftworkaward_awards_awardid",
                table: "craftworkaward",
                column: "awardid",
                principalTable: "awards",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_craftworkaward_craftworks_workid",
                table: "craftworkaward",
                column: "workid",
                principalTable: "craftworks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

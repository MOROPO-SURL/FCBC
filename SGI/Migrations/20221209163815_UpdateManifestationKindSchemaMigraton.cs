using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class UpdateManifestationKindSchemaMigraton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_manifestationkinds_manifestationkinds_parentid",
                table: "manifestationkinds");

            migrationBuilder.AlterColumn<Guid>(
                name: "parentid",
                table: "manifestationkinds",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_manifestationkinds_manifestationkinds_parentid",
                table: "manifestationkinds",
                column: "parentid",
                principalTable: "manifestationkinds",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_manifestationkinds_manifestationkinds_parentid",
                table: "manifestationkinds");

            migrationBuilder.AlterColumn<Guid>(
                name: "parentid",
                table: "manifestationkinds",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "fk_manifestationkinds_manifestationkinds_parentid",
                table: "manifestationkinds",
                column: "parentid",
                principalTable: "manifestationkinds",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

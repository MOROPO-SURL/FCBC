using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ChangeAdwardStaffSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_awards_countries_countryid",
                table: "awards");

            migrationBuilder.AlterColumn<Guid>(
                name: "countryid",
                table: "awards",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "awards",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_awards_countries_countryid",
                table: "awards",
                column: "countryid",
                principalSchema: "fcbc-sgi_sapressc_middleware_region",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_awards_countries_countryid",
                table: "awards");

            migrationBuilder.DropColumn(
                name: "description",
                table: "awards");

            migrationBuilder.AlterColumn<Guid>(
                name: "countryid",
                table: "awards",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_awards_countries_countryid",
                table: "awards",
                column: "countryid",
                principalSchema: "fcbc-sgi_sapressc_middleware_region",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

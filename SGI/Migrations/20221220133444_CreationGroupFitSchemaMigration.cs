using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class CreationGroupFitSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "registrationendat",
                table: "creationgroups",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "registrationnumber",
                table: "creationgroups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "resolutionid",
                table: "creationgroups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_creationgroups_resolutionid",
                table: "creationgroups",
                column: "resolutionid");

            migrationBuilder.AddForeignKey(
                name: "fk_creationgroups_resolutions_resolutionid",
                table: "creationgroups",
                column: "resolutionid",
                principalTable: "resolutions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_creationgroups_resolutions_resolutionid",
                table: "creationgroups");

            migrationBuilder.DropIndex(
                name: "ix_creationgroups_resolutionid",
                table: "creationgroups");

            migrationBuilder.DropColumn(
                name: "registrationendat",
                table: "creationgroups");

            migrationBuilder.DropColumn(
                name: "registrationnumber",
                table: "creationgroups");

            migrationBuilder.DropColumn(
                name: "resolutionid",
                table: "creationgroups");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class PrimaryClientRequirementsSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_creationgroups_resolutions_resolutionid",
                table: "creationgroups");

            migrationBuilder.RenameColumn(
                name: "resolutionid",
                table: "creationgroups",
                newName: "agreementid");

            migrationBuilder.RenameIndex(
                name: "ix_creationgroups_resolutionid",
                table: "creationgroups",
                newName: "ix_creationgroups_agreementid");

            migrationBuilder.RenameColumn(
                name: "brandname",
                table: "artists",
                newName: "brand");

            migrationBuilder.AlterColumn<DateTime>(
                name: "registrationendat",
                table: "creationgroups",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "brand",
                table: "creationgroups",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "registratedat",
                table: "creationgroups",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "registrationendat",
                table: "artists",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "registratedat",
                table: "artists",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "agreement",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    number = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    releaseat = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agreement", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_creationgroups_agreement_agreementid",
                table: "creationgroups",
                column: "agreementid",
                principalTable: "agreement",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_creationgroups_agreement_agreementid",
                table: "creationgroups");

            migrationBuilder.DropTable(
                name: "agreement");

            migrationBuilder.DropColumn(
                name: "brand",
                table: "creationgroups");

            migrationBuilder.DropColumn(
                name: "registratedat",
                table: "creationgroups");

            migrationBuilder.DropColumn(
                name: "registratedat",
                table: "artists");

            migrationBuilder.RenameColumn(
                name: "agreementid",
                table: "creationgroups",
                newName: "resolutionid");

            migrationBuilder.RenameIndex(
                name: "ix_creationgroups_agreementid",
                table: "creationgroups",
                newName: "ix_creationgroups_resolutionid");

            migrationBuilder.RenameColumn(
                name: "brand",
                table: "artists",
                newName: "brandname");

            migrationBuilder.AlterColumn<DateTime>(
                name: "registrationendat",
                table: "creationgroups",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "registrationendat",
                table: "artists",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddForeignKey(
                name: "fk_creationgroups_resolutions_resolutionid",
                table: "creationgroups",
                column: "resolutionid",
                principalTable: "resolutions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

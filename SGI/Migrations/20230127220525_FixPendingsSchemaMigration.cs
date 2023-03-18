using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class FixPendingsSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_creationgroups_agreement_agreementid",
                table: "creationgroups");

            migrationBuilder.DropPrimaryKey(
                name: "pk_agreement",
                table: "agreement");

            migrationBuilder.RenameTable(
                name: "agreement",
                newName: "agreements");

            migrationBuilder.AddPrimaryKey(
                name: "pk_agreements",
                table: "agreements",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_creationgroups_agreements_agreementid",
                table: "creationgroups",
                column: "agreementid",
                principalTable: "agreements",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_creationgroups_agreements_agreementid",
                table: "creationgroups");

            migrationBuilder.DropPrimaryKey(
                name: "pk_agreements",
                table: "agreements");

            migrationBuilder.RenameTable(
                name: "agreements",
                newName: "agreement");

            migrationBuilder.AddPrimaryKey(
                name: "pk_agreement",
                table: "agreement",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_creationgroups_agreement_agreementid",
                table: "creationgroups",
                column: "agreementid",
                principalTable: "agreement",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

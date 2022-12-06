using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ContractorOneToManyContractorKindSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_contractors_contractorkindid",
                table: "contractors");

            migrationBuilder.CreateIndex(
                name: "ix_contractors_contractorkindid",
                table: "contractors",
                column: "contractorkindid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_contractors_contractorkindid",
                table: "contractors");

            migrationBuilder.CreateIndex(
                name: "ix_contractors_contractorkindid",
                table: "contractors",
                column: "contractorkindid",
                unique: true);
        }
    }
}

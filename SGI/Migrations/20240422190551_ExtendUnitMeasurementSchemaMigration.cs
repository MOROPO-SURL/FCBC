using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ExtendUnitMeasurementSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "gramsratio",
                table: "unitmeasurements",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "milimetersratio",
                table: "unitmeasurements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gramsratio",
                table: "unitmeasurements");

            migrationBuilder.DropColumn(
                name: "milimetersratio",
                table: "unitmeasurements");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class descriptionNotRequiredMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "technique",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "services",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "contractorkinds",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "technique",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "services",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "contractorkinds",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

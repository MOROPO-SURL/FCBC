using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class UpdateArtWorkAwardSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "about",
                table: "artworkawards",
                newName: "context");

            migrationBuilder.AlterColumn<int>(
                name: "year",
                table: "artworkawards",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "context",
                table: "artworkawards",
                newName: "about");

            migrationBuilder.AlterColumn<int>(
                name: "year",
                table: "artworkawards",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

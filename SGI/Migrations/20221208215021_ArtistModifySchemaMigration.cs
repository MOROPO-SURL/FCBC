using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ArtistModifySchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "citizenid",
                table: "artists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "schoollevel",
                table: "artists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "specificeducation",
                table: "artists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "workplace",
                table: "artists",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "citizenid",
                table: "artists");

            migrationBuilder.DropColumn(
                name: "schoollevel",
                table: "artists");

            migrationBuilder.DropColumn(
                name: "specificeducation",
                table: "artists");

            migrationBuilder.DropColumn(
                name: "workplace",
                table: "artists");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class ArtistAwardSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_artistawards",
                table: "artistawards");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "artistawards",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "context",
                table: "artistawards",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_artistawards",
                table: "artistawards",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_artistawards_artistid",
                table: "artistawards",
                column: "artistid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_artistawards",
                table: "artistawards");

            migrationBuilder.DropIndex(
                name: "ix_artistawards_artistid",
                table: "artistawards");

            migrationBuilder.DropColumn(
                name: "id",
                table: "artistawards");

            migrationBuilder.DropColumn(
                name: "context",
                table: "artistawards");

            migrationBuilder.AddPrimaryKey(
                name: "pk_artistawards",
                table: "artistawards",
                columns: new[] { "artistid", "awardid" });
        }
    }
}

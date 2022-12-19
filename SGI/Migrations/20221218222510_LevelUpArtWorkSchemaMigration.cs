using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class LevelUpArtWorkSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "cost",
                table: "serviceworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "costcurrencyid",
                table: "serviceworks",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "serviceworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pricecurrencyid",
                table: "serviceworks",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "cost",
                table: "designworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "costcurrencyid",
                table: "designworks",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "designworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pricecurrencyid",
                table: "designworks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ispublic",
                table: "creationgroupattachments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "cost",
                table: "craftworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "costcurrencyid",
                table: "craftworks",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "craftworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pricecurrencyid",
                table: "craftworks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "collection",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "cost",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "costcurrencyid",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "height",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "heightunitmeasurementid",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pricecurrencyid",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "profundity",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "profundityunitmeasurementid",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "profunditytunitmeasurementid",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "weight",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "weightunitmeasurementid",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "width",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "widthunitmeasurementid",
                table: "artworks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ispublic",
                table: "artistattachments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "artworkattachments",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    ispublic = table.Column<bool>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    workid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworkattachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_artworkattachments_artworks_workid",
                        column: x => x.workid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artworkawards",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    workid = table.Column<Guid>(nullable: false),
                    awardid = table.Column<Guid>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworkawards", x => x.id);
                    table.ForeignKey(
                        name: "fk_artworkawards_awards_awardid",
                        column: x => x.awardid,
                        principalTable: "awards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworkawards_artworks_workid",
                        column: x => x.workid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artworkcreationgroupbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    creationgroupid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworkcreationgroupbridges", x => new { x.workid, x.creationgroupid });
                    table.ForeignKey(
                        name: "fk_artworkcreationgroupbridges_creationgroups_creationgroupid",
                        column: x => x.creationgroupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworkcreationgroupbridges_artworks_workid",
                        column: x => x.workid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artworkcreatorbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    creatorid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworkcreatorbridges", x => new { x.workid, x.creatorid });
                    table.ForeignKey(
                        name: "fk_artworkcreatorbridges_artists_creatorid",
                        column: x => x.creatorid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworkcreatorbridges_artworks_workid",
                        column: x => x.workid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artworkmaterialbridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    materialid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworkmaterialbridges", x => new { x.workid, x.materialid });
                    table.ForeignKey(
                        name: "fk_artworkmaterialbridges_materials_materialid",
                        column: x => x.materialid,
                        principalTable: "materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworkmaterialbridges_artworks_workid",
                        column: x => x.workid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artworkvaloration",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    workid = table.Column<Guid>(nullable: false),
                    criticid = table.Column<Guid>(nullable: false),
                    declaration = table.Column<string>(nullable: false),
                    declaredat = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworkvaloration", x => x.id);
                    table.ForeignKey(
                        name: "fk_artworkvaloration_critics_criticid",
                        column: x => x.criticid,
                        principalTable: "critics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworkvaloration_artworks_workid",
                        column: x => x.workid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_serviceworks_costcurrencyid",
                table: "serviceworks",
                column: "costcurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworks_pricecurrencyid",
                table: "serviceworks",
                column: "pricecurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_designworks_costcurrencyid",
                table: "designworks",
                column: "costcurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_designworks_pricecurrencyid",
                table: "designworks",
                column: "pricecurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworks_costcurrencyid",
                table: "craftworks",
                column: "costcurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworks_pricecurrencyid",
                table: "craftworks",
                column: "pricecurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_costcurrencyid",
                table: "artworks",
                column: "costcurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_heightunitmeasurementid",
                table: "artworks",
                column: "heightunitmeasurementid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_pricecurrencyid",
                table: "artworks",
                column: "pricecurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_profunditytunitmeasurementid",
                table: "artworks",
                column: "profunditytunitmeasurementid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_weightunitmeasurementid",
                table: "artworks",
                column: "weightunitmeasurementid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_widthunitmeasurementid",
                table: "artworks",
                column: "widthunitmeasurementid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkattachments_workid",
                table: "artworkattachments",
                column: "workid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkawards_awardid",
                table: "artworkawards",
                column: "awardid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkawards_workid",
                table: "artworkawards",
                column: "workid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkcreationgroupbridges_creationgroupid",
                table: "artworkcreationgroupbridges",
                column: "creationgroupid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkcreatorbridges_creatorid",
                table: "artworkcreatorbridges",
                column: "creatorid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkmaterialbridges_materialid",
                table: "artworkmaterialbridges",
                column: "materialid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkvaloration_criticid",
                table: "artworkvaloration",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkvaloration_workid",
                table: "artworkvaloration",
                column: "workid");

            migrationBuilder.AddForeignKey(
                name: "fk_artworks_currencies_costcurrencyid",
                table: "artworks",
                column: "costcurrencyid",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_artworks_unitmeasurements_heightunitmeasurementid",
                table: "artworks",
                column: "heightunitmeasurementid",
                principalTable: "unitmeasurements",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_artworks_currencies_pricecurrencyid",
                table: "artworks",
                column: "pricecurrencyid",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_artworks_unitmeasurements_profunditytunitmeasurementid",
                table: "artworks",
                column: "profunditytunitmeasurementid",
                principalTable: "unitmeasurements",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_artworks_unitmeasurements_weightunitmeasurementid",
                table: "artworks",
                column: "weightunitmeasurementid",
                principalTable: "unitmeasurements",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_artworks_unitmeasurements_widthunitmeasurementid",
                table: "artworks",
                column: "widthunitmeasurementid",
                principalTable: "unitmeasurements",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_craftworks_currencies_costcurrencyid",
                table: "craftworks",
                column: "costcurrencyid",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_craftworks_currencies_pricecurrencyid",
                table: "craftworks",
                column: "pricecurrencyid",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_designworks_currencies_costcurrencyid",
                table: "designworks",
                column: "costcurrencyid",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_designworks_currencies_pricecurrencyid",
                table: "designworks",
                column: "pricecurrencyid",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_serviceworks_currencies_costcurrencyid",
                table: "serviceworks",
                column: "costcurrencyid",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_serviceworks_currencies_pricecurrencyid",
                table: "serviceworks",
                column: "pricecurrencyid",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_artworks_currencies_costcurrencyid",
                table: "artworks");

            migrationBuilder.DropForeignKey(
                name: "fk_artworks_unitmeasurements_heightunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropForeignKey(
                name: "fk_artworks_currencies_pricecurrencyid",
                table: "artworks");

            migrationBuilder.DropForeignKey(
                name: "fk_artworks_unitmeasurements_profunditytunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropForeignKey(
                name: "fk_artworks_unitmeasurements_weightunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropForeignKey(
                name: "fk_artworks_unitmeasurements_widthunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropForeignKey(
                name: "fk_craftworks_currencies_costcurrencyid",
                table: "craftworks");

            migrationBuilder.DropForeignKey(
                name: "fk_craftworks_currencies_pricecurrencyid",
                table: "craftworks");

            migrationBuilder.DropForeignKey(
                name: "fk_designworks_currencies_costcurrencyid",
                table: "designworks");

            migrationBuilder.DropForeignKey(
                name: "fk_designworks_currencies_pricecurrencyid",
                table: "designworks");

            migrationBuilder.DropForeignKey(
                name: "fk_serviceworks_currencies_costcurrencyid",
                table: "serviceworks");

            migrationBuilder.DropForeignKey(
                name: "fk_serviceworks_currencies_pricecurrencyid",
                table: "serviceworks");

            migrationBuilder.DropTable(
                name: "artworkattachments");

            migrationBuilder.DropTable(
                name: "artworkawards");

            migrationBuilder.DropTable(
                name: "artworkcreationgroupbridges");

            migrationBuilder.DropTable(
                name: "artworkcreatorbridges");

            migrationBuilder.DropTable(
                name: "artworkmaterialbridges");

            migrationBuilder.DropTable(
                name: "artworkvaloration");

            migrationBuilder.DropIndex(
                name: "ix_serviceworks_costcurrencyid",
                table: "serviceworks");

            migrationBuilder.DropIndex(
                name: "ix_serviceworks_pricecurrencyid",
                table: "serviceworks");

            migrationBuilder.DropIndex(
                name: "ix_designworks_costcurrencyid",
                table: "designworks");

            migrationBuilder.DropIndex(
                name: "ix_designworks_pricecurrencyid",
                table: "designworks");

            migrationBuilder.DropIndex(
                name: "ix_craftworks_costcurrencyid",
                table: "craftworks");

            migrationBuilder.DropIndex(
                name: "ix_craftworks_pricecurrencyid",
                table: "craftworks");

            migrationBuilder.DropIndex(
                name: "ix_artworks_costcurrencyid",
                table: "artworks");

            migrationBuilder.DropIndex(
                name: "ix_artworks_heightunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropIndex(
                name: "ix_artworks_pricecurrencyid",
                table: "artworks");

            migrationBuilder.DropIndex(
                name: "ix_artworks_profunditytunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropIndex(
                name: "ix_artworks_weightunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropIndex(
                name: "ix_artworks_widthunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "cost",
                table: "serviceworks");

            migrationBuilder.DropColumn(
                name: "costcurrencyid",
                table: "serviceworks");

            migrationBuilder.DropColumn(
                name: "price",
                table: "serviceworks");

            migrationBuilder.DropColumn(
                name: "pricecurrencyid",
                table: "serviceworks");

            migrationBuilder.DropColumn(
                name: "cost",
                table: "designworks");

            migrationBuilder.DropColumn(
                name: "costcurrencyid",
                table: "designworks");

            migrationBuilder.DropColumn(
                name: "price",
                table: "designworks");

            migrationBuilder.DropColumn(
                name: "pricecurrencyid",
                table: "designworks");

            migrationBuilder.DropColumn(
                name: "ispublic",
                table: "creationgroupattachments");

            migrationBuilder.DropColumn(
                name: "cost",
                table: "craftworks");

            migrationBuilder.DropColumn(
                name: "costcurrencyid",
                table: "craftworks");

            migrationBuilder.DropColumn(
                name: "price",
                table: "craftworks");

            migrationBuilder.DropColumn(
                name: "pricecurrencyid",
                table: "craftworks");

            migrationBuilder.DropColumn(
                name: "collection",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "cost",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "costcurrencyid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "height",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "heightunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "location",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "price",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "pricecurrencyid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "profundity",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "profundityunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "profunditytunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "weightunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "width",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "widthunitmeasurementid",
                table: "artworks");

            migrationBuilder.DropColumn(
                name: "ispublic",
                table: "artistattachments");
        }
    }
}

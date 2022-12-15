using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class RedefinitionSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_administrativerolebridges_administrativeroles_administrativ~",
                table: "administrativerolebridges");

            migrationBuilder.DropForeignKey(
                name: "fk_artisticmanifestations_manifestationkinds_parentid",
                table: "artisticmanifestations");

            migrationBuilder.DropForeignKey(
                name: "fk_artists_personidentities_personidentityid",
                table: "artists");

            migrationBuilder.DropForeignKey(
                name: "fk_associations_organizationidentities_organizationidentityid",
                table: "associations");

            migrationBuilder.DropForeignKey(
                name: "fk_contractors_organizationidentities_organizationidentityid",
                table: "contractors");

            migrationBuilder.DropForeignKey(
                name: "fk_creationgroups_organizationidentities_organizationidentityid",
                table: "creationgroups");

            migrationBuilder.DropForeignKey(
                name: "fk_critics_personidentities_personidentityid",
                table: "critics");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_personidentities_personidentityid",
                table: "employees");

            migrationBuilder.DropTable(
                name: "artistmanifestationbridges");

            migrationBuilder.DropTable(
                name: "creationgroupartisticmanifestationbrigdes");

            migrationBuilder.DropTable(
                name: "manifestationkinds");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropIndex(
                name: "ix_artisticmanifestations_parentid",
                table: "artisticmanifestations");

            migrationBuilder.DropColumn(
                name: "numberresolution",
                table: "resolutions");

            migrationBuilder.DropColumn(
                name: "selectedvalues",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "selectedvalues",
                table: "creationgroups");

            migrationBuilder.DropColumn(
                name: "admitionmodel",
                table: "artists");

            migrationBuilder.DropColumn(
                name: "parentid",
                table: "artisticmanifestations");

            migrationBuilder.RenameColumn(
                name: "resolutiondate",
                table: "resolutions",
                newName: "releaseat");

            migrationBuilder.RenameColumn(
                name: "personidentityid",
                table: "employees",
                newName: "identityid");

            migrationBuilder.RenameIndex(
                name: "ix_employees_personidentityid",
                table: "employees",
                newName: "ix_employees_identityid");

            migrationBuilder.RenameColumn(
                name: "personidentityid",
                table: "critics",
                newName: "identityid");

            migrationBuilder.RenameIndex(
                name: "ix_critics_personidentityid",
                table: "critics",
                newName: "ix_critics_identityid");

            migrationBuilder.RenameColumn(
                name: "organizationidentityid",
                table: "creationgroups",
                newName: "identityid");

            migrationBuilder.RenameIndex(
                name: "ix_creationgroups_organizationidentityid",
                table: "creationgroups",
                newName: "ix_creationgroups_identityid");

            migrationBuilder.RenameColumn(
                name: "organizationidentityid",
                table: "contractors",
                newName: "identityid");

            migrationBuilder.RenameIndex(
                name: "ix_contractors_organizationidentityid",
                table: "contractors",
                newName: "ix_contractors_identityid");

            migrationBuilder.RenameColumn(
                name: "organizationidentityid",
                table: "associations",
                newName: "identityid");

            migrationBuilder.RenameIndex(
                name: "ix_associations_organizationidentityid",
                table: "associations",
                newName: "ix_associations_identityid");

            migrationBuilder.RenameColumn(
                name: "personidentityid",
                table: "artists",
                newName: "identityid");

            migrationBuilder.RenameColumn(
                name: "context",
                table: "artists",
                newName: "brandname");

            migrationBuilder.RenameIndex(
                name: "ix_artists_personidentityid",
                table: "artists",
                newName: "ix_artists_identityid");

            migrationBuilder.RenameColumn(
                name: "administrativeroleid",
                table: "administrativerolebridges",
                newName: "roleid");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "resolutions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "number",
                table: "resolutions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "citizenid",
                table: "artists",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "admitionmode",
                table: "artists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "admitionmodecontext",
                table: "artists",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "brandimageid",
                table: "artists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "artisticmanifestations",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "groupid",
                table: "artisticmanifestations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "artistartisticmanifestationbridges",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    manifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistartisticmanifestationbridges", x => new { x.artistid, x.manifestationid });
                    table.ForeignKey(
                        name: "fk_artistartisticmanifestationbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistartisticmanifestationbridges_artisticmanifestations_m~",
                        column: x => x.manifestationid,
                        principalTable: "artisticmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artistassociationlocks",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    associationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistassociationlocks", x => new { x.artistid, x.associationid });
                    table.ForeignKey(
                        name: "fk_artistassociationlocks_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistassociationlocks_associations_associationid",
                        column: x => x.associationid,
                        principalTable: "associations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "artistattachments",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    artistid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistattachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_artistattachments_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artisticmanifestationgroups",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    parentid = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artisticmanifestationgroups", x => x.id);
                    table.ForeignKey(
                        name: "fk_artisticmanifestationgroups_artisticmanifestationgroups_par~",
                        column: x => x.parentid,
                        principalTable: "artisticmanifestationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "artistresolutionlocks",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    resolutionid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistresolutionlocks", x => new { x.artistid, x.resolutionid });
                    table.ForeignKey(
                        name: "fk_artistresolutionlocks_artists_resolutionid",
                        column: x => x.resolutionid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistresolutionlocks_resolutions_resolutionid",
                        column: x => x.resolutionid,
                        principalTable: "resolutions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "artworks",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    bornat = table.Column<DateTime>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    manifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworks", x => x.id);
                    table.ForeignKey(
                        name: "fk_artworks_artisticmanifestations_manifestationid",
                        column: x => x.manifestationid,
                        principalTable: "artisticmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "craftmanifestationgroups",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    parentid = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftmanifestationgroups", x => x.id);
                    table.ForeignKey(
                        name: "fk_craftmanifestationgroups_craftmanifestationgroups_parentid",
                        column: x => x.parentid,
                        principalTable: "craftmanifestationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "creationgroupartisticmanifestationbridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    manifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupartisticmanifestationbridges", x => new { x.groupid, x.manifestationid });
                    table.ForeignKey(
                        name: "fk_creationgroupartisticmanifestationbridges_creationgroups_gr~",
                        column: x => x.groupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupartisticmanifestationbridges_artisticmanifesta~",
                        column: x => x.manifestationid,
                        principalTable: "artisticmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "creationgroupattachments",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    groupid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupattachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_creationgroupattachments_creationgroups_groupid",
                        column: x => x.groupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designmanifestationgroups",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    parentid = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designmanifestationgroups", x => x.id);
                    table.ForeignKey(
                        name: "fk_designmanifestationgroups_designmanifestationgroups_parentid",
                        column: x => x.parentid,
                        principalTable: "designmanifestationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "institutionalservices",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institutionalservices", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "servicelinegroups",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    parentid = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servicelinegroups", x => x.id);
                    table.ForeignKey(
                        name: "fk_servicelinegroups_servicelinegroups_parentid",
                        column: x => x.parentid,
                        principalTable: "servicelinegroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "craftmanifestations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    groupid = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftmanifestations", x => x.id);
                    table.ForeignKey(
                        name: "fk_craftmanifestations_craftmanifestationgroups_groupid",
                        column: x => x.groupid,
                        principalTable: "craftmanifestationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "designmanifestations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    groupid = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designmanifestations", x => x.id);
                    table.ForeignKey(
                        name: "fk_designmanifestations_designmanifestationgroups_groupid",
                        column: x => x.groupid,
                        principalTable: "designmanifestationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "servicelines",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    groupid = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servicelines", x => x.id);
                    table.ForeignKey(
                        name: "fk_servicelines_servicelinegroups_groupid",
                        column: x => x.groupid,
                        principalTable: "servicelinegroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "artistcraftmanifestationbridges",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    manifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistcraftmanifestationbridges", x => new { x.artistid, x.manifestationid });
                    table.ForeignKey(
                        name: "fk_artistcraftmanifestationbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistcraftmanifestationbridges_craftmanifestations_manifes~",
                        column: x => x.manifestationid,
                        principalTable: "craftmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "craftworks",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    bornat = table.Column<DateTime>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    manifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworks", x => x.id);
                    table.ForeignKey(
                        name: "fk_craftworks_craftmanifestations_manifestationid",
                        column: x => x.manifestationid,
                        principalTable: "craftmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "creationgroupcraftmanifestationbridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    manifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupcraftmanifestationbridges", x => new { x.groupid, x.manifestationid });
                    table.ForeignKey(
                        name: "fk_creationgroupcraftmanifestationbridges_creationgroups_group~",
                        column: x => x.groupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupcraftmanifestationbridges_craftmanifestations_~",
                        column: x => x.manifestationid,
                        principalTable: "craftmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artistdesignmanifestationbridges",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    manifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistdesignmanifestationbridges", x => new { x.artistid, x.manifestationid });
                    table.ForeignKey(
                        name: "fk_artistdesignmanifestationbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistdesignmanifestationbridges_designmanifestations_manif~",
                        column: x => x.manifestationid,
                        principalTable: "designmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "creationgroupdesignmanifestationbridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    manifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupdesignmanifestationbridges", x => new { x.groupid, x.manifestationid });
                    table.ForeignKey(
                        name: "fk_creationgroupdesignmanifestationbridges_creationgroups_grou~",
                        column: x => x.groupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupdesignmanifestationbridges_designmanifestation~",
                        column: x => x.manifestationid,
                        principalTable: "designmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designworks",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    bornat = table.Column<DateTime>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    manifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworks", x => x.id);
                    table.ForeignKey(
                        name: "fk_designworks_designmanifestations_manifestationid",
                        column: x => x.manifestationid,
                        principalTable: "designmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artistservicelinebridges",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    lineid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistservicelinebridges", x => new { x.artistid, x.lineid });
                    table.ForeignKey(
                        name: "fk_artistservicelinebridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistservicelinebridges_servicelines_lineid",
                        column: x => x.lineid,
                        principalTable: "servicelines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "creationgroupservicelinebridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    lineid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupservicelinebridges", x => new { x.groupid, x.lineid });
                    table.ForeignKey(
                        name: "fk_creationgroupservicelinebridges_creationgroups_groupid",
                        column: x => x.groupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupservicelinebridges_servicelines_lineid",
                        column: x => x.lineid,
                        principalTable: "servicelines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceworks",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    bornat = table.Column<DateTime>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    lineid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworks", x => x.id);
                    table.ForeignKey(
                        name: "fk_serviceworks_servicelines_lineid",
                        column: x => x.lineid,
                        principalTable: "servicelines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artists_brandimageid",
                table: "artists",
                column: "brandimageid");

            migrationBuilder.CreateIndex(
                name: "ix_artisticmanifestations_groupid",
                table: "artisticmanifestations",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_artistartisticmanifestationbridges_manifestationid",
                table: "artistartisticmanifestationbridges",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_artistassociationlocks_associationid",
                table: "artistassociationlocks",
                column: "associationid");

            migrationBuilder.CreateIndex(
                name: "ix_artistattachments_artistid",
                table: "artistattachments",
                column: "artistid");

            migrationBuilder.CreateIndex(
                name: "ix_artistcraftmanifestationbridges_manifestationid",
                table: "artistcraftmanifestationbridges",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_artistdesignmanifestationbridges_manifestationid",
                table: "artistdesignmanifestationbridges",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_artisticmanifestationgroups_parentid",
                table: "artisticmanifestationgroups",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_artistresolutionlocks_resolutionid",
                table: "artistresolutionlocks",
                column: "resolutionid");

            migrationBuilder.CreateIndex(
                name: "ix_artistservicelinebridges_lineid",
                table: "artistservicelinebridges",
                column: "lineid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_manifestationid",
                table: "artworks",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_craftmanifestationgroups_parentid",
                table: "craftmanifestationgroups",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_craftmanifestations_groupid",
                table: "craftmanifestations",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworks_manifestationid",
                table: "craftworks",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupartisticmanifestationbridges_manifestationid",
                table: "creationgroupartisticmanifestationbridges",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupattachments_groupid",
                table: "creationgroupattachments",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupcraftmanifestationbridges_manifestationid",
                table: "creationgroupcraftmanifestationbridges",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupdesignmanifestationbridges_manifestationid",
                table: "creationgroupdesignmanifestationbridges",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupservicelinebridges_lineid",
                table: "creationgroupservicelinebridges",
                column: "lineid");

            migrationBuilder.CreateIndex(
                name: "ix_designmanifestationgroups_parentid",
                table: "designmanifestationgroups",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_designmanifestations_groupid",
                table: "designmanifestations",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_designworks_manifestationid",
                table: "designworks",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_servicelinegroups_parentid",
                table: "servicelinegroups",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_servicelines_groupid",
                table: "servicelines",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworks_lineid",
                table: "serviceworks",
                column: "lineid");

            migrationBuilder.AddForeignKey(
                name: "fk_administrativerolebridges_administrativeroles_roleid",
                table: "administrativerolebridges",
                column: "roleid",
                principalTable: "administrativeroles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_artisticmanifestations_artisticmanifestationgroups_groupid",
                table: "artisticmanifestations",
                column: "groupid",
                principalTable: "artisticmanifestationgroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_artists_images_brandimageid",
                table: "artists",
                column: "brandimageid",
                principalSchema: "fcbc-sgi_sapressc_middleware_media",
                principalTable: "images",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_artists_personidentities_identityid",
                table: "artists",
                column: "identityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "personidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_associations_organizationidentities_identityid",
                table: "associations",
                column: "identityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "organizationidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_contractors_organizationidentities_identityid",
                table: "contractors",
                column: "identityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "organizationidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_creationgroups_organizationidentities_identityid",
                table: "creationgroups",
                column: "identityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "organizationidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_critics_personidentities_identityid",
                table: "critics",
                column: "identityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "personidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_personidentities_identityid",
                table: "employees",
                column: "identityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "personidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_administrativerolebridges_administrativeroles_roleid",
                table: "administrativerolebridges");

            migrationBuilder.DropForeignKey(
                name: "fk_artisticmanifestations_artisticmanifestationgroups_groupid",
                table: "artisticmanifestations");

            migrationBuilder.DropForeignKey(
                name: "fk_artists_images_brandimageid",
                table: "artists");

            migrationBuilder.DropForeignKey(
                name: "fk_artists_personidentities_identityid",
                table: "artists");

            migrationBuilder.DropForeignKey(
                name: "fk_associations_organizationidentities_identityid",
                table: "associations");

            migrationBuilder.DropForeignKey(
                name: "fk_contractors_organizationidentities_identityid",
                table: "contractors");

            migrationBuilder.DropForeignKey(
                name: "fk_creationgroups_organizationidentities_identityid",
                table: "creationgroups");

            migrationBuilder.DropForeignKey(
                name: "fk_critics_personidentities_identityid",
                table: "critics");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_personidentities_identityid",
                table: "employees");

            migrationBuilder.DropTable(
                name: "artistartisticmanifestationbridges");

            migrationBuilder.DropTable(
                name: "artistassociationlocks");

            migrationBuilder.DropTable(
                name: "artistattachments");

            migrationBuilder.DropTable(
                name: "artistcraftmanifestationbridges");

            migrationBuilder.DropTable(
                name: "artistdesignmanifestationbridges");

            migrationBuilder.DropTable(
                name: "artisticmanifestationgroups");

            migrationBuilder.DropTable(
                name: "artistresolutionlocks");

            migrationBuilder.DropTable(
                name: "artistservicelinebridges");

            migrationBuilder.DropTable(
                name: "artworks");

            migrationBuilder.DropTable(
                name: "craftworks");

            migrationBuilder.DropTable(
                name: "creationgroupartisticmanifestationbridges");

            migrationBuilder.DropTable(
                name: "creationgroupattachments");

            migrationBuilder.DropTable(
                name: "creationgroupcraftmanifestationbridges");

            migrationBuilder.DropTable(
                name: "creationgroupdesignmanifestationbridges");

            migrationBuilder.DropTable(
                name: "creationgroupservicelinebridges");

            migrationBuilder.DropTable(
                name: "designworks");

            migrationBuilder.DropTable(
                name: "institutionalservices");

            migrationBuilder.DropTable(
                name: "serviceworks");

            migrationBuilder.DropTable(
                name: "craftmanifestations");

            migrationBuilder.DropTable(
                name: "designmanifestations");

            migrationBuilder.DropTable(
                name: "servicelines");

            migrationBuilder.DropTable(
                name: "craftmanifestationgroups");

            migrationBuilder.DropTable(
                name: "designmanifestationgroups");

            migrationBuilder.DropTable(
                name: "servicelinegroups");

            migrationBuilder.DropIndex(
                name: "ix_artists_brandimageid",
                table: "artists");

            migrationBuilder.DropIndex(
                name: "ix_artisticmanifestations_groupid",
                table: "artisticmanifestations");

            migrationBuilder.DropColumn(
                name: "number",
                table: "resolutions");

            migrationBuilder.DropColumn(
                name: "admitionmode",
                table: "artists");

            migrationBuilder.DropColumn(
                name: "admitionmodecontext",
                table: "artists");

            migrationBuilder.DropColumn(
                name: "brandimageid",
                table: "artists");

            migrationBuilder.DropColumn(
                name: "description",
                table: "artisticmanifestations");

            migrationBuilder.DropColumn(
                name: "groupid",
                table: "artisticmanifestations");

            migrationBuilder.RenameColumn(
                name: "releaseat",
                table: "resolutions",
                newName: "resolutiondate");

            migrationBuilder.RenameColumn(
                name: "identityid",
                table: "employees",
                newName: "personidentityid");

            migrationBuilder.RenameIndex(
                name: "ix_employees_identityid",
                table: "employees",
                newName: "ix_employees_personidentityid");

            migrationBuilder.RenameColumn(
                name: "identityid",
                table: "critics",
                newName: "personidentityid");

            migrationBuilder.RenameIndex(
                name: "ix_critics_identityid",
                table: "critics",
                newName: "ix_critics_personidentityid");

            migrationBuilder.RenameColumn(
                name: "identityid",
                table: "creationgroups",
                newName: "organizationidentityid");

            migrationBuilder.RenameIndex(
                name: "ix_creationgroups_identityid",
                table: "creationgroups",
                newName: "ix_creationgroups_organizationidentityid");

            migrationBuilder.RenameColumn(
                name: "identityid",
                table: "contractors",
                newName: "organizationidentityid");

            migrationBuilder.RenameIndex(
                name: "ix_contractors_identityid",
                table: "contractors",
                newName: "ix_contractors_organizationidentityid");

            migrationBuilder.RenameColumn(
                name: "identityid",
                table: "associations",
                newName: "organizationidentityid");

            migrationBuilder.RenameIndex(
                name: "ix_associations_identityid",
                table: "associations",
                newName: "ix_associations_organizationidentityid");

            migrationBuilder.RenameColumn(
                name: "identityid",
                table: "artists",
                newName: "personidentityid");

            migrationBuilder.RenameColumn(
                name: "brandname",
                table: "artists",
                newName: "context");

            migrationBuilder.RenameIndex(
                name: "ix_artists_identityid",
                table: "artists",
                newName: "ix_artists_personidentityid");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "administrativerolebridges",
                newName: "administrativeroleid");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "resolutions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "numberresolution",
                table: "resolutions",
                nullable: true);

            migrationBuilder.AddColumn<Guid[]>(
                name: "selectedvalues",
                table: "employees",
                nullable: true);

            migrationBuilder.AddColumn<Guid[]>(
                name: "selectedvalues",
                table: "creationgroups",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "citizenid",
                table: "artists",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "admitionmodel",
                table: "artists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "parentid",
                table: "artisticmanifestations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "artistmanifestationbridges",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    artisticmanifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistmanifestationbridges", x => new { x.artistid, x.artisticmanifestationid });
                    table.ForeignKey(
                        name: "fk_artistmanifestationbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistmanifestationbridges_artisticmanifestations_artisticm~",
                        column: x => x.artisticmanifestationid,
                        principalTable: "artisticmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "creationgroupartisticmanifestationbrigdes",
                columns: table => new
                {
                    creationgroupid = table.Column<Guid>(nullable: false),
                    artisticmanifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupartisticmanifestationbrigdes", x => new { x.creationgroupid, x.artisticmanifestationid });
                    table.ForeignKey(
                        name: "fk_creationgroupartisticmanifestationbrigdes_artisticmanifesta~",
                        column: x => x.artisticmanifestationid,
                        principalTable: "artisticmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupartisticmanifestationbrigdes_creationgroups_cr~",
                        column: x => x.creationgroupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "manifestationkinds",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    parentid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_manifestationkinds", x => x.id);
                    table.ForeignKey(
                        name: "fk_manifestationkinds_manifestationkinds_parentid",
                        column: x => x.parentid,
                        principalTable: "manifestationkinds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_services", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_artisticmanifestations_parentid",
                table: "artisticmanifestations",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_artistmanifestationbridges_artisticmanifestationid",
                table: "artistmanifestationbridges",
                column: "artisticmanifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupartisticmanifestationbrigdes_artisticmanifesta~",
                table: "creationgroupartisticmanifestationbrigdes",
                column: "artisticmanifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_manifestationkinds_parentid",
                table: "manifestationkinds",
                column: "parentid");

            migrationBuilder.AddForeignKey(
                name: "fk_administrativerolebridges_administrativeroles_administrativ~",
                table: "administrativerolebridges",
                column: "administrativeroleid",
                principalTable: "administrativeroles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_artisticmanifestations_manifestationkinds_parentid",
                table: "artisticmanifestations",
                column: "parentid",
                principalTable: "manifestationkinds",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_artists_personidentities_personidentityid",
                table: "artists",
                column: "personidentityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "personidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_associations_organizationidentities_organizationidentityid",
                table: "associations",
                column: "organizationidentityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "organizationidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_contractors_organizationidentities_organizationidentityid",
                table: "contractors",
                column: "organizationidentityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "organizationidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_creationgroups_organizationidentities_organizationidentityid",
                table: "creationgroups",
                column: "organizationidentityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "organizationidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_critics_personidentities_personidentityid",
                table: "critics",
                column: "personidentityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "personidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_personidentities_personidentityid",
                table: "employees",
                column: "personidentityid",
                principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                principalTable: "personidentities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class InitialSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fcbc-sgi_sapressc_core_securitylocal");

            migrationBuilder.EnsureSchema(
                name: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.EnsureSchema(
                name: "fcbc-sgi_sapressc_middleware_media");

            migrationBuilder.EnsureSchema(
                name: "fcbc-sgi_sapressc_middleware_region");

            migrationBuilder.CreateTable(
                name: "administrativeroles",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_administrativeroles", x => x.id);
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
                name: "contractorkinds",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contractorkinds", x => x.id);
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
                name: "currencies",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: false),
                    symbol = table.Column<string>(nullable: true),
                    isspacedsymbol = table.Column<bool>(nullable: false),
                    position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currencies", x => x.id);
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
                name: "distinctions",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_distinctions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "institutionalinformations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institutionalinformations", x => x.id);
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
                name: "recognitions",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recognitions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "resolutions",
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
                    table.PrimaryKey("pk_resolutions", x => x.id);
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
                name: "technique",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_technique", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "thematics",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_thematics", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "traces",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    type = table.Column<int>(nullable: false),
                    author = table.Column<string>(nullable: true),
                    registered = table.Column<DateTime>(nullable: false),
                    entityname = table.Column<string>(nullable: false),
                    beforedata = table.Column<string>(nullable: false),
                    afterdata = table.Column<string>(nullable: true),
                    nextid = table.Column<long>(nullable: false),
                    signature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_traces", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "groups",
                schema: "fcbc-sgi_sapressc_core_securitylocal",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "fcbc-sgi_sapressc_core_securitylocal",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "fcbc-sgi_sapressc_core_securitylocal",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    enabled = table.Column<bool>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    passwordchangerequired = table.Column<bool>(nullable: false),
                    hasfullaccess = table.Column<bool>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    lastsessiondate = table.Column<DateTime>(nullable: false),
                    failauthenticationcount = table.Column<int>(nullable: false),
                    signature = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "emailcontactitems",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    emailaddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_emailcontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phonecontactitems",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    phonenumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_phonecontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "socialcontactitems",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    socialid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_socialcontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "websitecontactitems",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_websitecontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "thumbnails",
                schema: "fcbc-sgi_sapressc_middleware_media",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    about = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_thumbnails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                schema: "fcbc-sgi_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    isocode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "artisticmanifestations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    groupid = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artisticmanifestations", x => x.id);
                    table.ForeignKey(
                        name: "fk_artisticmanifestations_artisticmanifestationgroups_groupid",
                        column: x => x.groupid,
                        principalTable: "artisticmanifestationgroups",
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
                name: "grouprolebridges",
                schema: "fcbc-sgi_sapressc_core_securitylocal",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    roleid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grouprolebridges", x => new { x.groupid, x.roleid });
                    table.ForeignKey(
                        name: "fk_grouprolebridges_groups_groupid",
                        column: x => x.groupid,
                        principalSchema: "fcbc-sgi_sapressc_core_securitylocal",
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_grouprolebridges_roles_roleid",
                        column: x => x.roleid,
                        principalSchema: "fcbc-sgi_sapressc_core_securitylocal",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usergroupbridges",
                schema: "fcbc-sgi_sapressc_core_securitylocal",
                columns: table => new
                {
                    userid = table.Column<string>(nullable: false),
                    groupid = table.Column<Guid>(nullable: false),
                    signature = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usergroupbridges", x => new { x.userid, x.groupid });
                    table.ForeignKey(
                        name: "fk_usergroupbridges_groups_groupid",
                        column: x => x.groupid,
                        principalSchema: "fcbc-sgi_sapressc_core_securitylocal",
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usergroupbridges_users_userid",
                        column: x => x.userid,
                        principalSchema: "fcbc-sgi_sapressc_core_securitylocal",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images",
                schema: "fcbc-sgi_sapressc_middleware_media",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createat = table.Column<DateTime>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    about = table.Column<string>(maxLength: 255, nullable: true),
                    height = table.Column<int>(nullable: false),
                    width = table.Column<int>(nullable: false),
                    thumbnailid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_images_thumbnails_thumbnailid",
                        column: x => x.thumbnailid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "thumbnails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videos",
                schema: "fcbc-sgi_sapressc_middleware_media",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createat = table.Column<DateTime>(nullable: false),
                    extension = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    about = table.Column<string>(maxLength: 255, nullable: true),
                    duration = table.Column<TimeSpan>(nullable: false),
                    format = table.Column<string>(nullable: false),
                    framesize = table.Column<string>(nullable: false),
                    colormodel = table.Column<string>(nullable: false),
                    fps = table.Column<double>(nullable: false),
                    audioformat = table.Column<string>(nullable: false),
                    audiochanneloutput = table.Column<string>(nullable: false),
                    thumbnailid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_videos", x => x.id);
                    table.ForeignKey(
                        name: "fk_videos_thumbnails_thumbnailid",
                        column: x => x.thumbnailid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "thumbnails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "awards",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    countryid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_awards", x => x.id);
                    table.ForeignKey(
                        name: "fk_awards_countries_countryid",
                        column: x => x.countryid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_region",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "states",
                schema: "fcbc-sgi_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    countryid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_states", x => x.id);
                    table.ForeignKey(
                        name: "fk_states_countries_countryid",
                        column: x => x.countryid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_region",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "organizationidentities",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    borndate = table.Column<DateTime>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    ownerid = table.Column<Guid>(nullable: true),
                    imageid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationidentities", x => x.id);
                    table.ForeignKey(
                        name: "fk_organizationidentities_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_organizationidentities_organizationidentities_ownerid",
                        column: x => x.ownerid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personidentities",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    borndate = table.Column<DateTime>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    lastname = table.Column<string>(nullable: true),
                    alias = table.Column<string>(nullable: true),
                    sex = table.Column<int>(nullable: false),
                    civilstatus = table.Column<int>(nullable: false),
                    imageid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personidentities", x => x.id);
                    table.ForeignKey(
                        name: "fk_personidentities_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "municipalities",
                schema: "fcbc-sgi_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    stateid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_municipalities", x => x.id);
                    table.ForeignKey(
                        name: "fk_municipalities_states_stateid",
                        column: x => x.stateid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_region",
                        principalTable: "states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "associations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    identityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_associations", x => x.id);
                    table.ForeignKey(
                        name: "fk_associations_organizationidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contractors",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    identityid = table.Column<Guid>(nullable: false),
                    contractorkindid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contractors", x => x.id);
                    table.ForeignKey(
                        name: "fk_contractors_contractorkinds_contractorkindid",
                        column: x => x.contractorkindid,
                        principalTable: "contractorkinds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_contractors_organizationidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "creationgroups",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    identityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroups", x => x.id);
                    table.ForeignKey(
                        name: "fk_creationgroups_organizationidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationemailcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationemailcontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationemailcontactitembridges_emailcontactitems_conta~",
                        column: x => x.contactitemid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "emailcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationemailcontactitembridges_organizationidentities_~",
                        column: x => x.organizationid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationphonecontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationphonecontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationphonecontactitembridges_phonecontactitems_conta~",
                        column: x => x.contactitemid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "phonecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationphonecontactitembridges_organizationidentities_~",
                        column: x => x.organizationid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationsocialcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationsocialcontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationsocialcontactitembridges_socialcontactitems_con~",
                        column: x => x.contactitemid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "socialcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationsocialcontactitembridges_organizationidentities~",
                        column: x => x.organizationid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationwebsitecontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationwebsitecontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationwebsitecontactitembridges_websitecontactitems_c~",
                        column: x => x.contactitemid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "websitecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationwebsitecontactitembridges_organizationidentitie~",
                        column: x => x.organizationid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artists",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    identityid = table.Column<Guid>(nullable: false),
                    citizenid = table.Column<string>(nullable: false),
                    brandname = table.Column<string>(nullable: true),
                    brandimageid = table.Column<Guid>(nullable: true),
                    workplace = table.Column<int>(nullable: false),
                    schoollevel = table.Column<int>(nullable: false),
                    specificeducation = table.Column<string>(nullable: true),
                    admitionmode = table.Column<int>(nullable: false),
                    admitionmodecontext = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artists", x => x.id);
                    table.ForeignKey(
                        name: "fk_artists_images_brandimageid",
                        column: x => x.brandimageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_artists_personidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "critics",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    identityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_critics", x => x.id);
                    table.ForeignKey(
                        name: "fk_critics_personidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    identityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.id);
                    table.ForeignKey(
                        name: "fk_employees_personidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "memberships",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    organizationid = table.Column<Guid>(nullable: false),
                    role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_memberships", x => new { x.organizationid, x.personid });
                    table.ForeignKey(
                        name: "fk_memberships_organizationidentities_organizationid",
                        column: x => x.organizationid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_memberships_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personemailcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personemailcontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personemailcontactitembridges_emailcontactitems_contactitem~",
                        column: x => x.contactitemid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "emailcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personemailcontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personphonecontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personphonecontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personphonecontactitembridges_phonecontactitems_contactitem~",
                        column: x => x.contactitemid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "phonecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personphonecontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personsocialcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personsocialcontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personsocialcontactitembridges_socialcontactitems_contactit~",
                        column: x => x.contactitemid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "socialcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personsocialcontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personwebsitecontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personwebsitecontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personwebsitecontactitembridges_websitecontactitems_contact~",
                        column: x => x.contactitemid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "websitecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personwebsitecontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "localities",
                schema: "fcbc-sgi_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    municipalityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_localities", x => x.id);
                    table.ForeignKey(
                        name: "fk_localities_municipalities_municipalityid",
                        column: x => x.municipalityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_region",
                        principalTable: "municipalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "creationgroupartistbridges",
                columns: table => new
                {
                    creationgroupid = table.Column<Guid>(nullable: false),
                    artistid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupartistbridges", x => new { x.creationgroupid, x.artistid });
                    table.ForeignKey(
                        name: "fk_creationgroupartistbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupartistbridges_creationgroups_creationgroupid",
                        column: x => x.creationgroupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "administrativerolebridges",
                columns: table => new
                {
                    employeeid = table.Column<Guid>(nullable: false),
                    roleid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_administrativerolebridges", x => new { x.roleid, x.employeeid });
                    table.ForeignKey(
                        name: "fk_administrativerolebridges_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_administrativerolebridges_administrativeroles_roleid",
                        column: x => x.roleid,
                        principalTable: "administrativeroles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "regions",
                schema: "fcbc-sgi_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    discriminator = table.Column<string>(nullable: false),
                    countryid = table.Column<Guid>(nullable: true),
                    localityid = table.Column<Guid>(nullable: true),
                    municipalityid = table.Column<Guid>(nullable: true),
                    stateid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_regions", x => x.id);
                    table.ForeignKey(
                        name: "fk_regions_countries_countryid",
                        column: x => x.countryid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_region",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_regions_localities_localityid",
                        column: x => x.localityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_region",
                        principalTable: "localities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_regions_municipalities_municipalityid",
                        column: x => x.municipalityid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_region",
                        principalTable: "municipalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_regions_states_stateid",
                        column: x => x.stateid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_region",
                        principalTable: "states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "postalcontactitems",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: false),
                    regionid = table.Column<Guid>(nullable: false),
                    zipcode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_postalcontactitems", x => x.id);
                    table.ForeignKey(
                        name: "fk_postalcontactitems_regions_regionid",
                        column: x => x.regionid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_region",
                        principalTable: "regions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationpostalcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationpostalcontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationpostalcontactitembridges_postalcontactitems_con~",
                        column: x => x.contactitemid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "postalcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationpostalcontactitembridges_organizationidentities~",
                        column: x => x.organizationid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personpostalcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personpostalcontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personpostalcontactitembridges_postalcontactitems_contactit~",
                        column: x => x.contactitemid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "postalcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personpostalcontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_administrativerolebridges_employeeid",
                table: "administrativerolebridges",
                column: "employeeid");

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
                name: "ix_artisticmanifestations_groupid",
                table: "artisticmanifestations",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_artistresolutionlocks_resolutionid",
                table: "artistresolutionlocks",
                column: "resolutionid");

            migrationBuilder.CreateIndex(
                name: "ix_artists_brandimageid",
                table: "artists",
                column: "brandimageid");

            migrationBuilder.CreateIndex(
                name: "ix_artists_identityid",
                table: "artists",
                column: "identityid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_artistservicelinebridges_lineid",
                table: "artistservicelinebridges",
                column: "lineid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_manifestationid",
                table: "artworks",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_associations_identityid",
                table: "associations",
                column: "identityid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_awards_countryid",
                table: "awards",
                column: "countryid");

            migrationBuilder.CreateIndex(
                name: "ix_contractors_contractorkindid",
                table: "contractors",
                column: "contractorkindid");

            migrationBuilder.CreateIndex(
                name: "ix_contractors_identityid",
                table: "contractors",
                column: "identityid",
                unique: true);

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
                name: "ix_creationgroupartistbridges_artistid",
                table: "creationgroupartistbridges",
                column: "artistid");

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
                name: "ix_creationgroups_identityid",
                table: "creationgroups",
                column: "identityid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupservicelinebridges_lineid",
                table: "creationgroupservicelinebridges",
                column: "lineid");

            migrationBuilder.CreateIndex(
                name: "ix_critics_identityid",
                table: "critics",
                column: "identityid");

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
                name: "ix_employees_identityid",
                table: "employees",
                column: "identityid",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "ix_grouprolebridges_roleid",
                schema: "fcbc-sgi_sapressc_core_securitylocal",
                table: "grouprolebridges",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "ix_usergroupbridges_groupid",
                schema: "fcbc-sgi_sapressc_core_securitylocal",
                table: "usergroupbridges",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_memberships_personid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "memberships",
                column: "personid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationemailcontactitembridges_contactitemid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "organizationemailcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationidentities_imageid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "organizationidentities",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationidentities_ownerid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "organizationidentities",
                column: "ownerid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationphonecontactitembridges_contactitemid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "organizationphonecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationpostalcontactitembridges_contactitemid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "organizationpostalcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationsocialcontactitembridges_contactitemid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "organizationsocialcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationwebsitecontactitembridges_contactitemid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "organizationwebsitecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personemailcontactitembridges_contactitemid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "personemailcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personidentities_imageid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "personidentities",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_personphonecontactitembridges_contactitemid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "personphonecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personpostalcontactitembridges_contactitemid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "personpostalcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personsocialcontactitembridges_contactitemid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "personsocialcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personwebsitecontactitembridges_contactitemid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "personwebsitecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_postalcontactitems_regionid",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                table: "postalcontactitems",
                column: "regionid");

            migrationBuilder.CreateIndex(
                name: "ix_images_thumbnailid",
                schema: "fcbc-sgi_sapressc_middleware_media",
                table: "images",
                column: "thumbnailid");

            migrationBuilder.CreateIndex(
                name: "ix_videos_thumbnailid",
                schema: "fcbc-sgi_sapressc_middleware_media",
                table: "videos",
                column: "thumbnailid");

            migrationBuilder.CreateIndex(
                name: "ix_countries_isocode",
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "countries",
                column: "isocode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_countries_name",
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "countries",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_localities_municipalityid",
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "localities",
                column: "municipalityid");

            migrationBuilder.CreateIndex(
                name: "ix_localities_name_municipalityid",
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "localities",
                columns: new[] { "name", "municipalityid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_municipalities_stateid",
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "municipalities",
                column: "stateid");

            migrationBuilder.CreateIndex(
                name: "ix_municipalities_name_stateid",
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "municipalities",
                columns: new[] { "name", "stateid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_regions_countryid",
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "regions",
                column: "countryid");

            migrationBuilder.CreateIndex(
                name: "ix_regions_localityid",
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "regions",
                column: "localityid");

            migrationBuilder.CreateIndex(
                name: "ix_regions_municipalityid",
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "regions",
                column: "municipalityid");

            migrationBuilder.CreateIndex(
                name: "ix_regions_stateid",
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "regions",
                column: "stateid");

            migrationBuilder.CreateIndex(
                name: "ix_states_countryid",
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "states",
                column: "countryid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administrativerolebridges");

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
                name: "artistresolutionlocks");

            migrationBuilder.DropTable(
                name: "artistservicelinebridges");

            migrationBuilder.DropTable(
                name: "artworks");

            migrationBuilder.DropTable(
                name: "awards");

            migrationBuilder.DropTable(
                name: "contractors");

            migrationBuilder.DropTable(
                name: "craftworks");

            migrationBuilder.DropTable(
                name: "creationgroupartistbridges");

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
                name: "critics");

            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropTable(
                name: "designworks");

            migrationBuilder.DropTable(
                name: "distinctions");

            migrationBuilder.DropTable(
                name: "institutionalinformations");

            migrationBuilder.DropTable(
                name: "institutionalservices");

            migrationBuilder.DropTable(
                name: "recognitions");

            migrationBuilder.DropTable(
                name: "serviceworks");

            migrationBuilder.DropTable(
                name: "technique");

            migrationBuilder.DropTable(
                name: "thematics");

            migrationBuilder.DropTable(
                name: "traces");

            migrationBuilder.DropTable(
                name: "grouprolebridges",
                schema: "fcbc-sgi_sapressc_core_securitylocal");

            migrationBuilder.DropTable(
                name: "usergroupbridges",
                schema: "fcbc-sgi_sapressc_core_securitylocal");

            migrationBuilder.DropTable(
                name: "memberships",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationemailcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationphonecontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationpostalcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationsocialcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationwebsitecontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personemailcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personphonecontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personpostalcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personsocialcontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personwebsitecontactitembridges",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "videos",
                schema: "fcbc-sgi_sapressc_middleware_media");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "administrativeroles");

            migrationBuilder.DropTable(
                name: "associations");

            migrationBuilder.DropTable(
                name: "resolutions");

            migrationBuilder.DropTable(
                name: "contractorkinds");

            migrationBuilder.DropTable(
                name: "artists");

            migrationBuilder.DropTable(
                name: "artisticmanifestations");

            migrationBuilder.DropTable(
                name: "craftmanifestations");

            migrationBuilder.DropTable(
                name: "creationgroups");

            migrationBuilder.DropTable(
                name: "designmanifestations");

            migrationBuilder.DropTable(
                name: "servicelines");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "fcbc-sgi_sapressc_core_securitylocal");

            migrationBuilder.DropTable(
                name: "groups",
                schema: "fcbc-sgi_sapressc_core_securitylocal");

            migrationBuilder.DropTable(
                name: "users",
                schema: "fcbc-sgi_sapressc_core_securitylocal");

            migrationBuilder.DropTable(
                name: "emailcontactitems",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "phonecontactitems",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "postalcontactitems",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "socialcontactitems",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "websitecontactitems",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personidentities",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "artisticmanifestationgroups");

            migrationBuilder.DropTable(
                name: "craftmanifestationgroups");

            migrationBuilder.DropTable(
                name: "organizationidentities",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "designmanifestationgroups");

            migrationBuilder.DropTable(
                name: "servicelinegroups");

            migrationBuilder.DropTable(
                name: "regions",
                schema: "fcbc-sgi_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "images",
                schema: "fcbc-sgi_sapressc_middleware_media");

            migrationBuilder.DropTable(
                name: "localities",
                schema: "fcbc-sgi_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "thumbnails",
                schema: "fcbc-sgi_sapressc_middleware_media");

            migrationBuilder.DropTable(
                name: "municipalities",
                schema: "fcbc-sgi_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "states",
                schema: "fcbc-sgi_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "fcbc-sgi_sapressc_middleware_region");
        }
    }
}

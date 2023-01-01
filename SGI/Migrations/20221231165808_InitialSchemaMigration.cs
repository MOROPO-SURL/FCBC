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
                name: "fcbc-sgi_sapressc_core_security_common");

            migrationBuilder.EnsureSchema(
                name: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.EnsureSchema(
                name: "fcbc-sgi_sapressc_middleware_media");

            migrationBuilder.EnsureSchema(
                name: "fcbc-sgi_sapressc_middleware_region");

            migrationBuilder.EnsureSchema(
                name: "fcbc-sgi_sapressc_middleware_security_essential");

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
                name: "artistictechniques",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistictechniques", x => x.id);
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
                name: "crafttechniques",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_crafttechniques", x => x.id);
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
                name: "designtechniques",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designtechniques", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "distinctions",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
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
                name: "materials",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_materials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "recognitions",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
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
                name: "servicetechniques",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servicetechniques", x => x.id);
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
                name: "unitmeasurements",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unitmeasurements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "fcbc-sgi_sapressc_core_security_common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    family = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "fcbc-sgi_sapressc_core_security_common",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
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
                name: "users",
                schema: "fcbc-sgi_sapressc_middleware_security_essential",
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
                name: "rolepermissionbridges",
                schema: "fcbc-sgi_sapressc_core_security_common",
                columns: table => new
                {
                    roleid = table.Column<Guid>(nullable: false),
                    permissionid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rolepermissionbridges", x => new { x.roleid, x.permissionid });
                    table.ForeignKey(
                        name: "fk_rolepermissionbridges_permissions_permissionid",
                        column: x => x.permissionid,
                        principalSchema: "fcbc-sgi_sapressc_core_security_common",
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rolepermissionbridges_roles_roleid",
                        column: x => x.roleid,
                        principalSchema: "fcbc-sgi_sapressc_core_security_common",
                        principalTable: "roles",
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
                name: "userrolebridges",
                schema: "fcbc-sgi_sapressc_middleware_security_essential",
                columns: table => new
                {
                    userid = table.Column<string>(nullable: false),
                    roleid = table.Column<Guid>(nullable: false),
                    signature = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_userrolebridges", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "fk_userrolebridges_roles_roleid",
                        column: x => x.roleid,
                        principalSchema: "fcbc-sgi_sapressc_core_security_common",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_userrolebridges_users_userid",
                        column: x => x.userid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_security_essential",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artworks",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    bornyear = table.Column<int>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    featureimageid = table.Column<Guid>(nullable: true),
                    price = table.Column<decimal>(nullable: true),
                    pricecurrencyid = table.Column<int>(nullable: true),
                    cost = table.Column<decimal>(nullable: true),
                    costcurrencyid = table.Column<int>(nullable: true),
                    collection = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    manifestationid = table.Column<Guid>(nullable: false),
                    profundity = table.Column<decimal>(nullable: true),
                    height = table.Column<decimal>(nullable: true),
                    width = table.Column<decimal>(nullable: true),
                    dimensionsunitmeasurementid = table.Column<Guid>(nullable: true),
                    weight = table.Column<decimal>(nullable: true),
                    weightunitmeasurementid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworks", x => x.id);
                    table.ForeignKey(
                        name: "fk_artworks_currencies_costcurrencyid",
                        column: x => x.costcurrencyid,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_artworks_unitmeasurements_dimensionsunitmeasurementid",
                        column: x => x.dimensionsunitmeasurementid,
                        principalTable: "unitmeasurements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_artworks_images_featureimageid",
                        column: x => x.featureimageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_artworks_artisticmanifestations_manifestationid",
                        column: x => x.manifestationid,
                        principalTable: "artisticmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworks_currencies_pricecurrencyid",
                        column: x => x.pricecurrencyid,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_artworks_unitmeasurements_weightunitmeasurementid",
                        column: x => x.weightunitmeasurementid,
                        principalTable: "unitmeasurements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "craftworks",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    bornyear = table.Column<int>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    featureimageid = table.Column<Guid>(nullable: true),
                    price = table.Column<decimal>(nullable: true),
                    pricecurrencyid = table.Column<int>(nullable: true),
                    cost = table.Column<decimal>(nullable: true),
                    costcurrencyid = table.Column<int>(nullable: true),
                    manifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworks", x => x.id);
                    table.ForeignKey(
                        name: "fk_craftworks_currencies_costcurrencyid",
                        column: x => x.costcurrencyid,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_craftworks_images_featureimageid",
                        column: x => x.featureimageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_craftworks_craftmanifestations_manifestationid",
                        column: x => x.manifestationid,
                        principalTable: "craftmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworks_currencies_pricecurrencyid",
                        column: x => x.pricecurrencyid,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "designworks",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    bornyear = table.Column<int>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    featureimageid = table.Column<Guid>(nullable: true),
                    price = table.Column<decimal>(nullable: true),
                    pricecurrencyid = table.Column<int>(nullable: true),
                    cost = table.Column<decimal>(nullable: true),
                    costcurrencyid = table.Column<int>(nullable: true),
                    manifestationid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworks", x => x.id);
                    table.ForeignKey(
                        name: "fk_designworks_currencies_costcurrencyid",
                        column: x => x.costcurrencyid,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_designworks_images_featureimageid",
                        column: x => x.featureimageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_designworks_designmanifestations_manifestationid",
                        column: x => x.manifestationid,
                        principalTable: "designmanifestations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworks_currencies_pricecurrencyid",
                        column: x => x.pricecurrencyid,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "serviceworks",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    bornyear = table.Column<int>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    featureimageid = table.Column<Guid>(nullable: true),
                    price = table.Column<decimal>(nullable: true),
                    pricecurrencyid = table.Column<int>(nullable: true),
                    cost = table.Column<decimal>(nullable: true),
                    costcurrencyid = table.Column<int>(nullable: true),
                    lineid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworks", x => x.id);
                    table.ForeignKey(
                        name: "fk_serviceworks_currencies_costcurrencyid",
                        column: x => x.costcurrencyid,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_serviceworks_images_featureimageid",
                        column: x => x.featureimageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_serviceworks_servicelines_lineid",
                        column: x => x.lineid,
                        principalTable: "servicelines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworks_currencies_pricecurrencyid",
                        column: x => x.pricecurrencyid,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "organizationidentities",
                schema: "fcbc-sgi_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    borndate = table.Column<DateTime>(nullable: true),
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
                    borndate = table.Column<DateTime>(nullable: true),
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
                name: "artworkimagebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    imageid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworkimagebridges", x => new { x.workid, x.imageid });
                    table.ForeignKey(
                        name: "fk_artworkimagebridges_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworkimagebridges_artworks_workid",
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
                name: "artworktechniquebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    techniqueid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artworktechniquebridges", x => new { x.workid, x.techniqueid });
                    table.ForeignKey(
                        name: "fk_artworktechniquebridges_artistictechniques_techniqueid",
                        column: x => x.techniqueid,
                        principalTable: "artistictechniques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artworktechniquebridges_artworks_workid",
                        column: x => x.workid,
                        principalTable: "artworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "craftworkimagebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    imageid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworkimagebridges", x => new { x.workid, x.imageid });
                    table.ForeignKey(
                        name: "fk_craftworkimagebridges_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworkimagebridges_craftworks_workid",
                        column: x => x.workid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "craftworktechniquebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    techniqueid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_craftworktechniquebridges", x => new { x.workid, x.techniqueid });
                    table.ForeignKey(
                        name: "fk_craftworktechniquebridges_crafttechniques_techniqueid",
                        column: x => x.techniqueid,
                        principalTable: "crafttechniques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_craftworktechniquebridges_craftworks_workid",
                        column: x => x.workid,
                        principalTable: "craftworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designworkimagebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    imageid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworkimagebridges", x => new { x.workid, x.imageid });
                    table.ForeignKey(
                        name: "fk_designworkimagebridges_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworkimagebridges_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designworktechniquebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    techniqueid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designworktechniquebridges", x => new { x.workid, x.techniqueid });
                    table.ForeignKey(
                        name: "fk_designworktechniquebridges_designtechniques_techniqueid",
                        column: x => x.techniqueid,
                        principalTable: "designtechniques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_designworktechniquebridges_designworks_workid",
                        column: x => x.workid,
                        principalTable: "designworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceworkimagebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    imageid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworkimagebridges", x => new { x.workid, x.imageid });
                    table.ForeignKey(
                        name: "fk_serviceworkimagebridges_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "fcbc-sgi_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworkimagebridges_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceworktechniquebridges",
                columns: table => new
                {
                    workid = table.Column<Guid>(nullable: false),
                    techniqueid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_serviceworktechniquebridges", x => new { x.workid, x.techniqueid });
                    table.ForeignKey(
                        name: "fk_serviceworktechniquebridges_servicetechniques_techniqueid",
                        column: x => x.techniqueid,
                        principalTable: "servicetechniques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_serviceworktechniquebridges_serviceworks_workid",
                        column: x => x.workid,
                        principalTable: "serviceworks",
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
                    identityid = table.Column<Guid>(nullable: false),
                    resolutionid = table.Column<Guid>(nullable: true),
                    registrationnumber = table.Column<int>(nullable: false),
                    registrationendat = table.Column<DateTime>(nullable: true)
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
                    table.ForeignKey(
                        name: "fk_creationgroups_resolutions_resolutionid",
                        column: x => x.resolutionid,
                        principalTable: "resolutions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    admitionmodecontext = table.Column<string>(nullable: true),
                    registrationnumber = table.Column<int>(nullable: false),
                    registrationendat = table.Column<DateTime>(nullable: true)
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
                    ispublic = table.Column<bool>(nullable: false),
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
                name: "creationgroupawardbridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    awardid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupawardbridges", x => new { x.groupid, x.awardid });
                    table.ForeignKey(
                        name: "fk_creationgroupawardbridges_awards_awardid",
                        column: x => x.awardid,
                        principalTable: "awards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupawardbridges_creationgroups_groupid",
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
                name: "creationgroupdistinctionbridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    distinctionid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgroupdistinctionbridges", x => new { x.groupid, x.distinctionid });
                    table.ForeignKey(
                        name: "fk_creationgroupdistinctionbridges_creationgroups_distinctionid",
                        column: x => x.distinctionid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgroupdistinctionbridges_distinctions_distinctionid",
                        column: x => x.distinctionid,
                        principalTable: "distinctions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "creationgrouprecognitionbridges",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    recognitionid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_creationgrouprecognitionbridges", x => new { x.groupid, x.recognitionid });
                    table.ForeignKey(
                        name: "fk_creationgrouprecognitionbridges_creationgroups_groupid",
                        column: x => x.groupid,
                        principalTable: "creationgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_creationgrouprecognitionbridges_recognitions_recognitionid",
                        column: x => x.recognitionid,
                        principalTable: "recognitions",
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
                    ispublic = table.Column<bool>(nullable: false),
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
                name: "artistawards",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    awardid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistawards", x => new { x.artistid, x.awardid });
                    table.ForeignKey(
                        name: "fk_artistawards_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistawards_awards_awardid",
                        column: x => x.awardid,
                        principalTable: "awards",
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
                name: "artistdistinctionbridges",
                columns: table => new
                {
                    artistid = table.Column<Guid>(nullable: false),
                    distinctionid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistdistinctionbridges", x => new { x.artistid, x.distinctionid });
                    table.ForeignKey(
                        name: "fk_artistdistinctionbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistdistinctionbridges_distinctions_distinctionid",
                        column: x => x.distinctionid,
                        principalTable: "distinctions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artistrecognitionbridges",
                columns: table => new
                {
                    recognitionid = table.Column<Guid>(nullable: false),
                    artistid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_artistrecognitionbridges", x => new { x.artistid, x.recognitionid });
                    table.ForeignKey(
                        name: "fk_artistrecognitionbridges_artists_artistid",
                        column: x => x.artistid,
                        principalTable: "artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_artistrecognitionbridges_recognitions_recognitionid",
                        column: x => x.recognitionid,
                        principalTable: "recognitions",
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

            migrationBuilder.CreateTable(
                name: "administrativerolebridges",
                columns: table => new
                {
                    employeeid = table.Column<Guid>(nullable: false),
                    roleid = table.Column<Guid>(nullable: false),
                    employeeid1 = table.Column<Guid>(nullable: true)
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
                        name: "fk_administrativerolebridges_employees_employeeid1",
                        column: x => x.employeeid1,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "ix_administrativerolebridges_employeeid1",
                table: "administrativerolebridges",
                column: "employeeid1");

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
                name: "ix_artistawards_awardid",
                table: "artistawards",
                column: "awardid");

            migrationBuilder.CreateIndex(
                name: "ix_artistcraftmanifestationbridges_manifestationid",
                table: "artistcraftmanifestationbridges",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_artistdesignmanifestationbridges_manifestationid",
                table: "artistdesignmanifestationbridges",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_artistdistinctionbridges_distinctionid",
                table: "artistdistinctionbridges",
                column: "distinctionid");

            migrationBuilder.CreateIndex(
                name: "ix_artisticmanifestationgroups_parentid",
                table: "artisticmanifestationgroups",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_artisticmanifestations_groupid",
                table: "artisticmanifestations",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_artistrecognitionbridges_recognitionid",
                table: "artistrecognitionbridges",
                column: "recognitionid");

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
                name: "ix_artworkimagebridges_imageid",
                table: "artworkimagebridges",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkmaterialbridges_materialid",
                table: "artworkmaterialbridges",
                column: "materialid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_costcurrencyid",
                table: "artworks",
                column: "costcurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_dimensionsunitmeasurementid",
                table: "artworks",
                column: "dimensionsunitmeasurementid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_featureimageid",
                table: "artworks",
                column: "featureimageid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_manifestationid",
                table: "artworks",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_pricecurrencyid",
                table: "artworks",
                column: "pricecurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_artworks_weightunitmeasurementid",
                table: "artworks",
                column: "weightunitmeasurementid");

            migrationBuilder.CreateIndex(
                name: "ix_artworktechniquebridges_techniqueid",
                table: "artworktechniquebridges",
                column: "techniqueid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkvaloration_criticid",
                table: "artworkvaloration",
                column: "criticid");

            migrationBuilder.CreateIndex(
                name: "ix_artworkvaloration_workid",
                table: "artworkvaloration",
                column: "workid");

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
                name: "ix_craftworkimagebridges_imageid",
                table: "craftworkimagebridges",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworks_costcurrencyid",
                table: "craftworks",
                column: "costcurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworks_featureimageid",
                table: "craftworks",
                column: "featureimageid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworks_manifestationid",
                table: "craftworks",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworks_pricecurrencyid",
                table: "craftworks",
                column: "pricecurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_craftworktechniquebridges_techniqueid",
                table: "craftworktechniquebridges",
                column: "techniqueid");

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
                name: "ix_creationgroupawardbridges_awardid",
                table: "creationgroupawardbridges",
                column: "awardid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupcraftmanifestationbridges_manifestationid",
                table: "creationgroupcraftmanifestationbridges",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupdesignmanifestationbridges_manifestationid",
                table: "creationgroupdesignmanifestationbridges",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroupdistinctionbridges_distinctionid",
                table: "creationgroupdistinctionbridges",
                column: "distinctionid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgrouprecognitionbridges_recognitionid",
                table: "creationgrouprecognitionbridges",
                column: "recognitionid");

            migrationBuilder.CreateIndex(
                name: "ix_creationgroups_identityid",
                table: "creationgroups",
                column: "identityid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_creationgroups_resolutionid",
                table: "creationgroups",
                column: "resolutionid");

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
                name: "ix_designworkimagebridges_imageid",
                table: "designworkimagebridges",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_designworks_costcurrencyid",
                table: "designworks",
                column: "costcurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_designworks_featureimageid",
                table: "designworks",
                column: "featureimageid");

            migrationBuilder.CreateIndex(
                name: "ix_designworks_manifestationid",
                table: "designworks",
                column: "manifestationid");

            migrationBuilder.CreateIndex(
                name: "ix_designworks_pricecurrencyid",
                table: "designworks",
                column: "pricecurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_designworktechniquebridges_techniqueid",
                table: "designworktechniquebridges",
                column: "techniqueid");

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
                name: "ix_serviceworkimagebridges_imageid",
                table: "serviceworkimagebridges",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworks_costcurrencyid",
                table: "serviceworks",
                column: "costcurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworks_featureimageid",
                table: "serviceworks",
                column: "featureimageid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworks_lineid",
                table: "serviceworks",
                column: "lineid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworks_pricecurrencyid",
                table: "serviceworks",
                column: "pricecurrencyid");

            migrationBuilder.CreateIndex(
                name: "ix_serviceworktechniquebridges_techniqueid",
                table: "serviceworktechniquebridges",
                column: "techniqueid");

            migrationBuilder.CreateIndex(
                name: "ix_rolepermissionbridges_permissionid",
                schema: "fcbc-sgi_sapressc_core_security_common",
                table: "rolepermissionbridges",
                column: "permissionid");

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

            migrationBuilder.CreateIndex(
                name: "ix_userrolebridges_roleid",
                schema: "fcbc-sgi_sapressc_middleware_security_essential",
                table: "userrolebridges",
                column: "roleid");
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
                name: "artistawards");

            migrationBuilder.DropTable(
                name: "artistcraftmanifestationbridges");

            migrationBuilder.DropTable(
                name: "artistdesignmanifestationbridges");

            migrationBuilder.DropTable(
                name: "artistdistinctionbridges");

            migrationBuilder.DropTable(
                name: "artistrecognitionbridges");

            migrationBuilder.DropTable(
                name: "artistresolutionlocks");

            migrationBuilder.DropTable(
                name: "artistservicelinebridges");

            migrationBuilder.DropTable(
                name: "artworkattachments");

            migrationBuilder.DropTable(
                name: "artworkawards");

            migrationBuilder.DropTable(
                name: "artworkcreationgroupbridges");

            migrationBuilder.DropTable(
                name: "artworkcreatorbridges");

            migrationBuilder.DropTable(
                name: "artworkimagebridges");

            migrationBuilder.DropTable(
                name: "artworkmaterialbridges");

            migrationBuilder.DropTable(
                name: "artworktechniquebridges");

            migrationBuilder.DropTable(
                name: "artworkvaloration");

            migrationBuilder.DropTable(
                name: "contractors");

            migrationBuilder.DropTable(
                name: "craftworkimagebridges");

            migrationBuilder.DropTable(
                name: "craftworktechniquebridges");

            migrationBuilder.DropTable(
                name: "creationgroupartistbridges");

            migrationBuilder.DropTable(
                name: "creationgroupartisticmanifestationbridges");

            migrationBuilder.DropTable(
                name: "creationgroupattachments");

            migrationBuilder.DropTable(
                name: "creationgroupawardbridges");

            migrationBuilder.DropTable(
                name: "creationgroupcraftmanifestationbridges");

            migrationBuilder.DropTable(
                name: "creationgroupdesignmanifestationbridges");

            migrationBuilder.DropTable(
                name: "creationgroupdistinctionbridges");

            migrationBuilder.DropTable(
                name: "creationgrouprecognitionbridges");

            migrationBuilder.DropTable(
                name: "creationgroupservicelinebridges");

            migrationBuilder.DropTable(
                name: "designworkimagebridges");

            migrationBuilder.DropTable(
                name: "designworktechniquebridges");

            migrationBuilder.DropTable(
                name: "institutionalinformations");

            migrationBuilder.DropTable(
                name: "institutionalservices");

            migrationBuilder.DropTable(
                name: "serviceworkimagebridges");

            migrationBuilder.DropTable(
                name: "serviceworktechniquebridges");

            migrationBuilder.DropTable(
                name: "thematics");

            migrationBuilder.DropTable(
                name: "traces");

            migrationBuilder.DropTable(
                name: "rolepermissionbridges",
                schema: "fcbc-sgi_sapressc_core_security_common");

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
                name: "userrolebridges",
                schema: "fcbc-sgi_sapressc_middleware_security_essential");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "administrativeroles");

            migrationBuilder.DropTable(
                name: "associations");

            migrationBuilder.DropTable(
                name: "materials");

            migrationBuilder.DropTable(
                name: "artistictechniques");

            migrationBuilder.DropTable(
                name: "critics");

            migrationBuilder.DropTable(
                name: "artworks");

            migrationBuilder.DropTable(
                name: "contractorkinds");

            migrationBuilder.DropTable(
                name: "crafttechniques");

            migrationBuilder.DropTable(
                name: "craftworks");

            migrationBuilder.DropTable(
                name: "artists");

            migrationBuilder.DropTable(
                name: "awards");

            migrationBuilder.DropTable(
                name: "distinctions");

            migrationBuilder.DropTable(
                name: "recognitions");

            migrationBuilder.DropTable(
                name: "creationgroups");

            migrationBuilder.DropTable(
                name: "designtechniques");

            migrationBuilder.DropTable(
                name: "designworks");

            migrationBuilder.DropTable(
                name: "servicetechniques");

            migrationBuilder.DropTable(
                name: "serviceworks");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "fcbc-sgi_sapressc_core_security_common");

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
                name: "roles",
                schema: "fcbc-sgi_sapressc_core_security_common");

            migrationBuilder.DropTable(
                name: "users",
                schema: "fcbc-sgi_sapressc_middleware_security_essential");

            migrationBuilder.DropTable(
                name: "unitmeasurements");

            migrationBuilder.DropTable(
                name: "artisticmanifestations");

            migrationBuilder.DropTable(
                name: "craftmanifestations");

            migrationBuilder.DropTable(
                name: "personidentities",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationidentities",
                schema: "fcbc-sgi_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "resolutions");

            migrationBuilder.DropTable(
                name: "designmanifestations");

            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropTable(
                name: "servicelines");

            migrationBuilder.DropTable(
                name: "regions",
                schema: "fcbc-sgi_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "artisticmanifestationgroups");

            migrationBuilder.DropTable(
                name: "craftmanifestationgroups");

            migrationBuilder.DropTable(
                name: "images",
                schema: "fcbc-sgi_sapressc_middleware_media");

            migrationBuilder.DropTable(
                name: "designmanifestationgroups");

            migrationBuilder.DropTable(
                name: "servicelinegroups");

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

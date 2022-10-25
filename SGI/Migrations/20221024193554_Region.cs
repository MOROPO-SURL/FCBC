using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOROPO.FCBC.SGI.Migrations
{
    public partial class Region : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "municipalities",
                keyColumn: "id",
                keyValue: new Guid("aa5e92f9-4e99-4cff-9ba7-3b414284c119"));

            migrationBuilder.DeleteData(
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "states",
                keyColumn: "id",
                keyValue: new Guid("4194fe7a-5170-4a5a-b094-e40c37549a46"));

            migrationBuilder.DeleteData(
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "countries",
                keyColumn: "id",
                keyValue: new Guid("038278a9-bc0a-4ee5-9c4f-ff09f73d6976"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "countries",
                columns: new[] { "id", "isocode", "name" },
                values: new object[] { new Guid("038278a9-bc0a-4ee5-9c4f-ff09f73d6976"), "3166", "Cuba" });

            migrationBuilder.InsertData(
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "municipalities",
                columns: new[] { "id", "name", "stateid" },
                values: new object[] { new Guid("aa5e92f9-4e99-4cff-9ba7-3b414284c119"), "Camagüey", new Guid("831f4a4b-2581-4042-9869-b92c3a1d6734") });

            migrationBuilder.InsertData(
                schema: "fcbc-sgi_sapressc_middleware_region",
                table: "states",
                columns: new[] { "id", "countryid", "name" },
                values: new object[] { new Guid("4194fe7a-5170-4a5a-b094-e40c37549a46"), new Guid("038278a9-bc0a-4ee5-9c4f-ff09f73d6976"), "Camagüey" });
        }
    }
}

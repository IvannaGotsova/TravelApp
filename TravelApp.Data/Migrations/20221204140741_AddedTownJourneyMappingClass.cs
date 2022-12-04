using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApp.Data.Migrations
{
    public partial class AddedTownJourneyMappingClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TownsJourneys",
                columns: table => new
                {
                    TownId = table.Column<int>(type: "int", nullable: false),
                    JourneyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownsJourneys", x => new { x.JourneyId, x.TownId });
                    table.ForeignKey(
                        name: "FK_TownsJourneys_Journeys_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TownsJourneys_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0650610d-d23f-4f0a-bc74-792d4d795c44", "AQAAAAEAACcQAAAAEJRPWGe919TMH6mRU9ElsvdTh08EntKTRjyPlaRtgwYX8+BSZiFHCGJJFJKQ0bsqAA==", "4f576dab-680a-4613-b1b2-01514fa93480" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fire8756-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "567df97a-f1e9-4a0a-b215-be9124200eb4", "AQAAAAEAACcQAAAAEEL02YZ8uLgiTmxTS7+P9r62ZH4qEt9S4o8uxhxJAbc+Kzjq6ioTl2Dwev0MOEgStw==", "a0d0d875-5a65-4fff-8e17-8ed5f6d6750d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "roof9675-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "424d7310-be5f-46b8-94c2-0810e1a61980", "AQAAAAEAACcQAAAAEBxDeM+FJfgTEB3Np2uB4dv701FEezmlTkUkmKFgGH+/sfG04/UWbjgnegSol/E2VA==", "ba8bee92-83df-472d-88b6-d8502dbce3fc" });

            migrationBuilder.InsertData(
                table: "TownsJourneys",
                columns: new[] { "JourneyId", "TownId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TownsJourneys_TownId",
                table: "TownsJourneys",
                column: "TownId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TownsJourneys");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee4e17b8-fb58-4dbf-9c66-cbdc01f659bf", "AQAAAAEAACcQAAAAEM6VEMAEhlyRfUtRq/GPNKqiGRzwQ78y9XQBCI95oJ1P5sRwQUql/EWk+Gfg2VXHbQ==", "b63ca20e-b56c-4998-9d8b-e5da91e19551" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fire8756-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62a14236-1478-444d-990c-a97432c92e10", "AQAAAAEAACcQAAAAEMubs69yHh8Z9Ptdd1gnn0VavDZnkQd8kmH5HEkwtysjX4mrs85EmuQ06iZdMIp4hg==", "a761593b-8a26-4824-a979-ee9681d625ac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "roof9675-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64749097-ed2d-49f5-a106-9ab12ae0c9b4", "AQAAAAEAACcQAAAAEMupYjp+l/tVvdTpg6wTGHUNJAKQoWojAYvKK3kKylgsUwNu88cypyTiWArY149Eaw==", "d182553b-d4ec-4113-94d7-05e767373db1" });
        }
    }
}

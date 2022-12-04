using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApp.Data.Migrations
{
    public partial class CreateSeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    IsVIP = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    Area = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Journeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    Area = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towns_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsersJourneys",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JourneyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsersJourneys", x => new { x.JourneyId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUsersJourneys_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersJourneys_Journeys_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountriesJourneys",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    JourneyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountriesJourneys", x => new { x.JourneyId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_CountriesJourneys_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountriesJourneys_Journeys_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    JourneyId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsManaged = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Journeys_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    JourneyId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_Journeys_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsVIP", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "ee4e17b8-fb58-4dbf-9c66-cbdc01f659bf", "admin@admin.com", false, "Ivan", false, "Ivanov", false, null, "admin@admin.com", "admin@admin.com", "AQAAAAEAACcQAAAAEM6VEMAEhlyRfUtRq/GPNKqiGRzwQ78y9XQBCI95oJ1P5sRwQUql/EWk+Gfg2VXHbQ==", null, false, "b63ca20e-b56c-4998-9d8b-e5da91e19551", false, "admin@admin.com" },
                    { "fire8756-c198-4129-b3f3-b893d8395082", 0, "62a14236-1478-444d-990c-a97432c92e10", "guest1@guest.com", false, "Petar", false, "Petrov", false, null, "guest1@guest.com", "guest1@guest.com", "AQAAAAEAACcQAAAAEMubs69yHh8Z9Ptdd1gnn0VavDZnkQd8kmH5HEkwtysjX4mrs85EmuQ06iZdMIp4hg==", null, false, "a761593b-8a26-4824-a979-ee9681d625ac", false, "guest1@guest.com" },
                    { "roof9675-c198-4129-b3f3-b893d8395082", 0, "64749097-ed2d-49f5-a106-9ab12ae0c9b4", "guest2@guest.com", false, "Dimitar", false, "Dimitrov", false, null, "guest2@guest.com", "guest2@guest.com", "AQAAAAEAACcQAAAAEMupYjp+l/tVvdTpg6wTGHUNJAKQoWojAYvKK3kKylgsUwNu88cypyTiWArY149Eaw==", null, false, "d182553b-d4ec-4113-94d7-05e767373db1", false, "guest2@guest.com" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Area", "Description", "Image", "Name", "Population" },
                values: new object[,]
                {
                    { 1, 110994L, "Country in Europe", "/Photos/Bulgaria.jpg?a=123456", "Bulgaria", 6823493L },
                    { 2, 56594L, "Country in Europe", "/Photos/Croatia.jpg?a=123456", "Croatia", 4044782L },
                    { 3, 9833520L, "Country in NorthAmerica", "/Photos/USA.jpg?a=123456", "USA", 335649823L }
                });

            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "Id", "Days", "Description", "EndDate", "Image", "NumberOfPeople", "Price", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, 11, "Travel New York. Experience the city with our great proposal. Dont miss it.", new DateTime(2023, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Photos/NewYork.jpg?a=123456", 20, 10000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Journey through New York" },
                    { 2, 11, "Our new offer. Visit this amazing city. Experience this adventure.", new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Photos/LosAngeles.jpg?a=123456", 30, 10000m, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Journey through Los Angeles" },
                    { 3, 8, "Visit two beautiful countries. Experience this adventure.", new DateTime(2023, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Photos/Zagreb.jpg?a=123456", 20, 500m, new DateTime(2023, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Journey through Europe" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsersJourneys",
                columns: new[] { "ApplicationUserId", "JourneyId" },
                values: new object[,]
                {
                    { "dea12856-c198-4129-b3f3-b893d8395082", 1 },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 2 },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 3 }
                });

            migrationBuilder.InsertData(
                table: "CountriesJourneys",
                columns: new[] { "CountryId", "JourneyId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 3, 2 },
                    { 1, 3 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "ApplicationUserId", "IsApproved", "IsManaged", "JourneyId", "NumberOfPeople" },
                values: new object[,]
                {
                    { 1, "dea12856-c198-4129-b3f3-b893d8395082", false, false, 1, 2 },
                    { 2, "fire8756-c198-4129-b3f3-b893d8395082", false, false, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Towns",
                columns: new[] { "Id", "Area", "CountryId", "Description", "Image", "Name", "Population" },
                values: new object[,]
                {
                    { 1, 77818L, 3, "City in USA", "/Photos/NewYork.jpg?a=123456", "New York", 8930002L },
                    { 2, 121000L, 3, "City in USA", "/Photos/LosAngeles.jpg?a=123456", "Los Angeles", 3919973L },
                    { 3, 10200L, 1, "City in Bulgaria", "/Photos/Plovdiv.jpg?a=123456", "Plovdiv", 343424L },
                    { 4, 64100L, 2, "City in Croatia", "/Photos/Zagreb.jpg?a=123456", "Zagreb", 806341L }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "ApplicationUserId", "JourneyId", "Rating", "Title" },
                values: new object[,]
                {
                    { 1, "fire8756-c198-4129-b3f3-b893d8395082", 1, 10, "New York Trip" },
                    { 2, "roof9675-c198-4129-b3f3-b893d8395082", 2, 10, "Trip to Los Angeles" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Description", "Image", "Title", "TripId" },
                values: new object[] { 1, "My trip to New York was fun. The city is amazing. I love it.", "/Photos/NewYork.jpg?a=123456", "Post about my trip to New York.", 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Description", "Image", "Title", "TripId" },
                values: new object[] { 2, "My trip to Los Angeles is unforgettable. I miss the city", "/Photos/LosAngeles.jpg?a=123456", "My trip to Los Angeles.", 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Author", "Description", "PostId", "Title" },
                values: new object[] { 1, "guest1@guest.com", "My trip to NY was cool. I had amazing experience. I am coming back very soon.", 1, "New York is the best!" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Author", "Description", "PostId", "Title" },
                values: new object[] { 2, "guest2@guest.com", "Your trip looks fun. I will visit New York in two months. You have some tips.", 1, "Amazing New York" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Author", "Description", "PostId", "Title" },
                values: new object[] { 3, "guest2@guest.com", "Los Angeles looks great. I have great time there. Hope to be back soon.", 2, "My journey in Los Angeles!" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersJourneys_ApplicationUserId",
                table: "ApplicationUsersJourneys",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_CountriesJourneys_CountryId",
                table: "CountriesJourneys",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TripId",
                table: "Posts",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApplicationUserId",
                table: "Requests",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_JourneyId",
                table: "Requests",
                column: "JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_CountryId",
                table: "Towns",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ApplicationUserId",
                table: "Trips",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_JourneyId",
                table: "Trips",
                column: "JourneyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUsersJourneys");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CountriesJourneys");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Journeys");
        }
    }
}

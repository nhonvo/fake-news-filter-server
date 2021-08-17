using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class UpdateNewDB : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfigs");

            migrationBuilder.DropTable(
                name: "Follow");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "NewsInTopics");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "TopicNews");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppConfigs",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfigs", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OfficialRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialBeliefs = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    SourceLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopicNews",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicNews", x => x.TopicId);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PathMedia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    NewsId = table.Column<int>(type: "int", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_Media_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Media_TopicNews_TopicId",
                        column: x => x.TopicId,
                        principalTable: "TopicNews",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsInTopics",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsInTopics", x => new { x.TopicId, x.NewsId });
                    table.ForeignKey(
                        name: "FK_NewsInTopics_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsInTopics_TopicNews_TopicId",
                        column: x => x.TopicId,
                        principalTable: "TopicNews",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Follow",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follow", x => new { x.TopicId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Follow_TopicNews_TopicId",
                        column: x => x.TopicId,
                        principalTable: "TopicNews",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Follow_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppConfigs",
                columns: new[] { "Key", "Value" },
                values: new object[] { "Home Title", "This is homepage" });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "Description", "Name", "OfficialRating", "SourceLink", "Timestamp" },
                values: new object[,]
                {
                    { 1, "Taliban fighters poured into the Afghan capital on Sunday amid scenes of panic and chaos, bringing a swift and shocking close to the Afghan government and the 20-year American era in the country.", "Kabul’s Sudden Fall to Taliban Ends U.S. Era in Afghanistan", null, "https://www.nytimes.com/2021/08/15/world/asia/afghanistan-taliban-kabul-surrender.html", new DateTime(2021, 8, 17, 0, 8, 18, 502, DateTimeKind.Local).AddTicks(7890) },
                    { 2, "The masking orders in Dallas and Bexar counties were issued after a lower court ruled last week in favor of local officials.", "Texas high court blocks mask mandates in two of state's largest counties", null, "https://www.nbcnews.com/news/us-news/texas-high-court-blocks-mask-mandates-two-state-s-largest-n1276884", new DateTime(2021, 8, 17, 0, 8, 18, 503, DateTimeKind.Local).AddTicks(260) },
                    { 3, "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..", "Hospitalizations of Americans under 50 have reached new pandemic highs", null, "https://www.nytimes.com/live/2021/08/15/world/covid-delta-variant-vaccine/covid-hospitalizations-cdc", new DateTime(2021, 8, 17, 0, 8, 18, 503, DateTimeKind.Local).AddTicks(810) }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"), "e7a78abd-be52-4089-8e2e-29824a618ef0", "System Admin", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "TopicNews",
                columns: new[] { "TopicId", "Description", "Label", "Tag", "Timestamp" },
                values: new object[,]
                {
                    { 1, "Follow live as the Taliban seizes territory across Afghanistan in the wake of the U.S. withdrawal.", "breaking", "afghanistan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Best nonfiction features, in-depth stores and other long-form content from across the web.", "featured", "in-depth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Outbreak of respiratory virus that has killed over 1 million and infected 100 milion worldwide.", "feature", "coronavirus", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "The top business and economic news from around the world with a focus on the United State.", "feature", "top-business", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Follow the presidential transition of Joe Biden, including policy plans, appointments and more.", "feature", "biden-admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Top stories from around the world with a focus on news not covered in other feeds.", null, "top-news", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Follow important local news: politics, business, top events and more. Updated everything evening.", null, "boston", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"), new Guid("69db714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("69db714f-9576-45ba-b5b7-f00649be00de"), 0, "531feda6-c1ea-4fa7-b74d-7bf95e5d4426", "bp.khuyen@hutech.edu.vn", true, false, null, "Bui Phu Khuyen", "bp.khuyen@hutech.edu.vn", "khuyenpb", "AQAAAAEAACcQAAAAEPSTCuSTLQKDMBpBNopNFC3bHeTODQGc1Xm2CiGMw2++Z0FKAM63dPPq2xBUBrpwiQ==", null, false, "", false, "khuyenpb" });

            migrationBuilder.InsertData(
                table: "Media",
                columns: new[] { "MediaId", "Caption", "DateCreated", "Duration", "FileSize", "NewsId", "PathMedia", "SortOrder", "TopicId", "Type", "Url" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2021, 8, 17, 0, 8, 18, 472, DateTimeKind.Local).AddTicks(7310), 0, 0L, 1, null, 0, null, 1, "https://static01.nyt.com/images/2021/08/15/world/15afghanistan-kabul-airport/merlin_193320777_09900a3b-bd82-47c6-ad73-fddc1219018d-superJumbo.jpg?quality=90&auto=webp" },
                    { 2, null, new DateTime(2021, 8, 17, 0, 8, 18, 502, DateTimeKind.Local).AddTicks(2510), 0, 0L, 2, null, 0, null, 1, "https://media-cldnry.s-nbcnews.com/image/upload/t_fit-2000w,f_auto,q_auto:best/newscms/2021_30/3495573/210730-greg-abbott-ew-617p.jpg" }
                });

            migrationBuilder.InsertData(
                table: "NewsInTopics",
                columns: new[] { "NewsId", "TopicId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 2 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Follow_UserId",
                table: "Follow",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_NewsId",
                table: "Media",
                column: "NewsId",
                unique: true,
                filter: "[NewsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Media_TopicId",
                table: "Media",
                column: "TopicId",
                unique: true,
                filter: "[TopicId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NewsInTopics_NewsId",
                table: "NewsInTopics",
                column: "NewsId");
        }
    }
}
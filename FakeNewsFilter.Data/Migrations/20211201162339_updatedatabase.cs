using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class updatedatabase : Migration
    {
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
                name: "ForgotPassword",
                columns: table => new
                {
                    IdForgotPassword = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgotPassword", x => x.IdForgotPassword);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PathMedia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.MediaId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey, x.UserId });
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
                name: "Source",
                columns: table => new
                {
                    SourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LanguageId = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.SourceId);
                    table.ForeignKey(
                        name: "FK_Source_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficialRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThumbNews = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LanguageId = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsId);
                    table.ForeignKey(
                        name: "FK_News_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_News_Media_ThumbNews",
                        column: x => x.ThumbNews,
                        principalTable: "Media",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    AvatarId = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Users_Media_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Media",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Story",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thumbstory = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 12, 1, 23, 23, 39, 176, DateTimeKind.Local).AddTicks(2307)),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Story", x => x.StoryId);
                    table.ForeignKey(
                        name: "FK_Story_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Story_Media_Thumbstory",
                        column: x => x.Thumbstory,
                        principalTable: "Media",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Story_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Source",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicNews",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "normal"),
                    Tag = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ThumbTopic = table.Column<int>(type: "int", nullable: true),
                    LanguageId = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicNews", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_TopicNews_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicNews_Media_ThumbTopic",
                        column: x => x.ThumbTopic,
                        principalTable: "Media",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicNews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isReal = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 12, 1, 23, 23, 39, 173, DateTimeKind.Local).AddTicks(5665))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => new { x.UserId, x.NewsId });
                    table.ForeignKey(
                        name: "FK_Vote_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vote_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
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

            migrationBuilder.CreateTable(
                name: "NewsInTopics",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 12, 1, 23, 23, 39, 162, DateTimeKind.Local).AddTicks(2458))
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

            migrationBuilder.InsertData(
                table: "AppConfigs",
                columns: new[] { "Key", "Value" },
                values: new object[] { "Home Title", "This is homepage" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Flag", "IsDefault", "Name" },
                values: new object[,]
                {
                    { "vi", "vi.png", true, "Tiếng Việt" },
                    { "en", "en.png", false, "English" }
                });

            migrationBuilder.InsertData(
                table: "Media",
                columns: new[] { "MediaId", "Caption", "DateCreated", "Duration", "FileSize", "PathMedia", "SortOrder", "Type" },
                values: new object[,]
                {
                    { 13, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1318), 0, 0L, "the-thao1.jpeg", 0, 1 },
                    { 12, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1317), 0, 0L, "khoahocvn.jpeg", 0, 1 },
                    { 11, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1316), 0, 0L, "chungkhoan.jpeg", 0, 1 },
                    { 10, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1314), 0, 0L, "giaothong.jpeg", 0, 1 },
                    { 9, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1313), 0, 0L, "phongsu.jpeg", 0, 1 },
                    { 8, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1240), 0, 0L, "congnghemoi.jpeg", 0, 1 },
                    { 7, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1238), 0, 0L, "congnghegame.jpeg", 0, 1 },
                    { 6, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1237), 0, 0L, "chon truong.jpeg", 0, 1 },
                    { 5, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1236), 0, 0L, "doanh-nghiep.jpeg", 0, 1 },
                    { 4, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1235), 0, 0L, "ngvietnamchau.jpeg", 0, 1 },
                    { 3, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1233), 0, 0L, "kinh-te-tg.jpeg", 0, 1 },
                    { 2, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(1221), 0, 0L, "taliban.jpeg", 0, 1 },
                    { 1, null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(959), 0, 0L, "covid.jpeg", 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"), "570d3e06-927d-4baa-a68c-ca830fee9267", "Admin", "Admin" },
                    { new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"), "b573c0f7-b1b1-41dc-b4d3-8f60eb50d077", "Subscriber", "Subscriber" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AvatarId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("69db714f-9576-45ba-b5b7-f00649be00de"), 0, null, "744b486a-c1e6-411e-9119-a0493ed7ef5b", "bp.khuyen@hutech.edu.vn", true, false, null, "Bui Phu Khuyen", "BP.KHUYEN@HUTECH.EDU.VN", "khuyenpb", "AQAAAAEAACcQAAAAEHG5ZRaK5lu1VbBDZ4MbfVcwJRpON0gApa7zeWsZ/AX3D0/YxeouAaSCEIPoXqrCNg==", null, false, "", false, "khuyenpb" });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "Content", "DatePublished", "Description", "LanguageId", "Name", "OfficialRating", "Publisher", "Source", "ThumbNews", "Timestamp" },
                values: new object[,]
                {
                    { 5, "Ngoại giao", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hai cửa khẩu biên giới giữa Myanmar với Trung Quốc mở cửa lại từ ngày 26.11 với hy vọng hướng đến việc phục hồi thương mại bình thường giữa 2 nước..", "vi", "Myanmar-Trung Quốc mở lại 2 cửa khẩu biên giới sau 7 tháng đóng cửa chống Covid-19", null, null, "https://thanhnien.vn/myanmar-trung-quoc-mo-lai-2-cua-khau-bien-gioi-sau-7-thang-dong-cua-chong-covid-19-post1405895.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9255) },
                    { 1, "Test", new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taliban fighters poured into the Afghan capital on Sunday amid scenes of panic and chaos, bringing a swift and shocking close to the Afghan government and the 20-year American era in the country.", "en", "Kabul’s Sudden Fall to Taliban Ends U.S. Era in Afghanistan", null, "New York Times", "test", 1, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(7256) },
                    { 4, "Test", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..", "en", "Hospitalizations of Americans under 50 have reached new pandemic highs", null, null, "test", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9253) },
                    { 3, "Test", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..", "en", "Hospitalizations of Americans under 50 have reached new pandemic highs", null, null, "test", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9251) },
                    { 2, "Test", new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "The masking orders in Dallas and Bexar counties were issued after a lower court ruled last week in favor of local officials.", "en", "Texas high court blocks mask mandates in two of state's largest counties", null, "NBC News", "test", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9234) },
                    { 33, "Trò chơi", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bộ phim hoạt hình mang tên Arcane về thế giới trong Liên Minh Huyền Thoại đang nhận đánh giá tốt.", "vi", "Arcane giúp Vi và Jinx tăng vọt tỉ lệ được chọn trong LMHT", null, null, "https://thanhnien.vn/arcane-giup-vi-va-jinx-tang-vot-ti-le-duoc-chon-trong-lmht-post1401689.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9498) },
                    { 32, "Trò chơi", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tên của 4 thành phố dự kiến tổ chức giải Chung kết Thế giới 2022 bộ môn eSport Liên Minh Huyền Thoại vô tình bị lộ trong một video thông báo.", "vi", "Riot Games để lộ 4 địa điểm của Chung kết LMHT Thế giới 2022", null, null, "https://thanhnien.vn/riot-games-de-lo-4-dia-diem-cua-chung-ket-lmht-the-gioi-2022-post1403726.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9496) },
                    { 31, "Trò chơi", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giải đấu mang quy mô quốc tế đầu tiên của LMHT: Tốc Chiến vừa kết thúc tại Singapore và một đội tuyển của Việt Nam vào Top 5-6.", "vi", "Việt Nam lọt Top 5 đội LMHT: Tốc Chiến thế giới", null, null, "https://thanhnien.vn/viet-nam-lot-top-5-doi-lmht-toc-chien-the-gioi-post1404103.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9491) },
                    { 30, "Trò chơi", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phi Vụ Triệu Đô bất ngờ quay trở lại Đảo Quân Sự Free Fire lần thứ hai với phần đặc biệt.", "vi", "Phi Vụ Triệu Đô tái kết hợp Free Fire trong phần đặc biệt: Phi Vụ Cuối Cùng tháng 12 này", null, null, "https://thanhnien.vn/phi-vu-trieu-do-tai-ket-hop-free-fire-trong-phan-dac-biet-phi-vu-cuoi-cung-thang-12-nay-post1406503.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9300) },
                    { 29, "Trò chơi", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bản nâng cấp mới sẽ khả dụng vào năm 2022 và hoàn toàn miễn phí cho chủ sở hữu các thiết bị PS4 và Xbox One.", "vi", "Bản nâng cấp Cyberpunk 2077 sẽ miễn phí cho chủ sở hữu PS4 và Xbox One", null, null, "https://thanhnien.vn/ban-nang-cap-cyberpunk-2077-se-mien-phi-cho-chu-so-huu-ps4-va-xbox-one-post1406595.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9298) },
                    { 27, "Giáo viên", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thầy Nguyễn Viết Tước đã được các cấp từ trung ương đến địa phương khen thưởng hơn 7 triệu đồng.", "vi", "Thầy giáo dùng tiền khen thưởng ủng hộ Quỹ phòng chống Covid-19", null, null, "https://thanhnien.vn/thay-giao-dung-tien-khen-thuong-ung-ho-quy-phong-chong-covid-19-post1075098.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9295) },
                    { 26, "Giáo viên", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Từ một chàng thợ xây thích chơi đùa cùng trẻ em, thầy giáo Nguyễn Hồ Tây Phương đã trở thành người thầy hiếm hoi dấn thân mình với nghề dạy dỗ trẻ mầm non.", "vi", "Gặp người thầy… đẹp trai nhất trường mầm non!", null, null, "https://thanhnien.vn/gap-nguoi-thay-dep-trai-nhat-truong-mam-non-post1403127.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9293) },
                    { 25, "Đại học", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Những lưu ý gì cho thí sinh để vào được đúng ngành nghề yêu thích, phù hợp với điểm số, là vấn đề mà rất nhiều thí sinh hiện đang băn khoăn.", "vi", "Hướng vào đại học phù hợp với điểm thi", null, null, "https://thanhnien.vn/huong-vao-dai-hoc-phu-hop-voi-diem-thi-post989845.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9292) },
                    { 24, "Học hành", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dạy học môn lịch sử trong trường phổ thông như thế nào để học sinh không chán là vấn đề luôn luôn mới.", "vi", "Tin tức giáo dục đặc biệt 1.12: Dạy sử bằng nội dung cảm xúc hay sự kiện?", null, null, "https://thanhnien.vn/tin-tuc-giao-duc-dac-biet-1-12-day-su-bang-noi-dung-cam-xuc-hay-su-kien-post1406754.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9290) },
                    { 23, "Chứng khoán", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tiền gửi tiết kiệm sụt giảm trong khi dòng vốn tham gia vào thị trường chứng khoán ngày càng tăng.", "vi", "Dòng vốn mạnh đưa chứng khoán lập đỉnh", null, null, "https://thanhnien.vn/dong-von-manh-dua-chung-khoan-lap-dinh-post1404799.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9288) },
                    { 22, "Tài chính", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dự ước năm 2021, lượng kiều hối chuyển về VN sẽ đạt mức kỷ lục 18,1 tỉ USD, bất chấp dịch Covid-19.", "vi", "Lượng kiều hối tăng mạnh kỷ lục", null, null, "https://thanhnien.vn/luong-kieu-hoi-tang-manh-ky-luc-post1405536.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9287) },
                    { 28, "Đại học", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khoa Y ĐH Quốc gia TP.HCM thông báo xét tuyển bổ sung 3 ngành ĐH hệ chính quy, trong đó có ngành y khoa.", "vi", "Khoa Y ĐH Quốc gia TP.HCM xét tuyển bổ sung cả thí sinh tự do", null, null, "https://thanhnien.vn/khoa-y-dh-quoc-gia-tp-hcm-xet-tuyen-bo-sung-ca-thi-sinh-tu-do-post1116655.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9297) },
                    { 20, "Xuất khẩu", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Số liệu công bố từ Tổng cục Thống kê cho thấy 11 tháng năm 2021, Việt Nam xuất khẩu đạt tổng trị giá 299,67 tỉ USD.", "vi", "Xuất khẩu thép lần đầu cán mốc 10 tỉ USD", null, null, "https://thanhnien.vn/xuat-khau-thep-lan-dau-can-moc-10-ti-usd-post1406650.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9283) },
                    { 21, "Giao thông", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "UBND TP.HCM vừa có văn bản khẩn gửi Bộ Kế hoạch - Đầu tư liên quan đến dự kiến phương án phân bổ vốn đầu tư công năm 2022 nguồn vốn ngân sách T.Ư.", "vi", "TP.HCM khát vốn cho giao thông", null, null, "https://thanhnien.vn/tp-hcm-khat-von-cho-giao-thong-post1406453.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9285) },
                    { 7, "Ngoại giao", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quan hệ kinh tế Mỹ và Đài Loan vừa có thêm bước tiến mới, hàm chứa các hợp tác có vai trò quan trọng trong việc đối phó với sự trỗi dậy của Trung Quốc.", "vi", "Bước ngoặt liên minh kinh tế Mỹ - Đài Loan", null, null, "https://thanhnien.vn/buoc-ngoat-lien-minh-kinh-te-my-dai-loan-post1404652.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9260) },
                    { 8, "Du lịch", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lượng khách quốc tế đến Thái Lan đã tăng mạnh sau khi nước này nới lỏng quy định cách ly cho người đã tiêm vắc xin Covid-19.", "vi", "Lượng khách quốc tế đến Thái Lan tăng mạnh sau mở cửa", null, null, "https://thanhnien.vn/luong-khach-quoc-te-den-thai-lan-tang-manh-sau-mo-cua-post1405440.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9262) },
                    { 9, "Kinh tế", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Các bộ trưởng APEC khẳng định sẽ tăng cường phối hợp và đẩy mạnh nghị trình hoạt động nhằm đảm bảo khôi phục tự do và an toàn đi lại tại khu vực.", "vi", "Hướng đi phục hồi bền vững cho APEC", null, null, "https://thanhnien.vn/huong-di-phuc-hoi-ben-vung-cho-apec-post1400053.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9264) },
                    { 10, "Dân tộc", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Để “săn” ốc đá và cá chình, 2 sản vật ngon bậc nhất ở núi rừng Quảng Trị.", "vi", "Săn 'lộc trời': Lội suối nhặt ốc, vào thủ phủ cá chình", null, null, "https://thanhnien.vn/san-loc-troi-loi-suoi-nhat-oc-vao-thu-phu-ca-chinh-post1406363.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9265) },
                    { 6, "Ngoại giao", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giới phân tích cảnh báo rằng việc mất tín hiệu của nhiều tàu thuyền ở Trung Quốc gây xáo trộn chuỗi cung ứng.", "vi", "Vì sao nhiều tàu thuyền ‘biến mất’ ở vùng biển Trung Quốc?", null, null, "https://thanhnien.vn/vi-sao-nhieu-tau-thuyen-bien-mat-o-vung-bien-trung-quoc-post1405317.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9258) },
                    { 12, "Nông nghiệp", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khi mọi người bắt đầu lên giường đi ngủ, thì một ngày làm việc của công nhân cạo mủ cao su bắt đầu.", "vi", "Nỗi lòng người cạo mủ cao su", null, null, "https://thanhnien.vn/noi-long-nguoi-cao-mu-cao-su-post1404643.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9269) },
                    { 11, "Dân tộc", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuôn Ngọ là làng duy nhất cung cấp nguyên liệu các loại vỏ trai, ốc cho cả nước để làm đồ cẩn, khảm, thủ công mỹ nghệ.", "vi", "Nghề Việt - Nét Việt: Nghề trai Chuôn Ngọ", null, null, "https://thanhnien.vn/nghe-viet-net-viet-nghe-trai-chuon-ngo-post1404658.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9267) },
                    { 14, "Món ăn", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xôi là món ăn được rất nhiều người ưa thích vì dễ ăn và cách làm khá đơn giản, thế nhưng tại gia đình bà Nông Thị Mai.", "vi", "Chiếc nồi cổ ‘thần kỳ’ tạo ra món xôi độc đáo của người Nùng ở Đắk Lắk", null, null, "https://thanhnien.vn/chiec-noi-co-than-ky-tao-ra-mon-xoi-doc-dao-cua-nguoi-nung-o-dak-lak-post1403687.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9272) },
                    { 15, "Người Việt xa xứ", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Câu chuyện của hai anh em sống tại TP.Liverpool được kể lại trong loạt phim tài liệu Nail Bar Boys do Đài BBC khởi chiếu tuần qua.", "vi", "Những đứa con tìm về nguồn cội", null, null, "https://thanhnien.vn/nhung-dua-con-tim-ve-nguon-coi-post1405816.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9274) },
                    { 16, "Người Việt xa xứ", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Để tổ chức thành công triển lãm cá nhân đầu tiên tại Mỹ, họa sĩ tranh in Mai Trần đã trải qua một quá trình dài với nhiều gian nan, thử thách.", "vi", "Mang chất Việt vào tranh in trên đất Mỹ", null, null, "https://thanhnien.vn/mang-chat-viet-vao-tranh-in-tren-dat-my-post1403198.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9276) },
                    { 17, "Người Việt xa xứ", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Một nữ tiến sĩ người Việt được vinh danh là chuyên gia vật liệu hàng đầu tại Úc nhờ góp phần ứng phó cháy rừng tại nước này.", "vi", "Người phụ nữ Việt phát triển vật liệu phủ chống cháy ở Úc", null, null, "https://thanhnien.vn/nguoi-phu-nu-viet-phat-trien-vat-lieu-phu-chong-chay-o-uc-post1401084.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9277) },
                    { 18, "Người Việt xa xứ", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Những ký ức về người bà quá cố và các món ăn Việt mà bà chuẩn bị cho gia đình khi xưa đã dẫn dắt đầu bếp David Huynh.", "vi", "Cảm hứng từ bữa ăn Việt của bà", null, null, "https://thanhnien.vn/cam-hung-tu-bua-an-viet-cua-ba-post1401081.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9279) },
                    { 19, "Giao thông", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bốn đoàn tàu tuyến metro số 1 (tuyến Bến Thành - Suối Tiên) dự kiến từ Nhật Bản về TP.HCM cuối tháng 11 và đầu tháng 12.", "vi", "Gặp bão, đoàn tàu metro trễ hẹn về TP.HCM", null, null, "https://thanhnien.vn/gap-bao-doan-tau-metro-tre-hen-ve-tp-hcm-post1406682.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9281) },
                    { 13, "Cảnh đẹp", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thác Drai Dlông với dòng chảy mạnh mẽ quanh năm giữa núi rừng là điểm đến không thể bỏ qua của những ai muốn khám phá Tây Nguyên.", "vi", "Khám phá thác ba nhánh hùng vĩ ít người biết giữa Tây Nguyên", null, null, "https://thanhnien.vn/kham-pha-thac-ba-nhanh-hung-vi-it-nguoi-biet-giua-tay-nguyen-post1405776.html", null, new DateTime(2021, 12, 1, 23, 23, 39, 202, DateTimeKind.Local).AddTicks(9270) }
                });

            migrationBuilder.InsertData(
                table: "TopicNews",
                columns: new[] { "TopicId", "Description", "Label", "LanguageId", "Tag", "ThumbTopic", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { 13, " Sản phẩm công nghệ mới trong năm.", "Công Nghệ", "vi", "Sản phẩm", 8, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9965), null },
                    { 12, "Công nghệ mới trong game.", "Trò chơi", "vi", "Trò chơi", 7, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9964), null },
                    { 11, "Chọn trường nghề phù hợp với bản thân.", "GIÁO DỤC", "vi", "học hành", 6, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9962), null },
                    { 10, " Nền doanh nghiệp Việt Nam.", "TÀI CHÍNH - KINH DOANH", "vi", "Kinh tế", 5, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9960), null },
                    { 9, "Cuộc sống của người Việt Trên toàn thế giới.", "Thế giới", "vi", "người Việt Nam", 4, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9959), null },
                    { 8, "Kinh tế Việt Nam trong năm.", "Thế giới", "vi", "tin tức", 3, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9957), null },
                    { 1, "Follow live as the Taliban seizes territory across Afghanistan in the wake of the U.S. withdrawal.", "breaking", "en", "afghanistan", null, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9581), null },
                    { 7, "Follow important local news: politics, business, top events and more. Updated everything evening.", "featured", "en", "boston", 1, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9956), null },
                    { 6, "Top stories from around the world with a focus on news not covered in other feeds.", "featured", "en", "top-news", null, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9955), null }
                });

            migrationBuilder.InsertData(
                table: "TopicNews",
                columns: new[] { "TopicId", "Description", "Label", "LanguageId", "Tag", "ThumbTopic", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { 5, "Follow the presidential transition of Joe Biden, including policy plans, appointments and more.", "featured", "en", "biden-admin", null, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9953), null },
                    { 4, "The top business and economic news from around the world with a focus on the United State.", "featured", "en", "top-business", null, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9951), null },
                    { 3, "Outbreak of respiratory virus that has killed over 1 million and infected 100 milion worldwide.", "featured", "en", "coronavirus", null, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9949), null },
                    { 14, " Phóng sự đời sống thường nhật của người dân.", "Thời sự", "vi", "Phóng sự", 9, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9967), null },
                    { 2, "Best nonfiction features, in-depth stores and other long-form content from across the web.", "featured", "en", "in-depth", 2, new DateTime(2021, 12, 1, 23, 23, 39, 201, DateTimeKind.Local).AddTicks(9937), null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"), new Guid("69db714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "NewsInTopics",
                columns: new[] { "NewsId", "TopicId" },
                values: new object[,]
                {
                    { 8, 5 },
                    { 31, 13 },
                    { 30, 13 },
                    { 29, 12 },
                    { 28, 12 },
                    { 27, 12 },
                    { 26, 12 },
                    { 25, 12 },
                    { 24, 12 },
                    { 23, 11 },
                    { 22, 11 },
                    { 21, 11 },
                    { 20, 11 },
                    { 19, 11 },
                    { 32, 13 },
                    { 18, 10 },
                    { 16, 10 },
                    { 15, 10 },
                    { 14, 9 },
                    { 13, 9 },
                    { 12, 9 },
                    { 11, 9 },
                    { 10, 9 },
                    { 9, 8 },
                    { 7, 8 },
                    { 2, 2 },
                    { 3, 2 },
                    { 1, 1 },
                    { 8, 6 },
                    { 17, 10 },
                    { 33, 13 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Follow_UserId",
                table: "Follow",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_News_LanguageId",
                table: "News",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_News_ThumbNews",
                table: "News",
                column: "ThumbNews",
                unique: true,
                filter: "[ThumbNews] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NewsInTopics_NewsId",
                table: "NewsInTopics",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Source_LanguageId",
                table: "Source",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_LanguageId",
                table: "Story",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_SourceId",
                table: "Story",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_Thumbstory",
                table: "Story",
                column: "Thumbstory",
                unique: true,
                filter: "[Thumbstory] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TopicNews_LanguageId",
                table: "TopicNews",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicNews_ThumbTopic",
                table: "TopicNews",
                column: "ThumbTopic",
                unique: true,
                filter: "[ThumbTopic] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TopicNews_UserId",
                table: "TopicNews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarId",
                table: "Users",
                column: "AvatarId",
                unique: true,
                filter: "[AvatarId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_NewsId",
                table: "Vote",
                column: "NewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfigs");

            migrationBuilder.DropTable(
                name: "Follow");

            migrationBuilder.DropTable(
                name: "ForgotPassword");

            migrationBuilder.DropTable(
                name: "NewsInTopics");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Story");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "TopicNews");

            migrationBuilder.DropTable(
                name: "Source");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Media");
        }
    }
}

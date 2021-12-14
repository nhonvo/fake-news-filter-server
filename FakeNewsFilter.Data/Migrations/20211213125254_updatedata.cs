using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeNewsFilter.Data.Migrations
{
    public partial class updatedata : Migration
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
                name: "Nlog",
                columns: table => new
                {
                    IdLog = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logger = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nlog", x => x.IdLog);
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
                        principalColumn: "MediaId");
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
                        principalColumn: "MediaId");
                });

            migrationBuilder.CreateTable(
                name: "Story",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thumbstory = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 12, 13, 19, 52, 53, 877, DateTimeKind.Local).AddTicks(9126)),
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
                        principalColumn: "MediaId");
                    table.ForeignKey(
                        name: "FK_Story_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Source",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comment",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Comment_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
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
                        principalColumn: "MediaId");
                    table.ForeignKey(
                        name: "FK_TopicNews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
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
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 12, 13, 19, 52, 53, 876, DateTimeKind.Local).AddTicks(7816))
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
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 12, 13, 19, 52, 53, 873, DateTimeKind.Local).AddTicks(8855))
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
                    { "en", "en.png", false, "English" },
                    { "vi", "vi.png", true, "Tiếng Việt" }
                });

            migrationBuilder.InsertData(
                table: "Media",
                columns: new[] { "MediaId", "Caption", "DateCreated", "Duration", "FileSize", "PathMedia", "SortOrder", "Type" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8102), 0, 0L, "covid.jpeg", 0, 1 },
                    { 2, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8104), 0, 0L, "taliban.jpeg", 0, 1 },
                    { 3, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8105), 0, 0L, "kinh-te-tg.jpeg", 0, 1 },
                    { 4, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8106), 0, 0L, "ngvietnamchau.jpeg", 0, 1 },
                    { 5, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8107), 0, 0L, "doanh-nghiep.jpeg", 0, 1 },
                    { 6, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8108), 0, 0L, "chon truong.jpeg", 0, 1 },
                    { 7, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8109), 0, 0L, "congnghegame.jpeg", 0, 1 },
                    { 8, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8110), 0, 0L, "congnghemoi.jpeg", 0, 1 },
                    { 9, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8111), 0, 0L, "phongsu.jpeg", 0, 1 },
                    { 10, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8111), 0, 0L, "giaothong.jpeg", 0, 1 },
                    { 11, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8112), 0, 0L, "chungkhoan.jpeg", 0, 1 },
                    { 12, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8113), 0, 0L, "khoahocvn.jpeg", 0, 1 },
                    { 13, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8114), 0, 0L, "the-thao1.jpeg", 0, 1 },
                    { 14, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8115), 0, 0L, "newsid1.jpg", 0, 1 },
                    { 15, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8116), 0, 0L, "newid2.jpg", 0, 1 },
                    { 16, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8117), 0, 0L, "newid3.jpg", 0, 1 },
                    { 17, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8118), 0, 0L, "newid4.jpg", 0, 1 },
                    { 18, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8119), 0, 0L, "newid5.jpg", 0, 1 },
                    { 19, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8120), 0, 0L, "newid6.png", 0, 1 },
                    { 20, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8121), 0, 0L, "newid7.jpg", 0, 1 },
                    { 21, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8122), 0, 0L, "newid8.jpg", 0, 1 },
                    { 22, null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8123), 0, 0L, "newid9.jpg", 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"), "a9d91502-f203-4c46-8efd-cdf8b851ab5a", "Admin", "Admin" },
                    { new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"), "ac048048-75ef-4c3c-b056-15a8a81913e3", "Subscriber", "Subscriber" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AvatarId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be01de"), 0, null, "393b4f5b-04dc-44b7-a6ad-41af57014eac", "bp.khuyen@hutech.edu.vn", true, false, null, "Bui Phu Khuyen", "BP.KHUYEN@HUTECH.EDU.VN", "khuyenpb", "AQAAAAEAACcQAAAAEM0vcgJ2ydvo33c9LGeEraFiJv+qvCwJn6kTiqd6Ud9L1mv5PUAXZ9fAO8FosDqMrg==", null, false, "", false, "khuyenpb" },
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be02de"), 0, null, "02c8d8a5-c97d-4996-8ed7-a7034e279eb9", "thanh26092000@gmail.com", true, false, null, "Le Xuan Thanh", "THANH26092000@GMAIL.COM", "LXThanh", "AQAAAAEAACcQAAAAEFeyyUKfM4cRAmaOcY4RaXk9LDZCoRW3jqD57zxPe21fAB5pbiDh4nMB44qE/Wut5A==", null, false, "", false, "LXThanh" },
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be03de"), 0, null, "283a23e4-d0d8-4289-8041-7c5475f183e4", "khanh200111@gmail.com", true, false, null, "Huynh Huu Khanh", "KHANH200111@GMAIL.COM", "hkhansh27", "AQAAAAEAACcQAAAAEIFkvrP5HU2dixyTjrNxMFZpqrBJ+tvWmd0flbxW9I2J9mYnb1TS0kKeEk17vguCnQ==", null, false, "", false, "hkhansh27" },
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be04de"), 0, null, "436f19fa-5cf3-42c0-b582-719634891794", "hi@phucs.me", true, false, null, "To Hoang Phuc", "HI@PHUCS.ME", "HoangPhuc", "AQAAAAEAACcQAAAAEC3PVU8k0xnmHsyoQegO5obJb2/N3qzIVx8/lA1leACPTEALm8n0oKbzWSmybW0+Kg==", null, false, "", false, "HoangPhuc" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "Content", "DatePublished", "Description", "LanguageId", "Name", "OfficialRating", "Publisher", "Source", "ThumbNews", "Timestamp" },
                values: new object[,]
                {
                    { 1, "Test", new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taliban fighters poured into the Afghan capital on Sunday amid scenes of panic and chaos, bringing a swift and shocking close to the Afghan government and the 20-year American era in the country.", "en", "Kabul’s Sudden Fall to Taliban Ends U.S. Era in Afghanistan", null, "New York Times", "test", 14, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8156) },
                    { 2, "Test", new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "The masking orders in Dallas and Bexar counties were issued after a lower court ruled last week in favor of local officials.", "en", "Texas high court blocks mask mandates in two of state's largest counties", null, "NBC News", "test", 15, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8159) },
                    { 3, "Test", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..", "en", "Hospitalizations of Americans under 50 have reached new pandemic highs", null, null, "test", 16, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8160) },
                    { 4, "Test", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..", "en", "Hospitalizations of Americans under 50 have reached new pandemic highs", null, null, "test", 17, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8162) },
                    { 5, "Theo Sở TT&TT, hiện nay, trên mạng xã hội đang lan truyền thông tin “tối nay từ 11h40 không nên ra đường. Cửa ra vào và cửa sổ nên được đóng lại khi 5 máy bay trực thăng phun chất khử trùng vào không khí để diệt trừ Coronavirus”. Trao đổi với VietNamNet, ông Lâm Đình Thắng, Giám đốc Sở TT&TT cho hay, Bộ Tư lệnh TP.HCM khẳng định, thông tin trên hoàn toàn sai sự thật. Lực lượng quân đội phun khử khuẩn trên địa bàn TP.HCM Trước đó, sáng 23/7, Bộ Tư lệnh TP.HCM phối hợp với Lữ đoàn 87 Binh Chủng hóa học, Tiểu đoàn Phòng hóa 38 Quân khu 7 cùng với lực lượng vũ trang TP và 21 quận, huyện và TP Thủ Đức đồng loạt mở đợt cao điểm phun thuốc khử khuẩn phòng, chống dịch Covid-19 quy mô lớn nhất từ trước tới nay trên địa bàn TP, trong thời gian 7 ngày. Mỗi ngày sẽ có 20 lượt xe tham gia phun thuốc khử khuẩn phòng, chống Covid-19. Theo Hồ Văn/Báo điện tử VietnamNet https://vietnamnet.vn/vn/thoi-su/thong-tin-tp-hcm-dung-5-truc-thang-phun-khu-khuan-la-sai-su-that-759937.html", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lãnh đạo Sở TT&TT TP.HCM cho biết, thông tin dùng 5 trực thăng phun khử trùng diệt Covid-19 là sai...", "vi", "Thông tin TP.HCM dùng 5 trực thăng phun khử khuẩn là sai sự thật", null, null, "https://tingia.gov.vn/tin-tuc/thong-tin-tp-hcm-dung-5-truc-thang-phun-khu-khuan-la-sai-su-that/2695/", 18, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8163) },
                    { 6, "Đại dịch Covid-19 bùng phát trở lại, gây chồng chất thêm khó khăn cho doanh nghiệp, người dân, cũng vì thế mà thông tin về diễn biến đại dịch trở thành mối quan tâm hàng đầu của toàn xã hội. Bên cạnh những thông tin chính xác, tích cực, giúp mọi người nâng cao tinh thần cảnh giác, chung tay phòng chống dịch bệnh, cũng có không ít thông tin sai lệch, thiếu kiểm chứng trên mạng xã hội, gây hoang mang dư luận, tác động xấu đến tình hình an ninh trật tự trên địa bàn. http://brt.vn/thoi-su/dich-viem-phoi-virus-corona/202008/manh-tay-xu-ly-hanh-vi-dua-tin-gia-lien-quan-den-dich-covid-19-8179089/", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đại dịch Covid-19 bùng phát trở lại, gây chồng chất thêm khó khăn cho doanh nghiệp, người dân, cũng vì…", "vi", "Mạnh tay xử lý hành vi đưa tin giả liên quan đến dịch Covid – 19", null, null, "https://tingia.gov.vn/tin-video/manh-tay-xu-ly-hanh-vi-dua-tin-gia-lien-quan-den-dich-covid-19/1561/", 19, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8165) },
                    { 7, "Đăng tải thông tin sai sự thật trên trang Facebook cá nhân: “nguồn nước Thánh Thiên sẽ cứu chữa rất nhiều bệnh… đặc biệt là Covid-19” gây hoang mang dư luận, bà N.T.T (sinh năm 1969, ngụ huyện Bảo Lâm, Lâm Đồng) đã bị cơ quan chức năng xử phạt 5 triệu đồng. Làm việc với Cơ quan công an, bà N.T.T thừa nhận đã đăng tải thông tin sai sự thật. Thông tin lan truyền Qua công tác bảo đảm an ninh mạng, Phòng An ninh mạng và phòng, chống tội phạm sử dụng công nghệ cao (PA05), Công an tỉnh Lâm Đồng phát hiện bà N.T.T đăng tải trên Facebook cá nhân “T.A.P” bài viết có nội dung: “Nguồn nước Thánh Thiên này sẽ cứu chữa rất nhiều bệnh, đặc biệt là Covid-19…”, kèm theo hình ảnh hai chai nước ghi dòng chữ “nguồn Thánh Thiên”. Kiểm chứng Làm việc với cơ quan công an, bà T trình bày, “nguồn nước thánh thiên” có nguồn gốc từ nhóm tự xưng có tên “trừ quỷ Bảo Lộc” (địa chỉ ở 53/5 Hồ Tùng Mậu, TP Bảo Lộc, Lâm Đồng). Trong quá trình tham gia nhóm, bà T và các thành viên cho rằng, “qua việc cầu nguyện, chữa lành, uống nước thánh thiên thì có thể chữa khỏi Covid-19”, nên bà T đã đăng tải lên Facebook cá nhân. Bà T thừa nhận, “nước thánh thiên” không phải thuốc chữa bệnh Covid-19, không được các cơ quan chức năng cấp giấy phép; thông tin do bà T đăng tải là sai sự thật, không kiểm chứng trước khi đăng tải. Hành vi của bà N.T.T vi phạm pháp luật, quy định tại Nghị định số 15/2020/NĐ-CP, ngày 3/2/2020 của Chính phủ, quy định xử phạt vi phạm hành chính trong lĩnh vực bưu chính, viễn thông, tần số vô tuyến điện, công nghệ thông tin và giao dịch điện tử. Theo PA05 Công an tỉnh Lâm Đồng, nhóm tự xưng “trừ quỷ Bảo Lộc” có hoạt động chữa bệnh nhưng không có giấy phép. Ngày 17/9/2021, ông T.V.L.T.Q, một trong những người đứng đầu nhóm này, sử dụng nhà riêng tại địa chỉ nêu trên làm nơi chữa bệnh trái phép, đã bị UBND TP Bảo Lộc xử phạt vi phạm hành chính 45 triệu đồng, về hành vi “chữa bệnh mà không có giấy phép hoạt động chữa bệnh”. Theo Bảo Văn/Báo Nhân dân điện tử https://nhandan.vn/factcheck/thong-tin-nguon-nuoc-thanh-thien-chua-duoc-covid-19-la-sai-su-that-669233/", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đăng tải thông tin sai sự thật trên trang Facebook cá nhân: “nguồn nước Thánh Thiên sẽ cứu chữa rất…", "vi", "Thông tin nguồn nước Thánh Thiên chữa được Covid-19 là sai sự thật", null, null, "https://tingia.gov.vn/tin-tuc/thong-tin-nguon-nuoc-thanh-thien-chua-duoc-covid-19-la-sai-su-that/3203/", 20, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8222) },
                    { 8, "Thông tin về trường hợp bà Nguyễn Huỳnh Như (Giám đốc Công ty mỹ phẩm Đông Anh ở TP Bạc Liêu) cùng 2 người nhà được ra viện sau nhiều ngày điều trị Covid-19 dù còn dương tính là sai sự thật. Bà Nguyễn Huỳnh Như (Giám đốc Công ty mỹ phẩm Đông Anh ở TP Bạc Liêu) cùng 2 người nhà được ra viện sau nhiều ngày điều trị Covid-19 dù còn dương tính là sai sự thật. Mấy ngày qua, trên mạng xã hội và một vài tờ báo điện tử lan truyền thông tin về trường hợp bà Nguyễn Huỳnh Như (Giám đốc Công ty mỹ phẩm Đông Anh ở TP Bạc Liêu) cùng 2 người nhà được ra viện sau nhiều ngày điều trị Covid-19 dù còn dương tính, gây xôn xao, hoang mang trong nhân dân. Theo giải thích của Ban Chỉ đạo Phòng, chống dịch Covid-19 tỉnh Bạc Liêu, trường hợp bà N.H.N (Nguyễn Huỳnh Như) và bà H.T.K.C (trong gia đình bà Nguyễn Huỳnh Như) xuất viện sau khi được lấy 2 mẫu bệnh phẩm liên tiếp cách nhau 24 giờ, có kết quả xét nghiệm bằng phương pháp Real-time RT-PCR nồng độ vius thấp (Ct > 30); thời gian từ khi lấy mẫu bệnh phẩm cuối cùng tới khi ra viện không quá 24 giờ là đúng quy định của ngành y tế. “Người bệnh được xuất viện với kết quả xét nghiệm bằng phương pháp Real-time RT-PCR nồng độ virus thấp (Ct ≥ 30) thì không có khả năng lây bệnh cho cộng đồng. Người bệnh được xuất viện với kết quả dương tính với SARS-CoV-2 nồng độ virus thấp (Ct ≥ 30) hoàn toàn khác với người tái dương tính với SARS-CoV-2. Chiều 29/9, đại diện Ban Chỉ đạo phòng, chống dịch Covid-19 tỉnh Bạc Liêu nêu rõ: Mấy ngày qua, dư luận và một vài tờ báo điện tử nêu bệnh nhân Nguyễn Huỳnh Như và gia đình bị Covid-19, nhưng được “ưu ái điều trị tại nhà theo phác đồ” là sai sự thật. Đáng lưu ý, hiện nay tỉnh Bạc Liêu không tổ chức điều trị tại nhà cho người mắc Covid-19./. Theo TL/Báo điện tử Đảng Cộng sản https://dangcongsan.vn/canh-bao-thong-tin-gia/bac-lieu-mac-covid-19-duoc-uu-ai-dieu-tri-tai-nha-la-sai-su-that-592693.html", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thông tin về trường hợp bà Nguyễn Huỳnh Như (Giám đốc Công ty mỹ phẩm Đông Anh ở TP Bạc…", "vi", "Bạc Liêu: Mắc Covid-19 được “ưu ái điều trị tại nhà” là sai sự thật", null, null, "https://tingia.gov.vn/tin-tuc/bac-lieu-mac-covid-19-duoc-uu-ai-dieu-tri-tai-nha-la-sai-su-that/3182/", 21, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8225) },
                    { 9, "Hàng nghìn người đã xem 1 video trực tuyến, trong đó xuất hiện 1 người đàn ông nói rằng chiến dịch tiêm vaccine Covid-19 cho trẻ em từ 12 đến 15 tuổi ở xứ Anh (England) đã bị tạm hoãn do sai sót ở khâu giấy tờ. Tuy nhiên, thông tin này là sai sự thật. Đoạn video đăng tải thông tin sai sự thật về chiến dịch tiêm vaccine Covid-19 cho trẻ em 12-15 tuổi ở xứ Anh. (Ảnh chụp màn hình) Đoạn video được đăng tải trên mạng xã hội Facebook và Twitter ngày 20/9 – thời điểm mà chiến dịch tiêm chủng cho trẻ em 12-15 tuổi ở xứ Anh bắt đầu được triển khai. Trong video là hình ảnh 1 người đàn ông ngồi nói trước máy quay trong ô-tô. Người đàn ông này cho biết điện thoại của của mình “đang reo liên tục”, đồng thời nói thêm rằng: “Về cơ bản, việc triển khai tiêm chủng ở các trường học trên khắp xứ Anh đang bị tạm hoãn do Cơ quan Y tế công xứ Anh (PHE) đã không gửi giấy tờ chính xác liên quan đến chỉ dẫn nhóm bệnh nhân (PGD).” Chia sẻ với Reuters qua email, đại diện của PHE cho biết không hề có sự chậm trễ hay tạm dừng như những lời cáo buộc của người đàn ông trong đoạn video, bởi giấy tờ chỉ dẫn nhóm bệnh nhân (PGD) đã được bố trí kịp thời để phục vụ công tác triển khai tiêm chủng. Chiến dịch tiêm vaccine ngừa Covid-19 cho trẻ em từ 12 đến 15 tuổi ở xứ Anh chính thức khởi động ngày 20/9 sau khi đạt đươc khuyến nghị đồng thuận từ Giám đốc Y tế bốn quốc gia của Vương quốc Anh. Các quan chức này khuyến cáo trẻ em trong độ tuổi trên cần tiêm mũi đầu tiên với vaccine Covid-19 của Pfizer/BioNTech. Trước đó hồi đầu tháng 9, Ủy ban Hỗn hợp về Tiêm chủng (JCVI), cơ quan tư vấn về vaccine của chính phủ Anh, đã khuyến nghị không tiêm vaccine ngừa Covid-19 cho trẻ em khỏe mạnh từ 12-15 tuổi. Cùng với việc triển khai ở xứ Anh, các xứ Scotland, Wales và Bắc Ireland cũng sẽ đưa ra lịch trình thực hiện chiến dịch tiêm chủng của riêng mình trong thời gian tới. Do đó, thông tin người đàn ông đưa ra trong đoạn video là sai sự thật. Chiến dịch tiêm vaccine ngừa Covid-19 cho trẻ em từ 12 đến 15 tuổi ở xứ Anh đã bắt đầu ngày 20/9. PHE khẳng định giấy tờ chỉ dẫn nhóm bệnh nhân (PGD) đã được bố trí kịp thời để phục vụ quá trình triển khai./. Theo PV/Báo điện tử Đảng cộng sản https://dangcongsan.vn/canh-bao-thong-tin-gia/chien-dich-tiem-vaccine-ngua-covid-19-cho-tre-em-12-15-tuoi-o-xu-anh-bi-tam-dung-la-khong-chinh-xac-591591.html", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hàng nghìn người đã xem 1 video trực tuyến, trong đó xuất hiện 1 người đàn ông nói rằng chiến…", "vi", "“Chiến dịch tiêm vaccine ngừa Covid-19 cho trẻ em 12-15 tuổi ở xứ Anh bị tạm dừng” là không chính xác", null, null, "https://tingia.gov.vn/tin-tuc/chien-dich-tiem-vaccine-ngua-covid-19-cho-tre-em-12-15-tuoi-o-xu-anh-bi-tam-dung-la-khong-chinh-xac/3144/", 22, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8226) },
                    { 10, "Dân tộc", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Để “săn” ốc đá và cá chình, 2 sản vật ngon bậc nhất ở núi rừng Quảng Trị.", "vi", "Săn 'lộc trời': Lội suối nhặt ốc, vào thủ phủ cá chình", null, null, "https://thanhnien.vn/san-loc-troi-loi-suoi-nhat-oc-vao-thu-phu-ca-chinh-post1406363.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8228) },
                    { 11, "Dân tộc", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuôn Ngọ là làng duy nhất cung cấp nguyên liệu các loại vỏ trai, ốc cho cả nước để làm đồ cẩn, khảm, thủ công mỹ nghệ.", "vi", "Nghề Việt - Nét Việt: Nghề trai Chuôn Ngọ", null, null, "https://thanhnien.vn/nghe-viet-net-viet-nghe-trai-chuon-ngo-post1404658.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8229) },
                    { 12, "Nông nghiệp", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khi mọi người bắt đầu lên giường đi ngủ, thì một ngày làm việc của công nhân cạo mủ cao su bắt đầu.", "vi", "Nỗi lòng người cạo mủ cao su", null, null, "https://thanhnien.vn/noi-long-nguoi-cao-mu-cao-su-post1404643.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8231) },
                    { 13, "Cảnh đẹp", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thác Drai Dlông với dòng chảy mạnh mẽ quanh năm giữa núi rừng là điểm đến không thể bỏ qua của những ai muốn khám phá Tây Nguyên.", "vi", "Khám phá thác ba nhánh hùng vĩ ít người biết giữa Tây Nguyên", null, null, "https://thanhnien.vn/kham-pha-thac-ba-nhanh-hung-vi-it-nguoi-biet-giua-tay-nguyen-post1405776.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8233) },
                    { 14, "Món ăn", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xôi là món ăn được rất nhiều người ưa thích vì dễ ăn và cách làm khá đơn giản, thế nhưng tại gia đình bà Nông Thị Mai.", "vi", "Chiếc nồi cổ ‘thần kỳ’ tạo ra món xôi độc đáo của người Nùng ở Đắk Lắk", null, null, "https://thanhnien.vn/chiec-noi-co-than-ky-tao-ra-mon-xoi-doc-dao-cua-nguoi-nung-o-dak-lak-post1403687.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8234) },
                    { 15, "Người Việt xa xứ", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Câu chuyện của hai anh em sống tại TP.Liverpool được kể lại trong loạt phim tài liệu Nail Bar Boys do Đài BBC khởi chiếu tuần qua.", "vi", "Những đứa con tìm về nguồn cội", null, null, "https://thanhnien.vn/nhung-dua-con-tim-ve-nguon-coi-post1405816.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8236) },
                    { 16, "Người Việt xa xứ", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Để tổ chức thành công triển lãm cá nhân đầu tiên tại Mỹ, họa sĩ tranh in Mai Trần đã trải qua một quá trình dài với nhiều gian nan, thử thách.", "vi", "Mang chất Việt vào tranh in trên đất Mỹ", null, null, "https://thanhnien.vn/mang-chat-viet-vao-tranh-in-tren-dat-my-post1403198.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8237) },
                    { 17, "Người Việt xa xứ", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Một nữ tiến sĩ người Việt được vinh danh là chuyên gia vật liệu hàng đầu tại Úc nhờ góp phần ứng phó cháy rừng tại nước này.", "vi", "Người phụ nữ Việt phát triển vật liệu phủ chống cháy ở Úc", null, null, "https://thanhnien.vn/nguoi-phu-nu-viet-phat-trien-vat-lieu-phu-chong-chay-o-uc-post1401084.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8238) },
                    { 18, "Người Việt xa xứ", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Những ký ức về người bà quá cố và các món ăn Việt mà bà chuẩn bị cho gia đình khi xưa đã dẫn dắt đầu bếp David Huynh.", "vi", "Cảm hứng từ bữa ăn Việt của bà", null, null, "https://thanhnien.vn/cam-hung-tu-bua-an-viet-cua-ba-post1401081.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8240) },
                    { 19, "Giao thông", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bốn đoàn tàu tuyến metro số 1 (tuyến Bến Thành - Suối Tiên) dự kiến từ Nhật Bản về TP.HCM cuối tháng 11 và đầu tháng 12.", "vi", "Gặp bão, đoàn tàu metro trễ hẹn về TP.HCM", null, null, "https://thanhnien.vn/gap-bao-doan-tau-metro-tre-hen-ve-tp-hcm-post1406682.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8241) },
                    { 20, "Xuất khẩu", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Số liệu công bố từ Tổng cục Thống kê cho thấy 11 tháng năm 2021, Việt Nam xuất khẩu đạt tổng trị giá 299,67 tỉ USD.", "vi", "Xuất khẩu thép lần đầu cán mốc 10 tỉ USD", null, null, "https://thanhnien.vn/xuat-khau-thep-lan-dau-can-moc-10-ti-usd-post1406650.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8243) },
                    { 21, "Giao thông", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "UBND TP.HCM vừa có văn bản khẩn gửi Bộ Kế hoạch - Đầu tư liên quan đến dự kiến phương án phân bổ vốn đầu tư công năm 2022 nguồn vốn ngân sách T.Ư.", "vi", "TP.HCM khát vốn cho giao thông", null, null, "https://thanhnien.vn/tp-hcm-khat-von-cho-giao-thong-post1406453.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8244) },
                    { 22, "Tài chính", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dự ước năm 2021, lượng kiều hối chuyển về VN sẽ đạt mức kỷ lục 18,1 tỉ USD, bất chấp dịch Covid-19.", "vi", "Lượng kiều hối tăng mạnh kỷ lục", null, null, "https://thanhnien.vn/luong-kieu-hoi-tang-manh-ky-luc-post1405536.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8246) },
                    { 23, "Chứng khoán", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tiền gửi tiết kiệm sụt giảm trong khi dòng vốn tham gia vào thị trường chứng khoán ngày càng tăng.", "vi", "Dòng vốn mạnh đưa chứng khoán lập đỉnh", null, null, "https://thanhnien.vn/dong-von-manh-dua-chung-khoan-lap-dinh-post1404799.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8247) },
                    { 24, "Học hành", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dạy học môn lịch sử trong trường phổ thông như thế nào để học sinh không chán là vấn đề luôn luôn mới.", "vi", "Tin tức giáo dục đặc biệt 1.12: Dạy sử bằng nội dung cảm xúc hay sự kiện?", null, null, "https://thanhnien.vn/tin-tuc-giao-duc-dac-biet-1-12-day-su-bang-noi-dung-cam-xuc-hay-su-kien-post1406754.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8249) },
                    { 25, "Đại học", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Những lưu ý gì cho thí sinh để vào được đúng ngành nghề yêu thích, phù hợp với điểm số, là vấn đề mà rất nhiều thí sinh hiện đang băn khoăn.", "vi", "Hướng vào đại học phù hợp với điểm thi", null, null, "https://thanhnien.vn/huong-vao-dai-hoc-phu-hop-voi-diem-thi-post989845.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8251) },
                    { 26, "Giáo viên", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Từ một chàng thợ xây thích chơi đùa cùng trẻ em, thầy giáo Nguyễn Hồ Tây Phương đã trở thành người thầy hiếm hoi dấn thân mình với nghề dạy dỗ trẻ mầm non.", "vi", "Gặp người thầy… đẹp trai nhất trường mầm non!", null, null, "https://thanhnien.vn/gap-nguoi-thay-dep-trai-nhat-truong-mam-non-post1403127.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8252) },
                    { 27, "Giáo viên", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thầy Nguyễn Viết Tước đã được các cấp từ trung ương đến địa phương khen thưởng hơn 7 triệu đồng.", "vi", "Thầy giáo dùng tiền khen thưởng ủng hộ Quỹ phòng chống Covid-19", null, null, "https://thanhnien.vn/thay-giao-dung-tien-khen-thuong-ung-ho-quy-phong-chong-covid-19-post1075098.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8253) },
                    { 28, "Đại học", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khoa Y ĐH Quốc gia TP.HCM thông báo xét tuyển bổ sung 3 ngành ĐH hệ chính quy, trong đó có ngành y khoa.", "vi", "Khoa Y ĐH Quốc gia TP.HCM xét tuyển bổ sung cả thí sinh tự do", null, null, "https://thanhnien.vn/khoa-y-dh-quoc-gia-tp-hcm-xet-tuyen-bo-sung-ca-thi-sinh-tu-do-post1116655.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8255) },
                    { 29, "Trò chơi", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bản nâng cấp mới sẽ khả dụng vào năm 2022 và hoàn toàn miễn phí cho chủ sở hữu các thiết bị PS4 và Xbox One.", "vi", "Bản nâng cấp Cyberpunk 2077 sẽ miễn phí cho chủ sở hữu PS4 và Xbox One", null, null, "https://thanhnien.vn/ban-nang-cap-cyberpunk-2077-se-mien-phi-cho-chu-so-huu-ps4-va-xbox-one-post1406595.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8256) },
                    { 30, "Trò chơi", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phi Vụ Triệu Đô bất ngờ quay trở lại Đảo Quân Sự Free Fire lần thứ hai với phần đặc biệt.", "vi", "Phi Vụ Triệu Đô tái kết hợp Free Fire trong phần đặc biệt: Phi Vụ Cuối Cùng tháng 12 này", null, null, "https://thanhnien.vn/phi-vu-trieu-do-tai-ket-hop-free-fire-trong-phan-dac-biet-phi-vu-cuoi-cung-thang-12-nay-post1406503.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8258) },
                    { 31, "Trò chơi", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giải đấu mang quy mô quốc tế đầu tiên của LMHT: Tốc Chiến vừa kết thúc tại Singapore và một đội tuyển của Việt Nam vào Top 5-6.", "vi", "Việt Nam lọt Top 5 đội LMHT: Tốc Chiến thế giới", null, null, "https://thanhnien.vn/viet-nam-lot-top-5-doi-lmht-toc-chien-the-gioi-post1404103.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8259) },
                    { 32, "Trò chơi", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tên của 4 thành phố dự kiến tổ chức giải Chung kết Thế giới 2022 bộ môn eSport Liên Minh Huyền Thoại vô tình bị lộ trong một video thông báo.", "vi", "Riot Games để lộ 4 địa điểm của Chung kết LMHT Thế giới 2022", null, null, "https://thanhnien.vn/riot-games-de-lo-4-dia-diem-cua-chung-ket-lmht-the-gioi-2022-post1403726.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8260) },
                    { 33, "Trò chơi", new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bộ phim hoạt hình mang tên Arcane về thế giới trong Liên Minh Huyền Thoại đang nhận đánh giá tốt.", "vi", "Arcane giúp Vi và Jinx tăng vọt tỉ lệ được chọn trong LMHT", null, null, "https://thanhnien.vn/arcane-giup-vi-va-jinx-tang-vot-ti-le-duoc-chon-trong-lmht-post1401689.html", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8262) }
                });

            migrationBuilder.InsertData(
                table: "TopicNews",
                columns: new[] { "TopicId", "Description", "Label", "LanguageId", "Tag", "ThumbTopic", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { 1, "Follow live as the Taliban seizes territory across Afghanistan in the wake of the U.S. withdrawal.", "breaking", "en", "afghanistan", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8041), null },
                    { 2, "Best nonfiction features, in-depth stores and other long-form content from across the web.", "featured", "en", "in-depth", 2, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8053), null },
                    { 3, "Outbreak of respiratory virus that has killed over 1 million and infected 100 milion worldwide.", "featured", "en", "coronavirus", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8054), null },
                    { 4, "The top business and economic news from around the world with a focus on the United State.", "featured", "en", "top-business", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8056), null },
                    { 5, "Follow the presidential transition of Joe Biden, including policy plans, appointments and more.", "featured", "en", "biden-admin", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8057), null },
                    { 6, "Top stories from around the world with a focus on news not covered in other feeds.", "featured", "en", "top-news", null, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8058), null },
                    { 7, "Follow important local news: politics, business, top events and more. Updated everything evening.", "featured", "en", "boston", 1, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8060), null },
                    { 8, "Các thông tin về virut corona.", "Thế giới", "vi", "dịch bệnh", 3, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8061), null },
                    { 9, "Cuộc sống của người Việt Trên toàn thế giới.", "Thế giới", "vi", "người Việt Nam", 4, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8062), null }
                });

            migrationBuilder.InsertData(
                table: "TopicNews",
                columns: new[] { "TopicId", "Description", "Label", "LanguageId", "Tag", "ThumbTopic", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { 10, " Nền doanh nghiệp Việt Nam.", "TÀI CHÍNH - KINH DOANH", "vi", "Kinh tế", 5, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8063), null },
                    { 11, "Chọn trường nghề phù hợp với bản thân.", "GIÁO DỤC", "vi", "học hành", 6, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8065), null },
                    { 12, "Công nghệ mới trong game.", "Trò chơi", "vi", "Trò chơi", 7, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8066), null },
                    { 13, " Sản phẩm công nghệ mới trong năm.", "Công Nghệ", "vi", "Sản phẩm", 8, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8067), null },
                    { 14, " Phóng sự đời sống thường nhật của người dân.", "Thời sự", "vi", "Phóng sự", 9, new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8070), null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"), new Guid("69db714f-9576-45ba-b5b7-f00649be01de") },
                    { new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"), new Guid("69db714f-9576-45ba-b5b7-f00649be02de") },
                    { new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"), new Guid("69db714f-9576-45ba-b5b7-f00649be03de") },
                    { new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"), new Guid("69db714f-9576-45ba-b5b7-f00649be04de") }
                });

            migrationBuilder.InsertData(
                table: "NewsInTopics",
                columns: new[] { "NewsId", "TopicId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 5, 8 },
                    { 6, 8 },
                    { 7, 8 },
                    { 9, 8 },
                    { 10, 9 },
                    { 11, 9 },
                    { 12, 9 },
                    { 13, 9 },
                    { 14, 9 },
                    { 15, 10 },
                    { 16, 10 },
                    { 17, 10 },
                    { 18, 10 },
                    { 19, 11 },
                    { 20, 11 },
                    { 21, 11 },
                    { 22, 11 },
                    { 23, 11 },
                    { 24, 12 },
                    { 25, 12 },
                    { 26, 12 },
                    { 27, 12 },
                    { 28, 12 },
                    { 29, 12 },
                    { 30, 13 },
                    { 31, 13 },
                    { 32, 13 },
                    { 33, 13 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_NewsId",
                table: "Comment",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentId",
                table: "Comment",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

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
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Follow");

            migrationBuilder.DropTable(
                name: "ForgotPassword");

            migrationBuilder.DropTable(
                name: "NewsInTopics");

            migrationBuilder.DropTable(
                name: "Nlog");

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

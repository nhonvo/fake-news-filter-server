using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeNewsFilter.Data.Migrations
{
    public partial class UpdateNewDatabase : Migration
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 156, DateTimeKind.Local).AddTicks(5710))
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
                name: "Version",
                columns: table => new
                {
                    VersionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Platform = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 158, DateTimeKind.Local).AddTicks(2970)),
                    ReleaseTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isRequired = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version", x => x.VersionId);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    OfficialRating = table.Column<int>(type: "int", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlNews = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialBeliefs = table.Column<double>(type: "float", nullable: false),
                    IsVote = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    SourceCreate = table.Column<int>(type: "int", nullable: false),
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 155, DateTimeKind.Local).AddTicks(1660)),
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
                });

            migrationBuilder.CreateTable(
                name: "Source",
                columns: table => new
                {
                    SourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LanguageId = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
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
                name: "DetailNews",
                columns: table => new
                {
                    DetailNewsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbNews = table.Column<int>(type: "int", nullable: true),
                    NewsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailNews", x => x.DetailNewsId);
                    table.ForeignKey(
                        name: "FK_DetailNews_Media_ThumbNews",
                        column: x => x.ThumbNews,
                        principalTable: "Media",
                        principalColumn: "MediaId");
                    table.ForeignKey(
                        name: "FK_DetailNews_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Story",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thumbstory = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 157, DateTimeKind.Local).AddTicks(2640)),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
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
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 157, DateTimeKind.Local).AddTicks(6300))
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
                name: "NewsCommunity",
                columns: table => new
                {
                    NewsCommunityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 157, DateTimeKind.Local).AddTicks(6990)),
                    ThumbNews = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LanguageId = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsCommunity", x => x.NewsCommunityId);
                    table.ForeignKey(
                        name: "FK_NewsCommunity_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsCommunity_Media_ThumbNews",
                        column: x => x.ThumbNews,
                        principalTable: "Media",
                        principalColumn: "MediaId");
                    table.ForeignKey(
                        name: "FK_NewsCommunity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
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
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 154, DateTimeKind.Local).AddTicks(6470)),
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
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 156, DateTimeKind.Local).AddTicks(7970))
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
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 155, DateTimeKind.Local).AddTicks(6730))
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
                    { 1, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510), 0, 0L, "covid.jpeg", 0, 1 },
                    { 2, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510), 0, 0L, "taliban.jpeg", 0, 1 },
                    { 3, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510), 0, 0L, "kinh-te-tg.jpeg", 0, 1 },
                    { 4, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510), 0, 0L, "ngvietnamchau.jpeg", 0, 1 },
                    { 5, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510), 0, 0L, "doanh-nghiep.jpeg", 0, 1 },
                    { 6, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510), 0, 0L, "chon truong.jpeg", 0, 1 },
                    { 7, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510), 0, 0L, "congnghegame.jpeg", 0, 1 },
                    { 8, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520), 0, 0L, "congnghemoi.jpeg", 0, 1 },
                    { 9, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520), 0, 0L, "phongsu.jpeg", 0, 1 },
                    { 10, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520), 0, 0L, "giaothong.jpeg", 0, 1 },
                    { 11, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520), 0, 0L, "chungkhoan.jpeg", 0, 1 },
                    { 12, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520), 0, 0L, "khoahocvn.jpeg", 0, 1 },
                    { 13, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520), 0, 0L, "the-thao1.jpeg", 0, 1 },
                    { 14, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530), 0, 0L, "newsid1.jpg", 0, 1 },
                    { 15, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530), 0, 0L, "newsid2.jpg", 0, 1 },
                    { 16, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530), 0, 0L, "newsid3.jpg", 0, 1 },
                    { 17, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530), 0, 0L, "newsid4.jpg", 0, 1 },
                    { 18, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530), 0, 0L, "newsid5.jpg", 0, 1 },
                    { 19, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530), 0, 0L, "newsid6.png", 0, 1 },
                    { 20, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530), 0, 0L, "newsid7.jpg", 0, 1 },
                    { 21, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530), 0, 0L, "newsid8.jpg", 0, 1 },
                    { 22, null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1540), 0, 0L, "newsid9.jpg", 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"), "ee24aece-7c5b-481e-aa4a-2d1ad5b2f0a0", "Admin", "Admin" },
                    { new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"), "930a4d9b-5b66-4189-b37b-2adcbe86d71f", "Subscriber", "Subscriber" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AvatarId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be01de"), 0, null, "1fafb62b-b5fb-4228-afe0-47573475fe0c", "bp.khuyen@hutech.edu.vn", true, false, null, "Bui Phu Khuyen", "BP.KHUYEN@HUTECH.EDU.VN", "khuyenpb", "AQAAAAEAACcQAAAAELD5/0MVgRU2JxgNAAD/GPHmsidIDqB3MDwJ09Quqx+2779Dysy+mtplGjxa4dqa/g==", null, false, "", false, "khuyenpb" },
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be02de"), 0, null, "4ef805b7-145f-48f4-aa94-400de4f3e8ed", "thanh26092000@gmail.com", true, false, null, "Le Xuan Thanh", "THANH26092000@GMAIL.COM", "LXThanh", "AQAAAAEAACcQAAAAEF7QLYit/I+zXlUr5yTbTe8ZoxXpkFls5R0YVhU3JJYZPT+s7kK7QOAawPtXWyFh9Q==", null, false, "", false, "LXThanh" },
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be03de"), 0, null, "daa2c32a-a56d-4e2b-b26b-a761538de1c8", "khanh200111@gmail.com", true, false, null, "Huynh Huu Khanh", "KHANH200111@GMAIL.COM", "hkhansh27", "AQAAAAEAACcQAAAAEDFDmXhjZLO4IAW3xwImbQwVWqRxYbXKkj41K5JWOa/rlIGb/8JrdQVXBdIIuyGTqw==", null, false, "", false, "hkhansh27" },
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be04de"), 0, null, "f1554fff-d2d4-455c-a765-ea53c91b0618", "hi@phucs.me", true, false, null, "To Hoang Phuc", "HI@PHUCS.ME", "HoangPhuc", "AQAAAAEAACcQAAAAEM5EY3N1904KTm/JubILe8bWgqMF+PuIlh+a3l7usvVD0J/wfhmEUMXOtwY8xXOYlg==", null, false, "", false, "HoangPhuc" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "DatePublished", "ImageLink", "LanguageId", "OfficialRating", "Publisher", "SocialBeliefs", "SourceCreate", "Timestamp", "Title", "UrlNews", "ViewCount" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://travelweekly.co.uk/images/cmstw/original/4/e/6/4/4/easid-453165-media-id-34528.jpg", "en", 0, "New York Times", 0.0, 1, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1560), "Kabul’s Sudden Fall to Taliban Ends U.S. Era in Afghanistan", "https://www.independent.co.uk/arts-entertainment/eurovision/the-rasmus-eurovision-2022-finland-b2077365.html", 0 },
                    { 2, new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://media.wltx.com/assets/WLTX/images/cd8afe4e-86f9-487f-b8b4-5e9313da807e/cd8afe4e-86f9-487f-b8b4-5e9313da807e_1140x641.jpg", "en", 0, "NBC News", 0.0, 1, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1560), "Texas high court blocks mask mandates in two of state's largest counties", "https://www.wltx.com/article/sports/clemson/101-15d947ca-db30-4440-b99a-a1e5a6f4ca35", 0 },
                    { 3, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://media.wltx.com/assets/WLTX/images/cd8afe4e-86f9-487f-b8b4-5e9313da807e/cd8afe4e-86f9-487f-b8b4-5e9313da807e_1140x641.jpg", "en", 2, null, 0.0, 1, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1560), "Hospitalizations of Americans under 50 have reached new pandemic highs", "https://www.wltx.com/article/sports/clemson/101-15d947ca-db30-4440-b99a-a1e5a6f4ca35", 0 },
                    { 5, new DateTime(2021, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://cdnimg.vietnamplus.vn/t620/uploaded/fsmsy/2021_07_26/phun_khu_khuan.jpg", "vi", 2, "TTXVN/Vietnam", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1560), "Thông tin TP.HCM dùng 5 trực thăng phun khử khuẩn là sai sự thật", "https://www.vietnamplus.vn/thong-tin-tphcm-su-dung-5-truc-thang-phun-khu-trung-la-sai-su-that/729372.vnp", 0 },
                    { 6, new DateTime(2021, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.baobaclieu.vn/uploads/image/2021/08/06/13b.jpg", "vi", 1, "Báo Bạc Liêu", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1560), "Mạnh tay xử lý hành vi đưa tin giả liên quan đến dịch Covid – 19", "https://www.baobaclieu.vn/quoc-phong-an-ninh/manh-tay-xu-ly-tin-gia-tin-sai-su-that-ve-dich-covid-19-tren-mang-72306.html", 0 },
                    { 7, new DateTime(2022, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://img.nhandan.com.vn/Files/Images/2022/05/12/Hai_truong_hop_lam_viec_voi_co_q-1652343768786.jpg", "vi", 1, "Báo Nhân Dân", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570), "Thông tin nguồn nước Thánh Thiên chữa được Covid-19 là sai sự thật", "https://nhandan.vn/factcheck/thong-tin-nguon-nuoc-thanh-thien-co-the-chua-covid-19-la-sai-su-that-696816/", 0 },
                    { 8, new DateTime(2021, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://file1.dangcongsan.vn/data/0/images/2021/10/01/vulinh/dfhgdfh.jpg?dpi=150&quality=100&w=780", "vi", 2, "DCSVN", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570), "Bạc Liêu: Mắc Covid-19 được “ưu ái điều trị tại nhà” là sai sự thật", "https://dangcongsan.vn/canh-bao-thong-tin-gia/bac-lieu-mac-covid-19-duoc-uu-ai-dieu-tri-tai-nha-la-sai-su-that-592693.html", 0 },
                    { 9, new DateTime(2021, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://file1.dangcongsan.vn/data/0/images/2021/09/21/vulinh/video-man-1632200081574.jpg?dpi=150&quality=100&w=780", "vi", 1, "DCSVN", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570), "“Chiến dịch tiêm vaccine ngừa Covid-19 cho trẻ em 12-15 tuổi ở xứ Anh bị tạm dừng” là không chính xác", "https://dangcongsan.vn/canh-bao-thong-tin-gia/chien-dich-tiem-vaccine-ngua-covid-19-cho-tre-em-12-15-tuoi-o-xu-anh-bi-tam-dung-la-khong-chinh-xac-591591.html", 0 },
                    { 10, new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_29/1-bai-3-1687.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570), "Săn 'lộc trời': Lội suối nhặt ốc, vào thủ phủ cá chình", "https://thanhnien.vn/san-loc-troi-loi-suoi-nhat-oc-vao-thu-phu-ca-chinh-post1406363.html", 0 },
                    { 11, new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2021_11_24/nghe-viet-1372.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570), "Nghề Việt - Nét Việt: Nghề trai Chuôn Ngọ", "https://thanhnien.vn/nghe-viet-net-viet-nghe-trai-chuon-ngo-post1404658.html", 0 },
                    { 12, new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2021_11_24/cao-su-7917.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570), "Nỗi lòng người cạo mủ cao su", "https://thanhnien.vn/noi-long-nguoi-cao-mu-cao-su-post1404643.html", 0 },
                    { 13, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/fsmxy/2021_11_27/drai-dlong-8588.png", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1580), "Khám phá thác ba nhánh hùng vĩ ít người biết giữa Tây Nguyên", "https://thanhnien.vn/kham-pha-thac-ba-nhanh-hung-vi-it-nguoi-biet-giua-tay-nguyen-post1405776.html", 0 },
                    { 14, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/fsmxy/2021_11_21/noi-hap-xoi-co-4376.png", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1580), "Chiếc nồi cổ ‘thần kỳ’ tạo ra món xôi độc đáo của người Nùng ở Đắk Lắk", "https://thanhnien.vn/chiec-noi-co-than-ky-tao-ra-mon-xoi-doc-dao-cua-nguoi-nung-o-dak-lak-post1403687.html", 0 },
                    { 15, new DateTime(2021, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_27/22b1-5885.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1580), "Những đứa con tìm về nguồn cội", "https://thanhnien.vn/nhung-dua-con-tim-ve-nguon-coi-post1405816.html", 0 },
                    { 16, new DateTime(2021, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2021_11_20/trien-lam-5546.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1580), "Mang chất Việt vào tranh in trên đất Mỹ", "https://thanhnien.vn/mang-chat-viet-vao-tranh-in-tren-dat-my-post1403198.html", 0 },
                    { 17, new DateTime(2021, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_13/22a1-5500.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1580), "Người phụ nữ Việt phát triển vật liệu phủ chống cháy ở Úc", "https://thanhnien.vn/nguoi-phu-nu-viet-phat-trien-vat-lieu-phu-chong-chay-o-uc-post1401084.html", 0 },
                    { 18, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_13/hinh-1-3868.png", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590), "Cảm hứng từ bữa ăn Việt của bà", "https://thanhnien.vn/cam-hung-tu-bua-an-viet-cua-ba-post1401081.html", 0 },
                    { 19, new DateTime(2021, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/rfnmf/2021_11_30/tau-metro-2-acxy-8311.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590), "Gặp bão, đoàn tàu metro trễ hẹn về TP.HCM", "https://thanhnien.vn/gap-bao-doan-tau-metro-tre-hen-ve-tp-hcm-post1406682.html", 0 },
                    { 20, new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/vjryqdxwp/2021_11_30/satthep-chihieu-uver-keev-9147.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590), "Xuất khẩu thép lần đầu cán mốc 10 tỉ USD", "https://thanhnien.vn/xuat-khau-thep-lan-dau-can-moc-10-ti-usd-post1406650.html", 0 },
                    { 21, new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_30/2a2-8280.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590), "TP.HCM khát vốn cho giao thông", "https://thanhnien.vn/tp-hcm-khat-von-cho-giao-thong-post1406453.html", 0 },
                    { 22, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wpxlcqjwq/2021_11_26/kieu-hoi-3276.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590), "Lượng kiều hối tăng mạnh kỷ lục", "https://thanhnien.vn/luong-kieu-hoi-tang-manh-ky-luc-post1405536.html", 0 },
                    { 23, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wpxlcqjwq/2021_11_24/chung-khoan-9665.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590), "Dòng vốn mạnh đưa chứng khoán lập đỉnh", "https://thanhnien.vn/dong-von-manh-dua-chung-khoan-lap-dinh-post1404799.html", 0 },
                    { 24, new DateTime(2021, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/bpcgvoiv/2021_11_30/a1-dtan-4946.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600), "Tin tức giáo dục đặc biệt 1.12: Dạy sử bằng nội dung cảm xúc hay sự kiện?", "https://thanhnien.vn/tin-tuc-giao-duc-dac-biet-1-12-day-su-bang-noi-dung-cam-xuc-hay-su-kien-post1406754.html", 0 },
                    { 25, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2020_08_31/tuyen-sinh_chbt.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600), "Hướng vào đại học phù hợp với điểm thi", "https://thanhnien.vn/huong-vao-dai-hoc-phu-hop-voi-diem-thi-post989845.html", 0 },
                    { 26, new DateTime(2021, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wobjuko/2021_11_20/anh-1-7862.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600), "Gặp người thầy… đẹp trai nhất trường mầm non!", "https://thanhnien.vn/gap-nguoi-thay-dep-trai-nhat-truong-mam-non-post1403127.html", 0 },
                    { 27, new DateTime(2021, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wobjuko/2021_06_05/3_cdyb.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600), "Thầy giáo dùng tiền khen thưởng ủng hộ Quỹ phòng chống Covid-19", "https://thanhnien.vn/thay-giao-dung-tien-khen-thuong-ung-ho-quy-phong-chong-covid-19-post1075098.html", 0 },
                    { 28, new DateTime(2021, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/mffsm/2021_09_29/0-1_pldi.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600), "Khoa Y ĐH Quốc gia TP.HCM xét tuyển bổ sung cả thí sinh tự do", "https://thanhnien.vn/khoa-y-dh-quoc-gia-tp-hcm-xet-tuyen-bo-sung-ca-thi-sinh-tu-do-post1116655.html", 0 },
                    { 29, new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_30/cyberpunk-4917.png", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600), "Bản nâng cấp Cyberpunk 2077 sẽ miễn phí cho chủ sở hữu PS4 và Xbox One", "https://thanhnien.vn/ban-nang-cap-cyberpunk-2077-se-mien-phi-cho-chu-so-huu-ps4-va-xbox-one-post1406595.html", 0 },
                    { 30, new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/dbeyxqxqrs/2021_11_30/1-8294.png", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1610), "Phi Vụ Triệu Đô tái kết hợp Free Fire trong phần đặc biệt: Phi Vụ Cuối Cùng tháng 12 này", "https://thanhnien.vn/phi-vu-trieu-do-tai-ket-hop-free-fire-trong-phan-dac-biet-phi-vu-cuoi-cung-thang-12-nay-post1406503.html", 0 },
                    { 31, new DateTime(2021, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_23/image0-500.png", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1610), "Việt Nam lọt Top 5 đội LMHT: Tốc Chiến thế giới", "https://thanhnien.vn/viet-nam-lot-top-5-doi-lmht-toc-chien-the-gioi-post1404103.html", 0 },
                    { 32, new DateTime(2021, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_22/1-3692.jpg", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1610), "Riot Games để lộ 4 địa điểm của Chung kết LMHT Thế giới 2022", "https://thanhnien.vn/riot-games-de-lo-4-dia-diem-cua-chung-ket-lmht-the-gioi-2022-post1403726.html", 0 },
                    { 33, new DateTime(2021, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_15/picture2-6769.png", "vi", 0, "Báo Thanh Niên", 0.0, 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1610), "Arcane giúp Vi và Jinx tăng vọt tỉ lệ được chọn trong LMHT", "https://thanhnien.vn/arcane-giup-vi-va-jinx-tang-vot-ti-le-duoc-chon-trong-lmht-post1401689.html", 0 }
                });

            migrationBuilder.InsertData(
                table: "TopicNews",
                columns: new[] { "TopicId", "Description", "Label", "LanguageId", "Tag", "ThumbTopic", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { 1, "Follow live as the Taliban seizes territory across Afghanistan in the wake of the U.S. withdrawal.", "breaking", "en", "afghanistan", null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1470), null },
                    { 2, "Best nonfiction features, in-depth stores and other long-form content from across the web.", "featured", "en", "in-depth", 2, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1470), null },
                    { 3, "Outbreak of respiratory virus that has killed over 1 million and infected 100 milion worldwide.", "featured", "en", "coronavirus", null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480), null },
                    { 4, "The top business and economic news from around the world with a focus on the United State.", "featured", "en", "top-business", null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480), null },
                    { 5, "Follow the presidential transition of Joe Biden, including policy plans, appointments and more.", "featured", "en", "biden-admin", null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480), null },
                    { 6, "Top stories from around the world with a focus on news not covered in other feeds.", "featured", "en", "top-news", null, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480), null },
                    { 7, "Follow important local news: politics, business, top events and more. Updated everything evening.", "featured", "en", "boston", 1, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480), null },
                    { 8, "Các thông tin về virut corona.", "breaking", "vi", "dịch bệnh", 3, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480), null },
                    { 9, "Cuộc sống của người Việt Trên toàn thế giới.", "normal", "vi", "người Việt Nam", 4, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490), null },
                    { 10, " Nền doanh nghiệp Việt Nam.", "normal", "vi", "Kinh tế", 5, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490), null }
                });

            migrationBuilder.InsertData(
                table: "TopicNews",
                columns: new[] { "TopicId", "Description", "Label", "LanguageId", "Tag", "ThumbTopic", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { 11, "Chọn trường nghề phù hợp với bản thân.", "normal", "vi", "học hành", 6, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490), null },
                    { 12, "Công nghệ mới trong game.", "normal", "vi", "Trò chơi", 7, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490), null },
                    { 13, " Sản phẩm công nghệ mới trong năm.", "featured", "vi", "Sản phẩm", 8, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490), null },
                    { 14, " Phóng sự đời sống thường nhật của người dân.", "normal", "vi", "Phóng sự", 9, new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490), null }
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
                    { 1, 2 },
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
                name: "IX_DetailNews_NewsId",
                table: "DetailNews",
                column: "NewsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailNews_ThumbNews",
                table: "DetailNews",
                column: "ThumbNews",
                unique: true,
                filter: "[ThumbNews] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Follow_UserId",
                table: "Follow",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_News_LanguageId",
                table: "News",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCommunity_LanguageId",
                table: "NewsCommunity",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCommunity_ThumbNews",
                table: "NewsCommunity",
                column: "ThumbNews",
                unique: true,
                filter: "[ThumbNews] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCommunity_UserId",
                table: "NewsCommunity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsInTopics_NewsId",
                table: "NewsInTopics",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

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
                name: "DetailNews");

            migrationBuilder.DropTable(
                name: "Follow");

            migrationBuilder.DropTable(
                name: "ForgotPassword");

            migrationBuilder.DropTable(
                name: "NewsCommunity");

            migrationBuilder.DropTable(
                name: "NewsInTopics");

            migrationBuilder.DropTable(
                name: "RefreshToken");

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
                name: "Version");

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

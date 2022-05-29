﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeNewsFilter.Data.Migrations
{
    public partial class UpdateDB : Migration
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
                name: "News",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    OfficialRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialBeliefs = table.Column<double>(type: "float", nullable: false),
                    isVote = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 5, 15, 11, 42, 34, 153, DateTimeKind.Local).AddTicks(8060)),
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
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 5, 15, 11, 42, 34, 153, DateTimeKind.Local).AddTicks(3370))
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
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 5, 15, 11, 42, 34, 152, DateTimeKind.Local).AddTicks(2370))
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
                    { 1, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960), 0, 0L, "covid.jpeg", 0, 1 },
                    { 2, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960), 0, 0L, "taliban.jpeg", 0, 1 },
                    { 3, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960), 0, 0L, "kinh-te-tg.jpeg", 0, 1 },
                    { 4, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960), 0, 0L, "ngvietnamchau.jpeg", 0, 1 },
                    { 5, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960), 0, 0L, "doanh-nghiep.jpeg", 0, 1 },
                    { 6, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960), 0, 0L, "chon truong.jpeg", 0, 1 },
                    { 7, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960), 0, 0L, "congnghegame.jpeg", 0, 1 },
                    { 8, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970), 0, 0L, "congnghemoi.jpeg", 0, 1 },
                    { 9, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970), 0, 0L, "phongsu.jpeg", 0, 1 },
                    { 10, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970), 0, 0L, "giaothong.jpeg", 0, 1 },
                    { 11, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970), 0, 0L, "chungkhoan.jpeg", 0, 1 },
                    { 12, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970), 0, 0L, "khoahocvn.jpeg", 0, 1 },
                    { 13, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970), 0, 0L, "the-thao1.jpeg", 0, 1 },
                    { 14, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970), 0, 0L, "newsid1.jpg", 0, 1 },
                    { 15, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970), 0, 0L, "newsid2.jpg", 0, 1 },
                    { 16, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980), 0, 0L, "newsid3.jpg", 0, 1 },
                    { 17, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980), 0, 0L, "newsid4.jpg", 0, 1 },
                    { 18, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980), 0, 0L, "newsid5.jpg", 0, 1 },
                    { 19, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980), 0, 0L, "newsid6.png", 0, 1 },
                    { 20, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980), 0, 0L, "newsid7.jpg", 0, 1 },
                    { 21, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980), 0, 0L, "newsid8.jpg", 0, 1 },
                    { 22, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980), 0, 0L, "newsid9.jpg", 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"), "4da19fbe-9dc2-432d-b0a7-b7042c86b7c6", "Admin", "Admin" },
                    { new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"), "f4d47067-d5f1-4e78-a125-80020220a9c6", "Subscriber", "Subscriber" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AvatarId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be01de"), 0, null, "fecbdb65-cf61-4f75-b989-3a28c7e6245c", "bp.khuyen@hutech.edu.vn", true, false, null, "Bui Phu Khuyen", "BP.KHUYEN@HUTECH.EDU.VN", "khuyenpb", "AQAAAAEAACcQAAAAEA6uGfstzJWATUYAFIRiN5ZmkhahLRbfvkteVVfQAHjXm9qeMTBp34nKWT9SL/k0AQ==", null, false, "", false, "khuyenpb" },
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be02de"), 0, null, "b8b872a6-5d80-499c-b537-e4c87cd2d718", "thanh26092000@gmail.com", true, false, null, "Le Xuan Thanh", "THANH26092000@GMAIL.COM", "LXThanh", "AQAAAAEAACcQAAAAEDVF3kiW7ZQInyygiPXcm9dSA2Ys80Ybai5P0PhxwV5UrLYUsueKApnFaxVsX4zfUA==", null, false, "", false, "LXThanh" },
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be03de"), 0, null, "1692921c-87ee-4594-b09d-bd347eee0153", "khanh200111@gmail.com", true, false, null, "Huynh Huu Khanh", "KHANH200111@GMAIL.COM", "hkhansh27", "AQAAAAEAACcQAAAAEAXOG8Fi1vpljX9ObTpFuAFYqKy67HZG8UBXXophm0dha/5UzDili66h5IO1OaVTnA==", null, false, "", false, "hkhansh27" },
                    { new Guid("69db714f-9576-45ba-b5b7-f00649be04de"), 0, null, "e146edde-66cf-44c1-b6cd-dbd0e3c203e8", "hi@phucs.me", true, false, null, "To Hoang Phuc", "HI@PHUCS.ME", "HoangPhuc", "AQAAAAEAACcQAAAAECHxt68zql8LOgYJZBI/VbtJPgfHgA1pTCBl1U54gwv1igTlgfi+FtHNzViTIK7mkg==", null, false, "", false, "HoangPhuc" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "DatePublished", "ImageLink", "LanguageId", "OfficialRating", "Publisher", "SocialBeliefs", "Source", "Timestamp", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://travelweekly.co.uk/images/cmstw/original/4/e/6/4/4/easid-453165-media-id-34528.jpg", "en", null, "New York Times", 0.0, "https://www.independent.co.uk/arts-entertainment/eurovision/the-rasmus-eurovision-2022-finland-b2077365.html", new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), "Kabul’s Sudden Fall to Taliban Ends U.S. Era in Afghanistan" },
                    { 2, new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://media.wltx.com/assets/WLTX/images/cd8afe4e-86f9-487f-b8b4-5e9313da807e/cd8afe4e-86f9-487f-b8b4-5e9313da807e_1140x641.jpg", "en", null, "NBC News", 0.0, "https://www.wltx.com/article/sports/clemson/101-15d947ca-db30-4440-b99a-a1e5a6f4ca35", new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), "Texas high court blocks mask mandates in two of state's largest counties" },
                    { 3, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "en", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), "Hospitalizations of Americans under 50 have reached new pandemic highs" },
                    { 4, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "en", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), "Hospitalizations of Americans under 50 have reached new pandemic highs" },
                    { 5, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), "Thông tin TP.HCM dùng 5 trực thăng phun khử khuẩn là sai sự thật" },
                    { 6, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), "Mạnh tay xử lý hành vi đưa tin giả liên quan đến dịch Covid – 19" },
                    { 7, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), "Thông tin nguồn nước Thánh Thiên chữa được Covid-19 là sai sự thật" },
                    { 8, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), "Bạc Liêu: Mắc Covid-19 được “ưu ái điều trị tại nhà” là sai sự thật" },
                    { 9, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), "“Chiến dịch tiêm vaccine ngừa Covid-19 cho trẻ em 12-15 tuổi ở xứ Anh bị tạm dừng” là không chính xác" },
                    { 10, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), "Săn 'lộc trời': Lội suối nhặt ốc, vào thủ phủ cá chình" },
                    { 11, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), "Nghề Việt - Nét Việt: Nghề trai Chuôn Ngọ" },
                    { 12, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), "Nỗi lòng người cạo mủ cao su" },
                    { 13, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), "Khám phá thác ba nhánh hùng vĩ ít người biết giữa Tây Nguyên" },
                    { 14, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), "Chiếc nồi cổ ‘thần kỳ’ tạo ra món xôi độc đáo của người Nùng ở Đắk Lắk" },
                    { 15, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), "Những đứa con tìm về nguồn cội" },
                    { 16, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), "Mang chất Việt vào tranh in trên đất Mỹ" },
                    { 17, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), "Người phụ nữ Việt phát triển vật liệu phủ chống cháy ở Úc" },
                    { 18, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), "Cảm hứng từ bữa ăn Việt của bà" },
                    { 19, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), "Gặp bão, đoàn tàu metro trễ hẹn về TP.HCM" },
                    { 20, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), "Xuất khẩu thép lần đầu cán mốc 10 tỉ USD" },
                    { 21, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), "TP.HCM khát vốn cho giao thông" },
                    { 22, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), "Lượng kiều hối tăng mạnh kỷ lục" },
                    { 23, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), "Dòng vốn mạnh đưa chứng khoán lập đỉnh" },
                    { 24, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), "Tin tức giáo dục đặc biệt 1.12: Dạy sử bằng nội dung cảm xúc hay sự kiện?" },
                    { 25, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), "Hướng vào đại học phù hợp với điểm thi" },
                    { 26, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), "Gặp người thầy… đẹp trai nhất trường mầm non!" },
                    { 27, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), "Thầy giáo dùng tiền khen thưởng ủng hộ Quỹ phòng chống Covid-19" },
                    { 28, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), "Khoa Y ĐH Quốc gia TP.HCM xét tuyển bổ sung cả thí sinh tự do" },
                    { 29, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), "Bản nâng cấp Cyberpunk 2077 sẽ miễn phí cho chủ sở hữu PS4 và Xbox One" },
                    { 30, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), "Phi Vụ Triệu Đô tái kết hợp Free Fire trong phần đặc biệt: Phi Vụ Cuối Cùng tháng 12 này" },
                    { 31, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), "Việt Nam lọt Top 5 đội LMHT: Tốc Chiến thế giới" },
                    { 32, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), "Riot Games để lộ 4 địa điểm của Chung kết LMHT Thế giới 2022" },
                    { 33, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vi", null, null, 0.0, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), "Arcane giúp Vi và Jinx tăng vọt tỉ lệ được chọn trong LMHT" }
                });

            migrationBuilder.InsertData(
                table: "TopicNews",
                columns: new[] { "TopicId", "Description", "Label", "LanguageId", "Tag", "ThumbTopic", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { 1, "Follow live as the Taliban seizes territory across Afghanistan in the wake of the U.S. withdrawal.", "breaking", "en", "afghanistan", null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7920), null },
                    { 2, "Best nonfiction features, in-depth stores and other long-form content from across the web.", "featured", "en", "in-depth", 2, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7920), null },
                    { 3, "Outbreak of respiratory virus that has killed over 1 million and infected 100 milion worldwide.", "featured", "en", "coronavirus", null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7920), null },
                    { 4, "The top business and economic news from around the world with a focus on the United State.", "featured", "en", "top-business", null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930), null },
                    { 5, "Follow the presidential transition of Joe Biden, including policy plans, appointments and more.", "featured", "en", "biden-admin", null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930), null },
                    { 6, "Top stories from around the world with a focus on news not covered in other feeds.", "featured", "en", "top-news", null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930), null },
                    { 7, "Follow important local news: politics, business, top events and more. Updated everything evening.", "featured", "en", "boston", 1, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930), null },
                    { 8, "Các thông tin về virut corona.", "Thế giới", "vi", "dịch bệnh", 3, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930), null },
                    { 9, "Cuộc sống của người Việt Trên toàn thế giới.", "Thế giới", "vi", "người Việt Nam", 4, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930), null }
                });

            migrationBuilder.InsertData(
                table: "TopicNews",
                columns: new[] { "TopicId", "Description", "Label", "LanguageId", "Tag", "ThumbTopic", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { 10, " Nền doanh nghiệp Việt Nam.", "TÀI CHÍNH - KINH DOANH", "vi", "Kinh tế", 5, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930), null },
                    { 11, "Chọn trường nghề phù hợp với bản thân.", "GIÁO DỤC", "vi", "học hành", 6, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7940), null },
                    { 12, "Công nghệ mới trong game.", "Trò chơi", "vi", "Trò chơi", 7, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7940), null },
                    { 13, " Sản phẩm công nghệ mới trong năm.", "Công Nghệ", "vi", "Sản phẩm", 8, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7940), null },
                    { 14, " Phóng sự đời sống thường nhật của người dân.", "Thời sự", "vi", "Phóng sự", 9, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7940), null }
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
                table: "DetailNews",
                columns: new[] { "DetailNewsId", "Alias", "Content", "NewsId", "ThumbNews" },
                values: new object[,]
                {
                    { 3, "the-independent", "The Rasmus Q&A: Meet Finland’s entry for Eurovision 2022 Delta variant are driving a surge in Covid-19 hospitalizations in the United States..", 3, 16 },
                    { 4, "a-lagging-vaccine", "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..", 4, 17 },
                    { 5, "theo-so-hien-nay", "Theo Sở TT&TT, hiện nay, trên mạng xã hội đang lan truyền thông tin “tối nay từ 11h40 không nên ra đường. Cửa ra vào và cửa sổ nên được đóng lại khi 5 máy bay trực thăng phun chất khử trùng vào không khí để diệt trừ Coronavirus”. Trao đổi với VietNamNet, ông Lâm Đình Thắng, Giám đốc Sở TT&TT cho hay, Bộ Tư lệnh TP.HCM khẳng định, thông tin trên hoàn toàn sai sự thật. Lực lượng quân đội phun khử khuẩn trên địa bàn TP.HCM Trước đó, sáng 23/7, Bộ Tư lệnh TP.HCM phối hợp với Lữ đoàn 87 Binh Chủng hóa học, Tiểu đoàn Phòng hóa 38 Quân khu 7 cùng với lực lượng vũ trang TP và 21 quận, huyện và TP Thủ Đức đồng loạt mở đợt cao điểm phun thuốc khử khuẩn phòng, chống dịch Covid-19 quy mô lớn nhất từ trước tới nay trên địa bàn TP, trong thời gian 7 ngày. Mỗi ngày sẽ có 20 lượt xe tham gia phun thuốc khử khuẩn phòng, chống Covid-19. Theo Hồ Văn/Báo điện tử VietnamNet https://vietnamnet.vn/vn/thoi-su/thong-tin-tp-hcm-dung-5-truc-thang-phun-khu-khuan-la-sai-su-that-759937.html", 5, 18 },
                    { 6, "dai-dich-covid-bung-phat", "Đại dịch Covid-19 bùng phát trở lại, gây chồng chất thêm khó khăn cho doanh nghiệp, người dân, cũng vì thế mà thông tin về diễn biến đại dịch trở thành mối quan tâm hàng đầu của toàn xã hội. Bên cạnh những thông tin chính xác, tích cực, giúp mọi người nâng cao tinh thần cảnh giác, chung tay phòng chống dịch bệnh, cũng có không ít thông tin sai lệch, thiếu kiểm chứng trên mạng xã hội, gây hoang mang dư luận, tác động xấu đến tình hình an ninh trật tự trên địa bàn. http://brt.vn/thoi-su/dich-viem-phoi-virus-corona/202008/manh-tay-xu-ly-hanh-vi-dua-tin-gia-lien-quan-den-dich-covid-19-8179089/", 6, 19 },
                    { 7, "dang-tai-thong-tin-sai", "Đăng tải thông tin sai sự thật trên trang Facebook cá nhân: “nguồn nước Thánh Thiên sẽ cứu chữa rất nhiều bệnh… đặc biệt là Covid-19” gây hoang mang dư luận, bà N.T.T (sinh năm 1969, ngụ huyện Bảo Lâm, Lâm Đồng) đã bị cơ quan chức năng xử phạt 5 triệu đồng. Làm việc với Cơ quan công an, bà N.T.T thừa nhận đã đăng tải thông tin sai sự thật. Thông tin lan truyền Qua công tác bảo đảm an ninh mạng, Phòng An ninh mạng và phòng, chống tội phạm sử dụng công nghệ cao (PA05), Công an tỉnh Lâm Đồng phát hiện bà N.T.T đăng tải trên Facebook cá nhân “T.A.P” bài viết có nội dung: “Nguồn nước Thánh Thiên này sẽ cứu chữa rất nhiều bệnh, đặc biệt là Covid-19…”, kèm theo hình ảnh hai chai nước ghi dòng chữ “nguồn Thánh Thiên”. Kiểm chứng Làm việc với cơ quan công an, bà T trình bày, “nguồn nước thánh thiên” có nguồn gốc từ nhóm tự xưng có tên “trừ quỷ Bảo Lộc” (địa chỉ ở 53/5 Hồ Tùng Mậu, TP Bảo Lộc, Lâm Đồng). Trong quá trình tham gia nhóm, bà T và các thành viên cho rằng, “qua việc cầu nguyện, chữa lành, uống nước thánh thiên thì có thể chữa khỏi Covid-19”, nên bà T đã đăng tải lên Facebook cá nhân. Bà T thừa nhận, “nước thánh thiên” không phải thuốc chữa bệnh Covid-19, không được các cơ quan chức năng cấp giấy phép; thông tin do bà T đăng tải là sai sự thật, không kiểm chứng trước khi đăng tải. Hành vi của bà N.T.T vi phạm pháp luật, quy định tại Nghị định số 15/2020/NĐ-CP, ngày 3/2/2020 của Chính phủ, quy định xử phạt vi phạm hành chính trong lĩnh vực bưu chính, viễn thông, tần số vô tuyến điện, công nghệ thông tin và giao dịch điện tử. Theo PA05 Công an tỉnh Lâm Đồng, nhóm tự xưng “trừ quỷ Bảo Lộc” có hoạt động chữa bệnh nhưng không có giấy phép. Ngày 17/9/2021, ông T.V.L.T.Q, một trong những người đứng đầu nhóm này, sử dụng nhà riêng tại địa chỉ nêu trên làm nơi chữa bệnh trái phép, đã bị UBND TP Bảo Lộc xử phạt vi phạm hành chính 45 triệu đồng, về hành vi “chữa bệnh mà không có giấy phép hoạt động chữa bệnh”. Theo Bảo Văn/Báo Nhân dân điện tử https://nhandan.vn/factcheck/thong-tin-nguon-nuoc-thanh-thien-chua-duoc-covid-19-la-sai-su-that-669233/", 7, 20 },
                    { 8, "truong-hop-ba-Nguyen-Huynh-Nhu", "Thông tin về trường hợp bà Nguyễn Huỳnh Như (Giám đốc Công ty mỹ phẩm Đông Anh ở TP Bạc…", 8, 21 },
                    { 9, "hang-nghin-nguoi-xem", "Hàng nghìn người đã xem 1 video trực tuyến, trong đó xuất hiện 1 người đàn ông nói rằng chiến…", 9, 22 },
                    { 10, "dan-toc", "Để “săn” ốc đá và cá chình, 2 sản vật ngon bậc nhất ở núi rừng Quảng Trị.", 10, null },
                    { 11, "lang-duy-nhat", "Chuôn Ngọ là làng duy nhất cung cấp nguyên liệu các loại vỏ trai, ốc cho cả nước để làm đồ cẩn, khảm, thủ công mỹ nghệ.", 11, null },
                    { 12, "nguoi-bat-dau-len-giuong", "Khi mọi người bắt đầu lên giường đi ngủ, thì một ngày làm việc của công nhân cạo mủ cao su bắt đầu.", 12, null },
                    { 13, "thac-drai-dlong", "Thác Drai Dlông với dòng chảy mạnh mẽ quanh năm giữa núi rừng là điểm đến không thể bỏ qua của những ai muốn khám phá Tây Nguyên.", 13, null },
                    { 14, "mon-an-xoi", "Xôi là món ăn được rất nhiều người ưa thích vì dễ ăn và cách làm khá đơn giản, thế nhưng tại gia đình bà Nông Thị Mai.", 14, null },
                    { 15, "nguoi-viet-cau-chuyen", "Câu chuyện của hai anh em sống tại TP.Liverpool được kể lại trong loạt phim tài liệu Nail Bar Boys do Đài BBC khởi chiếu tuần qua.", 15, null },
                    { 16, "de-to-chuc-thanh-cong", "Để tổ chức thành công triển lãm cá nhân đầu tiên tại Mỹ, họa sĩ tranh in Mai Trần đã trải qua một quá trình dài với nhiều gian nan, thử thách.", 16, null },
                    { 17, "mot-nu-chien-si-nguoi-viet", "Một nữ tiến sĩ người Việt được vinh danh là chuyên gia vật liệu hàng đầu tại Úc nhờ góp phần ứng phó cháy rừng tại nước này.", 17, null },
                    { 18, "nhung-ky-uc-ve-nguoi-ba", "Những ký ức về người bà quá cố và các món ăn Việt mà bà chuẩn bị cho gia đình khi xưa đã dẫn dắt đầu bếp David Huynh.", 18, null },
                    { 19, "bon-doan-tau-tuyen", "Bốn đoàn tàu tuyến metro số 1 (tuyến Bến Thành - Suối Tiên) dự kiến từ Nhật Bản về TP.HCM cuối tháng 11 và đầu tháng 12.", 19, null },
                    { 20, "so-lieu-thong-ke-tong-cuc", "Số liệu công bố từ Tổng cục Thống kê cho thấy 11 tháng năm 2021, Việt Nam xuất khẩu đạt tổng trị giá 299,67 tỉ USD.", 20, null },
                    { 21, "giao-thong-TPHCM", "UBND TP.HCM vừa có văn bản khẩn gửi Bộ Kế hoạch - Đầu tư liên quan đến dự kiến phương án phân bổ vốn đầu tư công năm 2022 nguồn vốn ngân sách T.Ư.", 21, null },
                    { 22, "tai-chinh-2021", "Dự ước năm 2021, lượng kiều hối chuyển về VN sẽ đạt mức kỷ lục 18,1 tỉ USD, bất chấp dịch Covid-19.", 22, null },
                    { 23, "chung-khoan-sut-giam", "Tiền gửi tiết kiệm sụt giảm trong khi dòng vốn tham gia vào thị trường chứng khoán ngày càng tăng.", 23, null },
                    { 24, "day-mon-lich-su", "Dạy học môn lịch sử trong trường phổ thông như thế nào để học sinh không chán là vấn đề luôn luôn mới.", 24, null },
                    { 25, "nhung-luu-y-gi-cho-thi-sinh", "Những lưu ý gì cho thí sinh để vào được đúng ngành nghề yêu thích, phù hợp với điểm số, là vấn đề mà rất nhiều thí sinh hiện đang băn khoăn.", 25, null },
                    { 26, "tu-mot-chang-tho-xay", "Từ một chàng thợ xây thích chơi đùa cùng trẻ em, thầy giáo Nguyễn Hồ Tây Phương đã trở thành người thầy hiếm hoi dấn thân mình với nghề dạy dỗ trẻ mầm non.", 26, null },
                    { 27, "thay-Nguyen-Viet-Tuoc", "Thầy Nguyễn Viết Tước đã được các cấp từ trung ương đến địa phương khen thưởng hơn 7 triệu đồng.", 27, null },
                    { 28, "khoa-y-dhquoc-gia", "Khoa Y ĐH Quốc gia TP.HCM thông báo xét tuyển bổ sung 3 ngành ĐH hệ chính quy, trong đó có ngành y khoa.", 28, null },
                    { 29, "ban-nang-cap-moi", "Bản nâng cấp mới sẽ khả dụng vào năm 2022 và hoàn toàn miễn phí cho chủ sở hữu các thiết bị PS4 và Xbox One.", 29, null },
                    { 30, "phi-vu-trieu-do", "Phi Vụ Triệu Đô bất ngờ quay trở lại Đảo Quân Sự Free Fire lần thứ hai với phần đặc biệt.", 30, null },
                    { 31, "giai-dau-mang-quy-mo", "Giải đấu mang quy mô quốc tế đầu tiên của LMHT: Tốc Chiến vừa kết thúc tại Singapore và một đội tuyển của Việt Nam vào Top 5-6.", 31, null },
                    { 32, "ten-cua-4-thanh-pho", "Tên của 4 thành phố dự kiến tổ chức giải Chung kết Thế giới 2022 bộ môn eSport Liên Minh Huyền Thoại vô tình bị lộ trong một video thông báo.", 32, null },
                    { 33, "bo-phim-hoat-hinh", "Bộ phim hoạt hình mang tên Arcane về thế giới trong Liên Minh Huyền Thoại đang nhận đánh giá tốt.", 33, null }
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
                    { 12, 9 }
                });

            migrationBuilder.InsertData(
                table: "NewsInTopics",
                columns: new[] { "NewsId", "TopicId" },
                values: new object[,]
                {
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
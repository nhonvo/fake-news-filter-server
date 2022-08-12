using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeNewsFilter.Data.Migrations
{
    public partial class AddFeedbackTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 407, DateTimeKind.Local).AddTicks(9470),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 156, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "Version",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 409, DateTimeKind.Local).AddTicks(5090),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 158, DateTimeKind.Local).AddTicks(2970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "TopicNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 405, DateTimeKind.Local).AddTicks(8200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 154, DateTimeKind.Local).AddTicks(6470));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 408, DateTimeKind.Local).AddTicks(4380),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 157, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 406, DateTimeKind.Local).AddTicks(8550),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 155, DateTimeKind.Local).AddTicks(6730));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePublished",
                table: "NewsCommunity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 408, DateTimeKind.Local).AddTicks(8850),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 157, DateTimeKind.Local).AddTicks(6990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 406, DateTimeKind.Local).AddTicks(3410),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 155, DateTimeKind.Local).AddTicks(1660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Media",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 407, DateTimeKind.Local).AddTicks(7130),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 156, DateTimeKind.Local).AddTicks(5710));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 408, DateTimeKind.Local).AddTicks(8210),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 157, DateTimeKind.Local).AddTicks(6300));

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 409, DateTimeKind.Local).AddTicks(5360))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedback_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId");
                    table.ForeignKey(
                        name: "FK_Feedback_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5000));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5000));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5000));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5000));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5030));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5030));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5030));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5050));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5060));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5060));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5060));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5060));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5060));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5080));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5080));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5080));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5080));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5090));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5090));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5090));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5090));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5090));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5100));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5100));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5100));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5100));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5100));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5110));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5110));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5110));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5110));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5110));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5110));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(5120));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "50f1d854-c5fb-4d39-9286-295a03e601d8");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "42d27b5c-7541-419c-93b7-6a782389cf05");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4960));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4970));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4970));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4970));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4970));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4970));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 9, 6, 53, 416, DateTimeKind.Local).AddTicks(4990));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e7c49078-989a-4f21-81dc-f8d503da9669", "AQAAAAEAACcQAAAAEL9smXDeNb9M/a3aLHavLSlhwy29CtQcW3pM3ZqlMiAi2oHkryxvVwsDFoeNCIpo+w==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "db6470c9-9d0c-49ea-8723-c8729ac31b82", "AQAAAAEAACcQAAAAEDhoRZ+O+3MzgupQ/HFVm3xsBiqHjewQIJsxdyqoYYC/CBfez33tgTXWsCp2Mm2+jg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ae9cf141-8c9c-4a7b-9b6f-d88b6445ea6c", "AQAAAAEAACcQAAAAEJWWOE9mVmDuoWikEXODjIz31YyOlWH5eV8vcJKoaxUMLI8U2K5kZ7C+OsnkuCjH9w==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2e4e5951-7195-401e-92f6-8b5250b06292", "AQAAAAEAACcQAAAAEByB12/9WDn9AAFfiZK6oHCxmmZRHgdSdj1m1Rk4PJtJcO/KCgDh0+WZrusQeqnDVA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_NewsId",
                table: "Feedback",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserId",
                table: "Feedback",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 156, DateTimeKind.Local).AddTicks(7970),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 407, DateTimeKind.Local).AddTicks(9470));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "Version",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 158, DateTimeKind.Local).AddTicks(2970),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 409, DateTimeKind.Local).AddTicks(5090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "TopicNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 154, DateTimeKind.Local).AddTicks(6470),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 405, DateTimeKind.Local).AddTicks(8200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 157, DateTimeKind.Local).AddTicks(2640),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 408, DateTimeKind.Local).AddTicks(4380));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 155, DateTimeKind.Local).AddTicks(6730),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 406, DateTimeKind.Local).AddTicks(8550));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePublished",
                table: "NewsCommunity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 157, DateTimeKind.Local).AddTicks(6990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 408, DateTimeKind.Local).AddTicks(8850));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 155, DateTimeKind.Local).AddTicks(1660),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 406, DateTimeKind.Local).AddTicks(3410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Media",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 156, DateTimeKind.Local).AddTicks(5710),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 407, DateTimeKind.Local).AddTicks(7130));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 8, 15, 3, 50, 157, DateTimeKind.Local).AddTicks(6300),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 9, 6, 53, 408, DateTimeKind.Local).AddTicks(8210));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1510));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1520));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1540));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1580));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1580));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1580));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1580));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1580));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1610));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1610));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1610));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1610));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "ee24aece-7c5b-481e-aa4a-2d1ad5b2f0a0");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "930a4d9b-5b66-4189-b37b-2adcbe86d71f");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1470));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1470));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 8, 15, 3, 50, 165, DateTimeKind.Local).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1fafb62b-b5fb-4228-afe0-47573475fe0c", "AQAAAAEAACcQAAAAELD5/0MVgRU2JxgNAAD/GPHmsidIDqB3MDwJ09Quqx+2779Dysy+mtplGjxa4dqa/g==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4ef805b7-145f-48f4-aa94-400de4f3e8ed", "AQAAAAEAACcQAAAAEF7QLYit/I+zXlUr5yTbTe8ZoxXpkFls5R0YVhU3JJYZPT+s7kK7QOAawPtXWyFh9Q==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "daa2c32a-a56d-4e2b-b26b-a761538de1c8", "AQAAAAEAACcQAAAAEDFDmXhjZLO4IAW3xwImbQwVWqRxYbXKkj41K5JWOa/rlIGb/8JrdQVXBdIIuyGTqw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f1554fff-d2d4-455c-a765-ea53c91b0618", "AQAAAAEAACcQAAAAEM5EY3N1904KTm/JubILe8bWgqMF+PuIlh+a3l7usvVD0J/wfhmEUMXOtwY8xXOYlg==" });
        }
    }
}

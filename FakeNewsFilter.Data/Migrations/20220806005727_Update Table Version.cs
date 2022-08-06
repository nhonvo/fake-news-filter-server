using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeNewsFilter.Data.Migrations
{
    public partial class UpdateTableVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 619, DateTimeKind.Local).AddTicks(4640),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.AlterColumn<string>(
                name: "VersionNumber",
                table: "Version",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "Version",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 623, DateTimeKind.Local).AddTicks(8410),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 663, DateTimeKind.Local).AddTicks(950));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "TopicNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 616, DateTimeKind.Local).AddTicks(7060),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 659, DateTimeKind.Local).AddTicks(2590));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 622, DateTimeKind.Local).AddTicks(3670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(9890));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 618, DateTimeKind.Local).AddTicks(490),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 660, DateTimeKind.Local).AddTicks(3400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePublished",
                table: "NewsCommunity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 623, DateTimeKind.Local).AddTicks(410),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 662, DateTimeKind.Local).AddTicks(4760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 617, DateTimeKind.Local).AddTicks(3810),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 659, DateTimeKind.Local).AddTicks(7990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Media",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 619, DateTimeKind.Local).AddTicks(1650),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 622, DateTimeKind.Local).AddTicks(9510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 662, DateTimeKind.Local).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8780));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8780));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8780));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8810));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8840));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8840));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8890));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8890));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8890));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8890));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8890));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8900));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8900));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8900));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8900));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8900));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8910));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8910));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "6b35f1c9-3328-4beb-96c9-f43aa7e2fe0e");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "37dd105b-c4ee-4539-9ba8-022373fffd84");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8730));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8730));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8750));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8750));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8750));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8750));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8760));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8760));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 6, 7, 57, 26, 632, DateTimeKind.Local).AddTicks(8760));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1a75d590-5722-4adb-8efc-940cd80f2539", "AQAAAAEAACcQAAAAEOXoGQxqt6PQ7MxFo/Zh6r1pN4YWlDjSRsurH4ffFbrulgr/E59NtBvHuen2+/cIPA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0b26ea15-f697-4ca1-a0b6-e21c3fbf9728", "AQAAAAEAACcQAAAAEP8BdZ3wkg7ulm7SwShQBf2kKo+ke+ZxjBLIWM6RJhqD3U7S7tKqTtolEevQRLKjAw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f050a84c-0f47-4f77-b83f-2ce50445f456", "AQAAAAEAACcQAAAAEJE0DPxyEXynvUbaxII7v9J+lcAAeI3264vL/lWfLO+LBI3N2l8X4GL4eVadJQm0Xw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ab7a690d-e311-45fe-b40b-c4a099998f4c", "AQAAAAEAACcQAAAAEB194G6wK0Uif6UFRNQkhnv7Vu9KHzTBipYrzoGhARrC7nYs2mXGKyDZyUJ2vSEv+A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(5070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 619, DateTimeKind.Local).AddTicks(4640));

            migrationBuilder.AlterColumn<float>(
                name: "VersionNumber",
                table: "Version",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "Version",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 663, DateTimeKind.Local).AddTicks(950),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 623, DateTimeKind.Local).AddTicks(8410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "TopicNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 659, DateTimeKind.Local).AddTicks(2590),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 616, DateTimeKind.Local).AddTicks(7060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(9890),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 622, DateTimeKind.Local).AddTicks(3670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 660, DateTimeKind.Local).AddTicks(3400),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 618, DateTimeKind.Local).AddTicks(490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePublished",
                table: "NewsCommunity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 662, DateTimeKind.Local).AddTicks(4760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 623, DateTimeKind.Local).AddTicks(410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 659, DateTimeKind.Local).AddTicks(7990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 617, DateTimeKind.Local).AddTicks(3810));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Media",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(2660),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 619, DateTimeKind.Local).AddTicks(1650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 662, DateTimeKind.Local).AddTicks(4070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 6, 7, 57, 26, 622, DateTimeKind.Local).AddTicks(9510));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1790));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1800));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1820));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1820));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1850));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1850));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1850));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1850));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1850));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1860));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "c5ff33f9-c265-46b7-b8d2-b9eaef76351b");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "df8f1343-8385-4353-b214-bb8788f27ba5");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1750));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1750));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1750));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1750));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5aa79af9-794d-4f12-8d26-4fedbf17bc1d", "AQAAAAEAACcQAAAAEM/Dwa48CKgjYgDAnivKvfe0JfO9EKwVIbgIDLBxw6inwdKZ2VgRX1wmk7lk23uQCw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "742f7e80-1367-49d6-8169-d765c8118d72", "AQAAAAEAACcQAAAAEKzasFwLbjUjFTJffCfmJnt30I7Nfd2msx89PbFLY4XEkpAhgBiKLu6SkD1ocnWS7Q==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0db3421f-fb7f-4ee8-bfe8-273612783c77", "AQAAAAEAACcQAAAAEMYNdd7AbYzkHhbf6GVgZc3zjyWvwPJUc00eRSXIYZliUj6ceweeoZJY6+brwsd2jg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f1cba650-8f30-4008-b1d6-a765405cca49", "AQAAAAEAACcQAAAAEINcQbf6Esv+XzaNLbuUKwjJBc7wVpkV+c+LTbwutBMwVJ62HaOMrxh8yvGZPc+xdQ==" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeNewsFilter.Data.Migrations
{
    public partial class AddDescNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 618, DateTimeKind.Local).AddTicks(3750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 223, DateTimeKind.Local).AddTicks(6540));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "Version",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 620, DateTimeKind.Local).AddTicks(110),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 225, DateTimeKind.Local).AddTicks(6020));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "TopicNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 616, DateTimeKind.Local).AddTicks(710),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 221, DateTimeKind.Local).AddTicks(4660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 618, DateTimeKind.Local).AddTicks(8880),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 224, DateTimeKind.Local).AddTicks(1350));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 617, DateTimeKind.Local).AddTicks(2070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 222, DateTimeKind.Local).AddTicks(5770));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePublished",
                table: "NewsCommunity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 619, DateTimeKind.Local).AddTicks(3780),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 224, DateTimeKind.Local).AddTicks(9820));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 616, DateTimeKind.Local).AddTicks(6460),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 222, DateTimeKind.Local).AddTicks(590));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Media",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 618, DateTimeKind.Local).AddTicks(1400),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 223, DateTimeKind.Local).AddTicks(4290));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Feedback",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 620, DateTimeKind.Local).AddTicks(1810),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 225, DateTimeKind.Local).AddTicks(7670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 619, DateTimeKind.Local).AddTicks(3180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 224, DateTimeKind.Local).AddTicks(9120));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2670));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2670));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2670));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2670));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2700));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2700));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2700));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2700));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2710));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2710));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2710));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2710));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2710));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2720));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2720));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2720));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2720));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2720));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2720));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2730));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2730));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2730));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2730));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2730));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "4939490f-a606-4b78-af1c-ae12ca5dad4f");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "42f65ab2-01a1-417e-a036-831783b08898");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2580));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2590));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2590));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2610));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2610));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2610));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2610));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 21, 10, 32, 22, 627, DateTimeKind.Local).AddTicks(2610));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0cba16c4-3dc7-4045-bf5d-75eccf2ff61a", "AQAAAAEAACcQAAAAEO1lFGGNR0h6gSWmYWQvF+eHjX8elqyt8Zyi5xjHRoElN1jBCHv3PbXWbxkYZ4HT2Q==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9821e1d5-6e77-4616-af5f-bd67a6ed0056", "AQAAAAEAACcQAAAAEMxt+ITlOhsiZatSk/PvForrH/L3MFkCLYFJgUxj9iftQv6lmICkHFdjquC827tc6Q==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "49236e07-8e68-483e-8819-31bfa3e64432", "AQAAAAEAACcQAAAAEPbGoe5Rp8Aeb45YBuEk5rVLDwIsKlgzxSH21WsqQg8jj527xF+Jom8caoJ2f0EIpg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "47397b5f-7128-4c32-9cf0-f6f030af2b8b", "AQAAAAEAACcQAAAAEINhHQrhuPTArG1/JbPOqSWssJpBE6gWSO9IYfk1nfnyLJ5SAz/0CLxU10+ZDBP4Sw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 26);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "News");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 223, DateTimeKind.Local).AddTicks(6540),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 618, DateTimeKind.Local).AddTicks(3750));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "Version",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 225, DateTimeKind.Local).AddTicks(6020),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 620, DateTimeKind.Local).AddTicks(110));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "TopicNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 221, DateTimeKind.Local).AddTicks(4660),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 616, DateTimeKind.Local).AddTicks(710));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 224, DateTimeKind.Local).AddTicks(1350),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 618, DateTimeKind.Local).AddTicks(8880));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 222, DateTimeKind.Local).AddTicks(5770),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 617, DateTimeKind.Local).AddTicks(2070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePublished",
                table: "NewsCommunity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 224, DateTimeKind.Local).AddTicks(9820),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 619, DateTimeKind.Local).AddTicks(3780));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 222, DateTimeKind.Local).AddTicks(590),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 616, DateTimeKind.Local).AddTicks(6460));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Media",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 223, DateTimeKind.Local).AddTicks(4290),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 618, DateTimeKind.Local).AddTicks(1400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Feedback",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 225, DateTimeKind.Local).AddTicks(7670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 620, DateTimeKind.Local).AddTicks(1810));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 12, 16, 34, 59, 224, DateTimeKind.Local).AddTicks(9120),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 21, 10, 32, 22, 619, DateTimeKind.Local).AddTicks(3180));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7340));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7350));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7350));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7350));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7350));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7350));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7350));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7350));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7350));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7390));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "bba260c3-e298-4822-ab0e-a0123702cd5d");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "257b5145-b2da-41b7-8587-75279334a36d");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7300));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7310));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7310));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7310));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7310));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7330));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7330));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 8, 12, 16, 34, 59, 232, DateTimeKind.Local).AddTicks(7330));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a96eb8b2-2a6b-4013-87df-5b7c265f7af3", "AQAAAAEAACcQAAAAEOqkl69zCMUToDS4x7DBSztIQ+urpORmvxZJvyNvd9X1gbB8YFAZ5IK+oUmYstep1A==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6f928259-ff58-4386-b0ba-b1b97ee8b8b7", "AQAAAAEAACcQAAAAEMG7OS5WzDiZoo3b+yOWWTqXHc/KwM9Tax0F1CtV+jTSsy3amoelQzbdTsIJ+e/A0A==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "618d8d13-38a3-4daa-b6c3-01e2361bd77d", "AQAAAAEAACcQAAAAELImTV9d8dKNF0QCCgzSL612cFIM6Uujh1VAlWOeqSvJvmFGBXkFbkRvDiDAh7nFCg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "527976ca-a7ed-4d0e-9e08-7070b5ee4623", "AQAAAAEAACcQAAAAEAlfWjK/9y+YpEMqCY0C27plADl5lCeL3eqEUC6YE6QODkFK45DlmqFCMOWdL2QW6g==" });
        }
    }
}

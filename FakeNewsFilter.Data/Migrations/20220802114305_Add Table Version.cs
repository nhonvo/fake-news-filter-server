using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeNewsFilter.Data.Migrations
{
    public partial class AddTableVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(5070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 851, DateTimeKind.Local).AddTicks(2180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "TopicNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 659, DateTimeKind.Local).AddTicks(2590),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(9890),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 851, DateTimeKind.Local).AddTicks(6840));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Story",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Source",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 660, DateTimeKind.Local).AddTicks(3400),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 848, DateTimeKind.Local).AddTicks(5300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePublished",
                table: "NewsCommunity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 662, DateTimeKind.Local).AddTicks(4760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 659, DateTimeKind.Local).AddTicks(7990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Media",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(2660),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 662, DateTimeKind.Local).AddTicks(4070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Version",
                columns: table => new
                {
                    VersionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<float>(type: "real", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Platform = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 663, DateTimeKind.Local).AddTicks(950)),
                    ReleaseTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isRequired = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version", x => x.VersionId);
                });

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
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "breaking", new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "normal", new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "normal", new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "normal", new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "normal", new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "featured", new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "normal", new DateTime(2022, 8, 2, 18, 43, 4, 670, DateTimeKind.Local).AddTicks(1770) });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Version");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Source");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Comment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 851, DateTimeKind.Local).AddTicks(2180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "TopicNews",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 659, DateTimeKind.Local).AddTicks(2590));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 851, DateTimeKind.Local).AddTicks(6840),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(9890));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 848, DateTimeKind.Local).AddTicks(5300),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 660, DateTimeKind.Local).AddTicks(3400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePublished",
                table: "NewsCommunity",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 662, DateTimeKind.Local).AddTicks(4760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "News",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 659, DateTimeKind.Local).AddTicks(7990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Media",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 661, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 2, 18, 43, 4, 662, DateTimeKind.Local).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5270));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5270));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5270));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5300));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5300));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5300));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5320));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5320));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5320));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5330));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5330));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5330));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5330));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5330));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "d7371a9e-8106-4af6-8454-4511c9c859fc");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "745c71f5-9543-4cc3-ba48-d136d4c7bda3");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5230));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "Thế giới", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "Thế giới", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "TÀI CHÍNH - KINH DOANH", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "GIÁO DỤC", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "Trò chơi", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "Công Nghệ", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                columns: new[] { "Label", "Timestamp" },
                values: new object[] { "Thời sự", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "95c59485-6ab6-4a36-bc35-875ff5ae91bb", "AQAAAAEAACcQAAAAEIvwIWxaQXjWlK9EuSmHNMyS9o1xWH+gChDpHHFB7zPmoV3pQvdTsvwn5EscgLEnJQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "55321bac-6701-49a2-a5cb-195ee04cc206", "AQAAAAEAACcQAAAAEHse8vc3SHJMnizv2rKfUrFminFH9b5G6fC5j1f9DBOqqz/mAVcv3O4F+IRB/jGWaw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9023323c-a9bb-4194-a4c3-d5414b291448", "AQAAAAEAACcQAAAAEOZAbDEVtU+hpk+EL2BmPJmUmZXdq1FYWOHqkgvIeDBvTyTALj97vq3C+tILmD0EyQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "64f17984-d3d5-4f69-8307-d8d265440c0a", "AQAAAAEAACcQAAAAEDHemfJQF820r+qvpWJO0M7GdoLkC5WgAer0U+VwdA6LPMeC/VGEbCiuj3nr8vNHUw==" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class UpdateNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialBeliefs",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "PostURL",
                table: "News",
                newName: "Content");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 6, 15, 13, 37, 731, DateTimeKind.Local).AddTicks(3690),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 5, 21, 51, 28, 236, DateTimeKind.Local).AddTicks(9066));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 6, 15, 13, 37, 672, DateTimeKind.Local).AddTicks(2280),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 5, 21, 51, 28, 226, DateTimeKind.Local).AddTicks(5631));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 11, 6, 15, 13, 37, 781, DateTimeKind.Local).AddTicks(4630));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 11, 6, 15, 13, 37, 781, DateTimeKind.Local).AddTicks(6370));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                columns: new[] { "Content", "Timestamp" },
                values: new object[] { "Test", new DateTime(2021, 11, 6, 15, 13, 37, 782, DateTimeKind.Local).AddTicks(2430) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                columns: new[] { "Content", "Timestamp" },
                values: new object[] { "Test", new DateTime(2021, 11, 6, 15, 13, 37, 782, DateTimeKind.Local).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                columns: new[] { "Content", "Timestamp" },
                values: new object[] { "Test", new DateTime(2021, 11, 6, 15, 13, 37, 782, DateTimeKind.Local).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "06f6beef-37b8-44a2-86ed-b5f2298a93f4");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "62800401-d0f5-46fe-9659-7f5f855908a2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e8788366-44e0-408f-8e2c-3fe799d67085", "AQAAAAEAACcQAAAAEIeuCtMtSBFDzLOAY6FIftHBt8xtbBweo1+QX2un3DfR44N39Z0jb8IDhJZXew9rAw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "News",
                newName: "PostURL");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 5, 21, 51, 28, 236, DateTimeKind.Local).AddTicks(9066),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 6, 15, 13, 37, 731, DateTimeKind.Local).AddTicks(3690));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 5, 21, 51, 28, 226, DateTimeKind.Local).AddTicks(5631),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 6, 15, 13, 37, 672, DateTimeKind.Local).AddTicks(2280));

            migrationBuilder.AddColumn<double>(
                name: "SocialBeliefs",
                table: "News",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 11, 5, 21, 51, 28, 259, DateTimeKind.Local).AddTicks(6283));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 11, 5, 21, 51, 28, 259, DateTimeKind.Local).AddTicks(6613));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                columns: new[] { "PostURL", "Timestamp" },
                values: new object[] { "https://www.nytimes.com/2021/08/15/world/asia/afghanistan-taliban-kabul-surrender.html", new DateTime(2021, 11, 5, 21, 51, 28, 259, DateTimeKind.Local).AddTicks(8441) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                columns: new[] { "PostURL", "Timestamp" },
                values: new object[] { "https://www.nbcnews.com/news/us-news/texas-high-court-blocks-mask-mandates-two-state-s-largest-n1276884", new DateTime(2021, 11, 5, 21, 51, 28, 259, DateTimeKind.Local).AddTicks(8740) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                columns: new[] { "PostURL", "Timestamp" },
                values: new object[] { "https://www.nytimes.com/live/2021/08/15/world/covid-delta-variant-vaccine/covid-hospitalizations-cdc", new DateTime(2021, 11, 5, 21, 51, 28, 259, DateTimeKind.Local).AddTicks(8752) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "59196e13-4c9a-4d71-8ffc-b6c70a9d5328");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "44086566-f7c8-423c-b44f-4268e420d4c0");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bd2fb0c2-0c78-4431-a228-3e9846e73572", "AQAAAAEAACcQAAAAENi7iB0H5Ic7AIxTrZqmnULWqBVMZn5/4lVF5QYHNoREFKvJsDV0rn+Lzz3mMCwwsg==" });
        }
    }
}

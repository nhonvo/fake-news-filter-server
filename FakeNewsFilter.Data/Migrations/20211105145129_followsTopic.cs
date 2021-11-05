using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class followsTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 5, 21, 51, 28, 236, DateTimeKind.Local).AddTicks(9066),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 4, 20, 38, 17, 407, DateTimeKind.Local).AddTicks(8090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 5, 21, 51, 28, 226, DateTimeKind.Local).AddTicks(5631),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 4, 20, 38, 17, 331, DateTimeKind.Local).AddTicks(7200));

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
                column: "Timestamp",
                value: new DateTime(2021, 11, 5, 21, 51, 28, 259, DateTimeKind.Local).AddTicks(8441));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 11, 5, 21, 51, 28, 259, DateTimeKind.Local).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 11, 5, 21, 51, 28, 259, DateTimeKind.Local).AddTicks(8752));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 4, 20, 38, 17, 407, DateTimeKind.Local).AddTicks(8090),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 5, 21, 51, 28, 236, DateTimeKind.Local).AddTicks(9066));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 4, 20, 38, 17, 331, DateTimeKind.Local).AddTicks(7200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 5, 21, 51, 28, 226, DateTimeKind.Local).AddTicks(5631));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 11, 4, 20, 38, 17, 459, DateTimeKind.Local).AddTicks(9330));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 11, 4, 20, 38, 17, 460, DateTimeKind.Local).AddTicks(2430));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 20, 38, 17, 461, DateTimeKind.Local).AddTicks(680));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 20, 38, 17, 461, DateTimeKind.Local).AddTicks(4540));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 20, 38, 17, 461, DateTimeKind.Local).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "85d881fc-6872-4320-90de-354ac78bd675");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "8f06d233-f282-48db-b482-257ec4c6b942");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fe50a636-91dd-498b-bfc3-8e5dca910f9a", "AQAAAAEAACcQAAAAEAtes9LrnGnteCus+ZdrjLiLaH0On6YtTK7CM+0uy3+DvjoIgog2YN/q9GnmZIGOZQ==" });
        }
    }
}

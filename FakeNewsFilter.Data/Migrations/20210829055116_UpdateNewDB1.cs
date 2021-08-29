using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class UpdateNewDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TopicNews",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 29, 12, 51, 15, 768, DateTimeKind.Local).AddTicks(470),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 28, 15, 17, 47, 160, DateTimeKind.Local).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 29, 12, 51, 15, 853, DateTimeKind.Local).AddTicks(9370));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 8, 29, 12, 51, 15, 854, DateTimeKind.Local).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 8, 29, 12, 51, 15, 854, DateTimeKind.Local).AddTicks(6300));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 8, 29, 12, 51, 15, 854, DateTimeKind.Local).AddTicks(8370));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 8, 29, 12, 51, 15, 854, DateTimeKind.Local).AddTicks(8730));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "91fd2ac5-231f-43d6-807c-827790875db4");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "c05d5e14-7cb2-4656-8bde-704b2131d377");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d7875449-5dff-4a9c-a4d1-7d9820bb999c", "AQAAAAEAACcQAAAAELJ94uELzZuWT9ZE5xnZTfHydTmNHcxw1xRGTdxCZcKl/ZHGYgxhcXOZGAK6PkzK9g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TopicNews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 28, 15, 17, 47, 160, DateTimeKind.Local).AddTicks(210),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 29, 12, 51, 15, 768, DateTimeKind.Local).AddTicks(470));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 28, 15, 17, 47, 243, DateTimeKind.Local).AddTicks(8890));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 8, 28, 15, 17, 47, 244, DateTimeKind.Local).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 8, 28, 15, 17, 47, 244, DateTimeKind.Local).AddTicks(4680));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 8, 28, 15, 17, 47, 244, DateTimeKind.Local).AddTicks(7070));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 8, 28, 15, 17, 47, 244, DateTimeKind.Local).AddTicks(7480));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "d710e2b9-abe4-46ab-966f-00fc59d4d5bf");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "9adff7ff-50aa-4537-9b20-1d08cab8be1e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "acd2d4b2-bbfc-4331-a200-06179ec2b7be", "AQAAAAEAACcQAAAAEEPW5iJHf0tynL00TqC4gjHJ3Pyr05huWhdAbb8klwHYAJYImu2Ax/xLLohLCqKDjw==" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class VoteMigrationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Vote");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 4, 15, 43, 15, 591, DateTimeKind.Local).AddTicks(9760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 4, 15, 42, 13, 609, DateTimeKind.Local).AddTicks(3110));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 4, 15, 43, 15, 533, DateTimeKind.Local).AddTicks(9680),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 4, 15, 42, 13, 552, DateTimeKind.Local).AddTicks(8160));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 11, 4, 15, 43, 15, 632, DateTimeKind.Local).AddTicks(6360));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 11, 4, 15, 43, 15, 632, DateTimeKind.Local).AddTicks(8180));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 15, 43, 15, 633, DateTimeKind.Local).AddTicks(4250));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 15, 43, 15, 633, DateTimeKind.Local).AddTicks(7020));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 15, 43, 15, 633, DateTimeKind.Local).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "43249e86-6bd5-41c7-8686-c765016fcf07");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "97a862d9-f21c-4f6e-9b7c-cbc7db213ae1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b316426e-e518-4a63-93ab-b1b5ed5007a0", "AQAAAAEAACcQAAAAEFLzfOzxqdLzubzdU70vwuLwj9gp2VG7C+ypu711D9LIlY7RQ22NwxYgTN32+Rk5Rw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 4, 15, 42, 13, 609, DateTimeKind.Local).AddTicks(3110),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 4, 15, 43, 15, 591, DateTimeKind.Local).AddTicks(9760));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 4, 15, 42, 13, 552, DateTimeKind.Local).AddTicks(8160),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 4, 15, 43, 15, 533, DateTimeKind.Local).AddTicks(9680));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 11, 4, 15, 42, 13, 649, DateTimeKind.Local).AddTicks(8940));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 11, 4, 15, 42, 13, 650, DateTimeKind.Local).AddTicks(650));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 15, 42, 13, 650, DateTimeKind.Local).AddTicks(6390));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 15, 42, 13, 650, DateTimeKind.Local).AddTicks(9120));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 15, 42, 13, 650, DateTimeKind.Local).AddTicks(9540));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "8af68eed-9966-4df2-bfe6-922aff480230");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "46b08315-d547-4be7-bb84-9efc7c20810f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e2d6e18c-f1be-49b5-b6a1-c69c99dd383f", "AQAAAAEAACcQAAAAEOPqEklsHDWshkMxvghEHf/Kmj0sTmUc+fa4BWMECm5hIG1FwPfYQ2cRxtoFXqpdOg==" });
        }
    }
}

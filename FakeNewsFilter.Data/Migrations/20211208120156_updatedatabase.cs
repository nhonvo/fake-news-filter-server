using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeNewsFilter.Data.Migrations
{
    public partial class updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 8, 19, 1, 55, 711, DateTimeKind.Local).AddTicks(9275),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 6, 22, 0, 3, 495, DateTimeKind.Local).AddTicks(1537));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 8, 19, 1, 55, 712, DateTimeKind.Local).AddTicks(6443),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 6, 22, 0, 3, 495, DateTimeKind.Local).AddTicks(8886));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 8, 19, 1, 55, 710, DateTimeKind.Local).AddTicks(202),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 6, 22, 0, 3, 493, DateTimeKind.Local).AddTicks(2706));

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8875));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8876));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8877));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8878));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8879));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8879));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8880));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8881));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8882));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8883));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8884));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8885));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8886));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8910));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8913));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8915));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8918));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8919));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8921));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8922));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8923));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8926));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8927));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8928));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8929));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8931));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8931));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8933));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8934));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8935));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8936));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8937));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8938));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8940));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8941));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8942));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8943));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8944));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8945));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8946));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8947));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8953));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8954));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8955));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "41e74cce-7e2f-4ef3-81ae-58907e37e669");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "3dc42fe7-ef30-4c1b-824d-43ace36bf5f5");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8804));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8817));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8818));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8830));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8832));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8833));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8833));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8834));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8836));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8841));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8842));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8842));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8843));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2021, 12, 8, 19, 1, 55, 739, DateTimeKind.Local).AddTicks(8844));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "77fb584a-3e31-492a-b4fa-9639775459a2", "AQAAAAEAACcQAAAAEPX5X6LqRoFF2gO/hoxD//W3cStDwUmXt3gRKhpatEj3n5YM6oOGy6rBDn7VeA/q/A==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "41b8f26b-e74b-45e8-92f4-8625faddaafd", "AQAAAAEAACcQAAAAEOGeFaimSoksfMXk0FdHMbM9DslZ1ZBqgCoI9C4OIrK9xETOoq7cczrac+7juaCl1A==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "46d1d722-85ec-4bd3-9a7c-04d1c6b4aa06", "AQAAAAEAACcQAAAAEOtPEGIirRQKghSf+5dIOHyoYohY+OZn+C9RcBNBtfINiy+kpDrULlnqknoKcev8IA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1c9cbdbf-9232-41cf-a4fd-0e96bc878a7a", "AQAAAAEAACcQAAAAEIKFQyaKs/02as7LEGpiHg6Vweyb20HgMkpnljvLFs2h84GPYisU62bQ0z9CpXpzug==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Comment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 6, 22, 0, 3, 495, DateTimeKind.Local).AddTicks(1537),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 8, 19, 1, 55, 711, DateTimeKind.Local).AddTicks(9275));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 6, 22, 0, 3, 495, DateTimeKind.Local).AddTicks(8886),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 8, 19, 1, 55, 712, DateTimeKind.Local).AddTicks(6443));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 6, 22, 0, 3, 493, DateTimeKind.Local).AddTicks(2706),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 8, 19, 1, 55, 710, DateTimeKind.Local).AddTicks(202));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7076));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7077));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7078));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7079));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7079));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7080));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7081));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7081));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7082));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7084));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7085));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7113));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7115));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7116));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7117));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7118));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7119));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7120));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7121));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7122));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7123));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7124));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7125));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7127));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7128));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7129));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7130));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7132));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7133));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7134));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7135));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7136));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7137));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7138));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7139));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7139));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7146));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7147));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7148));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7149));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7150));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7153));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7154));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7155));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "cc733ae0-196d-4191-b5ab-6fb368f3f6f1");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "59629cc9-b21c-4828-bbc1-6e30841137be");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7009));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7022));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7023));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7024));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7025));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7027));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7028));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7029));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7036));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7037));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7038));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7039));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7040));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2021, 12, 6, 22, 0, 3, 522, DateTimeKind.Local).AddTicks(7041));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a6388f60-17d1-452d-88fd-6e615e4c21f6", "AQAAAAEAACcQAAAAEJZ1B1IM/gKexJJnwSGWq0CQy2HwrUP9ulOcR40xQlMzn70eLG2YnoeNVaWkz5wMnA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "90548851-1592-426c-9382-c821a65503bd", "AQAAAAEAACcQAAAAEHHLNjA9E2JY/9w8LIQJvtLIy89ukgxRuJt3yS1nOCba6lBrSDED+9SuXgYTggMlcQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b1127fac-124c-474d-bedc-27e0e8264d89", "AQAAAAEAACcQAAAAEBltkvBtCaYpNowflyQKMhKDlp8fR4aFpFqRRSgt3sjPquuADFVW7NkWLNC7xb2ycQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e110b19e-7bdb-41cd-896b-ca8d33a072d6", "AQAAAAEAACcQAAAAECfKI3Uq8AW7s0kc056gcwvhWNPfKWuuxUbSRKornYVSLyS2eHuW/H7jpairLxITmg==" });
        }
    }
}

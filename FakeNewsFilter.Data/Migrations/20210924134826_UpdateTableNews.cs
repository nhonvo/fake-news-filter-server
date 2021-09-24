using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class UpdateTableNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 24, 20, 48, 24, 996, DateTimeKind.Local).AddTicks(7270),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 10, 18, 51, 35, 426, DateTimeKind.Local).AddTicks(2890));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 24, 20, 48, 25, 90, DateTimeKind.Local).AddTicks(730));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 9, 24, 20, 48, 25, 90, DateTimeKind.Local).AddTicks(2730));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                columns: new[] { "ThumbNews", "Timestamp" },
                values: new object[] { null, new DateTime(2021, 9, 24, 20, 48, 25, 90, DateTimeKind.Local).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                columns: new[] { "ThumbNews", "Timestamp" },
                values: new object[] { null, new DateTime(2021, 9, 24, 20, 48, 25, 91, DateTimeKind.Local).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 9, 24, 20, 48, 25, 91, DateTimeKind.Local).AddTicks(1150));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "f1ecfd3a-3c4b-4696-88e0-5f69ab4f4d64");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "43d9ab47-4715-4124-8d53-2fc41d13e762");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "ThumbTopic",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "ThumbTopic",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "675df427-4111-4c23-be8e-5113f32377d1", "AQAAAAEAACcQAAAAEOp/05CREDiqeh08HrvfS6YRWhbuC85K7in8QCwwZPhfhnXnT4SL7kQu1FRW6E0Ctg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "News");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 10, 18, 51, 35, 426, DateTimeKind.Local).AddTicks(2890),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 24, 20, 48, 24, 996, DateTimeKind.Local).AddTicks(7270));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 10, 18, 51, 35, 514, DateTimeKind.Local).AddTicks(630));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 9, 10, 18, 51, 35, 514, DateTimeKind.Local).AddTicks(2870));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                columns: new[] { "ThumbNews", "Timestamp" },
                values: new object[] { 1, new DateTime(2021, 9, 10, 18, 51, 35, 514, DateTimeKind.Local).AddTicks(8670) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                columns: new[] { "ThumbNews", "Timestamp" },
                values: new object[] { 2, new DateTime(2021, 9, 10, 18, 51, 35, 515, DateTimeKind.Local).AddTicks(2090) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 9, 10, 18, 51, 35, 515, DateTimeKind.Local).AddTicks(2520));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "9a1a0935-f55d-4227-803d-62488439ee15");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "cfe05bfa-3dc7-4788-b9c2-2d73358d14cd");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "ThumbTopic",
                value: null);

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "ThumbTopic",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fbedf18e-1802-4e9b-8e0a-6911e2a63bc0", "AQAAAAEAACcQAAAAEJy2n5VMoQE9vhQzeaRvq2HNFB50uQdNdniFmovhkdp3/++ovbsmS4ubG6T8CpNF2A==" });
        }
    }
}

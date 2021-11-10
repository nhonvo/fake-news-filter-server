using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class StoryUpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 10, 21, 39, 16, 56, DateTimeKind.Local).AddTicks(4363),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 9, 22, 17, 35, 292, DateTimeKind.Local).AddTicks(7003));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 10, 21, 39, 16, 59, DateTimeKind.Local).AddTicks(7222),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 9, 22, 17, 35, 295, DateTimeKind.Local).AddTicks(2908));

            migrationBuilder.AddColumn<string>(
                name: "LanguageId",
                table: "Story",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 10, 21, 39, 16, 43, DateTimeKind.Local).AddTicks(4039),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 9, 22, 17, 35, 281, DateTimeKind.Local).AddTicks(7016));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 11, 10, 21, 39, 16, 83, DateTimeKind.Local).AddTicks(7375));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 11, 10, 21, 39, 16, 83, DateTimeKind.Local).AddTicks(7729));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 11, 10, 21, 39, 16, 83, DateTimeKind.Local).AddTicks(9995));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 11, 10, 21, 39, 16, 84, DateTimeKind.Local).AddTicks(344));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 11, 10, 21, 39, 16, 84, DateTimeKind.Local).AddTicks(357));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "c92644b1-d315-4442-98fc-2c60ac29cd04");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "c2fe8e6a-871c-4b0c-9215-f977b24338cb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e5b3c7ca-a90b-4ef7-8dbb-be1dece4c2c7", "AQAAAAEAACcQAAAAEGA5Ixv68G2ZkXIkYQvUxC10qCdCSBWH/YTThLEPO/vu4BLVg0GA470450IMeMp6Qw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Story_LanguageId",
                table: "Story",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Languages_LanguageId",
                table: "Story",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Languages_LanguageId",
                table: "Story");

            migrationBuilder.DropIndex(
                name: "IX_Story_LanguageId",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Story");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 9, 22, 17, 35, 292, DateTimeKind.Local).AddTicks(7003),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 10, 21, 39, 16, 56, DateTimeKind.Local).AddTicks(4363));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 9, 22, 17, 35, 295, DateTimeKind.Local).AddTicks(2908),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 10, 21, 39, 16, 59, DateTimeKind.Local).AddTicks(7222));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 9, 22, 17, 35, 281, DateTimeKind.Local).AddTicks(7016),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 10, 21, 39, 16, 43, DateTimeKind.Local).AddTicks(4039));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 11, 9, 22, 17, 35, 316, DateTimeKind.Local).AddTicks(8339));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 11, 9, 22, 17, 35, 316, DateTimeKind.Local).AddTicks(8672));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 11, 9, 22, 17, 35, 317, DateTimeKind.Local).AddTicks(2128));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 11, 9, 22, 17, 35, 317, DateTimeKind.Local).AddTicks(2515));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 11, 9, 22, 17, 35, 317, DateTimeKind.Local).AddTicks(2529));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "d857f1c1-bf15-4ac0-937b-3f9fec9ee156");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "538731a7-92f6-44b8-8406-ace883f06236");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3882a386-6f19-49d4-802d-f7cb0b98e78a", "AQAAAAEAACcQAAAAEF/MtHXXNDI/3cc6+eL3OLtGr3bL0KDqbAy6Wfd90H92BeqBfWug5+pwROtxaOIujg==" });
        }
    }
}

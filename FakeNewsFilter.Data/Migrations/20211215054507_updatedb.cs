using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeNewsFilter.Data.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nlog");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 15, 12, 45, 6, 539, DateTimeKind.Local).AddTicks(189),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 13, 19, 52, 53, 876, DateTimeKind.Local).AddTicks(7816));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 15, 12, 45, 6, 539, DateTimeKind.Local).AddTicks(8625),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 13, 19, 52, 53, 877, DateTimeKind.Local).AddTicks(9126));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 15, 12, 45, 6, 536, DateTimeKind.Local).AddTicks(8787),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 13, 19, 52, 53, 873, DateTimeKind.Local).AddTicks(8855));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4597));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4599));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4600));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4602));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4603));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4604));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4605));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4607));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4609));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4610));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4612));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4613));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4614));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4617));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4618));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4619));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4620));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4621));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4623));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4624));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4625));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4626));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4664));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4668));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4670));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4672));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4674));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4676));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4678));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4680));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4684));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4685));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4688));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4689));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4691));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4692));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4694));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4695));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4697));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4698));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4700));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4701));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4703));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4704));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4706));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4715));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4717));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4718));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4720));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4722));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4724));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4725));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4727));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4728));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "296c6373-839c-496c-ab2a-bf074589ce01");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "e56d67aa-3286-4a79-bdf2-94c46d6fb9a7");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4495));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4511));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4513));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4515));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4517));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4537));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4538));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4540));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4541));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4545));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4547));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4548));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2021, 12, 15, 12, 45, 6, 570, DateTimeKind.Local).AddTicks(4549));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9f1fe812-c506-482f-b702-846bc3e7e487", "AQAAAAEAACcQAAAAELMK4QCAJLww/PwSIEwe/2SCMrQPFm6rDveeQpnre40cmx1pAwvrX4qk8TSPdFUICw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "06517072-cb46-44f9-b313-963496cdf1bb", "AQAAAAEAACcQAAAAEO7Kus8JnMpM6SmfndrNEKWpkd/ZG5KgCWjIyzcfM0Bc4NgcCg8Wtn1eIwWaUW8E8A==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8cb63c70-3ca4-4cd2-92be-11ce70867c29", "AQAAAAEAACcQAAAAEHo4fwMLk7JzTVSEj2/IcAiW1uBREQst1x2fOvRJ5I/UxVo4coTvm18rNtM41a9Sjw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "92a4744e-430e-4c86-be29-8de0d8105c1f", "AQAAAAEAACcQAAAAEIRBG3GaOReNw+pDzgyGZp1ueXxOYrQSzjV/yJiHp/peuv5a2Y5cspEyPeXlMQ59iw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 13, 19, 52, 53, 876, DateTimeKind.Local).AddTicks(7816),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 15, 12, 45, 6, 539, DateTimeKind.Local).AddTicks(189));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 13, 19, 52, 53, 877, DateTimeKind.Local).AddTicks(9126),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 15, 12, 45, 6, 539, DateTimeKind.Local).AddTicks(8625));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 13, 19, 52, 53, 873, DateTimeKind.Local).AddTicks(8855),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 15, 12, 45, 6, 536, DateTimeKind.Local).AddTicks(8787));

            migrationBuilder.CreateTable(
                name: "Nlog",
                columns: table => new
                {
                    IdLog = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Logger = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    MachineName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nlog", x => x.IdLog);
                });

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8102));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8104));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8105));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8106));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8107));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8108));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8109));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8110));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8111));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8111));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8112));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8113));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8114));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8115));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8116));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8117));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8118));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8119));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8120));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8121));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8122));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8123));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8156));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8159));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8160));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8162));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8163));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8165));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8222));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8225));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8226));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8228));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8229));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8231));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8233));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8234));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8236));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8237));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8238));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8240));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8241));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8243));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8244));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8246));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8247));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8249));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8251));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8252));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8253));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8255));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8256));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8258));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8259));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8260));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8262));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "a9d91502-f203-4c46-8efd-cdf8b851ab5a");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "ac048048-75ef-4c3c-b056-15a8a81913e3");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8041));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8053));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8056));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8060));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8061));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8062));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8066));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8067));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2021, 12, 13, 19, 52, 53, 914, DateTimeKind.Local).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "393b4f5b-04dc-44b7-a6ad-41af57014eac", "AQAAAAEAACcQAAAAEM0vcgJ2ydvo33c9LGeEraFiJv+qvCwJn6kTiqd6Ud9L1mv5PUAXZ9fAO8FosDqMrg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "02c8d8a5-c97d-4996-8ed7-a7034e279eb9", "AQAAAAEAACcQAAAAEFeyyUKfM4cRAmaOcY4RaXk9LDZCoRW3jqD57zxPe21fAB5pbiDh4nMB44qE/Wut5A==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "283a23e4-d0d8-4289-8041-7c5475f183e4", "AQAAAAEAACcQAAAAEIFkvrP5HU2dixyTjrNxMFZpqrBJ+tvWmd0flbxW9I2J9mYnb1TS0kKeEk17vguCnQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "436f19fa-5cf3-42c0-b582-719634891794", "AQAAAAEAACcQAAAAEC3PVU8k0xnmHsyoQegO5obJb2/N3qzIVx8/lA1leACPTEALm8n0oKbzWSmybW0+Kg==" });
        }
    }
}

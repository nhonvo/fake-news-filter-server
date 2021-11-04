using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class VoteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicNews_Languages_LanguageId",
                table: "TopicNews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 4, 15, 18, 14, 932, DateTimeKind.Local).AddTicks(9930),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 2, 10, 33, 58, 537, DateTimeKind.Local).AddTicks(9810));

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 11, 4, 15, 18, 14, 990, DateTimeKind.Local).AddTicks(4600))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => new { x.UserId, x.NewsId });
                    table.ForeignKey(
                        name: "FK_Vote_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vote_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 11, 4, 15, 18, 15, 30, DateTimeKind.Local).AddTicks(6750));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 11, 4, 15, 18, 15, 30, DateTimeKind.Local).AddTicks(8440));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 15, 18, 15, 31, DateTimeKind.Local).AddTicks(4610));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 15, 18, 15, 31, DateTimeKind.Local).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 11, 4, 15, 18, 15, 31, DateTimeKind.Local).AddTicks(7900));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "55e91d7f-8f2d-4638-85a9-e5509046539c");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "cce9da28-3a9d-45f2-8258-a9082efe4c66");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1ef3a3b0-74ab-4a69-b7df-8b7856a3d8e7", "AQAAAAEAACcQAAAAEHcKqIMfMkdYSw5FK0BF5JU1vGYoT0NIlDqptjjhX4aNxCXI3S0dG2kUABRlCqybVA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Vote_NewsId",
                table: "Vote",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNews_Languages_LanguageId",
                table: "TopicNews",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicNews_Languages_LanguageId",
                table: "TopicNews");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 2, 10, 33, 58, 537, DateTimeKind.Local).AddTicks(9810),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 4, 15, 18, 14, 932, DateTimeKind.Local).AddTicks(9930));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 11, 2, 10, 33, 58, 611, DateTimeKind.Local).AddTicks(1960));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 11, 2, 10, 33, 58, 611, DateTimeKind.Local).AddTicks(4040));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 11, 2, 10, 33, 58, 611, DateTimeKind.Local).AddTicks(9350));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 11, 2, 10, 33, 58, 612, DateTimeKind.Local).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 11, 2, 10, 33, 58, 612, DateTimeKind.Local).AddTicks(2620));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "5d27d846-d868-4ba3-820c-ada9e5357319");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "e17fdbf9-4974-4af7-a814-6722afab8886");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d38825d9-7f75-4dae-a3ba-135d30caa993", "AQAAAAEAACcQAAAAECL7ef49iia4m6YlpbTEFna6783GzdN2M+tPcTloKeRJSVBnmwXftYIwWlgWjdQ5Xg==" });

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNews_Languages_LanguageId",
                table: "TopicNews",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

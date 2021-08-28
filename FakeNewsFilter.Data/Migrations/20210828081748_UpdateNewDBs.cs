using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class UpdateNewDBs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Media_MediaNews",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicNews_Media_MediaTopic",
                table: "TopicNews");

            migrationBuilder.DropIndex(
                name: "IX_TopicNews_MediaTopic",
                table: "TopicNews");

            migrationBuilder.DropIndex(
                name: "IX_News_MediaNews",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "MediaTopic",
                table: "TopicNews",
                newName: "ThumbTopic");

            migrationBuilder.RenameColumn(
                name: "MediaNews",
                table: "News",
                newName: "ThumbNews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 28, 15, 17, 47, 160, DateTimeKind.Local).AddTicks(210),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 28, 14, 54, 34, 679, DateTimeKind.Local).AddTicks(2500));

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

            migrationBuilder.CreateIndex(
                name: "IX_TopicNews_ThumbTopic",
                table: "TopicNews",
                column: "ThumbTopic",
                unique: true,
                filter: "[ThumbTopic] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_News_ThumbNews",
                table: "News",
                column: "ThumbNews",
                unique: true,
                filter: "[ThumbNews] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Media_ThumbNews",
                table: "News",
                column: "ThumbNews",
                principalTable: "Media",
                principalColumn: "MediaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNews_Media_ThumbTopic",
                table: "TopicNews",
                column: "ThumbTopic",
                principalTable: "Media",
                principalColumn: "MediaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Media_ThumbNews",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicNews_Media_ThumbTopic",
                table: "TopicNews");

            migrationBuilder.DropIndex(
                name: "IX_TopicNews_ThumbTopic",
                table: "TopicNews");

            migrationBuilder.DropIndex(
                name: "IX_News_ThumbNews",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "ThumbTopic",
                table: "TopicNews",
                newName: "MediaTopic");

            migrationBuilder.RenameColumn(
                name: "ThumbNews",
                table: "News",
                newName: "MediaNews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 28, 14, 54, 34, 679, DateTimeKind.Local).AddTicks(2500),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 28, 15, 17, 47, 160, DateTimeKind.Local).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 28, 14, 54, 34, 777, DateTimeKind.Local).AddTicks(5540));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 8, 28, 14, 54, 34, 777, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 8, 28, 14, 54, 34, 778, DateTimeKind.Local).AddTicks(2250));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 8, 28, 14, 54, 34, 778, DateTimeKind.Local).AddTicks(4910));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 8, 28, 14, 54, 34, 778, DateTimeKind.Local).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "d4885485-b7ab-4319-b891-e2c50899e71c");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "4b04756f-9d51-4c58-810c-3c5081b36ecc");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "49883870-1dfe-4718-9087-94f5bc73c397", "AQAAAAEAACcQAAAAENB9uXuTTMJ5n1LBZ7Cy5wdUNvgiENY+CgFOEItmh2SfgqC5ZcbAW139vTRis3GL/w==" });

            migrationBuilder.CreateIndex(
                name: "IX_TopicNews_MediaTopic",
                table: "TopicNews",
                column: "MediaTopic",
                unique: true,
                filter: "[MediaTopic] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_News_MediaNews",
                table: "News",
                column: "MediaNews",
                unique: true,
                filter: "[MediaNews] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Media_MediaNews",
                table: "News",
                column: "MediaNews",
                principalTable: "Media",
                principalColumn: "MediaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNews_Media_MediaTopic",
                table: "TopicNews",
                column: "MediaTopic",
                principalTable: "Media",
                principalColumn: "MediaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

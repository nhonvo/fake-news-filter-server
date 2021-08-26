using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class UpdateNewDBs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 26, 11, 34, 59, 200, DateTimeKind.Local).AddTicks(9340));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 8, 26, 11, 34, 59, 219, DateTimeKind.Local).AddTicks(1740));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 8, 26, 11, 34, 59, 219, DateTimeKind.Local).AddTicks(7020));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 8, 26, 11, 34, 59, 219, DateTimeKind.Local).AddTicks(9280));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 8, 26, 11, 34, 59, 219, DateTimeKind.Local).AddTicks(9850));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "81f3b660-9280-453a-80d1-e2e3e60006ae");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "9bf39f2a-1b2e-4835-ad14-61554dd730c3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8d546014-fceb-4c43-a1e7-3bfb065b498f", "AQAAAAEAACcQAAAAEL5DAk1KHs4ttggC49Rw2Bli93xR4ykegzTt/uiM/eKZtEwvY34HgwUa6jFtpj3HTA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarId",
                table: "Users",
                column: "AvatarId",
                unique: true,
                filter: "[AvatarId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Media_AvatarId",
                table: "Users",
                column: "AvatarId",
                principalTable: "Media",
                principalColumn: "MediaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Media_AvatarId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AvatarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 24, 23, 37, 51, 411, DateTimeKind.Local).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2021, 8, 24, 23, 37, 51, 428, DateTimeKind.Local).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 8, 24, 23, 37, 51, 428, DateTimeKind.Local).AddTicks(9910));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 8, 24, 23, 37, 51, 429, DateTimeKind.Local).AddTicks(2610));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2021, 8, 24, 23, 37, 51, 429, DateTimeKind.Local).AddTicks(3140));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "cce80dfe-167e-4427-aad2-921bd086002f");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "6fce4f11-3025-4da6-8f68-f0f548f5e544");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5dadbb09-71f8-4e0b-94f7-f3f1281525a6", "AQAAAAEAACcQAAAAEAFLLYuF5IfOZsSQjtsmg2vxPvK1TFPKTWM/Qk9SWIJ6WmuutQ7b65kp1nfVFx90Jw==" });
        }
    }
}

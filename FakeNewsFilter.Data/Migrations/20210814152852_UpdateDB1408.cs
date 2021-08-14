using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeNewsFilter.Data.Migrations
{
    public partial class UpdateDB1408 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "TopicNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 14, 22, 28, 51, 439, DateTimeKind.Local).AddTicks(4030),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 13, 13, 39, 46, 356, DateTimeKind.Local).AddTicks(1130));

            migrationBuilder.AlterColumn<double>(
                name: "SocialBeliefs",
                table: "News",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 14, 22, 28, 51, 460, DateTimeKind.Local).AddTicks(5890));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "4c801deb-05d9-413a-802a-34a18f59323c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "160977e3-776c-4fd9-90d8-8897abf72514", "AQAAAAEAACcQAAAAEEdWnaaey0jBU1PZz5zHiw0Zs5LOhXIJFbnl423cHe8X/O+PhGjO8/VxejVyWJbClA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "News");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "TopicNews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 13, 13, 39, 46, 356, DateTimeKind.Local).AddTicks(1130),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 14, 22, 28, 51, 439, DateTimeKind.Local).AddTicks(4030));

            migrationBuilder.AlterColumn<double>(
                name: "SocialBeliefs",
                table: "News",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldDefaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "168bf747-b14b-4fa1-b34f-e3abb337f53b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7b1e390b-9427-4e90-ad5c-509ad8fbbfdf", "AQAAAAEAACcQAAAAENfTEXoqp8BuFFipxYdeFBamEL2/SxOhy0wtDosTYYkht3qIAALYzv1UtRVN7einyw==" });
        }
    }
}

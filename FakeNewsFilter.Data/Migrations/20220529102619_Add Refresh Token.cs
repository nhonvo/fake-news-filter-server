using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeNewsFilter.Data.Migrations
{
    public partial class AddRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DetailNews",
                keyColumn: "DetailNewsId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "isVote",
                table: "News",
                newName: "IsVote");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 851, DateTimeKind.Local).AddTicks(2180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 15, 11, 42, 34, 153, DateTimeKind.Local).AddTicks(3370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 851, DateTimeKind.Local).AddTicks(6840),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 15, 11, 42, 34, 153, DateTimeKind.Local).AddTicks(8060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 848, DateTimeKind.Local).AddTicks(5300),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 15, 11, 42, 34, 152, DateTimeKind.Local).AddTicks(2370));

            migrationBuilder.AlterColumn<bool>(
                name: "IsVote",
                table: "News",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
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
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5270));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5270));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5270));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5280));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5290));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5300));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5300));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5300));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                columns: new[] { "IsVote", "Timestamp" },
                values: new object[] { null, new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                columns: new[] { "IsVote", "Timestamp" },
                values: new object[] { null, new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                columns: new[] { "IsVote", "Timestamp" },
                values: new object[] { null, new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://cdnimg.vietnamplus.vn/t620/uploaded/fsmsy/2021_07_26/phun_khu_khuan.jpg", null, "TTXVN/Vietnam", "https://www.vietnamplus.vn/thong-tin-tphcm-su-dung-5-truc-thang-phun-khu-trung-la-sai-su-that/729372.vnp", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.baobaclieu.vn/uploads/image/2021/08/06/13b.jpg", null, "Báo Bạc Liêu", "https://www.baobaclieu.vn/quoc-phong-an-ninh/manh-tay-xu-ly-tin-gia-tin-sai-su-that-ve-dich-covid-19-tren-mang-72306.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2022, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://img.nhandan.com.vn/Files/Images/2022/05/12/Hai_truong_hop_lam_viec_voi_co_q-1652343768786.jpg", null, "Báo Nhân Dân", "https://nhandan.vn/factcheck/thong-tin-nguon-nuoc-thanh-thien-co-the-chua-covid-19-la-sai-su-that-696816/", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://file1.dangcongsan.vn/data/0/images/2021/10/01/vulinh/dfhgdfh.jpg?dpi=150&quality=100&w=780", null, "DCSVN", "https://dangcongsan.vn/canh-bao-thong-tin-gia/bac-lieu-mac-covid-19-duoc-uu-ai-dieu-tri-tai-nha-la-sai-su-that-592693.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://file1.dangcongsan.vn/data/0/images/2021/09/21/vulinh/video-man-1632200081574.jpg?dpi=150&quality=100&w=780", null, "DCSVN", "https://dangcongsan.vn/canh-bao-thong-tin-gia/chien-dich-tiem-vaccine-ngua-covid-19-cho-tre-em-12-15-tuoi-o-xu-anh-bi-tam-dung-la-khong-chinh-xac-591591.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_29/1-bai-3-1687.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/san-loc-troi-loi-suoi-nhat-oc-vao-thu-phu-ca-chinh-post1406363.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2021_11_24/nghe-viet-1372.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/nghe-viet-net-viet-nghe-trai-chuon-ngo-post1404658.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2021_11_24/cao-su-7917.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/noi-long-nguoi-cao-mu-cao-su-post1404643.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                columns: new[] { "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { "https://image.thanhnien.vn/w2048/Uploaded/2022/fsmxy/2021_11_27/drai-dlong-8588.png", null, "Báo Thanh Niên", "https://thanhnien.vn/kham-pha-thac-ba-nhanh-hung-vi-it-nguoi-biet-giua-tay-nguyen-post1405776.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                columns: new[] { "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { "https://image.thanhnien.vn/w2048/Uploaded/2022/fsmxy/2021_11_21/noi-hap-xoi-co-4376.png", null, "Báo Thanh Niên", "https://thanhnien.vn/chiec-noi-co-than-ky-tao-ra-mon-xoi-doc-dao-cua-nguoi-nung-o-dak-lak-post1403687.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_27/22b1-5885.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/nhung-dua-con-tim-ve-nguon-coi-post1405816.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2021_11_20/trien-lam-5546.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/mang-chat-viet-vao-tranh-in-tren-dat-my-post1403198.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_13/22a1-5500.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/nguoi-phu-nu-viet-phat-trien-vat-lieu-phu-chong-chay-o-uc-post1401084.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                columns: new[] { "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_13/hinh-1-3868.png", null, "Báo Thanh Niên", "https://thanhnien.vn/cam-hung-tu-bua-an-viet-cua-ba-post1401081.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/rfnmf/2021_11_30/tau-metro-2-acxy-8311.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/gap-bao-doan-tau-metro-tre-hen-ve-tp-hcm-post1406682.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/vjryqdxwp/2021_11_30/satthep-chihieu-uver-keev-9147.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/xuat-khau-thep-lan-dau-can-moc-10-ti-usd-post1406650.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_30/2a2-8280.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/tp-hcm-khat-von-cho-giao-thong-post1406453.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                columns: new[] { "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { "https://image.thanhnien.vn/w2048/Uploaded/2022/wpxlcqjwq/2021_11_26/kieu-hoi-3276.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/luong-kieu-hoi-tang-manh-ky-luc-post1405536.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                columns: new[] { "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { "https://image.thanhnien.vn/w2048/Uploaded/2022/wpxlcqjwq/2021_11_24/chung-khoan-9665.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/dong-von-manh-dua-chung-khoan-lap-dinh-post1404799.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/bpcgvoiv/2021_11_30/a1-dtan-4946.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/tin-tuc-giao-duc-dac-biet-1-12-day-su-bang-noi-dung-cam-xuc-hay-su-kien-post1406754.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5360) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2020_08_31/tuyen-sinh_chbt.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/huong-vao-dai-hoc-phu-hop-voi-diem-thi-post989845.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5360) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wobjuko/2021_11_20/anh-1-7862.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/gap-nguoi-thay-dep-trai-nhat-truong-mam-non-post1403127.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5360) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/wobjuko/2021_06_05/3_cdyb.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/thay-giao-dung-tien-khen-thuong-ung-ho-quy-phong-chong-covid-19-post1075098.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5360) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/mffsm/2021_09_29/0-1_pldi.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/khoa-y-dh-quoc-gia-tp-hcm-xet-tuyen-bo-sung-ca-thi-sinh-tu-do-post1116655.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5360) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_30/cyberpunk-4917.png", null, "Báo Thanh Niên", "https://thanhnien.vn/ban-nang-cap-cyberpunk-2077-se-mien-phi-cho-chu-so-huu-ps4-va-xbox-one-post1406595.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/dbeyxqxqrs/2021_11_30/1-8294.png", null, "Báo Thanh Niên", "https://thanhnien.vn/phi-vu-trieu-do-tai-ket-hop-free-fire-trong-phan-dac-biet-phi-vu-cuoi-cung-thang-12-nay-post1406503.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_23/image0-500.png", null, "Báo Thanh Niên", "https://thanhnien.vn/viet-nam-lot-top-5-doi-lmht-toc-chien-the-gioi-post1404103.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_22/1-3692.jpg", null, "Báo Thanh Niên", "https://thanhnien.vn/riot-games-de-lo-4-dia-diem-cua-chung-ket-lmht-the-gioi-2022-post1403726.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                columns: new[] { "DatePublished", "ImageLink", "IsVote", "Publisher", "Source", "Timestamp" },
                values: new object[] { new DateTime(2021, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_15/picture2-6769.png", null, "Báo Thanh Niên", "https://thanhnien.vn/arcane-giup-vi-va-jinx-tang-vot-ti-le-duoc-chon-trong-lmht-post1401689.html", new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "d7371a9e-8106-4af6-8454-4511c9c859fc");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "745c71f5-9543-4cc3-ba48-d136d4c7bda3");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5230));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5260));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 5, 29, 17, 26, 18, 859, DateTimeKind.Local).AddTicks(5260));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "95c59485-6ab6-4a36-bc35-875ff5ae91bb", "AQAAAAEAACcQAAAAEIvwIWxaQXjWlK9EuSmHNMyS9o1xWH+gChDpHHFB7zPmoV3pQvdTsvwn5EscgLEnJQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "55321bac-6701-49a2-a5cb-195ee04cc206", "AQAAAAEAACcQAAAAEHse8vc3SHJMnizv2rKfUrFminFH9b5G6fC5j1f9DBOqqz/mAVcv3O4F+IRB/jGWaw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9023323c-a9bb-4194-a4c3-d5414b291448", "AQAAAAEAACcQAAAAEOZAbDEVtU+hpk+EL2BmPJmUmZXdq1FYWOHqkgvIeDBvTyTALj97vq3C+tILmD0EyQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "64f17984-d3d5-4f69-8307-d8d265440c0a", "AQAAAAEAACcQAAAAEDHemfJQF820r+qvpWJO0M7GdoLkC5WgAer0U+VwdA6LPMeC/VGEbCiuj3nr8vNHUw==" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "IsVote",
                table: "News",
                newName: "isVote");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 15, 11, 42, 34, 153, DateTimeKind.Local).AddTicks(3370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 851, DateTimeKind.Local).AddTicks(2180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Story",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 15, 11, 42, 34, 153, DateTimeKind.Local).AddTicks(8060),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 851, DateTimeKind.Local).AddTicks(6840));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "NewsInTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 15, 11, 42, 34, 152, DateTimeKind.Local).AddTicks(2370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 29, 17, 26, 18, 848, DateTimeKind.Local).AddTicks(5300));

            migrationBuilder.AlterColumn<bool>(
                name: "isVote",
                table: "News",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.InsertData(
                table: "DetailNews",
                columns: new[] { "DetailNewsId", "Alias", "Content", "NewsId", "ThumbNews" },
                values: new object[,]
                {
                    { 3, "the-independent", "The Rasmus Q&A: Meet Finland’s entry for Eurovision 2022 Delta variant are driving a surge in Covid-19 hospitalizations in the United States..", 3, 16 },
                    { 5, "theo-so-hien-nay", "Theo Sở TT&TT, hiện nay, trên mạng xã hội đang lan truyền thông tin “tối nay từ 11h40 không nên ra đường. Cửa ra vào và cửa sổ nên được đóng lại khi 5 máy bay trực thăng phun chất khử trùng vào không khí để diệt trừ Coronavirus”. Trao đổi với VietNamNet, ông Lâm Đình Thắng, Giám đốc Sở TT&TT cho hay, Bộ Tư lệnh TP.HCM khẳng định, thông tin trên hoàn toàn sai sự thật. Lực lượng quân đội phun khử khuẩn trên địa bàn TP.HCM Trước đó, sáng 23/7, Bộ Tư lệnh TP.HCM phối hợp với Lữ đoàn 87 Binh Chủng hóa học, Tiểu đoàn Phòng hóa 38 Quân khu 7 cùng với lực lượng vũ trang TP và 21 quận, huyện và TP Thủ Đức đồng loạt mở đợt cao điểm phun thuốc khử khuẩn phòng, chống dịch Covid-19 quy mô lớn nhất từ trước tới nay trên địa bàn TP, trong thời gian 7 ngày. Mỗi ngày sẽ có 20 lượt xe tham gia phun thuốc khử khuẩn phòng, chống Covid-19. Theo Hồ Văn/Báo điện tử VietnamNet https://vietnamnet.vn/vn/thoi-su/thong-tin-tp-hcm-dung-5-truc-thang-phun-khu-khuan-la-sai-su-that-759937.html", 5, 18 },
                    { 6, "dai-dich-covid-bung-phat", "Đại dịch Covid-19 bùng phát trở lại, gây chồng chất thêm khó khăn cho doanh nghiệp, người dân, cũng vì thế mà thông tin về diễn biến đại dịch trở thành mối quan tâm hàng đầu của toàn xã hội. Bên cạnh những thông tin chính xác, tích cực, giúp mọi người nâng cao tinh thần cảnh giác, chung tay phòng chống dịch bệnh, cũng có không ít thông tin sai lệch, thiếu kiểm chứng trên mạng xã hội, gây hoang mang dư luận, tác động xấu đến tình hình an ninh trật tự trên địa bàn. http://brt.vn/thoi-su/dich-viem-phoi-virus-corona/202008/manh-tay-xu-ly-hanh-vi-dua-tin-gia-lien-quan-den-dich-covid-19-8179089/", 6, 19 },
                    { 7, "dang-tai-thong-tin-sai", "Đăng tải thông tin sai sự thật trên trang Facebook cá nhân: “nguồn nước Thánh Thiên sẽ cứu chữa rất nhiều bệnh… đặc biệt là Covid-19” gây hoang mang dư luận, bà N.T.T (sinh năm 1969, ngụ huyện Bảo Lâm, Lâm Đồng) đã bị cơ quan chức năng xử phạt 5 triệu đồng. Làm việc với Cơ quan công an, bà N.T.T thừa nhận đã đăng tải thông tin sai sự thật. Thông tin lan truyền Qua công tác bảo đảm an ninh mạng, Phòng An ninh mạng và phòng, chống tội phạm sử dụng công nghệ cao (PA05), Công an tỉnh Lâm Đồng phát hiện bà N.T.T đăng tải trên Facebook cá nhân “T.A.P” bài viết có nội dung: “Nguồn nước Thánh Thiên này sẽ cứu chữa rất nhiều bệnh, đặc biệt là Covid-19…”, kèm theo hình ảnh hai chai nước ghi dòng chữ “nguồn Thánh Thiên”. Kiểm chứng Làm việc với cơ quan công an, bà T trình bày, “nguồn nước thánh thiên” có nguồn gốc từ nhóm tự xưng có tên “trừ quỷ Bảo Lộc” (địa chỉ ở 53/5 Hồ Tùng Mậu, TP Bảo Lộc, Lâm Đồng). Trong quá trình tham gia nhóm, bà T và các thành viên cho rằng, “qua việc cầu nguyện, chữa lành, uống nước thánh thiên thì có thể chữa khỏi Covid-19”, nên bà T đã đăng tải lên Facebook cá nhân. Bà T thừa nhận, “nước thánh thiên” không phải thuốc chữa bệnh Covid-19, không được các cơ quan chức năng cấp giấy phép; thông tin do bà T đăng tải là sai sự thật, không kiểm chứng trước khi đăng tải. Hành vi của bà N.T.T vi phạm pháp luật, quy định tại Nghị định số 15/2020/NĐ-CP, ngày 3/2/2020 của Chính phủ, quy định xử phạt vi phạm hành chính trong lĩnh vực bưu chính, viễn thông, tần số vô tuyến điện, công nghệ thông tin và giao dịch điện tử. Theo PA05 Công an tỉnh Lâm Đồng, nhóm tự xưng “trừ quỷ Bảo Lộc” có hoạt động chữa bệnh nhưng không có giấy phép. Ngày 17/9/2021, ông T.V.L.T.Q, một trong những người đứng đầu nhóm này, sử dụng nhà riêng tại địa chỉ nêu trên làm nơi chữa bệnh trái phép, đã bị UBND TP Bảo Lộc xử phạt vi phạm hành chính 45 triệu đồng, về hành vi “chữa bệnh mà không có giấy phép hoạt động chữa bệnh”. Theo Bảo Văn/Báo Nhân dân điện tử https://nhandan.vn/factcheck/thong-tin-nguon-nuoc-thanh-thien-chua-duoc-covid-19-la-sai-su-that-669233/", 7, 20 },
                    { 8, "truong-hop-ba-Nguyen-Huynh-Nhu", "Thông tin về trường hợp bà Nguyễn Huỳnh Như (Giám đốc Công ty mỹ phẩm Đông Anh ở TP Bạc…", 8, 21 },
                    { 9, "hang-nghin-nguoi-xem", "Hàng nghìn người đã xem 1 video trực tuyến, trong đó xuất hiện 1 người đàn ông nói rằng chiến…", 9, 22 },
                    { 10, "dan-toc", "Để “săn” ốc đá và cá chình, 2 sản vật ngon bậc nhất ở núi rừng Quảng Trị.", 10, null },
                    { 11, "lang-duy-nhat", "Chuôn Ngọ là làng duy nhất cung cấp nguyên liệu các loại vỏ trai, ốc cho cả nước để làm đồ cẩn, khảm, thủ công mỹ nghệ.", 11, null },
                    { 12, "nguoi-bat-dau-len-giuong", "Khi mọi người bắt đầu lên giường đi ngủ, thì một ngày làm việc của công nhân cạo mủ cao su bắt đầu.", 12, null },
                    { 13, "thac-drai-dlong", "Thác Drai Dlông với dòng chảy mạnh mẽ quanh năm giữa núi rừng là điểm đến không thể bỏ qua của những ai muốn khám phá Tây Nguyên.", 13, null },
                    { 14, "mon-an-xoi", "Xôi là món ăn được rất nhiều người ưa thích vì dễ ăn và cách làm khá đơn giản, thế nhưng tại gia đình bà Nông Thị Mai.", 14, null },
                    { 15, "nguoi-viet-cau-chuyen", "Câu chuyện của hai anh em sống tại TP.Liverpool được kể lại trong loạt phim tài liệu Nail Bar Boys do Đài BBC khởi chiếu tuần qua.", 15, null },
                    { 16, "de-to-chuc-thanh-cong", "Để tổ chức thành công triển lãm cá nhân đầu tiên tại Mỹ, họa sĩ tranh in Mai Trần đã trải qua một quá trình dài với nhiều gian nan, thử thách.", 16, null },
                    { 17, "mot-nu-chien-si-nguoi-viet", "Một nữ tiến sĩ người Việt được vinh danh là chuyên gia vật liệu hàng đầu tại Úc nhờ góp phần ứng phó cháy rừng tại nước này.", 17, null },
                    { 18, "nhung-ky-uc-ve-nguoi-ba", "Những ký ức về người bà quá cố và các món ăn Việt mà bà chuẩn bị cho gia đình khi xưa đã dẫn dắt đầu bếp David Huynh.", 18, null },
                    { 19, "bon-doan-tau-tuyen", "Bốn đoàn tàu tuyến metro số 1 (tuyến Bến Thành - Suối Tiên) dự kiến từ Nhật Bản về TP.HCM cuối tháng 11 và đầu tháng 12.", 19, null },
                    { 20, "so-lieu-thong-ke-tong-cuc", "Số liệu công bố từ Tổng cục Thống kê cho thấy 11 tháng năm 2021, Việt Nam xuất khẩu đạt tổng trị giá 299,67 tỉ USD.", 20, null },
                    { 21, "giao-thong-TPHCM", "UBND TP.HCM vừa có văn bản khẩn gửi Bộ Kế hoạch - Đầu tư liên quan đến dự kiến phương án phân bổ vốn đầu tư công năm 2022 nguồn vốn ngân sách T.Ư.", 21, null },
                    { 22, "tai-chinh-2021", "Dự ước năm 2021, lượng kiều hối chuyển về VN sẽ đạt mức kỷ lục 18,1 tỉ USD, bất chấp dịch Covid-19.", 22, null },
                    { 23, "chung-khoan-sut-giam", "Tiền gửi tiết kiệm sụt giảm trong khi dòng vốn tham gia vào thị trường chứng khoán ngày càng tăng.", 23, null },
                    { 24, "day-mon-lich-su", "Dạy học môn lịch sử trong trường phổ thông như thế nào để học sinh không chán là vấn đề luôn luôn mới.", 24, null },
                    { 25, "nhung-luu-y-gi-cho-thi-sinh", "Những lưu ý gì cho thí sinh để vào được đúng ngành nghề yêu thích, phù hợp với điểm số, là vấn đề mà rất nhiều thí sinh hiện đang băn khoăn.", 25, null },
                    { 26, "tu-mot-chang-tho-xay", "Từ một chàng thợ xây thích chơi đùa cùng trẻ em, thầy giáo Nguyễn Hồ Tây Phương đã trở thành người thầy hiếm hoi dấn thân mình với nghề dạy dỗ trẻ mầm non.", 26, null },
                    { 27, "thay-Nguyen-Viet-Tuoc", "Thầy Nguyễn Viết Tước đã được các cấp từ trung ương đến địa phương khen thưởng hơn 7 triệu đồng.", 27, null },
                    { 28, "khoa-y-dhquoc-gia", "Khoa Y ĐH Quốc gia TP.HCM thông báo xét tuyển bổ sung 3 ngành ĐH hệ chính quy, trong đó có ngành y khoa.", 28, null },
                    { 29, "ban-nang-cap-moi", "Bản nâng cấp mới sẽ khả dụng vào năm 2022 và hoàn toàn miễn phí cho chủ sở hữu các thiết bị PS4 và Xbox One.", 29, null },
                    { 30, "phi-vu-trieu-do", "Phi Vụ Triệu Đô bất ngờ quay trở lại Đảo Quân Sự Free Fire lần thứ hai với phần đặc biệt.", 30, null },
                    { 31, "giai-dau-mang-quy-mo", "Giải đấu mang quy mô quốc tế đầu tiên của LMHT: Tốc Chiến vừa kết thúc tại Singapore và một đội tuyển của Việt Nam vào Top 5-6.", 31, null },
                    { 32, "ten-cua-4-thanh-pho", "Tên của 4 thành phố dự kiến tổ chức giải Chung kết Thế giới 2022 bộ môn eSport Liên Minh Huyền Thoại vô tình bị lộ trong một video thông báo.", 32, null },
                    { 33, "bo-phim-hoat-hinh", "Bộ phim hoạt hình mang tên Arcane về thế giới trong Liên Minh Huyền Thoại đang nhận đánh giá tốt.", 33, null }
                });

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "MediaId",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 1,
                columns: new[] { "Timestamp", "isVote" },
                values: new object[] { new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 2,
                columns: new[] { "Timestamp", "isVote" },
                values: new object[] { new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 3,
                columns: new[] { "Timestamp", "isVote" },
                values: new object[] { new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 5,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 6,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 7,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 8,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 9,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 10,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 11,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 12,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 13,
                columns: new[] { "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 14,
                columns: new[] { "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8020), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 15,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 16,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 17,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 18,
                columns: new[] { "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 19,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 20,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 21,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8030), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 22,
                columns: new[] { "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 23,
                columns: new[] { "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 24,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 25,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 26,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 27,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8040), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 28,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 29,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 30,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 31,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 32,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), false });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsId",
                keyValue: 33,
                columns: new[] { "DatePublished", "ImageLink", "Publisher", "Source", "Timestamp", "isVote" },
                values: new object[] { new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8050), false });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "DatePublished", "ImageLink", "LanguageId", "OfficialRating", "Publisher", "SocialBeliefs", "Source", "Status", "Timestamp", "Title", "isVote" },
                values: new object[] { 4, new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "en", null, null, 0.0, null, 0, new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(8010), "Hospitalizations of Americans under 50 have reached new pandemic highs", false });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                column: "ConcurrencyStamp",
                value: "4da19fbe-9dc2-432d-b0a7-b7042c86b7c6");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                column: "ConcurrencyStamp",
                value: "f4d47067-d5f1-4e78-a125-80020220a9c6");

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7920));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7920));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7920));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 5,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 6,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 7,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 8,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 9,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 10,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7930));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 11,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 12,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "TopicNews",
                keyColumn: "TopicId",
                keyValue: 14,
                column: "Timestamp",
                value: new DateTime(2022, 5, 15, 11, 42, 34, 161, DateTimeKind.Local).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be01de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fecbdb65-cf61-4f75-b989-3a28c7e6245c", "AQAAAAEAACcQAAAAEA6uGfstzJWATUYAFIRiN5ZmkhahLRbfvkteVVfQAHjXm9qeMTBp34nKWT9SL/k0AQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be02de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b8b872a6-5d80-499c-b537-e4c87cd2d718", "AQAAAAEAACcQAAAAEDVF3kiW7ZQInyygiPXcm9dSA2Ys80Ybai5P0PhxwV5UrLYUsueKApnFaxVsX4zfUA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be03de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1692921c-87ee-4594-b09d-bd347eee0153", "AQAAAAEAACcQAAAAEAXOG8Fi1vpljX9ObTpFuAFYqKy67HZG8UBXXophm0dha/5UzDili66h5IO1OaVTnA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69db714f-9576-45ba-b5b7-f00649be04de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e146edde-66cf-44c1-b6cd-dbd0e3c203e8", "AQAAAAEAACcQAAAAECHxt68zql8LOgYJZBI/VbtJPgfHgA1pTCBl1U54gwv1igTlgfi+FtHNzViTIK7mkg==" });

            migrationBuilder.InsertData(
                table: "DetailNews",
                columns: new[] { "DetailNewsId", "Alias", "Content", "NewsId", "ThumbNews" },
                values: new object[] { 4, "a-lagging-vaccine", "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..", 4, 17 });
        }
    }
}

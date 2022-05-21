using System;
using FakeNewsFilter.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "Home Title", Value = "This is homepage" });

            var RoleId = new Guid("A3314BE5-4C77-4FB6-82AD-302014682A73");

            var AdminId1 = new Guid("69DB714F-9576-45BA-B5B7-F00649BE01DE");
            var AdminId2 = new Guid("69DB714F-9576-45BA-B5B7-F00649BE02DE");
            var AdminId3 = new Guid("69DB714F-9576-45BA-B5B7-F00649BE03DE");
            var AdminId4 = new Guid("69DB714F-9576-45BA-B5B7-F00649BE04DE");
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = RoleId,
                Name = "Admin",
                NormalizedName = "Admin",
            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = new Guid("B4314BE5-4C77-4FB6-82AD-302014682B13"),
                Name = "Subscriber",
                NormalizedName = "Subscriber",
            });

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = AdminId1,
                UserName = "khuyenpb",
                NormalizedUserName = "khuyenpb",
                Email = "bp.khuyen@hutech.edu.vn",
                NormalizedEmail = "BP.KHUYEN@HUTECH.EDU.VN",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Khuyenpb@123"),
                SecurityStamp = string.Empty,
                Name = "Bui Phu Khuyen",
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = AdminId2,
                UserName = "LXThanh",
                NormalizedUserName = "LXThanh",
                Email = "thanh26092000@gmail.com",
                NormalizedEmail = "THANH26092000@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Thanh@123"),
                SecurityStamp = string.Empty,
                Name = "Le Xuan Thanh",
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = AdminId3,
                UserName = "hkhansh27",
                NormalizedUserName = "hkhansh27",
                Email = "khanh200111@gmail.com",
                NormalizedEmail = "KHANH200111@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Khanh@123"),
                SecurityStamp = string.Empty,
                Name = "Huynh Huu Khanh",
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = AdminId4,
                UserName = "HoangPhuc",
                NormalizedUserName = "HoangPhuc",
                Email = "hi@phucs.me",
                NormalizedEmail = "HI@PHUCS.ME",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Phuc@123"),
                SecurityStamp = string.Empty,
                Name = "To Hoang Phuc",
            });

            modelBuilder.Entity<Language>().HasData(
               new Language() { Id = "vi", Name = "Tiếng Việt", Flag = "vi.png", IsDefault = true },
               new Language() { Id = "en", Name = "English", Flag = "en.png", IsDefault = false });


            modelBuilder.Entity<UserRoles>().HasData(new UserRoles
            {
                RoleId = RoleId,
                UserId = AdminId1
            });
            modelBuilder.Entity<UserRoles>().HasData(new UserRoles
            {
                RoleId = RoleId,
                UserId = AdminId2
            });
            modelBuilder.Entity<UserRoles>().HasData(new UserRoles
            {
                RoleId = RoleId,
                UserId = AdminId3
            });
            modelBuilder.Entity<UserRoles>().HasData(new UserRoles
            {
                RoleId = RoleId,
                UserId = AdminId4
            });

            modelBuilder.Entity<TopicNews>().HasData(
               new TopicNews()
               {
                   TopicId = 1,
                   Label = "breaking",
                   Tag = "afghanistan",
                   ThumbTopic = 3,
                   LanguageId = "en",
                   Description = "Follow live as the Taliban seizes territory across Afghanistan in the wake of the U.S. withdrawal.",
                   Timestamp = DateTime.Now

               },
               new TopicNews()
               {
                   TopicId = 2,
                   Label = "featured",
                   Tag = "in-depth",
                   ThumbTopic = 2,
                   LanguageId = "en",
                   Description = "Best nonfiction features, in-depth stores and other long-form content from across the web.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 3,
                   Label = "featured",
                   Tag = "coronavirus",
                   ThumbTopic = 1,
                   LanguageId = "en",
                   Description = "Outbreak of respiratory virus that has killed over 1 million and infected 100 milion worldwide.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 4,
                   Label = "featured",
                   Tag = "top-business",
                   ThumbTopic = 1,
                   LanguageId = "en",
                   Description = "The top business and economic news from around the world with a focus on the United State.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 5,
                   Label = "featured",
                   Tag = "biden-admin",
                   ThumbTopic = 1,
                   LanguageId = "en",
                   Description = "Follow the presidential transition of Joe Biden, including policy plans, appointments and more.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 6,
                   Label = "featured",
                   Tag = "top-news",
                   ThumbTopic = 1,
                   LanguageId = "en",
                   Description = "Top stories from around the world with a focus on news not covered in other feeds.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 7,
                   Label = "featured",
                   Tag = "boston",
                   ThumbTopic = 1,
                   LanguageId = "en",
                   Description = "Follow important local news: politics, business, top events and more. Updated everything evening.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 8,
                   Label = "Thế giới",
                   Tag = "dịch bệnh",
                   ThumbTopic = 3,
                   LanguageId = "vi",
                   Description = "Các thông tin về virut corona.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 9,
                   Label = "Thế giới",
                   Tag = "người Việt Nam",
                   ThumbTopic = 4,
                   LanguageId = "vi",
                   Description = "Cuộc sống của người Việt Trên toàn thế giới.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 10,
                   Label = "TÀI CHÍNH - KINH DOANH",
                   Tag = "Kinh tế",
                   ThumbTopic = 5,
                   LanguageId = "vi",
                   Description = " Nền doanh nghiệp Việt Nam.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 11,
                   Label = "GIÁO DỤC",
                   Tag = "học hành",
                   ThumbTopic = 6,
                   LanguageId = "vi",
                   Description = "Chọn trường nghề phù hợp với bản thân.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 12,
                   Label = "Trò chơi",
                   Tag = "Trò chơi",
                   ThumbTopic = 7,
                   LanguageId = "vi",
                   Description = "Công nghệ mới trong game.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 13,
                   Label = "Công Nghệ",
                   Tag = "Sản phẩm",
                   ThumbTopic = 8,
                   LanguageId = "vi",
                   Description = " Sản phẩm công nghệ mới trong năm.",
                   Timestamp = DateTime.Now
               },
               new TopicNews()
               {
                   TopicId = 14,
                   Label = "Thời sự",
                   Tag = "Phóng sự",
                   ThumbTopic = 9,
                   LanguageId = "vi",
                   Description = " Phóng sự đời sống thường nhật của người dân.",
                   Timestamp = DateTime.Now
               }
               );

            modelBuilder.Entity<Media>().HasData(
                new Media
                {
                    MediaId = 1,
                    Type = Enums.MediaType.Image,
                    PathMedia = "covid.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 2,
                    Type = Enums.MediaType.Image,
                    PathMedia = "taliban.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 3,
                    Type = Enums.MediaType.Image,
                    PathMedia = "kinh-te-tg.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 4,
                    Type = Enums.MediaType.Image,
                    PathMedia = "ngvietnamchau.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 5,
                    Type = Enums.MediaType.Image,
                    PathMedia = "doanh-nghiep.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 6,
                    Type = Enums.MediaType.Image,
                    PathMedia = "chon truong.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 7,
                    Type = Enums.MediaType.Image,
                    PathMedia = "congnghegame.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 8,
                    Type = Enums.MediaType.Image,
                    PathMedia = "congnghemoi.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 9,
                    Type = Enums.MediaType.Image,
                    PathMedia = "phongsu.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 10,
                    Type = Enums.MediaType.Image,
                    PathMedia = "giaothong.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 11,
                    Type = Enums.MediaType.Image,
                    PathMedia = "chungkhoan.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 12,
                    Type = Enums.MediaType.Image,
                    PathMedia = "khoahocvn.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 13,
                    Type = Enums.MediaType.Image,
                    PathMedia = "the-thao1.jpeg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 14,
                    Type = Enums.MediaType.Image,
                    PathMedia = "newsid1.jpg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 15,
                    Type = Enums.MediaType.Image,
                    PathMedia = "newsid2.jpg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 16,
                    Type = Enums.MediaType.Image,
                    PathMedia = "newsid3.jpg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 17,
                    Type = Enums.MediaType.Image,
                    PathMedia = "newsid4.jpg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 18,
                    Type = Enums.MediaType.Image,
                    PathMedia = "newsid5.jpg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 19,
                    Type = Enums.MediaType.Image,
                    PathMedia = "newsid6.png",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 20,
                    Type = Enums.MediaType.Image,
                    PathMedia = "newsid7.jpg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 21,
                    Type = Enums.MediaType.Image,
                    PathMedia = "newsid8.jpg",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 22,
                    Type = Enums.MediaType.Image,
                    PathMedia = "newsid9.jpg",
                    DateCreated = DateTime.Now,
                }
           );
            modelBuilder.Entity<News>().HasData(
                new News
                {
                    NewsId = 1,
                    Title = "Kabul’s Sudden Fall to Taliban Ends U.S. Era in Afghanistan",
                    LanguageId = "en",
                    Source = "https://www.independent.co.uk/arts-entertainment/eurovision/the-rasmus-eurovision-2022-finland-b2077365.html",
                    ImageLink = "https://travelweekly.co.uk/images/cmstw/original/4/e/6/4/4/easid-453165-media-id-34528.jpg",
                    Publisher = "New York Times",
                    DatePublished = new DateTime(2021,02,10),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 2,
                    Title = "Texas high court blocks mask mandates in two of state's largest counties",
                    Source = "https://www.wltx.com/article/sports/clemson/101-15d947ca-db30-4440-b99a-a1e5a6f4ca35",
                    ImageLink = "https://media.wltx.com/assets/WLTX/images/cd8afe4e-86f9-487f-b8b4-5e9313da807e/cd8afe4e-86f9-487f-b8b4-5e9313da807e_1140x641.jpg",
                    LanguageId = "en",
                    Publisher = "NBC News",
                    DatePublished = new DateTime(2021, 02, 20),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 3,
                    Title = "Hospitalizations of Americans under 50 have reached new pandemic highs",
                    LanguageId = "en",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 5,
                    Title = "Thông tin TP.HCM dùng 5 trực thăng phun khử khuẩn là sai sự thật",
                    Source = "https://www.vietnamplus.vn/thong-tin-tphcm-su-dung-5-truc-thang-phun-khu-trung-la-sai-su-that/729372.vnp",
                    ImageLink = "https://cdnimg.vietnamplus.vn/t620/uploaded/fsmsy/2021_07_26/phun_khu_khuan.jpg",
                    Publisher = "TTXVN/Vietnam",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 07, 26),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 6,
                    Title = "Mạnh tay xử lý hành vi đưa tin giả liên quan đến dịch Covid – 19",
                    Source = "https://www.baobaclieu.vn/quoc-phong-an-ninh/manh-tay-xu-ly-tin-gia-tin-sai-su-that-ve-dich-covid-19-tren-mang-72306.html",
                    ImageLink = "https://www.baobaclieu.vn/uploads/image/2021/08/06/13b.jpg",
                    Publisher = "Báo Bạc Liêu",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 08, 06),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 7,
                    Title = "Thông tin nguồn nước Thánh Thiên chữa được Covid-19 là sai sự thật",
                    Source = "https://nhandan.vn/factcheck/thong-tin-nguon-nuoc-thanh-thien-co-the-chua-covid-19-la-sai-su-that-696816/",
                    ImageLink = "https://img.nhandan.com.vn/Files/Images/2022/05/12/Hai_truong_hop_lam_viec_voi_co_q-1652343768786.jpg",
                    Publisher = "Báo Nhân Dân",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2022, 05, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 8,
                    Title = "Bạc Liêu: Mắc Covid-19 được “ưu ái điều trị tại nhà” là sai sự thật",
                    Source = "https://dangcongsan.vn/canh-bao-thong-tin-gia/bac-lieu-mac-covid-19-duoc-uu-ai-dieu-tri-tai-nha-la-sai-su-that-592693.html",
                    ImageLink = "https://file1.dangcongsan.vn/data/0/images/2021/10/01/vulinh/dfhgdfh.jpg?dpi=150&quality=100&w=780",
                    Publisher = "DCSVN",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 10, 01),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 9,
                    Title = "“Chiến dịch tiêm vaccine ngừa Covid-19 cho trẻ em 12-15 tuổi ở xứ Anh bị tạm dừng” là không chính xác",
                    Source = "https://dangcongsan.vn/canh-bao-thong-tin-gia/chien-dich-tiem-vaccine-ngua-covid-19-cho-tre-em-12-15-tuoi-o-xu-anh-bi-tam-dung-la-khong-chinh-xac-591591.html",
                    ImageLink = "https://file1.dangcongsan.vn/data/0/images/2021/09/21/vulinh/video-man-1632200081574.jpg?dpi=150&quality=100&w=780",
                    Publisher = "DCSVN",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 09, 21),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 10,
                    Title = "Săn 'lộc trời': Lội suối nhặt ốc, vào thủ phủ cá chình",
                    Source = "https://thanhnien.vn/san-loc-troi-loi-suoi-nhat-oc-vao-thu-phu-ca-chinh-post1406363.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_29/1-bai-3-1687.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 30),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 11,
                    Title = "Nghề Việt - Nét Việt: Nghề trai Chuôn Ngọ",
                    Source = "https://thanhnien.vn/nghe-viet-net-viet-nghe-trai-chuon-ngo-post1404658.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2021_11_24/nghe-viet-1372.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 25),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 12,
                    Title = "Nỗi lòng người cạo mủ cao su",
                    Source = "https://thanhnien.vn/noi-long-nguoi-cao-mu-cao-su-post1404643.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2021_11_24/cao-su-7917.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 25),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 13,
                    Title = "Khám phá thác ba nhánh hùng vĩ ít người biết giữa Tây Nguyên",
                    Source = "https://thanhnien.vn/kham-pha-thac-ba-nhanh-hung-vi-it-nguoi-biet-giua-tay-nguyen-post1405776.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/fsmxy/2021_11_27/drai-dlong-8588.png",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 14,
                    Title = "Chiếc nồi cổ ‘thần kỳ’ tạo ra món xôi độc đáo của người Nùng ở Đắk Lắk",
                    Source = "https://thanhnien.vn/chiec-noi-co-than-ky-tao-ra-mon-xoi-doc-dao-cua-nguoi-nung-o-dak-lak-post1403687.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/fsmxy/2021_11_21/noi-hap-xoi-co-4376.png",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 15,
                    Title = "Những đứa con tìm về nguồn cội",
                    Source = "https://thanhnien.vn/nhung-dua-con-tim-ve-nguon-coi-post1405816.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_27/22b1-5885.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 28),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 16,
                    Title = "Mang chất Việt vào tranh in trên đất Mỹ",
                    Source = "https://thanhnien.vn/mang-chat-viet-vao-tranh-in-tren-dat-my-post1403198.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2021_11_20/trien-lam-5546.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 21),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 17,
                    Title = "Người phụ nữ Việt phát triển vật liệu phủ chống cháy ở Úc",
                    Source = "https://thanhnien.vn/nguoi-phu-nu-viet-phat-trien-vat-lieu-phu-chong-chay-o-uc-post1401084.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_13/22a1-5500.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 25),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 18,
                    Title = "Cảm hứng từ bữa ăn Việt của bà",
                    Source = "https://thanhnien.vn/cam-hung-tu-bua-an-viet-cua-ba-post1401081.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_13/hinh-1-3868.png",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 19,
                    Title = "Gặp bão, đoàn tàu metro trễ hẹn về TP.HCM",
                    Source = "https://thanhnien.vn/gap-bao-doan-tau-metro-tre-hen-ve-tp-hcm-post1406682.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/rfnmf/2021_11_30/tau-metro-2-acxy-8311.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 09, 21),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 20,
                    Title = "Xuất khẩu thép lần đầu cán mốc 10 tỉ USD",
                    Source = "https://thanhnien.vn/xuat-khau-thep-lan-dau-can-moc-10-ti-usd-post1406650.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/vjryqdxwp/2021_11_30/satthep-chihieu-uver-keev-9147.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 25),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 21,
                    Title = "TP.HCM khát vốn cho giao thông",
                    Source = "https://thanhnien.vn/tp-hcm-khat-von-cho-giao-thong-post1406453.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/pwivoviu/2021_11_30/2a2-8280.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 30),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 22,
                    Title = "Lượng kiều hối tăng mạnh kỷ lục",
                    Source = "https://thanhnien.vn/luong-kieu-hoi-tang-manh-ky-luc-post1405536.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/wpxlcqjwq/2021_11_26/kieu-hoi-3276.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 23,
                    Title = "Dòng vốn mạnh đưa chứng khoán lập đỉnh",
                    Source = "https://thanhnien.vn/dong-von-manh-dua-chung-khoan-lap-dinh-post1404799.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/wpxlcqjwq/2021_11_24/chung-khoan-9665.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 24,
                    Title = "Tin tức giáo dục đặc biệt 1.12: Dạy sử bằng nội dung cảm xúc hay sự kiện?",
                    Source = "https://thanhnien.vn/tin-tuc-giao-duc-dac-biet-1-12-day-su-bang-noi-dung-cam-xuc-hay-su-kien-post1406754.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/bpcgvoiv/2021_11_30/a1-dtan-4946.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 12, 23),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 25,
                    Title = "Hướng vào đại học phù hợp với điểm thi",
                    Source = "https://thanhnien.vn/huong-vao-dai-hoc-phu-hop-voi-diem-thi-post989845.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/wsxrxqeiod/2020_08_31/tuyen-sinh_chbt.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2020, 09, 01),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 26,
                    Title = "Gặp người thầy… đẹp trai nhất trường mầm non!",
                    Source = "https://thanhnien.vn/gap-nguoi-thay-dep-trai-nhat-truong-mam-non-post1403127.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/wobjuko/2021_11_20/anh-1-7862.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 20),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 27,
                    Title = "Thầy giáo dùng tiền khen thưởng ủng hộ Quỹ phòng chống Covid-19",
                    Source = "https://thanhnien.vn/thay-giao-dung-tien-khen-thuong-ung-ho-quy-phong-chong-covid-19-post1075098.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/wobjuko/2021_06_05/3_cdyb.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 06, 05),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 28,
                    Title = "Khoa Y ĐH Quốc gia TP.HCM xét tuyển bổ sung cả thí sinh tự do",
                    Source = "https://thanhnien.vn/khoa-y-dh-quoc-gia-tp-hcm-xet-tuyen-bo-sung-ca-thi-sinh-tu-do-post1116655.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/mffsm/2021_09_29/0-1_pldi.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 09, 29),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 29,
                    Title = "Bản nâng cấp Cyberpunk 2077 sẽ miễn phí cho chủ sở hữu PS4 và Xbox One",
                    Source = "https://thanhnien.vn/ban-nang-cap-cyberpunk-2077-se-mien-phi-cho-chu-so-huu-ps4-va-xbox-one-post1406595.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_30/cyberpunk-4917.png",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 30),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 30,
                    Title = "Phi Vụ Triệu Đô tái kết hợp Free Fire trong phần đặc biệt: Phi Vụ Cuối Cùng tháng 12 này",
                    Source = "https://thanhnien.vn/phi-vu-trieu-do-tai-ket-hop-free-fire-trong-phan-dac-biet-phi-vu-cuoi-cung-thang-12-nay-post1406503.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/dbeyxqxqrs/2021_11_30/1-8294.png",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 30),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 31,
                    Title = "Việt Nam lọt Top 5 đội LMHT: Tốc Chiến thế giới",
                    Source = "https://thanhnien.vn/viet-nam-lot-top-5-doi-lmht-toc-chien-the-gioi-post1404103.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_23/image0-500.png",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 23),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 32,
                    Title = "Riot Games để lộ 4 địa điểm của Chung kết LMHT Thế giới 2022",
                    Source = "https://thanhnien.vn/riot-games-de-lo-4-dia-diem-cua-chung-ket-lmht-the-gioi-2022-post1403726.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_22/1-3692.jpg",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 21),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 33,
                    Title = "Arcane giúp Vi và Jinx tăng vọt tỉ lệ được chọn trong LMHT",
                    Source = "https://thanhnien.vn/arcane-giup-vi-va-jinx-tang-vot-ti-le-duoc-chon-trong-lmht-post1401689.html",
                    ImageLink = "https://image.thanhnien.vn/w2048/Uploaded/2022/xdrkxrvekx/2021_11_15/picture2-6769.png",
                    Publisher = "Báo Thanh Niên",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 11, 16),
                    Timestamp = DateTime.Now,
                }
            );
            modelBuilder.Entity<NewsInTopics>().HasData(
                new NewsInTopics()
                {
                    NewsId = 3,
                    TopicId = 2,
                },
                new NewsInTopics()
                {
                    NewsId = 2,
                    TopicId = 2,
                },
                new NewsInTopics()
                {
                    NewsId = 1,
                    TopicId = 1,
                },
                new NewsInTopics()
                {
                    NewsId = 1,
                    TopicId = 2,
                },
                new NewsInTopics()
                {
                    NewsId = 5,
                    TopicId = 8,
                },
                new NewsInTopics()
                {
                    NewsId = 6,
                    TopicId = 8,
                },
                new NewsInTopics()
                {
                    TopicId = 8,
                    NewsId = 7,
                },
                new NewsInTopics()
                {
                    TopicId = 8,
                    NewsId = 9,
                },
                new NewsInTopics()
                {
                    TopicId = 9,
                    NewsId =10,
                },
                new NewsInTopics()
                {
                    TopicId = 9,
                    NewsId = 11,
                },
                new NewsInTopics()
                {
                    TopicId = 9,
                    NewsId = 12,
                },
                new NewsInTopics()
                {
                    TopicId = 9,
                    NewsId = 13,
                },
                new NewsInTopics()
                {
                    TopicId = 9,
                    NewsId = 14,
                },
                new NewsInTopics()
                {
                    TopicId = 10,
                    NewsId = 15,
                },
                new NewsInTopics()
                {
                    TopicId = 10,
                    NewsId = 16,
                },
                new NewsInTopics()
                {
                    TopicId = 10,
                    NewsId = 17,
                },
                new NewsInTopics()
                {
                    TopicId = 10,
                    NewsId = 18,
                },
                new NewsInTopics()
                {
                    TopicId = 11,
                    NewsId = 19,
                },
                new NewsInTopics()
                {
                    TopicId = 11,
                    NewsId = 20,
                },
                new NewsInTopics()
                {
                    TopicId = 11,
                    NewsId = 21,
                },
                new NewsInTopics()
                {
                    TopicId = 11,
                    NewsId = 22,
                },
                new NewsInTopics()
                {
                    TopicId = 11,
                    NewsId = 23,
                },
                new NewsInTopics()
                {
                    TopicId = 12,
                    NewsId = 24,
                },
                new NewsInTopics()
                {
                    TopicId = 12,
                    NewsId = 25,
                },
                new NewsInTopics()
                {
                    TopicId = 12,
                    NewsId = 26,
                },
                new NewsInTopics()
                {
                    TopicId = 12,
                    NewsId = 27,
                },
                new NewsInTopics()
                {
                    TopicId = 12,
                    NewsId = 28,
                },
                new NewsInTopics()
                {
                    TopicId = 12,
                    NewsId = 29,
                },
                new NewsInTopics()
                {
                    TopicId = 13,
                    NewsId = 30,
                },
                new NewsInTopics()
                {
                    TopicId = 13,
                    NewsId = 31,
                },
                new NewsInTopics()
                {
                    TopicId = 13,
                    NewsId = 32,
                },
                new NewsInTopics()
                {
                    TopicId = 13,
                    NewsId = 33,
                }
                );
        }
    }
}
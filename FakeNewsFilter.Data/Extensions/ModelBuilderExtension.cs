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
            var AdminId2= new Guid("69DB714F-9576-45BA-B5B7-F00649BE02DE");
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
                   Tag = "tin tức",
                   ThumbTopic = 3,
                   LanguageId = "vi",
                   Description = "Kinh tế Việt Nam trong năm.",
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
                }
           );

            modelBuilder.Entity<News>().HasData(
                new News
                {
                    NewsId = 1,
                    Name = "Kabul’s Sudden Fall to Taliban Ends U.S. Era in Afghanistan",
                    Description = "Taliban fighters poured into the Afghan capital on Sunday amid scenes of panic and chaos, bringing a swift and shocking close to the Afghan government and the 20-year American era in the country.",
                    Content = "Test",
                    LanguageId = "en",
                    Source = "test",
                    Publisher = "New York Times",
                    DatePublished = new DateTime(2021,02,10),
                    Timestamp = DateTime.Now,
                    ThumbNews = 1
                },
                new News
                {
                    NewsId = 2,
                    Name = "Texas high court blocks mask mandates in two of state's largest counties",
                    Description = "The masking orders in Dallas and Bexar counties were issued after a lower court ruled last week in favor of local officials.",
                    Content = "Test",
                    Source = "test",
                    LanguageId = "en",
                    Publisher = "NBC News",
                    DatePublished = new DateTime(2021, 02, 20),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 3,
                    Name = "Hospitalizations of Americans under 50 have reached new pandemic highs",
                    Description = "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..",
                    Content = "Test",
                    Source = "test",
                    LanguageId = "en",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 4,
                    Name = "Hospitalizations of Americans under 50 have reached new pandemic highs",
                    Description = "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..",
                    Content = "Test",
                    Source = "test",
                    LanguageId = "en",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 5,
                    Name = "Myanmar-Trung Quốc mở lại 2 cửa khẩu biên giới sau 7 tháng đóng cửa chống Covid-19",
                    Description = "Hai cửa khẩu biên giới giữa Myanmar với Trung Quốc mở cửa lại từ ngày 26.11 với hy vọng hướng đến việc phục hồi thương mại bình thường giữa 2 nước..",
                    Content = "Ngoại giao",
                    Source = "https://thanhnien.vn/myanmar-trung-quoc-mo-lai-2-cua-khau-bien-gioi-sau-7-thang-dong-cua-chong-covid-19-post1405895.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 6,
                    Name = "Vì sao nhiều tàu thuyền ‘biến mất’ ở vùng biển Trung Quốc?",
                    Description = "Giới phân tích cảnh báo rằng việc mất tín hiệu của nhiều tàu thuyền ở Trung Quốc gây xáo trộn chuỗi cung ứng.",
                    Content = "Ngoại giao",
                    Source = "https://thanhnien.vn/vi-sao-nhieu-tau-thuyen-bien-mat-o-vung-bien-trung-quoc-post1405317.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 7,
                    Name = "Bước ngoặt liên minh kinh tế Mỹ - Đài Loan",
                    Description = "Quan hệ kinh tế Mỹ và Đài Loan vừa có thêm bước tiến mới, hàm chứa các hợp tác có vai trò quan trọng trong việc đối phó với sự trỗi dậy của Trung Quốc.",
                    Content = "Ngoại giao",
                    Source = "https://thanhnien.vn/buoc-ngoat-lien-minh-kinh-te-my-dai-loan-post1404652.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 8,
                    Name = "Lượng khách quốc tế đến Thái Lan tăng mạnh sau mở cửa",
                    Description = "Lượng khách quốc tế đến Thái Lan đã tăng mạnh sau khi nước này nới lỏng quy định cách ly cho người đã tiêm vắc xin Covid-19.",
                    Content = "Du lịch",
                    Source = "https://thanhnien.vn/luong-khach-quoc-te-den-thai-lan-tang-manh-sau-mo-cua-post1405440.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 9,
                    Name = "Hướng đi phục hồi bền vững cho APEC",
                    Description = "Các bộ trưởng APEC khẳng định sẽ tăng cường phối hợp và đẩy mạnh nghị trình hoạt động nhằm đảm bảo khôi phục tự do và an toàn đi lại tại khu vực.",
                    Content = "Kinh tế",
                    Source = "https://thanhnien.vn/huong-di-phuc-hoi-ben-vung-cho-apec-post1400053.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 10,
                    Name = "Săn 'lộc trời': Lội suối nhặt ốc, vào thủ phủ cá chình",
                    Description = "Để “săn” ốc đá và cá chình, 2 sản vật ngon bậc nhất ở núi rừng Quảng Trị.",
                    Content = "Dân tộc",
                    Source = "https://thanhnien.vn/san-loc-troi-loi-suoi-nhat-oc-vao-thu-phu-ca-chinh-post1406363.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 11,
                    Name = "Nghề Việt - Nét Việt: Nghề trai Chuôn Ngọ",
                    Description = "Chuôn Ngọ là làng duy nhất cung cấp nguyên liệu các loại vỏ trai, ốc cho cả nước để làm đồ cẩn, khảm, thủ công mỹ nghệ.",
                    Content = "Dân tộc",
                    Source = "https://thanhnien.vn/nghe-viet-net-viet-nghe-trai-chuon-ngo-post1404658.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 12,
                    Name = "Nỗi lòng người cạo mủ cao su",
                    Description = "Khi mọi người bắt đầu lên giường đi ngủ, thì một ngày làm việc của công nhân cạo mủ cao su bắt đầu.",
                    Content = "Nông nghiệp",
                    Source = "https://thanhnien.vn/noi-long-nguoi-cao-mu-cao-su-post1404643.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 13,
                    Name = "Khám phá thác ba nhánh hùng vĩ ít người biết giữa Tây Nguyên",
                    Description = "Thác Drai Dlông với dòng chảy mạnh mẽ quanh năm giữa núi rừng là điểm đến không thể bỏ qua của những ai muốn khám phá Tây Nguyên.",
                    Content = "Cảnh đẹp",
                    Source = "https://thanhnien.vn/kham-pha-thac-ba-nhanh-hung-vi-it-nguoi-biet-giua-tay-nguyen-post1405776.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 14,
                    Name = "Chiếc nồi cổ ‘thần kỳ’ tạo ra món xôi độc đáo của người Nùng ở Đắk Lắk",
                    Description = "Xôi là món ăn được rất nhiều người ưa thích vì dễ ăn và cách làm khá đơn giản, thế nhưng tại gia đình bà Nông Thị Mai.",
                    Content = "Món ăn",
                    Source = "https://thanhnien.vn/chiec-noi-co-than-ky-tao-ra-mon-xoi-doc-dao-cua-nguoi-nung-o-dak-lak-post1403687.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 15,
                    Name = "Những đứa con tìm về nguồn cội",
                    Description = "Câu chuyện của hai anh em sống tại TP.Liverpool được kể lại trong loạt phim tài liệu Nail Bar Boys do Đài BBC khởi chiếu tuần qua.",
                    Content = "Người Việt xa xứ",
                    Source = "https://thanhnien.vn/nhung-dua-con-tim-ve-nguon-coi-post1405816.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 16,
                    Name = "Mang chất Việt vào tranh in trên đất Mỹ",
                    Description = "Để tổ chức thành công triển lãm cá nhân đầu tiên tại Mỹ, họa sĩ tranh in Mai Trần đã trải qua một quá trình dài với nhiều gian nan, thử thách.",
                    Content = "Người Việt xa xứ",
                    Source = "https://thanhnien.vn/mang-chat-viet-vao-tranh-in-tren-dat-my-post1403198.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 17,
                    Name = "Người phụ nữ Việt phát triển vật liệu phủ chống cháy ở Úc",
                    Description = "Một nữ tiến sĩ người Việt được vinh danh là chuyên gia vật liệu hàng đầu tại Úc nhờ góp phần ứng phó cháy rừng tại nước này.",
                    Content = "Người Việt xa xứ",
                    Source = "https://thanhnien.vn/nguoi-phu-nu-viet-phat-trien-vat-lieu-phu-chong-chay-o-uc-post1401084.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 18,
                    Name = "Cảm hứng từ bữa ăn Việt của bà",
                    Description = "Những ký ức về người bà quá cố và các món ăn Việt mà bà chuẩn bị cho gia đình khi xưa đã dẫn dắt đầu bếp David Huynh.",
                    Content = "Người Việt xa xứ",
                    Source = "https://thanhnien.vn/cam-hung-tu-bua-an-viet-cua-ba-post1401081.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 19,
                    Name = "Gặp bão, đoàn tàu metro trễ hẹn về TP.HCM",
                    Description = "Bốn đoàn tàu tuyến metro số 1 (tuyến Bến Thành - Suối Tiên) dự kiến từ Nhật Bản về TP.HCM cuối tháng 11 và đầu tháng 12.",
                    Content = "Giao thông",
                    Source = "https://thanhnien.vn/gap-bao-doan-tau-metro-tre-hen-ve-tp-hcm-post1406682.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 20,
                    Name = "Xuất khẩu thép lần đầu cán mốc 10 tỉ USD",
                    Description = "Số liệu công bố từ Tổng cục Thống kê cho thấy 11 tháng năm 2021, Việt Nam xuất khẩu đạt tổng trị giá 299,67 tỉ USD.",
                    Content = "Xuất khẩu",
                    Source = "https://thanhnien.vn/xuat-khau-thep-lan-dau-can-moc-10-ti-usd-post1406650.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 21,
                    Name = "TP.HCM khát vốn cho giao thông",
                    Description = "UBND TP.HCM vừa có văn bản khẩn gửi Bộ Kế hoạch - Đầu tư liên quan đến dự kiến phương án phân bổ vốn đầu tư công năm 2022 nguồn vốn ngân sách T.Ư.",
                    Content = "Giao thông",
                    Source = "https://thanhnien.vn/tp-hcm-khat-von-cho-giao-thong-post1406453.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 22,
                    Name = "Lượng kiều hối tăng mạnh kỷ lục",
                    Description = "Dự ước năm 2021, lượng kiều hối chuyển về VN sẽ đạt mức kỷ lục 18,1 tỉ USD, bất chấp dịch Covid-19.",
                    Content = "Tài chính",
                    Source = "https://thanhnien.vn/luong-kieu-hoi-tang-manh-ky-luc-post1405536.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 23,
                    Name = "Dòng vốn mạnh đưa chứng khoán lập đỉnh",
                    Description = "Tiền gửi tiết kiệm sụt giảm trong khi dòng vốn tham gia vào thị trường chứng khoán ngày càng tăng.",
                    Content = "Chứng khoán",
                    Source = "https://thanhnien.vn/dong-von-manh-dua-chung-khoan-lap-dinh-post1404799.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 24,
                    Name = "Tin tức giáo dục đặc biệt 1.12: Dạy sử bằng nội dung cảm xúc hay sự kiện?",
                    Description = "Dạy học môn lịch sử trong trường phổ thông như thế nào để học sinh không chán là vấn đề luôn luôn mới.",
                    Content = "Học hành",
                    Source = "https://thanhnien.vn/tin-tuc-giao-duc-dac-biet-1-12-day-su-bang-noi-dung-cam-xuc-hay-su-kien-post1406754.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 25,
                    Name = "Hướng vào đại học phù hợp với điểm thi",
                    Description = "Những lưu ý gì cho thí sinh để vào được đúng ngành nghề yêu thích, phù hợp với điểm số, là vấn đề mà rất nhiều thí sinh hiện đang băn khoăn.",
                    Content = "Đại học",
                    Source = "https://thanhnien.vn/huong-vao-dai-hoc-phu-hop-voi-diem-thi-post989845.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 26,
                    Name = "Gặp người thầy… đẹp trai nhất trường mầm non!",
                    Description = "Từ một chàng thợ xây thích chơi đùa cùng trẻ em, thầy giáo Nguyễn Hồ Tây Phương đã trở thành người thầy hiếm hoi dấn thân mình với nghề dạy dỗ trẻ mầm non.",
                    Content = "Giáo viên",
                    Source = "https://thanhnien.vn/gap-nguoi-thay-dep-trai-nhat-truong-mam-non-post1403127.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 27,
                    Name = "Thầy giáo dùng tiền khen thưởng ủng hộ Quỹ phòng chống Covid-19",
                    Description = "Thầy Nguyễn Viết Tước đã được các cấp từ trung ương đến địa phương khen thưởng hơn 7 triệu đồng.",
                    Content = "Giáo viên",
                    Source = "https://thanhnien.vn/thay-giao-dung-tien-khen-thuong-ung-ho-quy-phong-chong-covid-19-post1075098.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 28,
                    Name = "Khoa Y ĐH Quốc gia TP.HCM xét tuyển bổ sung cả thí sinh tự do",
                    Description = "Khoa Y ĐH Quốc gia TP.HCM thông báo xét tuyển bổ sung 3 ngành ĐH hệ chính quy, trong đó có ngành y khoa.",
                    Content = "Đại học",
                    Source = "https://thanhnien.vn/khoa-y-dh-quoc-gia-tp-hcm-xet-tuyen-bo-sung-ca-thi-sinh-tu-do-post1116655.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 29,
                    Name = "Bản nâng cấp Cyberpunk 2077 sẽ miễn phí cho chủ sở hữu PS4 và Xbox One",
                    Description = "Bản nâng cấp mới sẽ khả dụng vào năm 2022 và hoàn toàn miễn phí cho chủ sở hữu các thiết bị PS4 và Xbox One.",
                    Content = "Trò chơi",
                    Source = "https://thanhnien.vn/ban-nang-cap-cyberpunk-2077-se-mien-phi-cho-chu-so-huu-ps4-va-xbox-one-post1406595.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 30,
                    Name = "Phi Vụ Triệu Đô tái kết hợp Free Fire trong phần đặc biệt: Phi Vụ Cuối Cùng tháng 12 này",
                    Description = "Phi Vụ Triệu Đô bất ngờ quay trở lại Đảo Quân Sự Free Fire lần thứ hai với phần đặc biệt.",
                    Content = "Trò chơi",
                    Source = "https://thanhnien.vn/phi-vu-trieu-do-tai-ket-hop-free-fire-trong-phan-dac-biet-phi-vu-cuoi-cung-thang-12-nay-post1406503.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 31,
                    Name = "Việt Nam lọt Top 5 đội LMHT: Tốc Chiến thế giới",
                    Description = "Giải đấu mang quy mô quốc tế đầu tiên của LMHT: Tốc Chiến vừa kết thúc tại Singapore và một đội tuyển của Việt Nam vào Top 5-6.",
                    Content = "Trò chơi",
                    Source = "https://thanhnien.vn/viet-nam-lot-top-5-doi-lmht-toc-chien-the-gioi-post1404103.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 32,
                    Name = "Riot Games để lộ 4 địa điểm của Chung kết LMHT Thế giới 2022",
                    Description = "Tên của 4 thành phố dự kiến tổ chức giải Chung kết Thế giới 2022 bộ môn eSport Liên Minh Huyền Thoại vô tình bị lộ trong một video thông báo.",
                    Content = "Trò chơi",
                    Source = "https://thanhnien.vn/riot-games-de-lo-4-dia-diem-cua-chung-ket-lmht-the-gioi-2022-post1403726.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 33,
                    Name = "Arcane giúp Vi và Jinx tăng vọt tỉ lệ được chọn trong LMHT",
                    Description = "Bộ phim hoạt hình mang tên Arcane về thế giới trong Liên Minh Huyền Thoại đang nhận đánh giá tốt.",
                    Content = "Trò chơi",
                    Source = "https://thanhnien.vn/arcane-giup-vi-va-jinx-tang-vot-ti-le-duoc-chon-trong-lmht-post1401689.html",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
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
                    TopicId = 1,
                    NewsId = 1,
                },
                new NewsInTopics()
                {
                    NewsId = 8,
                    TopicId = 5,
                },
                new NewsInTopics()
                {
                    NewsId = 8,
                    TopicId = 6,
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
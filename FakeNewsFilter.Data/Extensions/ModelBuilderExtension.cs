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
                    Name = "Kabul’s Sudden Fall to Taliban Ends U.S. Era in Afghanistan",
                    Description = "Taliban fighters poured into the Afghan capital on Sunday amid scenes of panic and chaos, bringing a swift and shocking close to the Afghan government and the 20-year American era in the country.",
                    Content = "Test",
                    LanguageId = "en",
                    Source = "test",
                    Publisher = "New York Times",
                    DatePublished = new DateTime(2021,02,10),
                    Timestamp = DateTime.Now,
                    ThumbNews = 14
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
                    ThumbNews = 15
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
                    ThumbNews = 16
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
                    ThumbNews = 17
                },
                new News
                {
                    NewsId = 5,
                    Name = "Thông tin TP.HCM dùng 5 trực thăng phun khử khuẩn là sai sự thật",
                    Description = "Lãnh đạo Sở TT&TT TP.HCM cho biết, thông tin dùng 5 trực thăng phun khử trùng diệt Covid-19 là sai...",
                    Content = "Theo Sở TT&TT, hiện nay, trên mạng xã hội đang lan truyền thông tin “tối nay từ 11h40 không nên ra đường. Cửa ra vào và cửa sổ nên được đóng lại khi 5 máy bay trực thăng phun chất khử trùng vào không khí để diệt trừ Coronavirus”. Trao đổi với VietNamNet, ông Lâm Đình Thắng, Giám đốc Sở TT&TT cho hay, Bộ Tư lệnh TP.HCM khẳng định, thông tin trên hoàn toàn sai sự thật. Lực lượng quân đội phun khử khuẩn trên địa bàn TP.HCM Trước đó, sáng 23/7, Bộ Tư lệnh TP.HCM phối hợp với Lữ đoàn 87 Binh Chủng hóa học, Tiểu đoàn Phòng hóa 38 Quân khu 7 cùng với lực lượng vũ trang TP và 21 quận, huyện và TP Thủ Đức đồng loạt mở đợt cao điểm phun thuốc khử khuẩn phòng, chống dịch Covid-19 quy mô lớn nhất từ trước tới nay trên địa bàn TP, trong thời gian 7 ngày. Mỗi ngày sẽ có 20 lượt xe tham gia phun thuốc khử khuẩn phòng, chống Covid-19. Theo Hồ Văn/Báo điện tử VietnamNet https://vietnamnet.vn/vn/thoi-su/thong-tin-tp-hcm-dung-5-truc-thang-phun-khu-khuan-la-sai-su-that-759937.html",
                    Source = "https://tingia.gov.vn/tin-tuc/thong-tin-tp-hcm-dung-5-truc-thang-phun-khu-khuan-la-sai-su-that/2695/",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                    ThumbNews = 18
                },
                new News
                {
                    NewsId = 6,
                    Name = "Mạnh tay xử lý hành vi đưa tin giả liên quan đến dịch Covid – 19",
                    Description = "Đại dịch Covid-19 bùng phát trở lại, gây chồng chất thêm khó khăn cho doanh nghiệp, người dân, cũng vì…",
                    Content = "Đại dịch Covid-19 bùng phát trở lại, gây chồng chất thêm khó khăn cho doanh nghiệp, người dân, cũng vì thế mà thông tin về diễn biến đại dịch trở thành mối quan tâm hàng đầu của toàn xã hội. Bên cạnh những thông tin chính xác, tích cực, giúp mọi người nâng cao tinh thần cảnh giác, chung tay phòng chống dịch bệnh, cũng có không ít thông tin sai lệch, thiếu kiểm chứng trên mạng xã hội, gây hoang mang dư luận, tác động xấu đến tình hình an ninh trật tự trên địa bàn. http://brt.vn/thoi-su/dich-viem-phoi-virus-corona/202008/manh-tay-xu-ly-hanh-vi-dua-tin-gia-lien-quan-den-dich-covid-19-8179089/",
                    Source = "https://tingia.gov.vn/tin-video/manh-tay-xu-ly-hanh-vi-dua-tin-gia-lien-quan-den-dich-covid-19/1561/",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                    ThumbNews = 19
                },
                new News
                {
                    NewsId = 7,
                    Name = "Thông tin nguồn nước Thánh Thiên chữa được Covid-19 là sai sự thật",
                    Description = "Đăng tải thông tin sai sự thật trên trang Facebook cá nhân: “nguồn nước Thánh Thiên sẽ cứu chữa rất…",
                    Content = "Đăng tải thông tin sai sự thật trên trang Facebook cá nhân: “nguồn nước Thánh Thiên sẽ cứu chữa rất nhiều bệnh… đặc biệt là Covid-19” gây hoang mang dư luận, bà N.T.T (sinh năm 1969, ngụ huyện Bảo Lâm, Lâm Đồng) đã bị cơ quan chức năng xử phạt 5 triệu đồng. Làm việc với Cơ quan công an, bà N.T.T thừa nhận đã đăng tải thông tin sai sự thật. Thông tin lan truyền Qua công tác bảo đảm an ninh mạng, Phòng An ninh mạng và phòng, chống tội phạm sử dụng công nghệ cao (PA05), Công an tỉnh Lâm Đồng phát hiện bà N.T.T đăng tải trên Facebook cá nhân “T.A.P” bài viết có nội dung: “Nguồn nước Thánh Thiên này sẽ cứu chữa rất nhiều bệnh, đặc biệt là Covid-19…”, kèm theo hình ảnh hai chai nước ghi dòng chữ “nguồn Thánh Thiên”. Kiểm chứng Làm việc với cơ quan công an, bà T trình bày, “nguồn nước thánh thiên” có nguồn gốc từ nhóm tự xưng có tên “trừ quỷ Bảo Lộc” (địa chỉ ở 53/5 Hồ Tùng Mậu, TP Bảo Lộc, Lâm Đồng). Trong quá trình tham gia nhóm, bà T và các thành viên cho rằng, “qua việc cầu nguyện, chữa lành, uống nước thánh thiên thì có thể chữa khỏi Covid-19”, nên bà T đã đăng tải lên Facebook cá nhân. Bà T thừa nhận, “nước thánh thiên” không phải thuốc chữa bệnh Covid-19, không được các cơ quan chức năng cấp giấy phép; thông tin do bà T đăng tải là sai sự thật, không kiểm chứng trước khi đăng tải. Hành vi của bà N.T.T vi phạm pháp luật, quy định tại Nghị định số 15/2020/NĐ-CP, ngày 3/2/2020 của Chính phủ, quy định xử phạt vi phạm hành chính trong lĩnh vực bưu chính, viễn thông, tần số vô tuyến điện, công nghệ thông tin và giao dịch điện tử. Theo PA05 Công an tỉnh Lâm Đồng, nhóm tự xưng “trừ quỷ Bảo Lộc” có hoạt động chữa bệnh nhưng không có giấy phép. Ngày 17/9/2021, ông T.V.L.T.Q, một trong những người đứng đầu nhóm này, sử dụng nhà riêng tại địa chỉ nêu trên làm nơi chữa bệnh trái phép, đã bị UBND TP Bảo Lộc xử phạt vi phạm hành chính 45 triệu đồng, về hành vi “chữa bệnh mà không có giấy phép hoạt động chữa bệnh”. Theo Bảo Văn/Báo Nhân dân điện tử https://nhandan.vn/factcheck/thong-tin-nguon-nuoc-thanh-thien-chua-duoc-covid-19-la-sai-su-that-669233/",
                    Source = "https://tingia.gov.vn/tin-tuc/thong-tin-nguon-nuoc-thanh-thien-chua-duoc-covid-19-la-sai-su-that/3203/",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                    ThumbNews = 20
                },
                new News
                {
                    NewsId = 8,
                    Name = "Bạc Liêu: Mắc Covid-19 được “ưu ái điều trị tại nhà” là sai sự thật",
                    Description = "Thông tin về trường hợp bà Nguyễn Huỳnh Như (Giám đốc Công ty mỹ phẩm Đông Anh ở TP Bạc…",
                    Content = "Thông tin về trường hợp bà Nguyễn Huỳnh Như (Giám đốc Công ty mỹ phẩm Đông Anh ở TP Bạc Liêu) cùng 2 người nhà được ra viện sau nhiều ngày điều trị Covid-19 dù còn dương tính là sai sự thật. Bà Nguyễn Huỳnh Như (Giám đốc Công ty mỹ phẩm Đông Anh ở TP Bạc Liêu) cùng 2 người nhà được ra viện sau nhiều ngày điều trị Covid-19 dù còn dương tính là sai sự thật. Mấy ngày qua, trên mạng xã hội và một vài tờ báo điện tử lan truyền thông tin về trường hợp bà Nguyễn Huỳnh Như (Giám đốc Công ty mỹ phẩm Đông Anh ở TP Bạc Liêu) cùng 2 người nhà được ra viện sau nhiều ngày điều trị Covid-19 dù còn dương tính, gây xôn xao, hoang mang trong nhân dân. Theo giải thích của Ban Chỉ đạo Phòng, chống dịch Covid-19 tỉnh Bạc Liêu, trường hợp bà N.H.N (Nguyễn Huỳnh Như) và bà H.T.K.C (trong gia đình bà Nguyễn Huỳnh Như) xuất viện sau khi được lấy 2 mẫu bệnh phẩm liên tiếp cách nhau 24 giờ, có kết quả xét nghiệm bằng phương pháp Real-time RT-PCR nồng độ vius thấp (Ct > 30); thời gian từ khi lấy mẫu bệnh phẩm cuối cùng tới khi ra viện không quá 24 giờ là đúng quy định của ngành y tế. “Người bệnh được xuất viện với kết quả xét nghiệm bằng phương pháp Real-time RT-PCR nồng độ virus thấp (Ct ≥ 30) thì không có khả năng lây bệnh cho cộng đồng. Người bệnh được xuất viện với kết quả dương tính với SARS-CoV-2 nồng độ virus thấp (Ct ≥ 30) hoàn toàn khác với người tái dương tính với SARS-CoV-2. Chiều 29/9, đại diện Ban Chỉ đạo phòng, chống dịch Covid-19 tỉnh Bạc Liêu nêu rõ: Mấy ngày qua, dư luận và một vài tờ báo điện tử nêu bệnh nhân Nguyễn Huỳnh Như và gia đình bị Covid-19, nhưng được “ưu ái điều trị tại nhà theo phác đồ” là sai sự thật. Đáng lưu ý, hiện nay tỉnh Bạc Liêu không tổ chức điều trị tại nhà cho người mắc Covid-19./. Theo TL/Báo điện tử Đảng Cộng sản https://dangcongsan.vn/canh-bao-thong-tin-gia/bac-lieu-mac-covid-19-duoc-uu-ai-dieu-tri-tai-nha-la-sai-su-that-592693.html",
                    Source = "https://tingia.gov.vn/tin-tuc/bac-lieu-mac-covid-19-duoc-uu-ai-dieu-tri-tai-nha-la-sai-su-that/3182/",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                    ThumbNews = 21
                },
                new News
                {
                    NewsId = 9,
                    Name = "“Chiến dịch tiêm vaccine ngừa Covid-19 cho trẻ em 12-15 tuổi ở xứ Anh bị tạm dừng” là không chính xác",
                    Description = "Hàng nghìn người đã xem 1 video trực tuyến, trong đó xuất hiện 1 người đàn ông nói rằng chiến…",
                    Content = "Hàng nghìn người đã xem 1 video trực tuyến, trong đó xuất hiện 1 người đàn ông nói rằng chiến dịch tiêm vaccine Covid-19 cho trẻ em từ 12 đến 15 tuổi ở xứ Anh (England) đã bị tạm hoãn do sai sót ở khâu giấy tờ. Tuy nhiên, thông tin này là sai sự thật. Đoạn video đăng tải thông tin sai sự thật về chiến dịch tiêm vaccine Covid-19 cho trẻ em 12-15 tuổi ở xứ Anh. (Ảnh chụp màn hình) Đoạn video được đăng tải trên mạng xã hội Facebook và Twitter ngày 20/9 – thời điểm mà chiến dịch tiêm chủng cho trẻ em 12-15 tuổi ở xứ Anh bắt đầu được triển khai. Trong video là hình ảnh 1 người đàn ông ngồi nói trước máy quay trong ô-tô. Người đàn ông này cho biết điện thoại của của mình “đang reo liên tục”, đồng thời nói thêm rằng: “Về cơ bản, việc triển khai tiêm chủng ở các trường học trên khắp xứ Anh đang bị tạm hoãn do Cơ quan Y tế công xứ Anh (PHE) đã không gửi giấy tờ chính xác liên quan đến chỉ dẫn nhóm bệnh nhân (PGD).” Chia sẻ với Reuters qua email, đại diện của PHE cho biết không hề có sự chậm trễ hay tạm dừng như những lời cáo buộc của người đàn ông trong đoạn video, bởi giấy tờ chỉ dẫn nhóm bệnh nhân (PGD) đã được bố trí kịp thời để phục vụ công tác triển khai tiêm chủng. Chiến dịch tiêm vaccine ngừa Covid-19 cho trẻ em từ 12 đến 15 tuổi ở xứ Anh chính thức khởi động ngày 20/9 sau khi đạt đươc khuyến nghị đồng thuận từ Giám đốc Y tế bốn quốc gia của Vương quốc Anh. Các quan chức này khuyến cáo trẻ em trong độ tuổi trên cần tiêm mũi đầu tiên với vaccine Covid-19 của Pfizer/BioNTech. Trước đó hồi đầu tháng 9, Ủy ban Hỗn hợp về Tiêm chủng (JCVI), cơ quan tư vấn về vaccine của chính phủ Anh, đã khuyến nghị không tiêm vaccine ngừa Covid-19 cho trẻ em khỏe mạnh từ 12-15 tuổi. Cùng với việc triển khai ở xứ Anh, các xứ Scotland, Wales và Bắc Ireland cũng sẽ đưa ra lịch trình thực hiện chiến dịch tiêm chủng của riêng mình trong thời gian tới. Do đó, thông tin người đàn ông đưa ra trong đoạn video là sai sự thật. Chiến dịch tiêm vaccine ngừa Covid-19 cho trẻ em từ 12 đến 15 tuổi ở xứ Anh đã bắt đầu ngày 20/9. PHE khẳng định giấy tờ chỉ dẫn nhóm bệnh nhân (PGD) đã được bố trí kịp thời để phục vụ quá trình triển khai./. Theo PV/Báo điện tử Đảng cộng sản https://dangcongsan.vn/canh-bao-thong-tin-gia/chien-dich-tiem-vaccine-ngua-covid-19-cho-tre-em-12-15-tuoi-o-xu-anh-bi-tam-dung-la-khong-chinh-xac-591591.html",
                    Source = "https://tingia.gov.vn/tin-tuc/chien-dich-tiem-vaccine-ngua-covid-19-cho-tre-em-12-15-tuoi-o-xu-anh-bi-tam-dung-la-khong-chinh-xac/3144/",
                    LanguageId = "vi",
                    DatePublished = new DateTime(2021, 04, 12),
                    Timestamp = DateTime.Now,
                    ThumbNews = 22
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
using System;
using System.Collections.Generic;
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
            var AdminId = new Guid("69DB714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = RoleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "System Admin",
            });

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = AdminId,
                UserName = "khuyenpb",
                NormalizedUserName = "khuyenpb",
                Email = "bp.khuyen@hutech.edu.vn",
                NormalizedEmail = "BP.KHUYEN@HUTECH.EDU.VN",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "khuyenpb@123"),
                SecurityStamp = string.Empty,
                Name = "Bui Phu Khuyen",
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = RoleId,
                UserId = AdminId
            });

            modelBuilder.Entity<TopicNews>().HasData(
               new TopicNews()
               {
                   TopicId = 1,
                   Label = "breaking",
                   Tag = "afghanistan",
                   Description = "Follow live as the Taliban seizes territory across Afghanistan in the wake of the U.S. withdrawal."
               },
               new TopicNews()
               {
                   TopicId = 2,
                   Label = "featured",
                   Tag = "in-depth",
                   Description = "Best nonfiction features, in-depth stores and other long-form content from across the web."
               },
               new TopicNews()
               {
                   TopicId = 3,
                   Label = "feature",
                   Tag = "coronavirus",
                   Description = "Outbreak of respiratory virus that has killed over 1 million and infected 100 milion worldwide."
               },
               new TopicNews()
               {
                   TopicId = 4,
                   Label = "feature",
                   Tag = "top-business",
                   Description = "The top business and economic news from around the world with a focus on the United State."
               },
               new TopicNews()
               {
                   TopicId = 5,
                   Label = "feature",
                   Tag = "biden-admin",
                   Description = "Follow the presidential transition of Joe Biden, including policy plans, appointments and more."
               },
               new TopicNews()
               {
                   TopicId = 6,
                   Tag = "top-news",
                   Description = "Top stories from around the world with a focus on news not covered in other feeds."
               },
               new TopicNews()
               {
                   TopicId = 7,
                   Tag = "boston",
                   Description = "Follow important local news: politics, business, top events and more. Updated everything evening."
               }
               );

            modelBuilder.Entity<Media>().HasData(
                new Media
                {
                    MediaId = 1,
                    Type = Enums.MediaType.Image,
                    Url = "https://static01.nyt.com/images/2021/08/15/world/15afghanistan-kabul-airport/merlin_193320777_09900a3b-bd82-47c6-ad73-fddc1219018d-superJumbo.jpg?quality=90&auto=webp",
                    DateCreated = DateTime.Now,
                },
                new Media
                {
                    MediaId = 2,
                    Type = Enums.MediaType.Image,
                    Url = "https://media-cldnry.s-nbcnews.com/image/upload/t_fit-2000w,f_auto,q_auto:best/newscms/2021_30/3495573/210730-greg-abbott-ew-617p.jpg",
                    DateCreated = DateTime.Now,
                }
           );

            modelBuilder.Entity<News>().HasData(
                new News
                {
                    NewsId = 1,
                    Name = "Kabul’s Sudden Fall to Taliban Ends U.S. Era in Afghanistan",
                    Description = "Taliban fighters poured into the Afghan capital on Sunday amid scenes of panic and chaos, bringing a swift and shocking close to the Afghan government and the 20-year American era in the country.",
                    SourceLink = "https://www.nytimes.com/2021/08/15/world/asia/afghanistan-taliban-kabul-surrender.html",
                    MediaNews = 1,
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 2,
                    Name = "Texas high court blocks mask mandates in two of state's largest counties",
                    Description = "The masking orders in Dallas and Bexar counties were issued after a lower court ruled last week in favor of local officials.",
                    SourceLink = "https://www.nbcnews.com/news/us-news/texas-high-court-blocks-mask-mandates-two-state-s-largest-n1276884",
                    MediaNews = 2,
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 3,
                    Name = "Hospitalizations of Americans under 50 have reached new pandemic highs",
                    Description = "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..",
                    SourceLink = "https://www.nytimes.com/live/2021/08/15/world/covid-delta-variant-vaccine/covid-hospitalizations-cdc",
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
                }
                );
        }
    }
}
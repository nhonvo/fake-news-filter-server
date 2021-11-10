﻿using System;
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

            modelBuilder.Entity<Language>().HasData(
               new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
               new Language() { Id = "en", Name = "English", IsDefault = false });


            modelBuilder.Entity<UserRoles>().HasData(new UserRoles
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
                   ThumbTopic = 2,
                   LanguageId = "en",
                   Description = "Follow live as the Taliban seizes territory across Afghanistan in the wake of the U.S. withdrawal."
                   
               },
               new TopicNews()
               {
                   TopicId = 2,
                   Label = "featured",
                   Tag = "in-depth",
                   LanguageId = "en",
                   Description = "Best nonfiction features, in-depth stores and other long-form content from across the web."
               },
               new TopicNews()
               {
                   TopicId = 3,
                   Label = "featured",
                   Tag = "coronavirus",
                   ThumbTopic = 1,
                   LanguageId = "en",
                   Description = "Outbreak of respiratory virus that has killed over 1 million and infected 100 milion worldwide."
               },
               new TopicNews()
               {
                   TopicId = 4,
                   Label = "featured",
                   Tag = "top-business",
                   LanguageId = "en",
                   Description = "The top business and economic news from around the world with a focus on the United State."
               },
               new TopicNews()
               {
                   TopicId = 5,
                   Label = "featured",
                   Tag = "biden-admin",
                   LanguageId = "en",
                   Description = "Follow the presidential transition of Joe Biden, including policy plans, appointments and more."
               },
               new TopicNews()
               {
                   TopicId = 6,
                   Tag = "top-news",
                   LanguageId = "en",
                   Description = "Top stories from around the world with a focus on news not covered in other feeds."
               },
               new TopicNews()
               {
                   TopicId = 7,
                   Tag = "boston",
                   LanguageId = "en",
                   Description = "Follow important local news: politics, business, top events and more. Updated everything evening."
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
                    Publisher = "New York Times",
                    DatePublished = new DateTime(2021,02,10),
                    Timestamp = DateTime.Now,
                },
                new News
                {
                    NewsId = 2,
                    Name = "Texas high court blocks mask mandates in two of state's largest counties",
                    Description = "The masking orders in Dallas and Bexar counties were issued after a lower court ruled last week in favor of local officials.",
                    Content = "Test",
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
                    LanguageId = "en",
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
                }
                );
        }
    }
}
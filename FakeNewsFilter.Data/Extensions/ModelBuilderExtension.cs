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
                NormalizedEmail = "bp.khuyen@hutech.edu.vn",
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
               new TopicNews() {
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
        }
    }
}

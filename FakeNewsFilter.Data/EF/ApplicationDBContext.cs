using System;
using FakeNewsFilter.Data.Configurations;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Data.EF
{
    public class ApplicationDBContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<AppConfig> AppConfigs { get; set; }

        public DbSet<Media> Media { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<NewsInTopics> NewsInTopics { get; set; }

        public DbSet<TopicNews> TopicNews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration using Fluent API
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TopicNewsConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new NewsInTopicsConfiguration());
            modelBuilder.ApplyConfiguration(new FollowConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new MediaConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(x => x.UserId);

            //Data seeding
            modelBuilder.Seed();
        }
    }
}
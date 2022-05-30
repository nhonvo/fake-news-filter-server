using System;
using FakeNewsFilter.Data.Configurations;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Data.EF
{
    public class ApplicationDBContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRoles, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<AppConfig> AppConfigs { get; set; }

        public DbSet<Media> Media { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<DetailNews> DetailNews { get; set; }

        public DbSet<TopicNews> TopicNews { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<NewsInTopics> NewsInTopics { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Vote> Vote { get; set; }

        public DbSet<Follow> Follow { get; set; }

        public DbSet<Source> Source { get; set; }

        public DbSet<Story> Story { get; set; }

        public DbSet<ForgotPassword> ForgotPassword { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<NewsCommunity> NewsCommunity { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration using Fluent API
            modelBuilder.ApplyConfiguration(new AppConfigs());
            modelBuilder.ApplyConfiguration(new UserConfig()); 
            modelBuilder.ApplyConfiguration(new TopicNewsConfig());
            modelBuilder.ApplyConfiguration(new LanguageConfig());
            modelBuilder.ApplyConfiguration(new NewsConfig());
            modelBuilder.ApplyConfiguration(new DetailNewsConfig());
            modelBuilder.ApplyConfiguration(new NewsInTopicsConfig());
            modelBuilder.ApplyConfiguration(new FollowConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            modelBuilder.ApplyConfiguration(new MediaConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfig());
            modelBuilder.ApplyConfiguration(new VoteConfig());
            modelBuilder.ApplyConfiguration(new StoryConfig());
            modelBuilder.ApplyConfiguration(new SourceConfig());
            modelBuilder.ApplyConfiguration(new ForgotPasswordConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new NewsCommunityConfig());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfig());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin").HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(x => x.UserId);

            //Data seeding
            modelBuilder.Seed();
        }
    }
}
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

        public DbSet<TopicNews> TopicNews { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<NewsInTopics> NewsInTopics { get; set; }

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
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration()); 
            modelBuilder.ApplyConfiguration(new TopicNewsConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new NewsInTopicsConfiguration());
            modelBuilder.ApplyConfiguration(new FollowConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new MediaConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());
            modelBuilder.ApplyConfiguration(new VoteConfiguration());
            modelBuilder.ApplyConfiguration(new StoryConfiguration());
            modelBuilder.ApplyConfiguration(new SourceConfiguration());
            modelBuilder.ApplyConfiguration(new ForgotPasswordConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new NewsCommunityConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin").HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(x => x.UserId);

            //Data seeding
            modelBuilder.Seed();
        }
    }
}
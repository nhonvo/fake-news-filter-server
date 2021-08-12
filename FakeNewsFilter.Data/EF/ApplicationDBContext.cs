
using FakeNewsFilter.Data.Configurations;
using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Data.EF
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TopicNewsConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new NewsInTopicsConfiguration());
            modelBuilder.ApplyConfiguration(new FollowConfiguration());
        }

        public DbSet<User> Users { get; set; }
    }
}

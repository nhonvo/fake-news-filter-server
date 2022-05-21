using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    internal class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");

            builder.HasKey(k => k.NewsId);

            builder.Property(k => k.NewsId).UseIdentityColumn();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(150);

            builder.Property(x => x.Timestamp);

            builder.Property(x => x.isVote).HasDefaultValue(true);

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.HasOne(x => x.Language).WithMany(x => x.News).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DetailNews).WithOne(x => x.News).HasForeignKey<DetailNews>(x => x.NewsId);

            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
        }
    }
}

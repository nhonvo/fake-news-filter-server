using System;
using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
                builder.ToTable("News");

                builder.HasKey(k => k.NewsId);

                builder.Property(k => k.NewsId).UseIdentityColumn();

                builder.Property(x => x.Name).IsRequired().HasMaxLength(150);

                builder.Property(x => x.Description).IsRequired().HasMaxLength(250);

                builder.Property(x => x.SourceLink).IsRequired();

                builder.Property(x => x.SocialBeliefs).HasDefaultValue(0);

                builder.Property(x => x.Timestamp);

                builder.HasOne(x => x.Media).WithOne(x => x.News).HasForeignKey<Media>(x => x.NewsId);

        }
    }
}

using System;
using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    public class NewsInTopicsConfiguration : IEntityTypeConfiguration<NewsInTopics>
    {
        public void Configure(EntityTypeBuilder<NewsInTopics> builder)
        {
            builder.HasKey(t => new { t.TopicId, t.NewsId });

            builder.ToTable("NewsInTopics");

            builder.HasOne(t => t.News).WithMany(t => t.NewsInTopics).HasForeignKey(t => t.NewsId);

            builder.HasOne(t => t.TopicNews).WithMany(t => t.NewsInTopics).HasForeignKey(t => t.TopicId);

            builder.Property(t => t.Timestamp).HasDefaultValue(DateTime.Now);

        }
    }
}
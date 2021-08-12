using System;
using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    public class TopicNewsConfiguration : IEntityTypeConfiguration<TopicNews>
    {
        public void Configure(EntityTypeBuilder<TopicNews> builder)
        {
            builder.ToTable("TopicNews");

            builder.HasKey(k => k.TopicId);

            builder.Property(x => x.TopicName).IsRequired().HasMaxLength(30);

            builder.Property(x => x.Tag).IsRequired().HasMaxLength(15);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(250);

            builder.Property(x => x.Timestamp).HasDefaultValue(DateTime.Now);
        }
    }
}

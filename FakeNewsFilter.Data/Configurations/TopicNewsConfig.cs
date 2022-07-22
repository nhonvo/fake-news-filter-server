using System;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    public class TopicNewsConfig : IEntityTypeConfiguration<TopicNews>
    {
        public void Configure(EntityTypeBuilder<TopicNews> builder)
        {
            builder.ToTable("TopicNews");

            builder.HasKey(k => k.TopicId);

            builder.Property(k => k.TopicId).UseIdentityColumn();

            builder.Property(x => x.Tag).IsRequired().HasMaxLength(15);

            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.Status).HasDefaultValue(Status.Active);

            builder.Property(x => x.Label).HasDefaultValue("normal");

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.HasOne(x => x.Language).WithMany(x => x.TopicNews).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Timestamp).HasDefaultValue(DateTime.Now);

            builder.HasOne(x => x.Media).WithOne(x => x.TopicNews).HasForeignKey<TopicNews>(x => x.ThumbTopic);

            

        }
    }
}
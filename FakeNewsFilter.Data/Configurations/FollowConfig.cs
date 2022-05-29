using System;
using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    public class FollowConfig : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasKey(t => new { t.TopicId, t.UserId });

            builder.ToTable("Follow");

            builder.HasOne(t => t.User).WithMany(t => t.Follows).HasForeignKey(t => t.UserId);

            builder.HasOne(t => t.TopicNews).WithMany(t => t.Follows).HasForeignKey(t => t.TopicId);
        }
    }
}
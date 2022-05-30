using System;
using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    public class VoteConfig : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasKey(k => new { k.UserId, k.NewsId });

            builder.ToTable("Vote");

            builder.HasOne(n => n.News).WithMany(v => v.Vote).HasForeignKey(n => n.NewsId);

            builder.HasOne(u => u.User).WithMany(v => v.Vote).HasForeignKey(u => u.UserId);

            builder.Property(t => t.Timestamp).HasDefaultValue(DateTime.Now);

        }
    }
}

using System;
using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    public class DetailNewsConfiguration : IEntityTypeConfiguration<DetailNews>
    {
        public void Configure(EntityTypeBuilder<DetailNews> builder)
        {
            builder.ToTable("DetailNews");

            builder.HasKey(k => k.DetailNewsId);

            builder.Property(k => k.DetailNewsId).UseIdentityColumn();

            builder.Property(x => x.Alias).IsRequired();

            builder.Property(x => x.Content).IsRequired();

            builder.HasOne(x => x.Media).WithOne(x => x.DetailNews).HasForeignKey<DetailNews>(x => x.ThumbNews);

        }
    }
}


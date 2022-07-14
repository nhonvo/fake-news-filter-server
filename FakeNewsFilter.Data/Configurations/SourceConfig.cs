using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FakeNewsFilter.Data.Configurations
{
    public class SourceConfig : IEntityTypeConfiguration<Source>
    {
        public void Configure(EntityTypeBuilder<Source> builder)
        {
            builder.ToTable("Source");

            builder.HasKey(k => k.SourceId);

            builder.Property(x => x.SourceName).IsRequired().HasMaxLength(250);

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.HasOne(x => x.Language).WithMany(x => x.Source).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.Status).HasDefaultValue(Status.Active);

        }
    }
}

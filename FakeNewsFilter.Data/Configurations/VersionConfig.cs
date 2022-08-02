using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Version = FakeNewsFilter.Data.Entities.Version;

namespace FakeNewsFilter.Data.Configurations
{
    public class VersionConfig : IEntityTypeConfiguration<Version>
    {
        public void Configure(EntityTypeBuilder<Version> builder)
        {
            builder.ToTable("Version");

            builder.HasKey(k => k.VersionId);

            builder.Property(k => k.VersionId).UseIdentityColumn();

            builder.Property(k => k.CreateTime).HasDefaultValue(DateTime.Now);
        }
    }
}


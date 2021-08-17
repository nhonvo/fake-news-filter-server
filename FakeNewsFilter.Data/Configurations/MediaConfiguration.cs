using System;
using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("Media");

            builder.HasKey(k => k.MediaId);

            builder.Property(k => k.MediaId).UseIdentityColumn();

            builder.Property(x => x.Type).IsRequired();

            builder.Property(x => x.DateCreated);
        }
    }
}
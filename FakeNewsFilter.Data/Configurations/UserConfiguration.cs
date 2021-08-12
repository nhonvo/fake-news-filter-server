using System;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(k => k.UserId);

            builder.Property(x => x.Password).IsRequired().IsUnicode(false).HasMaxLength(20);

            builder.Property(x => x.Email).IsRequired().IsUnicode(false).HasMaxLength(70);

            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
        }
    }
}

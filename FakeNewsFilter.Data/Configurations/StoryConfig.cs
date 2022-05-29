using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FakeNewsFilter.Data.Configurations
{
    public class StoryConfig : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.ToTable("Story");

            builder.HasKey(k => k.StoryId);

            builder.HasOne(t => t.Source).WithMany(t => t.Story).HasForeignKey(t => t.SourceId);

            builder.HasOne(x => x.Media).WithOne(x => x.Story).HasForeignKey<Story>(x => x.Thumbstory);

            builder.Property(t => t.Timestamp).HasDefaultValue(DateTime.Now);

            builder.Property(x => x.Link);

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.HasOne(x => x.Language).WithMany(x => x.Story).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

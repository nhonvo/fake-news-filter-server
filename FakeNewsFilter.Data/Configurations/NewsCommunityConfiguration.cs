using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    internal class NewsCommunityConfiguration : IEntityTypeConfiguration<NewsCommunity>
    {
        public void Configure(EntityTypeBuilder<NewsCommunity> builder)
        {
            builder.ToTable("NewsCommunity");

            builder.HasKey(k => k.NewsCommunityId);

            builder.Property(k => k.NewsCommunityId).UseIdentityColumn();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(150);

            builder.Property(x => x.Content).IsRequired();

            builder.Property(x => x.IsPopular).HasDefaultValue(false);

            builder.Property(x => x.DatePublished);

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.HasOne(x => x.Language).WithMany(x => x.newsCommunities).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Status).HasDefaultValue(Status.Pending);

            builder.HasOne(x => x.Media).WithOne(x => x.NewsCommunity).HasForeignKey<NewsCommunity>(x => x.ThumbNews);
        }
    }
}

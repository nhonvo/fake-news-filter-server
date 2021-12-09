using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");

            builder.HasKey(k => k.CommentId);

            builder.Property(x => x.Content);

            builder.HasOne(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId);

            builder.HasOne(t => t.News).WithMany(t => t.Comment).HasForeignKey(t => t.NewsId);

            builder.HasOne(t => t.User).WithMany(t => t.Comment).HasForeignKey(t => t.UserId);

            builder.Property(x => x.Timestamp);

        }
    }
}

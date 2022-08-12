using System;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    public class FeedbackConfig : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
           
            builder.ToTable("Feedback");

            builder.HasOne(f => f.Media).WithOne(f => f.MediaFeedback).HasForeignKey<Feedback>(f => f.Screenshoot);

            builder.Property(t => t.Status).HasDefaultValue(Status.Pending);

            builder.Property(t => t.Timestamp).HasDefaultValue(DateTime.Now);

        }
    }
}


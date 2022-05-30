using FakeNewsFilter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeNewsFilter.Data.Configurations
{
    public class ForgotPasswordConfig : IEntityTypeConfiguration<ForgotPassword>
    {
        public void Configure(EntityTypeBuilder<ForgotPassword> builder)
        {
            builder.ToTable("ForgotPassword");

            builder.HasKey(x => x.IdForgotPassword);

            builder.Property(k => k.IdForgotPassword).UseIdentityColumn();

            builder.Property(x => x.Email);

        }
    }
}

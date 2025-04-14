using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class DepositProductConfiguration : IEntityTypeConfiguration<DepositProduct>
    {
        public void Configure(EntityTypeBuilder<DepositProduct> builder)
        {
            builder.Property(lm => lm.Name).HasMaxLength(100);

            builder.HasOne(l => l.Deposit)
            .WithMany(m => m.DepositProducts)
            .HasForeignKey(m => m.DepositId);

            builder.HasOne(l => l.LevelMembership)
                .WithMany(m => m.DepositProducts)
                .HasForeignKey(m => m.LevelMembershipId);

            builder.HasOne(b => b.Coupon)
                .WithMany(l => l.DepositProducts)
                .HasForeignKey(b => b.CouponId);
        }
    }
}

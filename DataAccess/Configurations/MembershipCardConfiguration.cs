using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class MembershipCardConfiguration : IEntityTypeConfiguration<MembershipCard>
    {
        public void Configure(EntityTypeBuilder<MembershipCard> builder)
        {
            builder.HasKey(mc => mc.Id);
            builder.Property(mc => mc.CustomerId).IsRequired();
            builder.Property(mc => mc.LevelMemberShipId).IsRequired();
            builder.Property(mc => mc.Points).HasDefaultValue(0);
            builder.Property(mc => mc.Expired).HasDefaultValueSql("GETDATE()");
            builder.Property(mc => mc.Description).HasMaxLength(500);

            builder.HasOne(mc => mc.Customer)
                .WithMany(c => c.MembershipCards)
                .HasForeignKey(mc => mc.CustomerId);

            builder.HasOne(mc => mc.LevelMemberShip)
                .WithMany(lm => lm.MembershipCards)
                .HasForeignKey(mc => mc.LevelMemberShipId);

            builder.HasOne(mc => mc.CreatedBy)
                .WithMany(u => u.CreatedMembershipCards)
                .HasForeignKey(mc => mc.CreatedById);

            builder.HasOne(mc => mc.LastModifiedBy)
                .WithMany(u => u.LastModifiedMembershipCards)
                .HasForeignKey(mc => mc.LastModifiedById);
        }
    }
}

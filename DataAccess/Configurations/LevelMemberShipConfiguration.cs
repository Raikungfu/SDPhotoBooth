using BussinessObject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations
{
    public class LevelMemberShipConfiguration : IEntityTypeConfiguration<LevelMemberShip>
    {
        public void Configure(EntityTypeBuilder<LevelMemberShip> builder)
        {
            builder.Property(lm => lm.Name).IsRequired().HasMaxLength(100);

            builder.Property(lm => lm.Description).HasMaxLength(500);

            builder.Property(lm => lm.Price).HasPrecision(18, 2);

            builder.HasMany(l => l.MembershipCards)
            .WithOne(m => m.LevelMemberShip)
            .HasForeignKey(m => m.LevelMemberShipId);

            builder.HasOne(b => b.CreatedBy)
               .WithMany(l => l.CreatedLevelMemberships)
               .HasForeignKey(b => b.CreatedById);

            builder.HasOne(b => b.LastModifiedBy)
                .WithMany(l => l.LastModifiedLevelMemberships)
                .HasForeignKey(b => b.LastModifiedById);
        }
    }
}

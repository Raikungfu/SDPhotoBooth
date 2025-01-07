using BussinessObject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations
{
    public class LevelMemberShipConfiguration : IEntityTypeConfiguration<LevelMemberShip>
    {
        public void Configure(EntityTypeBuilder<LevelMemberShip> builder)
        {
            builder.HasKey(lm => lm.Id);
            builder.Property(lm => lm.Name).IsRequired().HasMaxLength(100);
            builder.Property(lm => lm.Description).HasMaxLength(500);
            builder.Property(lm => lm.Price).HasPrecision(18, 2);
        }
    }
}

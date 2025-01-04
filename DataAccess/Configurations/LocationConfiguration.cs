using BussinessObject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.LocationName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(l => l.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(l => l.Booths)
                .WithOne(b => b.Location)
                .HasForeignKey(b => b.LocationId);

            builder.HasMany(l => l.Staffs)
                .WithOne(u => u.Location)
                .HasForeignKey(u => u.LocationId);
        }
    }
}

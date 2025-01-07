using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class BoothConfiguration : IEntityTypeConfiguration<Booth>
    {
        public void Configure(EntityTypeBuilder<Booth> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.LocationId).IsRequired();
            builder.Property(b => b.BoothName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(b => b.Description).HasMaxLength(500);
            builder.Property(b => b.Status).HasDefaultValue(true);

            builder.HasOne(b => b.Location)
                .WithMany(l => l.Booths)
                .HasForeignKey(b => b.LocationId);
        }
    }
}

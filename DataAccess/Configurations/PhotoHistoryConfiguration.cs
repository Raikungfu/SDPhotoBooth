using BussinessObject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations
{
    public class PhotoHistoryConfiguration : IEntityTypeConfiguration<PhotoHistory>
    {
        public void Configure(EntityTypeBuilder<PhotoHistory> builder)
        {
            builder.HasKey(ph => ph.Id);
            builder.Property(ph => ph.Amount).HasDefaultValue(0).HasPrecision(18, 2);

            builder.HasOne(ph => ph.Session)
                .WithOne(s => s.PhotoHistory)
                .HasForeignKey<PhotoHistory>(ph => ph.SessionCode)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ph => ph.Customer)
                .WithMany(u => u.PhotoHistories)
                .HasForeignKey(ph => ph.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(ph => ph.Location)
                .WithMany(l => l.PhotoHistories)
                .HasForeignKey(ph => ph.LocationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

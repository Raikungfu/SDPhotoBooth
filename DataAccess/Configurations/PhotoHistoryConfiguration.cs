using BussinessObject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations
{
    public class PhotoHistoryConfiguration : IEntityTypeConfiguration<PhotoHistory>
    {
        public void Configure(EntityTypeBuilder<PhotoHistory> builder)
        {
            builder.Property(ph => ph.Amount).HasDefaultValue(0).HasPrecision(18, 2);

            builder.HasOne(ph => ph.Session)
                .WithOne(s => s.PhotoHistory)
                .HasForeignKey<PhotoHistory>(ph => ph.SessionCode)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ph => ph.Customer)
                .WithMany(u => u.PhotoHistories)
                .HasForeignKey(ph => ph.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Booth)
            .WithMany(l => l.PhotoHistories)  
            .HasForeignKey(p => p.LocationId)
            .OnDelete(DeleteBehavior.SetNull);


            builder.HasMany(p => p.Photos)
           .WithOne(ph => ph.PhotoHistory) 
           .HasForeignKey(ph => ph.PhotoHistoryId);
        }
    }
}

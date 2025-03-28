using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasIndex(o => o.Code)
                .IsUnique();

            builder.Property(s => s.IsActive).HasDefaultValue(true);

            builder.Property(s => s.Expired).HasDefaultValueSql("GETDATE()");

            builder.HasOne(o => o.Order)
                .WithOne(o => o.Session)
                .HasForeignKey<Session>(o => o.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ph => ph.PhotoHistory)
                .WithOne(s => s.Session)
                .HasForeignKey<PhotoHistory>(ph => ph.SessionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

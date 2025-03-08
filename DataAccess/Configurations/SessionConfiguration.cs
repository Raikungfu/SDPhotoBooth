using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.Code);
            builder.Property(s => s.IsActive).HasDefaultValue(true);
            builder.Property(s => s.Expired).HasDefaultValueSql("GETDATE()");

            builder.HasOne(s => s.TypeSession)
                .WithMany(ts => ts.Sessions)
                .HasForeignKey(s => s.TypeSessionId);

            builder.HasOne(s => s.Booth)
                .WithMany(b => b.Sessions)
                .HasForeignKey(s => s.BoothId);

            builder.HasOne(u => u.Order)
              .WithOne(p => p.Session)
              .HasForeignKey<Session>(s => s.OrderId);
        }
    }
}

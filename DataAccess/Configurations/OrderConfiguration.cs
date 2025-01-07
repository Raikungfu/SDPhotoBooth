using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Amount).IsRequired().HasPrecision(18, 2);
            builder.Property(o => o.SessionId).IsRequired();

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            builder.HasOne(o => o.Payment)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PaymentId);

            builder.HasOne(o => o.Session)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.SessionId);
        }
    }
}

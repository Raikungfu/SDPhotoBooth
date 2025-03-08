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

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            builder.HasOne(o => o.PaymentMethod)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PaymentMethodId);

            builder.HasOne(o => o.TypeSession)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.TypeSessionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.CreatedBy)
                .WithMany(u => u.CreatedOrders)
                .HasForeignKey(o => o.CreatedById);

            builder.HasOne(o => o.LastModifiedBy)
                .WithMany(u => u.LastModifiedOrders)
                .HasForeignKey(o => o.LastModifiedById);
        }
    }
}

using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.MethodName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(500);

            builder.HasOne(b => b.CreatedBy)
               .WithMany(l => l.CreatedPaymentMethods)
               .HasForeignKey(b => b.CreatedById);

            builder.HasOne(b => b.LastModifiedBy)
                .WithMany(l => l.LastModifiedPaymentMethods)
                .HasForeignKey(b => b.LastModifiedById);
        }
    }
}

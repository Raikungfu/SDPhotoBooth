using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class BoothStyleConfiguration : IEntityTypeConfiguration<BoothStyle>
    {
        public void Configure(EntityTypeBuilder<BoothStyle> builder)
        {
            builder.HasKey(bs => bs.Id);
            builder.Property(bs => bs.StyleName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(bs => bs.Description)
                .HasMaxLength(500);
            builder.Property(lm => lm.Price).HasPrecision(18, 2);
            builder.HasOne(b => b.CreatedBy)
                .WithMany(l => l.CreatedBoothStyles)
                .HasForeignKey(b => b.CreatedById);
            builder.HasOne(b => b.LastModifiedBy)
                .WithMany(l => l.LastModifiedBoothStyles)
                .HasForeignKey(b => b.LastModifiedById);
        }
    }
}

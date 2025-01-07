using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class PhotoStyleConfiguration : IEntityTypeConfiguration<PhotoStyle>
    {
        public void Configure(EntityTypeBuilder<PhotoStyle> builder)
        {
            // Thiết lập khóa chính
            builder.HasKey(ps => ps.Id);

            // Thiết lập thuộc tính Type
            builder.Property(ps => ps.Type)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Type");

            // Thiết lập thuộc tính Description
            builder.Property(ps => ps.Description)
                .HasMaxLength(500);

            builder.HasOne(b => b.LastModifiedBy)
               .WithMany(l => l.LastModifiedPhotoStyles)
               .HasForeignKey(b => b.LastModifiedById);
        }
    }
}

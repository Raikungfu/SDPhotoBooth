using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class PhotoStyleConfiguration : IEntityTypeConfiguration<PhotoStyle>
    {
        public void Configure(EntityTypeBuilder<PhotoStyle> builder)
        {
            builder.Property(ps => ps.Name)
                .IsRequired();

            builder.Property(ps => ps.Description)
                .HasMaxLength(500);

            builder.HasMany(ps => ps.Photos)
                .WithOne(p => p.PhotoStyle)
                .HasForeignKey(p => p.PhotoStyleId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class PhotoStyleConfiguration : IEntityTypeConfiguration<PhotoStyle>
    {
        public void Configure(EntityTypeBuilder<PhotoStyle> builder)
        {
            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Name)
                .IsRequired();

            builder.Property(ps => ps.Description)
                .HasMaxLength(500);

            builder.Property(ps => ps.ImageUrl)
                .HasMaxLength(500);
        }
    }
}

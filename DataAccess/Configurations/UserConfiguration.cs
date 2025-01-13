using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Gender)
                .IsRequired();

            builder.Property(u => u.Balance)
                .HasPrecision(18, 2);

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(u => u.Location)
                .WithMany(l => l.Staffs)
                .HasForeignKey(u => u.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

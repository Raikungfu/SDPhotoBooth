using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(255);
            builder.Property(u => u.Phone).HasMaxLength(20);
            builder.Property(u => u.UserType).IsRequired();
            builder.Property(u => u.Balance).HasPrecision(18, 2);

            builder.HasOne(u => u.Location)
                .WithMany(l => l.Staffs)
                .HasForeignKey(u => u.LocationId);

            builder.HasOne(u => u.Location)        
                .WithMany(l => l.Staffs)      
                .HasForeignKey(u => u.LocationId)  
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}

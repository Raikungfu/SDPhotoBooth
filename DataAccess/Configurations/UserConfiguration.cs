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

            builder.HasOne(u => u.Location)
                .WithMany(l => l.Staffs)
                .HasForeignKey(u => u.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<User>(u => u.WalletId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

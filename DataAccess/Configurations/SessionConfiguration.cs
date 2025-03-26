using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasIndex(o => o.Code)
                .IsUnique();

            builder.HasOne(o => o.PhotoHistory)
                .WithOne(o => o.Session)
                .HasForeignKey<Session>(o => o.PhotoHistoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Order)
                .WithOne(o => o.Session)
                .HasForeignKey<Session>(o => o.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

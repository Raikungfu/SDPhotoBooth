using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class TypeSessionConfiguration : IEntityTypeConfiguration<TypeSession>
    {
        public void Configure(EntityTypeBuilder<TypeSession> builder)
        {
            builder.HasKey(ts => ts.Id);
            builder.Property(ts => ts.Name).IsRequired().HasMaxLength(100);
            builder.Property(ts => ts.Description).HasMaxLength(500);
        }
    }
}

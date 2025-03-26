using BussinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class AIPhotoboothDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AIPhotoboothDbContext(DbContextOptions<AIPhotoboothDbContext> options) : base(options)
        {
        }

        public DbSet<Location> Location { get; set; }
        public DbSet<Booth> Booth { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<PhotoHistory> PhotoHistory { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<TypeSession> TypeSession { get; set; }
        public DbSet<PhotoStyle> PhotoStyle { get; set; }
        public DbSet<MembershipCard> MembershipCard { get; set; }
        public DbSet<LevelMemberShip> LevelMemberShip { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AIPhotoboothDbContext).Assembly);
        }
    }
}

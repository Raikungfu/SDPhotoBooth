using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class AIPhotoboothDbContext : DbContext
    {
        public AIPhotoboothDbContext(DbContextOptions<AIPhotoboothDbContext> options) : base(options)
        {
        }

        public DbSet<User> User{ get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Booth> Booth { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<PhotoHistory> PhotoHistory { get; set; }
        public DbSet<PhotoStyle> PhotoStyle { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<MembershipCard> MembershipCard { get; set; }
        public DbSet<LevelMemberShip> LevelMemberShip { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<TypeSession> TypeSession { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //SeedData.Seed(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AIPhotoboothDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModified = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

    }
}

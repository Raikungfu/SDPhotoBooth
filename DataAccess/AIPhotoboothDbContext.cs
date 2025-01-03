using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AIPhotoboothDbContext : DbContext
    {
        public AIPhotoboothDbContext(DbContextOptions<AIPhotoboothDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Booth> Booth { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<PhotoHistory> PhotoHistory { get; set; }
        public DbSet<PhotoStyle> PhotoStyle { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<MembershipCard> MembershipCard { get; set; }
        public DbSet<LevelMembership> LevelMemberShip { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<TypeSession> TypeSession { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booth>()
                .HasOne(b => b.Location)
                .WithMany(l => l.Booths)
                .HasForeignKey(b => b.LocationId);

            modelBuilder.Entity<MembershipCard>()
                .HasOne(mc => mc.Customer)
                .WithMany(u => u.MembershipCards)
                .HasForeignKey(mc => mc.CustomerId);

            modelBuilder.Entity<MembershipCard>()
                .HasOne(mc => mc.LevelMembership)
                .WithMany()
                .HasForeignKey(mc => mc.LevelMemberShipId);

            modelBuilder.Entity<PhotoHistory>()
                .HasOne(ph => ph.Customer)
                .WithMany(u => u.PhotoHistories)
                .HasForeignKey(ph => ph.CustomerId);

            modelBuilder.Entity<PhotoHistory>()
                .HasOne(ph => ph.Location)
                .WithMany(l => l.PhotoHistories)
                .HasForeignKey(ph => ph.LocationId);

            modelBuilder.Entity<PhotoHistory>()
                .HasOne(ph => ph.Booth)
                .WithMany(b => b.PhotoHistories)
                .HasForeignKey(ph => ph.BoothId);

            modelBuilder.Entity<PhotoHistory>()
                .HasOne(ph => ph.Session)
                .WithOne(s => s.PhotoHistory)
                .HasForeignKey<PhotoHistory>(ph => ph.SessionId);

            modelBuilder.Entity<Photo>()
                .HasOne(p => p.PhotoStyle)
                .WithMany(ps => ps.Photos)
                .HasForeignKey(p => p.PhotoStyleId);

            modelBuilder.Entity<Photo>()
                .HasOne(p => p.PhotoHistory)
                .WithMany(ph => ph.Photos)
                .HasForeignKey(p => p.PhotoHistoryId);

            modelBuilder.Entity<Order>()
                .HasOne(t => t.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(t => t.Payment)
                .WithMany(p => p.Orders)
                .HasForeignKey(t => t.PaymentId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.TypeSession)
                .WithMany(ts => ts.Sessions)
                .HasForeignKey(s => s.TypeSessionId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Location)
                .WithMany(l => l.Staffs)
                .HasForeignKey(u => u.LocationId);
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

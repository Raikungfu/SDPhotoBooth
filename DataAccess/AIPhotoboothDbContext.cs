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

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Booth> Booths { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoHistory> PhotoHistories { get; set; }
        public DbSet<PhotoStyle> PhotoStyles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<MembershipCard> MembershipCards { get; set; }
        public DbSet<LevelMemberShip> LevelMemberShips { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<TypeSession> TypeSessions { get; set; }

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
                .HasOne(mc => mc.LevelMemberShip)
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

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Payment)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.PaymentId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.TypeSession)
                .WithMany(ts => ts.Sessions)
                .HasForeignKey(s => s.TypeSessionId);
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

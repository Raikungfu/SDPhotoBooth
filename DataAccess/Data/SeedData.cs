using BussinessObject.Models;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Seed(AIPhotoboothDbContext context)
    {
        using var transaction = context.Database.BeginTransaction();

        // Seed data for Location
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Location] ON");
        if (!context.Location.Any())
        {
            context.Location.AddRange(
                new Location { Id = 1, LocationName = "Main Office", Address = "123 Main St", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 },
                new Location { Id = 2, LocationName = "Branch Office", Address = "456 Side St", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 }
            );
        }
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Location] OFF");

        // Seed data for User
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [User] ON");
        if (!context.User.Any())
        {
            context.User.AddRange(
                new User { Id = 1, Username = "admin", PasswordHash = "hashedpassword1", Email = "admin@example.com", Phone = "1234567890", UserType = "Admin", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                new User { Id = 2, Username = "user1", PasswordHash = "hashedpassword2", Email = "user1@example.com", Phone = "0987654321", UserType = "Customer", BirthDate = new DateTime(1990, 1, 1), Balance = 100.00m, LocationId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false }
            );
        }
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [User] OFF");

        // Seed data for Payment
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Payment] ON");
        if (!context.PaymentMethod.Any())
        {
            context.PaymentMethod.AddRange(
                new PaymentMethod { Id = 1, MethodName = "Credit Card", Description = "Pay using credit card", IsActive = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 },
                new PaymentMethod { Id = 2, MethodName = "PayPal", Description = "Pay using PayPal", IsActive = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 }
            );
        }
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Payment] OFF");

        // Seed data for LevelMemberShip
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [LevelMemberShip] ON");
        if (!context.LevelMemberShip.Any())
        {
            context.LevelMemberShip.AddRange(
                new LevelMemberShip { Id = 1, Name = "Basic", Description = "Basic membership", Price = 10.00m, Duration = 30, Point = 100, IsActive = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 },
                new LevelMemberShip { Id = 2, Name = "Premium", Description = "Premium membership", Price = 50.00m, Duration = 90, Point = 500, IsActive = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 }
            );
        }
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [LevelMemberShip] OFF");

        // Seed data for Booth
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Booth] ON");
        if (!context.Booth.Any())
        {
            context.Booth.AddRange(
                new Booth { Id = 1, LocationId = 1, BoothName = "Booth A", Description = "Main booth at Main Office", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 },
                new Booth { Id = 2, LocationId = 2, BoothName = "Booth B", Description = "Branch booth", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 }
            );
        }
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Booth] OFF");

        // Seed data for TypeSession
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [TypeSession] ON");
        if (!context.TypeSession.Any())
        {
            context.TypeSession.AddRange(
                new TypeSession { Id = 1, Name = "Photography", Description = "Photo session", Duration = 60, AbleTakenNumber = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 },
                new TypeSession { Id = 2, Name = "Videography", Description = "Video session", Duration = 120, AbleTakenNumber = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 }
            );
        }
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [TypeSession] OFF");

        // Seed data for Session
        //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Session] ON");
        if (!context.Session.Any())
        {
            context.Session.AddRange(
                new Session { Code = "SESSION1", IsActive = true, TypeSessionId = 1, BoothId = 1, CreatedAt = DateTime.UtcNow },
                new Session { Code = "SESSION2", IsActive = true, TypeSessionId = 2, BoothId = 2, CreatedAt = DateTime.UtcNow }
            );
        }
        context.SaveChanges();
        //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Session] OFF");

        // Seed data for PhotoHistory
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoHistory] ON");
        if (!context.PhotoHistory.Any())
        {
            context.PhotoHistory.AddRange(
                new PhotoHistory { Id = 1, BoothId = 1, CustomerId = 2, LocationId = 1, Amount = 100.00m, SessionCode = "SESSION1", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                new PhotoHistory { Id = 2, BoothId = 2, CustomerId = 2, LocationId = 2, Amount = 150.00m, SessionCode = "SESSION2", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false }
            );
        }
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoHistory] OFF");

        // Seed data for PhotoStyle
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoStyle] ON");
        if (!context.PhotoStyle.Any())
        {
            context.PhotoStyle.AddRange(
                new PhotoStyle { Id = 1, Type = "Portrait", Description = "Portrait photography", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 },
                new PhotoStyle { Id = 2, Type = "Landscape", Description = "Landscape photography", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 }
            );
        }
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoStyle] OFF");

        // Seed data for Photo
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Photo] ON");
        if (!context.Photo.Any())
        {
            context.Photo.AddRange(
                new Photo { Id = 1, PhotoHistoryId = 1, Url = "https://example.com/photo1.jpg", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                new Photo { Id = 2, PhotoHistoryId = 2, Url = "https://example.com/photo2.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false }
            );
        }
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Photo] OFF");

        // Seed data for Order
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Order] ON");
        if (!context.Order.Any())
        {
            context.Order.AddRange(
                new Order { Id = 1, UserId = 2, PaymentId = 1, Amount = 100.00m, SessionId = "SESSION1", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 },
                new Order { Id = 2, UserId = 2, PaymentId = 2, Amount = 150.00m, SessionId = "SESSION2", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = 1, LastModifiedById = 1 }
            );
        }
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Order] OFF");

        transaction.Commit();
    }
}

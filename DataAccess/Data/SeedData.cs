using BussinessObject.Models;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static async Task Seed(AIPhotoboothDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
        if (!await roleManager.RoleExistsAsync("Customer"))
        {
            await roleManager.CreateAsync(new IdentityRole("Customer"));
        }
        if (!await roleManager.RoleExistsAsync("Staff"))
        {
            await roleManager.CreateAsync(new IdentityRole("Staff"));
        }
        if (!await roleManager.RoleExistsAsync("Manager"))
        {
            await roleManager.CreateAsync(new IdentityRole("Manager"));
        }

        using var transaction = await context.Database.BeginTransactionAsync(); // Bắt đầu giao dịch bất đồng bộ
        try
        {
            //Gen location
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Location] ON");
            if (!context.Location.Any())
            {
                context.Location.AddRange(
                    new Location { Id = 1, LocationName = "Main Office", Address = "123 Main St", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new Location {Id = 2, LocationName = "Branch Office", Address = "456 Side St", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false }
                );
            }
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Location] OFF");


            if (!context.Users.Any())
            {
                // Băm mật khẩu
                var passwordHasher = new PasswordHasher<User>();

                // Tạo người dùng admin
                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    PhoneNumber = "1234567890",
                    Gender = BussinessObject.Enums.UserGender.Male,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                };
                var resultAdmin = await userManager.CreateAsync(admin, "Admin123");
                if (resultAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                if (!resultAdmin.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {string.Join(", ", resultAdmin.Errors.Select(e => e.Description))}");
                }

                admin = new User
                {
                    UserName = "admin1",
                    Email = "hienphannguyenxuan@gmail.com",
                    PhoneNumber = "07774567890",
                    Gender = BussinessObject.Enums.UserGender.Male,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                };
                resultAdmin = await userManager.CreateAsync(admin, "Admin123");

                if (resultAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                if (!resultAdmin.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {string.Join(", ", resultAdmin.Errors.Select(e => e.Description))}");
                }

                var user1 = new User
                {
                    UserName = "user1",
                    Email = "user1@example.com",
                    PhoneNumber = "0987654321",
                    Gender = BussinessObject.Enums.UserGender.Female,
                    BirthDate = new DateTime(1990, 1, 1),
                    Balance = 100.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    LocationId = 1
                };

                var resultUser1 = await userManager.CreateAsync(user1, "Cus123");
                if (resultUser1.Succeeded)
                {
                    await userManager.AddToRoleAsync(user1, "Customer");
                }


                // Seed data for Payment
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PaymentMethod] ON");
                if (!context.PaymentMethod.Any())
                {
                    context.PaymentMethod.AddRange(
                        new PaymentMethod { Id = 1, MethodName = "Credit Card", Description = "Pay using credit card", IsActive = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id },
                        new PaymentMethod { Id = 2, MethodName = "PayPal", Description = "Pay using PayPal", IsActive = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id }
                    );
                }
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PaymentMethod] OFF");

               

                // Seed data for Booth
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Booth] ON");
                if (!context.Booth.Any())
                {
                    context.Booth.AddRange(
                        new Booth { Id = 1, LocationId = 1, BoothName = "Booth A", Description = "Main booth at Main Office", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id },
                        new Booth { Id = 2, LocationId = 2, BoothName = "Booth B", Description = "Branch booth", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id}
                    );
                }
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Booth] OFF");

                // Seed data for TypeSession
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [TypeSession] ON");
                if (!context.TypeSession.Any())
                {
                    context.TypeSession.AddRange(
                        new TypeSession { Id = 1, Name = "Photography", Description = "Photo session", Price = 100000, Duration = 60, AbleTakenNumber = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id, isPrinting = false },
                        new TypeSession { Id = 2, Name = "Videography", Description = "Video session", Price = 60000, Duration = 120, AbleTakenNumber = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id, isPrinting = true }
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
                        new PhotoHistory { Id = 1, BoothId = 1, CustomerId = user1.Id, LocationId = 1, Amount = 100.00m, SessionCode = "SESSION1", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                        new PhotoHistory { Id = 2, BoothId = 2, CustomerId = user1.Id, LocationId = 2, Amount = 150.00m, SessionCode = "SESSION2", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false }
                    );
                }
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoHistory] OFF");


                // Seed data for Photo
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Photo] ON");
                if (!context.Photo.Any())
                {
                    context.Photo.AddRange(
                        new Photo { Id = 1, PhotoHistoryId = 1, Url = "https://example.com/photo1.jpg", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false, Style = BussinessObject.Enums.Style.Abstract },
                        new Photo { Id = 2, PhotoHistoryId = 2, Url = "https://example.com/photo2.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false, Style = BussinessObject.Enums.Style.Portrait }
                    );
                }
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Photo] OFF");

                
                if (!context.Order.Any())
                {
                    context.Order.AddRange(
                        new Order { TypeSessionId = 1, UserId = user1.Id, PaymentMethodId = 1, Amount = 100000, Status = BussinessObject.Enums.OrderStatus.Pending, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id },
                        new Order { TypeSessionId = 2, PaymentMethodId = 2, Amount = 60000, Status = BussinessObject.Enums.OrderStatus.Pending, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id }
                    );
                }
                context.SaveChanges();

                // Commit transaction nếu không có lỗi
                await transaction.CommitAsync();
            }
        }
        catch (Exception ex)
        {
            // Rollback giao dịch nếu có lỗi
            await transaction.RollbackAsync();
            throw new Exception("Error seeding data: " + ex.Message);
        }
    }

}

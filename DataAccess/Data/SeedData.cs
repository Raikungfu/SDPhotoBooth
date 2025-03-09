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
                var password = "User123";

                var users = new List<User>
                {
                    new User { UserName = "admin1", Email = "hienphannguyenxuan@gmail.com", PhoneNumber = "07774567890", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "raikungfu", Email = "raikungfu@gmail.com", PhoneNumber = "07774567891", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "doannguyenhuyentrang", Email = "doannguyenhuyentrang9303@gmail.com", PhoneNumber = "07774567892", Gender = BussinessObject.Enums.UserGender.Female, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "raikungfu98", Email = "raikungfu98@gmail.com", PhoneNumber = "07774567891", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "sinhhoctebao0903", Email = "sinhhoctebao0903@gmail.com", PhoneNumber = "07774567892", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "vovannam641", Email = "vovannam641@gmail.com", PhoneNumber = "07774567893", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "phuongtbde160476", Email = "phuongtbde160476@fpt.edu.vn", PhoneNumber = "07774567894", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "trangdnhqe170154", Email = "trangdnhqe170154@fpt.edu.vn", PhoneNumber = "07774567895", Gender = BussinessObject.Enums.UserGender.Female, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "hienpnxqe170189", Email = "hienpnxqe170189@fpt.edu.vn", PhoneNumber = "07774567896", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "customer", Email = "customer@example.com", PhoneNumber = "07774567897", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsDeleted = false }
                };

                var roles = new Dictionary<string, string>
                {
                    { "admin1", "Admin" },
                    { "raikungfu", "Admin" },
                    { "doannguyenhuyentrang", "Admin" },
                    { "raikungfu98", "Manager" },
                    { "sinhhoctebao0903", "Manager" },
                    { "vovannam641", "Manager" },
                    { "phuongtbde160476", "Staff" },
                    { "trangdnhqe170154", "Staff" },
                    { "hienpnxqe170189", "Staff" },
                    { "customer", "Customer" }
                };

                foreach (var user in users)
                {
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roles[user.UserName]);
                    }
                    else
                    {
                        throw new Exception($"Failed to create user {user.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            var admin = await userManager.FindByNameAsync("admin1");
            var manager = await userManager.FindByNameAsync("raikungfu98");
            var staff = await userManager.FindByNameAsync("phuongtbde160476");
            var user1 = await userManager.FindByNameAsync("customer");
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

                // Seed data for Coupon
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Coupon] ON");
                if (!context.Coupon.Any())
                {
                    context.Coupon.AddRange(
                        new Coupon
                        {
                            Id = 1,
                            Name = "Summer Sale",
                            Description = "Get 20% off your next purchase!",
                            Code = "SUMMER20",
                            Discount = 10000,
                            StartDate = DateTime.UtcNow.AddDays(-10),
                            EndDate = DateTime.UtcNow.AddDays(30),
                            MaxUse = 100,
                            UsedAmount = 0,
                            MaxDiscount = 50.00m,
                            MinOrder = 10.00m,
                            IsActive = true
                        },
                        new Coupon
                        {
                            Id = 2,
                            Name = "Black Friday Deal",
                            Description = "Get 50% off on selected items!",
                            Code = "BLACKFRIDAY",
                            DiscountPercent = 0.50m,
                            StartDate = DateTime.UtcNow.AddDays(-5),
                            EndDate = DateTime.UtcNow.AddDays(25),
                            MaxUse = 200,
                            UsedAmount = 0,
                            MaxDiscount = 100.00m,
                            MinOrder = 50.00m,
                            IsActive = true
                        }
                    );
                }
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Coupon] OFF");


                // Seed data for TypeSession
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [TypeSession] ON");
                if (!context.TypeSession.Any())
                {
                    context.TypeSession.AddRange(
                        new TypeSession
                        {
                            Id = 1,
                            Name = "Standard Session",
                            Description = "A standard photo session.",
                            Duration = 60,
                            Price = 100.00m,
                            AbleTakenNumber = 10
                        },
                        new TypeSession
                        {
                            Id = 2,
                            Name = "VIP Session",
                            Description = "A premium photo session with extra perks.",
                            Duration = 90,
                            Price = 200.00m,
                            AbleTakenNumber = 5
                        }
                    );
                }
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [TypeSession] OFF");


                if (!context.Order.Any())
                {
                    context.Order.AddRange(
                        new Order { TypeSessionId = 1, UserId = user1.Id, PaymentMethodId = 1, Amount = 100000, Status = BussinessObject.Enums.OrderStatus.Pending, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id },
                        new Order { TypeSessionId = 2, PaymentMethodId = 2, Amount = 60000, Status = BussinessObject.Enums.OrderStatus.Pending, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id }
                    );
                }
                context.SaveChanges();

                if (!context.Session.Any())
                {
                    context.Session.AddRange(
                        new Session { OrderId = context.Order.FirstOrDefault(o => o.TypeSessionId == 1).Id, Code = "SESSION1", IsActive = true, TypeSessionId = 1, BoothId = 1, CreatedAt = DateTime.UtcNow },
                        new Session { OrderId = context.Order.FirstOrDefault(o => o.TypeSessionId == 2).Id, Code = "SESSION2", IsActive = true, TypeSessionId = 2, BoothId = 2, CreatedAt = DateTime.UtcNow }
                    );
                }
                context.SaveChanges();

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

            await transaction.CommitAsync();
            
        }
        catch (Exception ex)
        {
            // Rollback giao dịch nếu có lỗi
            await transaction.RollbackAsync();
            throw new Exception("Error seeding data: " + ex.Message);
        }
    }

}

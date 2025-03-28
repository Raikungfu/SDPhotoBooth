using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static async Task Seed(AIPhotoboothDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            #region Seed data for Role
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
            #endregion

            #region Seed data for User

            if (!context.Users.Any())
            {
                var password = "User123";

                var users = new List<User>
                {
                    new User { UserName = "admin1", Email = "hienphannguyenxuan@gmail.com", PhoneNumber = "07774567890", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "raikungfu", Email = "raikungfu@gmail.com", PhoneNumber = "07774567891", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "doannguyenhuyentrang", Email = "doannguyenhuyentrang9303@gmail.com", PhoneNumber = "07774567892", Gender = BussinessObject.Enums.UserGender.Female, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "raikungfu98", Email = "raikungfu98@gmail.com", PhoneNumber = "07774567891", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "sinhhoctebao0903", Email = "sinhhoctebao0903@gmail.com", PhoneNumber = "07774567892", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "vovannam641", Email = "vovannam641@gmail.com", PhoneNumber = "07774567893", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "phuongtbde160476", Email = "phuongtbde160476@fpt.edu.vn", PhoneNumber = "07774567894", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "trangdnhqe170154", Email = "trangdnhqe170154@fpt.edu.vn", PhoneNumber = "07774567895", Gender = BussinessObject.Enums.UserGender.Female, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "hienpnxqe170189", Email = "hienpnxqe170189@fpt.edu.vn", PhoneNumber = "07774567896", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "customer", Email = "customer@example.com", PhoneNumber = "07774567897", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false }
                };

                var roles = new Dictionary<string, string>
                {
                    { "admin1", "Admin" },
                    { "raikungfu", "Admin" },
                    { "doannguyenhuyentrang", "Admin" },
                    { "raikungfu98", "Manager" },
                    { "sinhhoctebao0903", "Manager" },
                    { "vovannam641", "Manager" },
                    { "phuongtbde160476", "Customer" },
                    { "trangdnhqe170154", "Staff" },
                    { "hienpnxqe170189", "Customer" },
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
            var staff = await userManager.FindByNameAsync("trangdnhqe170154");
            var user1 = await userManager.FindByNameAsync("phuongtbde160476");

            #endregion

            #region Seed data for Location
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Location] ON");

            if (!context.Location.Any())
            {
                context.Location.AddRange(
                    new Location { Id = 1, LocationName = "Main Office", Address = "123 Central Ave, City A", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Location { Id = 2, LocationName = "Branch Office 1", Address = "456 East Street, City B", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Location { Id = 3, LocationName = "Branch Office 2", Address = "789 West Blvd, City C", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Location { Id = 4, LocationName = "Remote Site 1", Address = "12 Mountain Road, City D", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Location { Id = 5, LocationName = "Remote Site 2", Address = "34 Lake View, City E", CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id }
                );

                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Location] OFF");
            #endregion

            #region Seed data for Booth
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Booth] ON");

            if (!context.Booth.Any())
            {
                context.Booth.AddRange(
                    new Booth { Id = 1, LocationId = 1, BoothName = "Booth A1", Description = "Booth 1 at Main Office", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 2, LocationId = 1, BoothName = "Booth A2", Description = "Booth 2 at Main Office", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 3, LocationId = 1, BoothName = "Booth A3", Description = "Booth 3 at Main Office", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 4, LocationId = 1, BoothName = "Booth A4", Description = "Booth 4 at Main Office", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },

                    new Booth { Id = 5, LocationId = 2, BoothName = "Booth B1", Description = "Booth 1 at Branch Office 1", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 6, LocationId = 2, BoothName = "Booth B2", Description = "Booth 2 at Branch Office 1", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 7, LocationId = 2, BoothName = "Booth B3", Description = "Booth 3 at Branch Office 1", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 8, LocationId = 2, BoothName = "Booth B4", Description = "Booth 4 at Branch Office 1", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },

                    new Booth { Id = 9, LocationId = 3, BoothName = "Booth C1", Description = "Booth 1 at Branch Office 2", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 10, LocationId = 3, BoothName = "Booth C2", Description = "Booth 2 at Branch Office 2", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 11, LocationId = 3, BoothName = "Booth C3", Description = "Booth 3 at Branch Office 2", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 12, LocationId = 3, BoothName = "Booth C4", Description = "Booth 4 at Branch Office 2", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },

                    new Booth { Id = 13, LocationId = 4, BoothName = "Booth D1", Description = "Booth 1 at Remote Site 1", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 14, LocationId = 4, BoothName = "Booth D2", Description = "Booth 2 at Remote Site 1", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 15, LocationId = 4, BoothName = "Booth D3", Description = "Booth 3 at Remote Site 1", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 16, LocationId = 4, BoothName = "Booth D4", Description = "Booth 4 at Remote Site 1", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },

                    new Booth { Id = 17, LocationId = 5, BoothName = "Booth E1", Description = "Booth 1 at Remote Site 2", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 18, LocationId = 5, BoothName = "Booth E2", Description = "Booth 2 at Remote Site 2", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 19, LocationId = 5, BoothName = "Booth E3", Description = "Booth 3 at Remote Site 2", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Booth { Id = 20, LocationId = 5, BoothName = "Booth E4", Description = "Booth 4 at Remote Site 2", Status = true, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = manager.Id, LastModifiedById = manager.Id }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Booth] OFF");

            #endregion

            #region Seed data for Coupon
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Coupon] ON");

            if (!context.Coupon.Any())
            {
                context.Coupon.AddRange(
                    new Coupon { Id = 1, Name = "New Year Deal", Code = "NY2025", Description = "New Year 2025 promotion", Discount = 50000, DiscountPercent = 0.10m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), MaxUse = 100, MaxDiscount = 100000, MinOrder = 200000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 2, Name = "Spring Sale", Code = "SPRING25", Description = "Spring discount", Discount = 30000, DiscountPercent = 0.05m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(2), MaxUse = 50, MaxDiscount = 50000, MinOrder = 150000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 3, Name = "Summer Heat", Code = "SUMMERHEAT", Description = "Hot summer deal", Discount = 40000, DiscountPercent = 0.07m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(2), MaxUse = 70, MaxDiscount = 60000, MinOrder = 180000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 4, Name = "Back to School", Code = "SCHOOL25", Description = "School season sale", Discount = 25000, DiscountPercent = 0.08m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), MaxUse = 30, MaxDiscount = 40000, MinOrder = 120000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 5, Name = "Autumn Bonus", Code = "AUTUMN22", Description = "Fall special offer", Discount = 22000, DiscountPercent = 0.06m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), MaxUse = 60, MaxDiscount = 45000, MinOrder = 130000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 6, Name = "Black Friday", Code = "BLKFRI50", Description = "Black Friday 50% OFF", Discount = 100000, DiscountPercent = 0.50m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(10), MaxUse = 500, MaxDiscount = 200000, MinOrder = 250000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 7, Name = "Cyber Monday", Code = "CYBER2025", Description = "Cyber Monday sale", Discount = 75000, DiscountPercent = 0.35m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(7), MaxUse = 300, MaxDiscount = 150000, MinOrder = 220000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 8, Name = "Xmas Cheer", Code = "XMAS2025", Description = "Christmas discount", Discount = 90000, DiscountPercent = 0.40m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), MaxUse = 250, MaxDiscount = 180000, MinOrder = 200000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 9, Name = "Birthday Gift", Code = "BIRTHDAY", Description = "Special birthday offer", Discount = 60000, DiscountPercent = 0.20m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(5), MaxUse = 100, MaxDiscount = 80000, MinOrder = 150000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 10, Name = "Flash Sale", Code = "FLASH2025", Description = "Limited time discount", Discount = 35000, DiscountPercent = 0.15m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(2), MaxUse = 80, MaxDiscount = 60000, MinOrder = 140000, CreatedById = admin.Id, LastModifiedById = admin.Id },

                    new Coupon { Id = 11, Name = "Weekend Deal", Code = "WEEKEND", Description = "Weekend only", Discount = 25000, DiscountPercent = 0.12m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), MaxUse = 50, MaxDiscount = 40000, MinOrder = 100000, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Coupon { Id = 12, Name = "Midweek Saver", Code = "MIDWEEK", Description = "Midweek coupon", Discount = 20000, DiscountPercent = 0.10m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(4), MaxUse = 40, MaxDiscount = 35000, MinOrder = 90000, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Coupon { Id = 13, Name = "Lucky Code", Code = "LUCKY7", Description = "Use your luck", Discount = 77777, DiscountPercent = 0.07m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), MaxUse = 77, MaxDiscount = 77777, MinOrder = 177777, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Coupon { Id = 14, Name = "Mega Drop", Code = "MEGA2025", Description = "Massive discount", Discount = 120000, DiscountPercent = 0.60m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(5), MaxUse = 100, MaxDiscount = 220000, MinOrder = 300000, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Coupon { Id = 15, Name = "Last Minute", Code = "LASTCALL", Description = "Last chance discount", Discount = 45000, DiscountPercent = 0.18m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1), MaxUse = 20, MaxDiscount = 50000, MinOrder = 110000, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Coupon { Id = 16, Name = "Daily Deal", Code = "DAILY01", Description = "Today only!", Discount = 15000, DiscountPercent = 0.05m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1), MaxUse = 10, MaxDiscount = 20000, MinOrder = 70000, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Coupon { Id = 17, Name = "First Order", Code = "WELCOME", Description = "For first-time users", Discount = 30000, DiscountPercent = 0.10m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(3), MaxUse = 500, MaxDiscount = 70000, MinOrder = 150000, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Coupon { Id = 18, Name = "Referral", Code = "REFERME", Description = "Refer & save", Discount = 40000, DiscountPercent = 0.25m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(2), MaxUse = 200, MaxDiscount = 90000, MinOrder = 160000, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Coupon { Id = 19, Name = "VIP Only", Code = "VIP2025", Description = "Exclusive for VIP", Discount = 100000, DiscountPercent = 0.45m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), MaxUse = 100, MaxDiscount = 200000, MinOrder = 300000, CreatedById = manager.Id, LastModifiedById = manager.Id },
                    new Coupon { Id = 20, Name = "Test Coupon", Code = "TEST20", Description = "Sample coupon for testing", Discount = 5000, DiscountPercent = 0.01m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(10), MaxUse = 5, MaxDiscount = 10000, MinOrder = 30000, CreatedById = manager.Id, LastModifiedById = manager.Id }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Coupon] OFF");

            #endregion

            #region Seed data for LevelMembership
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [LevelMembership] ON");

            if (!context.LevelMembership.Any())
            {
                context.LevelMembership.AddRange(
                    new LevelMembership { Id = 1, Name = Level.Basic, Description = "Basic membership level", Price = 0, Duration = 0, Point = 0, DiscountPercent = 0, MaxDiscount = 0, MinOrder = 0, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new LevelMembership { Id = 2, Name = Level.Bronze, Description = "Bronze membership level", Price = 50000, Duration = 30, Point = 100, DiscountPercent = 0.05m, MaxDiscount = 50000, MinOrder = 100000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new LevelMembership { Id = 3, Name = Level.Silver, Description = "Silver membership level", Price = 100000, Duration = 60, Point = 200, DiscountPercent = 0.10m, MaxDiscount = 100000, MinOrder = 150000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new LevelMembership { Id = 4, Name = Level.Gold, Description = "Gold membership level", Price = 200000, Duration = 90, Point = 400, DiscountPercent = 0.15m, MaxDiscount = 150000, MinOrder = 200000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new LevelMembership { Id = 5, Name = Level.Platinum, Description = "Platinum membership level", Price = 300000, Duration = 120, Point = 600, DiscountPercent = 0.20m, MaxDiscount = 200000, MinOrder = 300000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [LevelMembership] OFF");

            #endregion

            #region Seed data for PaymentMethod
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PaymentMethod] ON");

            if (!context.PaymentMethod.Any())
            {
                context.PaymentMethod.AddRange(
                    new PaymentMethod { Id = 1, MethodName = "Wallet", Description = "Internal wallet balance", IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PaymentMethod { Id = 2, MethodName = "Cash", Description = "Pay with cash upon delivery", IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PaymentMethod { Id = 3, MethodName = "Banking", Description = "Bank transfer via online banking", IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PaymentMethod { Id = 4, MethodName = "ZaloPay", Description = "ZaloPay mobile payment", IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PaymentMethod { Id = 5, MethodName = "Momo", Description = "Momo e-wallet", IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PaymentMethod] OFF");

            #endregion

            #region Seed data for PhotoStyle
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoStyle] ON");

            if (!context.PhotoStyle.Any())
            {
                context.PhotoStyle.AddRange(
                    new PhotoStyle { Id = 1, Name = "anime", Description = "Anime-style photography", ImageUrl = "https://static.vecteezy.com/system/resources/thumbnails/033/662/051/small/cartoon-lofi-young-manga-style-girl-while-listening-to-music-in-the-rain-ai-generative-photo.jpg", Prompt = "", NegativePrompt = "", CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"), LastModified = DateTime.Parse("2025-03-27T08:04:20.5382419"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PhotoStyle { Id = 2, Name = "artistic_portrait", Description = "High artistic portraits", ImageUrl = "https://images5.alphacoders.com/135/thumb-1920-1352020.png", Prompt = "", NegativePrompt = "", CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"), LastModified = DateTime.Parse("2025-03-27T08:06:34.262168"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PhotoStyle { Id = 3, Name = "cartoon", Description = "Cartoon-themed photography", ImageUrl = "https://static.vecteezy.com/system/resources/thumbnails/046/238/401/small/a-3d-character-illustration-for-a-childrens-book-photo.jpg", Prompt = "", NegativePrompt = "", CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"), LastModified = DateTime.Parse("2025-03-27T08:10:24.5847326"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PhotoStyle { Id = 4, Name = "fantasy", Description = "Fantasy-themed photography", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSynwqUs6fsgDDN5tl8UG5NhyLK9nQcC7WCYg&s", Prompt = "", NegativePrompt = "", CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"), LastModified = DateTime.Parse("2025-03-14T05:45:36.871683"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PhotoStyle { Id = 5, Name = "fashion_style", Description = "Fashion and stylish photos", ImageUrl = "https://www.pixelstalk.net/wp-content/uploads/2016/06/Nature-girl-winter-fashion-wallpaper-1920x1080.jpg", Prompt = "", NegativePrompt = "", CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"), LastModified = DateTime.Parse("2025-03-27T08:16:22.4802536"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PhotoStyle { Id = 6, Name = "id_photo", Description = "Professional ID photos", ImageUrl = "https://cdn.tgdd.vn/Files/2023/07/10/1537629/cachkhacphucloitaianh4x6lenhochieuonline-130723-145414-800-resize.jpg", Prompt = "", NegativePrompt = "", CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"), LastModified = DateTime.Parse("2025-03-27T08:18:44.2862617"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PhotoStyle { Id = 7, Name = "modern", Description = "Modern photography styles", ImageUrl = "https://img.freepik.com/premium-photo/modern-woman-portrait-hd-8k-wallpaper-stock-photographic-image_890746-53098.jpg", Prompt = "", NegativePrompt = "", CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"), LastModified = DateTime.Parse("2025-03-27T08:27:24.4834447"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PhotoStyle { Id = 8, Name = "nature", Description = "Nature-inspired photography", ImageUrl = "https://wallpapercave.com/wp/wp7118266.jpg", Prompt = "", NegativePrompt = "", CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"), LastModified = DateTime.Parse("2025-03-27T08:29:05.8312544"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PhotoStyle { Id = 9, Name = "vintage", Description = "Vintage and classic photography", ImageUrl = "https://images7.alphacoders.com/573/573462.jpg", Prompt = "", NegativePrompt = "", CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"), LastModified = DateTime.Parse("2025-03-27T08:34:08.9543789"), CreatedById = admin.Id, LastModifiedById = admin.Id }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoStyle] OFF");

            #endregion

            #region Seed data for StickerStyle
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [StickerStyle] ON");

            if (!context.StickerStyle.Any())
            {
                context.StickerStyle.AddRange(
                    new StickerStyle { Id = 1, StickerStyleName = "Cute", Description = "Adorable and playful stickers", CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new StickerStyle { Id = 2, StickerStyleName = "Minimal", Description = "Simple and clean sticker designs", CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new StickerStyle { Id = 3, StickerStyleName = "Retro", Description = "Vintage and classic themed stickers", CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new StickerStyle { Id = 4, StickerStyleName = "Nature", Description = "Leaves, trees, and natural elements", CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new StickerStyle { Id = 5, StickerStyleName = "Emoji", Description = "Expressive emoji-style stickers", CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new StickerStyle { Id = 6, StickerStyleName = "Cartoon", Description = "Fun and colorful cartoon stickers", CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new StickerStyle { Id = 7, StickerStyleName = "Food", Description = "Food and drink-themed stickers", CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new StickerStyle { Id = 8, StickerStyleName = "Animal", Description = "Cute animal illustrations", CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new StickerStyle { Id = 9, StickerStyleName = "Holiday", Description = "Christmas, Halloween, and festive themes", CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new StickerStyle { Id = 10, StickerStyleName = "Love", Description = "Hearts, kisses, and romantic designs", CreatedById = admin.Id, LastModifiedById = admin.Id }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [StickerStyle] OFF");

            #endregion

            #region Seed data for TypeSession
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [TypeSession] ON");

            if (!context.TypeSession.Any())
            {
                context.TypeSession.AddRange(
                    new TypeSession { Id = 1, Name = "Photography", Description = "Photo session", Duration = 60, Price = 100000, isPrinting = false, AbleTakenNumber = 5, CreatedAt = DateTime.Parse("2025-03-14T05:45:36.4862448"), LastModified = DateTime.Parse("2025-03-14T05:45:36.4862448"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 2, Name = "Videography", Description = "Video session", Duration = 120, Price = 60000, isPrinting = true, AbleTakenNumber = 3, CreatedAt = DateTime.Parse("2025-03-14T05:45:36.4862448"), LastModified = DateTime.Parse("2025-03-14T05:45:36.4862448"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 5, Name = "Gói Cơ Bản", Description = "Gói dịch vụ in cơ bản", Duration = 30, Price = 50000, isPrinting = true, AbleTakenNumber = 10, CreatedAt = DateTime.Parse("2025-03-28T04:24:45.6577221"), LastModified = DateTime.Parse("2025-03-28T04:24:45.6577221"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 6, Name = "Gói Nâng Cao", Description = "Dịch vụ in chất lượng cao", Duration = 60, Price = 120000, isPrinting = true, AbleTakenNumber = 5, CreatedAt = DateTime.Parse("2025-03-28T04:24:57.935814"), LastModified = DateTime.Parse("2025-03-28T04:24:57.935814"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 7, Name = "Tư Vấn Thiết Kế", Description = "Hỗ trợ tư vấn và chỉnh sửa thiết kế", Duration = 45, Price = 80000, isPrinting = false, AbleTakenNumber = 3, CreatedAt = DateTime.Parse("2025-03-28T04:25:06.3817052"), LastModified = DateTime.Parse("2025-03-28T04:25:06.3817052"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 8, Name = "In Nhanh", Description = "Dịch vụ in nhanh lấy liền", Duration = 20, Price = 70000, isPrinting = true, AbleTakenNumber = 8, CreatedAt = DateTime.Parse("2025-03-28T04:25:15.7805502"), LastModified = DateTime.Parse("2025-03-28T04:25:15.7805502"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 9, Name = "In Màu", Description = "In màu chất lượng cao", Duration = 40, Price = 90000, isPrinting = true, AbleTakenNumber = 6, CreatedAt = DateTime.Parse("2025-03-28T04:25:23.9206205"), LastModified = DateTime.Parse("2025-03-28T04:25:23.9206205"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 10, Name = "Scan Tài Liệu", Description = "Scan tài liệu lưu trữ dạng số", Duration = 25, Price = 30000, isPrinting = false, AbleTakenNumber = 15, CreatedAt = DateTime.Parse("2025-03-28T04:25:29.5577387"), LastModified = DateTime.Parse("2025-03-28T04:25:29.5577387"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 11, Name = "Scan Hồ Sơ", Description = "Chuyển đổi hồ sơ giấy sang bản số", Duration = 30, Price = 35000, isPrinting = false, AbleTakenNumber = 10, CreatedAt = DateTime.Parse("2025-03-28T05:11:40.457483"), LastModified = DateTime.Parse("2025-03-28T05:11:40.457483"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 12, Name = "Scan Biểu Mẫu", Description = "Lưu trữ biểu mẫu hành chính", Duration = 20, Price = 25000, isPrinting = false, AbleTakenNumber = 12, CreatedAt = DateTime.Parse("2025-03-28T05:11:47.0305943"), LastModified = DateTime.Parse("2025-03-28T05:11:47.0305943"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 13, Name = "Scan Bài Giảng", Description = "Số hóa bài giảng dạng PDF", Duration = 40, Price = 40000, isPrinting = false, AbleTakenNumber = 8, CreatedAt = DateTime.Parse("2025-03-28T05:11:54.5815379"), LastModified = DateTime.Parse("2025-03-28T05:11:54.5815379"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 14, Name = "Scan Tạp Chí", Description = "Lưu trữ nội dung tạp chí", Duration = 35, Price = 38000, isPrinting = false, AbleTakenNumber = 6, CreatedAt = DateTime.Parse("2025-03-28T05:12:01.0023415"), LastModified = DateTime.Parse("2025-03-28T05:12:01.0023415"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 15, Name = "Scan Luận Văn", Description = "Chuyển luận văn thành bản mềm", Duration = 50, Price = 50000, isPrinting = false, AbleTakenNumber = 5, CreatedAt = DateTime.Parse("2025-03-28T05:12:07.5845421"), LastModified = DateTime.Parse("2025-03-28T05:12:07.5845421"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 16, Name = "Scan Tài Liệu Cũ", Description = "Số hóa tài liệu giấy cũ", Duration = 45, Price = 45000, isPrinting = false, AbleTakenNumber = 7, CreatedAt = DateTime.Parse("2025-03-28T05:12:13.2268526"), LastModified = DateTime.Parse("2025-03-28T05:12:13.2268526"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 17, Name = "Scan Sách", Description = "Scan toàn bộ nội dung sách", Duration = 60, Price = 60000, isPrinting = false, AbleTakenNumber = 4, CreatedAt = DateTime.Parse("2025-03-28T05:12:19.7889762"), LastModified = DateTime.Parse("2025-03-28T05:12:19.7889762"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 18, Name = "Scan Hợp Đồng", Description = "Chuyển hợp đồng sang file PDF", Duration = 30, Price = 32000, isPrinting = false, AbleTakenNumber = 9, CreatedAt = DateTime.Parse("2025-03-28T05:12:26.1047679"), LastModified = DateTime.Parse("2025-03-28T05:12:26.1047679"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 19, Name = "Scan Giấy Tờ Cá Nhân", Description = "Lưu trữ giấy tờ cá nhân dạng số", Duration = 15, Price = 20000, isPrinting = false, AbleTakenNumber = 20, CreatedAt = DateTime.Parse("2025-03-28T05:12:31.7729663"), LastModified = DateTime.Parse("2025-03-28T05:12:31.7729663"), CreatedById = admin.Id, LastModifiedById = admin.Id }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [TypeSession] OFF");

            #endregion

            #region Seed data for Wallet
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Wallet] ON");
            if (!context.Wallet.Any())
            {
                context.Wallet.AddRange(
                    new Wallet { Id = 1, CustomerId = admin.Id, Balance = 500000, IsLocked = false },
                    new Wallet { Id = 2, CustomerId = manager.Id, Balance = 350000, IsLocked = false },
                    new Wallet { Id = 3, CustomerId = staff.Id, Balance = 200000, IsLocked = false },
                    new Wallet { Id = 4, CustomerId = user1.Id, Balance = 100000, IsLocked = false }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Wallet] OFF");

            #endregion

            #region Seed data for MembershipCard
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [MembershipCard] ON");
            if (!context.MembershipCard.Any())
            {
                context.MembershipCard.AddRange(
                    new MembershipCard
                    {
                        Id = 1,
                        CustomerId = admin.Id,
                        LevelMemberShipId = 5, // Platinum
                        Points = 1000,
                        Expired = DateTime.UtcNow.AddYears(1),
                        Description = "Admin's Platinum membership",
                        IsActive = true,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new MembershipCard
                    {
                        Id = 2,
                        CustomerId = manager.Id,
                        LevelMemberShipId = 4, // Gold
                        Points = 600,
                        Expired = DateTime.UtcNow.AddYears(1),
                        Description = "Manager's Gold membership",
                        IsActive = true,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new MembershipCard
                    {
                        Id = 3,
                        CustomerId = staff.Id,
                        LevelMemberShipId = 3, // Silver
                        Points = 300,
                        Expired = DateTime.UtcNow.AddYears(1),
                        Description = "Staff's Silver membership",
                        IsActive = true,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new MembershipCard
                    {
                        Id = 4,
                        CustomerId = user1.Id,
                        LevelMemberShipId = 2, // Bronze
                        Points = 100,
                        Expired = DateTime.UtcNow.AddYears(1),
                        Description = "User's Bronze membership",
                        IsActive = true,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [MembershipCard] OFF");

            #endregion

            #region Seed data for Order
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Order] ON");

            if (!context.Order.Any())
            {
                context.Order.AddRange(
                    new Order
                    {
                        Id = 1,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        UserId = admin.Id,
                        Status = OrderStatus.Pending,
                        Amount = 100000,
                        Email = "admin@example.com",
                        Phone = "123456789",
                        CouponId = 1,
                        TypeSessionId = 1,
                        TransactionId = 1,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 2,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        UserId = manager.Id,
                        Status = OrderStatus.Completed,
                        Amount = 500000,
                        Email = "manager@example.com",
                        Phone = "987654321",
                        CouponId = 2,
                        TypeSessionId = 2,
                        TransactionId = 2,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 3,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        UserId = staff.Id,
                        Status = OrderStatus.Pending,
                        Amount = 200000,
                        Email = "staff@example.com",
                        Phone = "555123456",
                        CouponId = 3,
                        TransactionId = 3,
                        TypeSessionId = 5,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 4,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        UserId = user1.Id,
                        Status = OrderStatus.Cancelled,
                        Amount = 150000,
                        Email = "user1@example.com",
                        Phone = "555654321",
                        CouponId = 4,
                        TransactionId = 4,
                        TypeSessionId = 6,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 5,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        UserId = admin.Id,
                        Status = OrderStatus.Pending,
                        Amount = 250000,
                        Email = "admin@example.com",
                        Phone = "123456789",
                        CouponId = 5,
                        TypeSessionId = 7,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 6,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        UserId = manager.Id,
                        Status = OrderStatus.Completed,
                        Amount = 450000,
                        Email = "manager@example.com",
                        Phone = "987654321",
                        CouponId = 1,
                        TypeSessionId = 8,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 7,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        UserId = staff.Id,
                        Status = OrderStatus.Pending,
                        Amount = 80000,
                        Email = "staff@example.com",
                        Phone = "555123456",
                        CouponId = 2,
                        TypeSessionId = 9,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 8,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        UserId = user1.Id,
                        Status = OrderStatus.Completed,
                        Amount = 350000,
                        Email = "user1@example.com",
                        Phone = "555654321",
                        CouponId = 3,
                        TypeSessionId = 10,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 9,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        UserId = admin.Id,
                        Status = OrderStatus.Pending,
                        Amount = 300000,
                        Email = "admin@example.com",
                        Phone = "123456789",
                        CouponId = 4,
                        TypeSessionId = 11,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 10,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        UserId = manager.Id,
                        Status = OrderStatus.Completed,
                        Amount = 550000,
                        Email = "manager@example.com",
                        Phone = "987654321",
                        CouponId = 5,
                        TypeSessionId = 12,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Order] OFF");

            #endregion

            #region Seed data for SessionCode
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Session] ON");
            if (!context.Session.Any())
            {
                context.Session.AddRange(
                    new Session { Id = 1, IsActive = true, OrderId = 1 },
                    new Session { Id = 2, IsActive = true, OrderId = 2 },
                    new Session { Id = 3, IsActive = true, OrderId = 3 },
                    new Session { Id = 4, IsActive = true, OrderId = 4 },
                    new Session { Id = 5, IsActive = true, OrderId = 5 },
                    new Session { Id = 6, IsActive = true, OrderId = 6 },
                    new Session { Id = 7, IsActive = true, OrderId = 7 },
                    new Session { Id = 8, IsActive = true, OrderId = 8 },
                    new Session { Id = 9, IsActive = true, OrderId = 9 },
                    new Session { Id = 10, IsActive = true, OrderId = 10 }
                    
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Session] OFF");

            #endregion

            #region Seed data for PhotoHistory
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoHistory] ON");

            if (!context.PhotoHistory.Any())
            {
                context.PhotoHistory.AddRange(
                    new PhotoHistory { Id = 1, CustomerId = admin.Id, LocationId = 1, BoothId = 1, Amount = 150000, SessionId = 1},
                    new PhotoHistory {Id = 2, CustomerId = manager.Id, LocationId = 2, BoothId = 2, Amount = 200000, SessionId = 2 },
                    new PhotoHistory {Id = 3, CustomerId = staff.Id, LocationId = 3, BoothId = 3, Amount = 120000, SessionId = 3 },
                    new PhotoHistory {Id = 4, CustomerId = user1.Id, LocationId = 4, BoothId = 4, Amount = 180000, SessionId = 4 },
                    new PhotoHistory {Id = 5, CustomerId = admin.Id, LocationId = 5, BoothId = 5, Amount = 220000, SessionId = 5 },
                    new PhotoHistory {Id = 6, CustomerId = manager.Id, LocationId = 1, BoothId = 6, Amount = 250000, SessionId = 6 },
                    new PhotoHistory {Id = 7, CustomerId = staff.Id, LocationId = 2, BoothId = 7, Amount = 300000, SessionId = 7 },
                    new PhotoHistory {Id = 8, CustomerId = user1.Id, LocationId = 3, BoothId = 8, Amount = 140000, SessionId = 8 },
                    new PhotoHistory {Id = 9, CustomerId = admin.Id, LocationId = 4, BoothId = 9, Amount = 160000, SessionId = 9 },
                    new PhotoHistory {Id = 10, CustomerId = manager.Id, LocationId = 5, BoothId = 10, Amount = 200000, SessionId = 10 }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoHistory] OFF");

            #endregion

            #region Seed data for Photo
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Photo] ON");
            if (!context.Photo.Any())
            {
                context.Photo.AddRange(
        // Anime (PhotoStyleId = 1)
        new Photo { Id = 1, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 2, PhotoHistoryId = 2, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 3, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQVYaWlae56yEO5Ry7LWETaKyXxwy8CyyDi2Q&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },

        // Artistic Portrait (PhotoStyleId = 2)
        new Photo { Id = 4, PhotoHistoryId = 1, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 5, PhotoHistoryId = 2, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTwLld0c-HQJt-3qazUl19H9KnsO8xexKEKoQ&s", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 6, PhotoHistoryId = 1, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },

        // Cartoon (PhotoStyleId = 3)
        new Photo { Id = 7, PhotoHistoryId = 1, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 8, PhotoHistoryId = 2, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 9, PhotoHistoryId = 1, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },

        // Fantasy (PhotoStyleId = 4)
        new Photo { Id = 10, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ-yOzUd64E_PQSxF3n-oOptUxI2oc739s84A&s", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 11, PhotoHistoryId = 2, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 12, PhotoHistoryId = 1, Url = "https://i.pinimg.com/474x/8b/87/01/8b870164990240413ad38e084efbedb4.jpg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },

        // Fashion Style (PhotoStyleId = 5)
        new Photo { Id = 13, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSemyOiNH9PMQsYvOjUuiPq0OQO-yX_vX5Y7A&s", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 14, PhotoHistoryId = 2, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQN15MC5myA5r1ZrqXwQax3b9iGgKCsP2om_w&s", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 15, PhotoHistoryId = 1, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },

        // ID Photo (PhotoStyleId = 6)
        new Photo { Id = 16, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTt2NyaC5JGEuI4nQFhO-zI9hzVFHin5REBEA&s", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 17, PhotoHistoryId = 2, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0JzbMJLCg8rfL_QYtbMT23p2Ak_T7OXOYlA&s", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 18, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSOvHez3KJAh5PajcCWrGS0qKWxRM47HjzNEw&s", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },

        // Modern (PhotoStyleId = 7)
        new Photo { Id = 19, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSczBLVKt2WrRPilS2a5ZgIncqKkp8hMRBPng&s", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 20, PhotoHistoryId = 2, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQyV-6Bfs5XWni5MUXFfP9zxz6zko9FGfXrMg&s", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 21, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },

        // Nature (PhotoStyleId = 8)
        new Photo { Id = 22, PhotoHistoryId = 1, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 23, PhotoHistoryId = 2, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmZhb4EZu_m3WvtZkDumqH4tpWBHkvqT5t2A&s", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 24, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQznYZPScxnJpX5PTl4vHU78vUkGA_tH7LXxQ&s", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },

        // Vintage (PhotoStyleId = 9)
        new Photo { Id = 25, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRvY8PLcsBAqdsI6ptQiysf2fshxWrToyPpog&s", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 26, PhotoHistoryId = 2, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
        new Photo { Id = 27, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false }
                );
            }
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Photo] OFF");

            #endregion

            #region Seed data for Transaction
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Transaction] ON");

            if (!context.Transaction.Any())
            {
                context.Transaction.AddRange(
                    new Transaction { Id = 1, IsSuccess = true, Amount = 50000, WalletId = 1 },
                    new Transaction { Id = 2, IsSuccess = false, Amount = 100000, WalletId = 2 },
                    new Transaction { Id = 3, IsSuccess = true, Amount = 200000, WalletId = 3 },
                    new Transaction { Id = 4, IsSuccess = true, Amount = 150000, WalletId = 4 }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Transaction] OFF");

            #endregion

            #region Seed data for Payment
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Payment] ON");

            if (!context.Payment.Any())
            {
                context.Payment.AddRange(
                    new Payment { Id = 1, OrderId = 1, TransactionId = 1, PaymentMethodId = 1, IsSuccess = true, Amount = 150000 },
                    new Payment { Id = 2, OrderId = 2, TransactionId = 2, PaymentMethodId = 2, IsSuccess = false, Amount = 200000 },
                    new Payment { Id = 3, OrderId = 3, TransactionId = 3, PaymentMethodId = 3, IsSuccess = true, Amount = 100000 },
                    new Payment { Id = 4, OrderId = 4, TransactionId = 4, PaymentMethodId = 4, IsSuccess = true, Amount = 250000 }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Payment] OFF");

            #endregion

            #region Seed data for Sticker
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Sticker] ON");

            if (!context.Sticker.Any())
            {
                context.Sticker.AddRange(
                    new Sticker { Id = 1, Name = "Cute Cat", StickerUrl = "https://example.com/sticker1.jpg", StickerStyleId = 1 },
                    new Sticker { Id = 2, Name = "Happy Dog", StickerUrl = "https://example.com/sticker2.jpg", StickerStyleId = 2 },
                    new Sticker { Id = 3, Name = "Cool Panda", StickerUrl = "https://example.com/sticker3.jpg", StickerStyleId = 3 },
                    new Sticker { Id = 4, Name = "Funny Rabbit", StickerUrl = "https://example.com/sticker4.jpg", StickerStyleId = 4 },
                    new Sticker { Id = 5, Name = "Funny Frog", StickerUrl = "https://example.com/sticker5.jpg", StickerStyleId = 5 }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Sticker] OFF");

            #endregion

            #region Seed data for FrameStyle
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [FrameStyle] ON");

            if (!context.FrameStyle.Any())
            {
                context.FrameStyle.AddRange(
                    new FrameStyle { Id = 1, Name = "Classic", Description = "Traditional wooden frame", ImageUrl = "https://example.com/classic-frame.jpg" },
                    new FrameStyle { Id = 2, Name = "Modern", Description = "Sleek, minimalistic design", ImageUrl = "https://example.com/modern-frame.jpg" },
                    new FrameStyle { Id = 3, Name = "Rustic", Description = "Distressed wood, vintage feel", ImageUrl = "https://example.com/rustic-frame.jpg" },
                    new FrameStyle { Id = 4, Name = "Art Deco", Description = "Geometric and bold", ImageUrl = "https://example.com/art-deco-frame.jpg" },
                    new FrameStyle { Id = 5, Name = "Luxury", Description = "Premium materials and elegant design", ImageUrl = "https://example.com/luxury-frame.jpg" }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [FrameStyle] OFF");


            #endregion

            #region Seed data for Frame
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Frame] ON");

            if (!context.Frame.Any())
            {
                context.Frame.AddRange(
                    new Frame { Id = 1, Name = "Classic Frame", FrameUrl = "https://example.com/classic-frame.jpg", FrameStyleId = 1, SlotCount = 4 },
                    new Frame { Id = 2, Name = "Modern Frame", FrameUrl = "https://example.com/modern-frame.jpg", FrameStyleId = 2, SlotCount = 6 },
                    new Frame { Id = 3, Name = "Rustic Frame", FrameUrl = "https://example.com/rustic-frame.jpg", FrameStyleId = 3, SlotCount = 5 },
                    new Frame { Id = 4, Name = "Art Deco Frame", FrameUrl = "https://example.com/art-deco-frame.jpg", FrameStyleId = 4, SlotCount = 3 },
                    new Frame { Id = 5, Name = "Luxury Frame", FrameUrl = "https://example.com/luxury-frame.jpg", FrameStyleId = 5, SlotCount = 7 }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Frame] OFF");

            #endregion

            #region Seed data for Coordinate
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Coordinate] ON");

            if (!context.Coordinate.Any())
            {
                context.Coordinate.AddRange(
                    new Coordinate { Id = 1, FrameId = 1, X = 10, Y = 20, Width = 100, Height = 150 },
                    new Coordinate { Id = 2, FrameId = 2, X = 30, Y = 40, Width = 120, Height = 160 },
                    new Coordinate { Id = 3, FrameId = 3, X = 50, Y = 60, Width = 130, Height = 170 },
                    new Coordinate { Id = 4, FrameId = 4, X = 70, Y = 80, Width = 140, Height = 180 },
                    new Coordinate { Id = 5, FrameId = 5, X = 90, Y = 100, Width = 150, Height = 200 },
                    new Coordinate { Id = 6, FrameId = 1, X = 110, Y = 120, Width = 110, Height = 130 },
                    new Coordinate { Id = 7, FrameId = 2, X = 130, Y = 140, Width = 115, Height = 135 },
                    new Coordinate { Id = 8, FrameId = 3, X = 150, Y = 160, Width = 125, Height = 145 },
                    new Coordinate { Id = 9, FrameId = 4, X = 170, Y = 180, Width = 135, Height = 155 },
                    new Coordinate { Id = 10, FrameId = 5, X = 190, Y = 200, Width = 145, Height = 165 }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Coordinate] OFF");

            #endregion


            await transaction.CommitAsync();
            
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Error seeding data: " + ex.Message);
        }
    }

}

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
                    new LevelMembership { Id = 1, Name = "Basic", Description = "Basic membership level", Price = 0, Duration = 0, Point = 0, DiscountPercent = 0, MaxDiscount = 0, MinOrder = 0, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new LevelMembership { Id = 2, Name = "Bronze", Description = "Bronze membership level", Price = 50000, Duration = 30, Point = 100, DiscountPercent = 0.05m, MaxDiscount = 50000, MinOrder = 100000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new LevelMembership { Id = 3, Name = "Silver", Description = "Silver membership level", Price = 100000, Duration = 60, Point = 200, DiscountPercent = 0.10m, MaxDiscount = 100000, MinOrder = 150000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new LevelMembership { Id = 4, Name = "Gold", Description = "Gold membership level", Price = 200000, Duration = 90, Point = 400, DiscountPercent = 0.15m, MaxDiscount = 150000, MinOrder = 200000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new LevelMembership { Id = 5, Name = "Platinum", Description = "Platinum membership level", Price = 300000, Duration = 120, Point = 600, DiscountPercent = 0.20m, MaxDiscount = 200000, MinOrder = 300000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id }
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
                    new PaymentMethod { Id = 1, MethodName = "Wallet", ForMobile = true, IsOnline = true, Description = "Internal wallet balance", IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PaymentMethod { Id = 2, MethodName = "Cash", Description = "Pay with cash upon delivery", IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PaymentMethod { Id = 3, MethodName = "Banking", IsOnline = true, Description = "Bank transfer via online banking", IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new PaymentMethod { Id = 4, MethodName = "Google Billing", ForMobile = true, IsOnline = true, Description = "Momo e-wallet", IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id }
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
                    new PhotoStyle
                    {
                        Id = 1,
                        Name = "ID Photo",
                        Description = "Professional ID photo with studio lighting and sharp focus.",
                        ImageUrl = "https://cdn.tgdd.vn/Files/2023/07/10/1537629/cachkhacphucloitaianh4x6lenhochieuonline-130723-145414-800-resize.jpg",
                        Prompt = "Super Portrait, professional ID photo, studio lighting, sharp focus, neutral background, ultra-high resolution, natural skin texture, neutral expression.",
                        NegativePrompt = "blur, distortion, extra hair, heavy makeup, artificial skin smoothing, exaggerated features, harsh shadows, unrealistic retouching.",
                        Controlnets = null,
                        Mode = InpaintMode.KeepFace,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 20,
                        GuidanceScale = 4.0,
                        Strength = 0.3,
                        FaceImage = true,
                        BackgroundRemover = true,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:04:20.5382419"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 2,
                        Name = "ID Photo (ControlNet)",
                        Description = "Professional ID photo with enhanced details using ControlNet.",
                        ImageUrl = "https://cdn.tgdd.vn/Files/2023/07/10/1537629/cachkhacphucloitaianh4x6lenhochieuonline-130723-145414-800-resize.jpg",
                        Prompt = "Super Portrait, professional ID photo, studio lighting, sharp focus, neutral background, ultra-high resolution, natural skin texture, neutral expression.",
                        NegativePrompt = "blur, distortion, extra hair, heavy makeup, artificial skin smoothing, exaggerated features, harsh shadows, unrealistic retouching.",
                        Controlnets = "softedge, depth",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 30,
                        GuidanceScale = 7.5,
                        Strength = 0.8,
                        FaceImage = true,
                        BackgroundRemover = true,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:06:00.0000000"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 3,
                        Name = "Fashion Style",
                        Description = "Editorial fashion portrait with high-definition details and elegant outfit.",
                        ImageUrl = "https://www.pixelstalk.net/wp-content/uploads/2016/06/Nature-girl-winter-fashion-wallpaper-1920x1080.jpg",
                        Prompt = "Editorial fashion portrait, striking pose, high-definition details, elegant outfit, perfect fit, confident expression, soft studio lighting. The background is clean and minimalistic, enhancing the subject.",
                        NegativePrompt = "bad lighting, ill-fitting clothes, low-quality textures, unnatural expression, distorted proportions, cluttered background.",
                        Controlnets = "canny, depth, pose",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 26,
                        GuidanceScale = 6.5,
                        Strength = 0.7,
                        FaceImage = true,
                        BackgroundRemover = false,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:16:22.4802536"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 4,
                        Name = "Cartoon",
                        Description = "Vibrant cartoon character with smooth shading and bold outlines.",
                        ImageUrl = "https://static.vecteezy.com/system/resources/thumbnails/046/238/401/small/a-3d-character-illustration-for-a-childrens-book-photo.jpg",
                        Prompt = "Vibrant cartoon character, smooth shading, expressive eyes, bold outlines, dynamic pose. The background is colorful and playful, enhancing the lively personality.",
                        NegativePrompt = "blurred lines, dull colors, photorealistic details, excessive shading, missing body parts, distorted proportions.",
                        Controlnets = "canny, scribble",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 32,
                        GuidanceScale = 8,
                        Strength = 0.85,
                        FaceImage = false,
                        BackgroundRemover = false,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:10:24.5847326"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 5,
                        Name = "Artistic",
                        Description = "Fine art portrait with soft lighting and warm color harmony.",
                        ImageUrl = "https://images5.alphacoders.com/135/thumb-1920-1352020.png",
                        Prompt = "Fine art portrait, soft and balanced lighting, rich textures, warm color harmony. The subject exudes elegance and emotion, resembling classical masterpieces.",
                        NegativePrompt = "oversaturated, artificial blur, harsh contrast, distorted proportions, lifeless expression, missing elements.",
                        Controlnets = "scribble, depth",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 28,
                        GuidanceScale = 7.0,
                        Strength = 0.75,
                        FaceImage = true,
                        BackgroundRemover = false,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:06:34.262168"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 6,
                        Name = "Vintage",
                        Description = "Classic vintage portrait with warm sepia tones and subtle grain texture.",
                        ImageUrl = "https://images7.alphacoders.com/573/573462.jpg",
                        Prompt = "Classic vintage portrait, warm sepia tones, soft film-like contrast, subtle grain texture. The subject appears timeless with natural lighting and elegant styling.",
                        NegativePrompt = "modern sharpness, digital artifacts, unnatural lighting, reflections, artificial blur, excessive grain.",
                        Controlnets = "softedge, scribble",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 25,
                        GuidanceScale = 7.0,
                        Strength = 0.8,
                        FaceImage = true,
                        BackgroundRemover = false,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:34:08.9543789"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 7,
                        Name = "Fantasy",
                        Description = "Ethereal fantasy portrait with glowing atmosphere and magical details.",
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSynwqUs6fsgDDN5tl8UG5NhyLK9nQcC7WCYg&s",
                        Prompt = "Ethereal fantasy portrait, glowing atmosphere, intricate costume details, vibrant magical background. The subject radiates an enchanting aura with a dreamlike aesthetic.",
                        NegativePrompt = "lifeless expression, dull tones, distorted anatomy, harsh shadows, CGI artifacts, unnatural blur.",
                        Controlnets = "softedge, depth",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 35,
                        GuidanceScale = 7.8,
                        Strength = 0.8,
                        FaceImage = true,
                        BackgroundRemover = false,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 8,
                        Name = "Modern",
                        Description = "Sleek modern portrait with high-definition textures and stylish composition.",
                        ImageUrl = "https://img.freepik.com/premium-photo/modern-woman-portrait-hd-8k-wallpaper-stock-photographic-image_890746-53098.jpg",
                        Prompt = "Sleek modern portrait, sharp lighting, high-definition textures, dynamic composition, stylish outfit. The background is minimalistic with a futuristic touch.",
                        NegativePrompt = "low resolution, washed-out colors, bad lighting, asymmetry, distorted shapes, excessive blur.",
                        Controlnets = "canny, depth, softedge",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 25,
                        GuidanceScale = 6.8,
                        Strength = 0.7,
                        FaceImage = false,
                        BackgroundRemover = false,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:27:24.4834447"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 9,
                        Name = "Nature",
                        Description = "Natural outdoor portrait with warm sunlight and scenic backgrounds.",
                        ImageUrl = "https://wallpapercave.com/wp/wp7118266.jpg",
                        Prompt = "Natural outdoor portrait, warm sunlight, soft breeze, fresh atmosphere, glowing skin. The background features lush greenery or scenic landscapes, complementing the subject.",
                        NegativePrompt = "harsh lighting, artificial colors, dull tones, cluttered background, distortion, missing details.",
                        Controlnets = "softedge, depth",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 30,
                        GuidanceScale = 7.2,
                        Strength = 0.75,
                        FaceImage = false,
                        BackgroundRemover = false,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:29:05.8312544"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 10,
                        Name = "Cyberpunk",
                        Description = "Futuristic cyberpunk aesthetic with neon lights and high-tech details.",
                        ImageUrl = "https://techwearstorm.com/cdn/shop/files/cyberpunk-techwearstorm-14.webp",
                        Prompt = "Futuristic cyberpunk aesthetic, neon lights, high-tech details, glowing cityscape, cybernetic enhancements.",
                        NegativePrompt = "low contrast, dull colors, historical elements, medieval designs, washed-out lighting.",
                        Controlnets = "depth, canny",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 30,
                        GuidanceScale = 7.5,
                        Strength = 0.78,
                        FaceImage = true,
                        BackgroundRemover = false,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:38:00.0000000"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 11,
                        Name = "Watercolor Painting",
                        Description = "Soft and elegant watercolor painting with delicate brush strokes.",
                        ImageUrl = "https://images.stockcake.com/public/2/7/8/278d43d3-77ec-4aa3-b4ea-6ae292a706fb_large/dancing-through-mist-stockcake.jpg",
                        Prompt = "Soft and elegant watercolor painting, delicate brush strokes, smooth color blending, artistic expression.",
                        NegativePrompt = "pixelated edges, harsh shadows, digital artifacts, rigid outlines, unrealistic proportions.",
                        Controlnets = "softedge, scribble",
                        Mode = null,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 25,
                        GuidanceScale = 6.8,
                        Strength = 0.72,
                        FaceImage = false,
                        BackgroundRemover = false,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:39:00.0000000"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 12,
                        Name = "Black & White Classic",
                        Description = "Timeless black and white portrait with deep contrast and classic lighting.",
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ_3py34SENuvKrCNTcP7Gn8ss7MyUXGYF9fA&s",
                        Prompt = "Timeless black and white portrait, deep contrast, classic lighting, elegant composition.",
                        NegativePrompt = "grainy texture, washed-out details, weak contrast, digital noise, unbalanced lighting.",
                        Controlnets = "depth, canny",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 22,
                        GuidanceScale = 6.2,
                        Strength = 0.65,
                        FaceImage = true,
                        BackgroundRemover = false,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:40:00.0000000"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new PhotoStyle
                    {
                        Id = 13,
                        Name = "Oil Painting",
                        Description = "Rich and textured oil painting style with thick brush strokes.",
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTROHzxiGPY3o4Uv7OxGSSzbmWEF9jrRbgS0Q&s",
                        Prompt = "Rich and textured oil painting style, thick brush strokes, deep color saturation, artistic rendering.",
                        NegativePrompt = "flat colors, digital sharpness, pixelation, unrealistic lighting, blurred edges.",
                        Controlnets = "scribble, softedge",
                        Mode = null,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 27,
                        GuidanceScale = 7.3,
                        Strength = 0.76,
                        FaceImage = false,
                        BackgroundRemover = false,
                        CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                        LastModified = DateTime.Parse("2025-03-27T08:41:00.0000000"),
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    }
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
                    new TypeSession { Id = 1, Name = "Photography", Description = "Photo session", Duration = 60, Price = 1000, isPrinting = false, AbleTakenNumber = 5, CreatedAt = DateTime.Parse("2025-03-14T05:45:36.4862448"), LastModified = DateTime.Parse("2025-03-14T05:45:36.4862448"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 2, Name = "Videography", Description = "Video session", Duration = 120, Price = 2000, isPrinting = true, AbleTakenNumber = 3, CreatedAt = DateTime.Parse("2025-03-14T05:45:36.4862448"), LastModified = DateTime.Parse("2025-03-14T05:45:36.4862448"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 5, Name = "Gói Cơ Bản", Description = "Gói dịch vụ in cơ bản", Duration = 30, Price = 5000, isPrinting = true, AbleTakenNumber = 10, CreatedAt = DateTime.Parse("2025-03-28T04:24:45.6577221"), LastModified = DateTime.Parse("2025-03-28T04:24:45.6577221"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 6, Name = "Gói Nâng Cao", Description = "Dịch vụ in chất lượng cao", Duration = 60, Price = 1000, isPrinting = true, AbleTakenNumber = 5, CreatedAt = DateTime.Parse("2025-03-28T04:24:57.935814"), LastModified = DateTime.Parse("2025-03-28T04:24:57.935814"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 7, Name = "Tư Vấn Thiết Kế", Description = "Hỗ trợ tư vấn và chỉnh sửa thiết kế", Duration = 45, Price = 3000, isPrinting = false, AbleTakenNumber = 3, CreatedAt = DateTime.Parse("2025-03-28T04:25:06.3817052"), LastModified = DateTime.Parse("2025-03-28T04:25:06.3817052"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 8, Name = "In Nhanh", Description = "Dịch vụ in nhanh lấy liền", Duration = 20, Price = 1000, isPrinting = true, AbleTakenNumber = 8, CreatedAt = DateTime.Parse("2025-03-28T04:25:15.7805502"), LastModified = DateTime.Parse("2025-03-28T04:25:15.7805502"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 9, Name = "In Màu", Description = "In màu chất lượng cao", Duration = 40, Price = 2000, isPrinting = true, AbleTakenNumber = 6, CreatedAt = DateTime.Parse("2025-03-28T04:25:23.9206205"), LastModified = DateTime.Parse("2025-03-28T04:25:23.9206205"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 10, Name = "Scan Tài Liệu", Description = "Scan tài liệu lưu trữ dạng số", Duration = 25, Price = 3000, isPrinting = false, AbleTakenNumber = 15, CreatedAt = DateTime.Parse("2025-03-28T04:25:29.5577387"), LastModified = DateTime.Parse("2025-03-28T04:25:29.5577387"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 11, Name = "Scan Hồ Sơ", Description = "Chuyển đổi hồ sơ giấy sang bản số", Duration = 30, Price = 4000, isPrinting = false, AbleTakenNumber = 10, CreatedAt = DateTime.Parse("2025-03-28T05:11:40.457483"), LastModified = DateTime.Parse("2025-03-28T05:11:40.457483"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 12, Name = "Scan Biểu Mẫu", Description = "Lưu trữ biểu mẫu hành chính", Duration = 20, Price = 5000, isPrinting = false, AbleTakenNumber = 12, CreatedAt = DateTime.Parse("2025-03-28T05:11:47.0305943"), LastModified = DateTime.Parse("2025-03-28T05:11:47.0305943"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 13, Name = "Scan Bài Giảng", Description = "Số hóa bài giảng dạng PDF", Duration = 40, Price = 1000, isPrinting = false, AbleTakenNumber = 8, CreatedAt = DateTime.Parse("2025-03-28T05:11:54.5815379"), LastModified = DateTime.Parse("2025-03-28T05:11:54.5815379"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 14, Name = "Scan Tạp Chí", Description = "Lưu trữ nội dung tạp chí", Duration = 35, Price = 2000, isPrinting = false, AbleTakenNumber = 6, CreatedAt = DateTime.Parse("2025-03-28T05:12:01.0023415"), LastModified = DateTime.Parse("2025-03-28T05:12:01.0023415"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 15, Name = "Scan Luận Văn", Description = "Chuyển luận văn thành bản mềm", Duration = 50, Price = 3000, isPrinting = false, AbleTakenNumber = 5, CreatedAt = DateTime.Parse("2025-03-28T05:12:07.5845421"), LastModified = DateTime.Parse("2025-03-28T05:12:07.5845421"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 16, Name = "Scan Tài Liệu Cũ", Description = "Số hóa tài liệu giấy cũ", Duration = 45, Price = 4000, isPrinting = false, AbleTakenNumber = 7, CreatedAt = DateTime.Parse("2025-03-28T05:12:13.2268526"), LastModified = DateTime.Parse("2025-03-28T05:12:13.2268526"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 17, Name = "Scan Sách", Description = "Scan toàn bộ nội dung sách", Duration = 60, Price = 6000, isPrinting = false, AbleTakenNumber = 4, CreatedAt = DateTime.Parse("2025-03-28T05:12:19.7889762"), LastModified = DateTime.Parse("2025-03-28T05:12:19.7889762"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 18, Name = "Scan Hợp Đồng", Description = "Chuyển hợp đồng sang file PDF", Duration = 30, Price = 3000, isPrinting = false, AbleTakenNumber = 9, CreatedAt = DateTime.Parse("2025-03-28T05:12:26.1047679"), LastModified = DateTime.Parse("2025-03-28T05:12:26.1047679"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 19, Name = "Scan Giấy Tờ Cá Nhân", Description = "Lưu trữ giấy tờ cá nhân dạng số", Duration = 15, Price = 2000, isPrinting = false, AbleTakenNumber = 20, CreatedAt = DateTime.Parse("2025-03-28T05:12:31.7729663"), LastModified = DateTime.Parse("2025-03-28T05:12:31.7729663"), CreatedById = admin.Id, LastModifiedById = admin.Id }
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
                        CustomerId = admin.Id,
                        Status = OrderStatus.Pending,
                        Amount = 100000,
                        Email = "admin@example.com",
                        Phone = "123456789",
                        CouponId = 1,
                        TypeSessionId = 1,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    }, new Order
                    {
                        Id = 2,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        CustomerId = admin.Id,
                        Status = OrderStatus.Pending,
                        Amount = 100000,
                        Email = "admin@example.com",
                        Phone = "123456789",
                        CouponId = 1,
                        TypeSessionId = 1,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    }, new Order
                    {
                        Id = 3,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        CustomerId = admin.Id,
                        Status = OrderStatus.Pending,
                        Amount = 100000,
                        Email = "admin@example.com",
                        Phone = "123456789",
                        CouponId = 1,
                        TypeSessionId = 1,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 4,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        CustomerId = manager.Id,
                        Status = OrderStatus.Completed,
                        Amount = 500000,
                        Email = "manager@example.com",
                        Phone = "987654321",
                        CouponId = 2,
                        TypeSessionId = 2,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 5,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        CustomerId = manager.Id,
                        Status = OrderStatus.Completed,
                        Amount = 500000,
                        Email = "manager@example.com",
                        Phone = "987654321",
                        CouponId = 2,
                        TypeSessionId = 2,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 6,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        CustomerId = manager.Id,
                        Status = OrderStatus.Completed,
                        Amount = 500000,
                        Email = "manager@example.com",
                        Phone = "987654321",
                        CouponId = 2,
                        TypeSessionId = 2,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 7,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        CustomerId = manager.Id,
                        Status = OrderStatus.Completed,
                        Amount = 500000,
                        Email = "manager@example.com",
                        Phone = "987654321",
                        CouponId = 2,
                        TypeSessionId = 2,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 8,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        CustomerId = manager.Id,
                        Status = OrderStatus.Completed,
                        Amount = 500000,
                        Email = "manager@example.com",
                        Phone = "987654321",
                        CouponId = 2,
                        TypeSessionId = 2,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 9,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        CustomerId = manager.Id,
                        Status = OrderStatus.Completed,
                        Amount = 500000,
                        Email = "manager@example.com",
                        Phone = "987654321",
                        CouponId = 2,
                        TypeSessionId = 2,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new Order
                    {
                        Id = 10,
                        Code = new Random().NextInt64(1000000000, 9999999999),
                        CustomerId = manager.Id,
                        Status = OrderStatus.Completed,
                        Amount = 500000,
                        Email = "manager@example.com",
                        Phone = "987654321",
                        CouponId = 2,
                        TypeSessionId = 2,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Order] OFF");

            #endregion

            #region Seed data for Order
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Deposit] ON");

            if (!context.Deposit.Any())
            {
                context.Deposit.AddRange(
                    new Deposit
                    {
                        Id = 1,
                        WalletId = 1,
                        Amount = 100000,
                        Status = DepositStatus.Success,
                    },
                    new Deposit
                    {
                        Id = 2,
                        WalletId = 2,
                        Amount = 100000,
                        Status = DepositStatus.Success,
                    }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Deposit] OFF");

            #endregion


            #region Seed data for Payment
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Payment] ON");

            if (!context.Payment.Any())
            {
                context.Payment.AddRange(
                    new Payment { Id = 1, OrderId = 1, PaymentMethodId = 1, Status = PaymentStatus.Success, Amount = 150000 },
                    new Payment { Id = 2, OrderId = 2, PaymentMethodId = 2, Status = PaymentStatus.Success, Amount = 200000 },
                    new Payment { Id = 3, DepositId = 1, PaymentMethodId = 3, Status = PaymentStatus.Success, Amount = 100000 },
                    new Payment { Id = 4, DepositId = 2, PaymentMethodId = 4, Status = PaymentStatus.Success, Amount = 250000 }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Payment] OFF");

            #endregion

            #region Seed data for Transaction
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Transaction] ON");

            if (!context.Transaction.Any())
            {
                context.Transaction.AddRange(
                    new Transaction { Id = 1, Type = TransactionType.Purchase, Amount = 50000, PaymentId = 1 },
                    new Transaction { Id = 2, Type = TransactionType.Purchase, Amount = 100000, PaymentId = 2 },
                    new Transaction { Id = 3, Type = TransactionType.Deposit, Amount = 200000, PaymentId = 3 },
                    new Transaction { Id = 4, Type = TransactionType.Deposit, Amount = 150000, PaymentId = 4 }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Transaction] OFF");

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
                    new Session { Id = 10,Code = "SESSION",Expired = DateTime.Now.AddDays(60), IsActive = true, OrderId = 10 }

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
                    new PhotoHistory { Id = 1, CustomerId = admin.Id,  BoothId = 1,  SessionId = 1},
                    new PhotoHistory {Id = 2, CustomerId = manager.Id, BoothId = 2, SessionId = 2 },
                    new PhotoHistory {Id = 3, CustomerId = staff.Id, BoothId = 3, SessionId = 3 },
                    new PhotoHistory {Id = 4, CustomerId = user1.Id, BoothId = 4, SessionId = 4 },
                    new PhotoHistory {Id = 5, CustomerId = admin.Id, BoothId = 5, SessionId = 5 },
                    new PhotoHistory {Id = 6, CustomerId = manager.Id, BoothId = 6, SessionId = 6 },
                    new PhotoHistory {Id = 7, CustomerId = staff.Id, BoothId = 7, SessionId = 7 },
                    new PhotoHistory {Id = 8, CustomerId = user1.Id, BoothId = 8, SessionId = 8 },
                    new PhotoHistory {Id = 9, CustomerId = admin.Id, BoothId = 9, SessionId = 9 },
                    new PhotoHistory {Id = 10, CustomerId = manager.Id, BoothId = 10, SessionId = 10 }
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

            #region Seed data for Sticker
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Sticker] ON");

            if (!context.Sticker.Any())
            {
                context.Sticker.AddRange(
                    new Sticker { Id = 1, Name = "Pray", StickerUrl = "https://drive.google.com/file/d/1BZonx3SOuicTnQU_k-yzZgHacziAzP2k/view?usp=drive_link", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 2, Name = "Coffee", StickerUrl = "https://drive.google.com/file/d/1JZzmBczOnHxAsfDPeode9Q5hAipWmTi9/view?usp=drive_link", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 3, Name = "Cloud", StickerUrl = "https://drive.google.com/file/d/1YqYTDETYKfsSAfxcEyN72xFyerqVt-4e/view?usp=drive_link", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 4, Name = "Bean", StickerUrl = "https://drive.google.com/file/d/146ne7Z04Lxa2dlOE0l-0lTNnWd6EBWVy/view", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 5, Name = "Ice_cream", StickerUrl = "https://drive.google.com/file/d/1XXyj8VAKc3Z-vqhxp8U7LomPKXuZQdJY/view?usp=drive_link", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 6, Name = "Movie_Sticket", StickerUrl = "https://drive.google.com/file/d/1BtQzit4aWtgygrJf2QbN8NCS5s3sI257/view?usp=drive_link", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 7, Name = "Plant", StickerUrl = "https://drive.google.com/file/d/1cHP9JLTY7zO4VKpSljAjWTRyYznfwbs2/view?usp=drive_link", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 8, Name = "Rainbow", StickerUrl = "https://drive.google.com/file/d/1AAdxKhOxcNCHSw-BwgTf0oyAH8tmYcWg/view?usp=drive_link", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 9, Name = "Smile", StickerUrl = "https://drive.google.com/file/d/18_rCFDs2o7I9c1WdAAf3OUeqzZrp3u6A/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 10, Name = "Wink", StickerUrl = "https://drive.google.com/file/d/1QGv4ZXk5MZaGyVhBDMvxgwSOW9OWGFwQ/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 11, Name = "Cool", StickerUrl = "https://drive.google.com/file/d/1EejiUETR7Ofdjo_Zb8heBC7utJdntMhq/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 12, Name = "Scare", StickerUrl = "https://drive.google.com/file/d/1ggF3lEobZBD0SbBRc3wPZDwbwr05PuNl/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 13, Name = "Proud", StickerUrl = "https://drive.google.com/file/d/1LeP0-AYgW3URwPrDAp_zBDDllhJwmpZH/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 14, Name = "Swag", StickerUrl = "https://drive.google.com/file/d/1RR1LyM7YhZAK1D7uXA5MK-oPS6oNpIV8/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 15, Name = "Angry", StickerUrl = "https://drive.google.com/file/d/1ayDyVxPdeuse7lGr0ll81pUoUrndiR5-/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 16, Name = "Confused", StickerUrl = "https://drive.google.com/file/d/12oDluYCDgJmMcXnzvoNcTAqWL0QH9JCR/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 17, Name = "Happy", StickerUrl = "https://drive.google.com/file/d/10vrMuKNSl9NtLhVKwdJstBodewqhZzXB/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 18, Name = "Love", StickerUrl = "https://drive.google.com/file/d/1PueFMYnXVpdcIP3_DgCR1_5Zr8r9REek/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 19, Name = "Sad", StickerUrl = "https://drive.google.com/file/d/1W1kVS1qGIXSKnmGq-8CBkT5JkaUgP1WO/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 20, Name = "Sick", StickerUrl = "https://drive.google.com/file/d/1v3ghCS8hM7WPsgCKyghKBN8bcM35Yoyn/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 21, Name = "Sleep", StickerUrl = "https://drive.google.com/file/d/10UCZKTWQ9-_N2qzMd8enDaaqoaTusyi7/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 22, Name = "Smiley", StickerUrl = "https://drive.google.com/file/d/1fIMPlwsOqYMMrPXxXj-YRNsMDsswV4om/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 23, Name = "Star", StickerUrl = "https://drive.google.com/file/d/1I85qNS7qjF8RIkjy8sQC3HZlMs0ytGZX/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 24, Name = "Suprises", StickerUrl = "https://drive.google.com/file/d/1Z44M58oL314jRBm2ry5TYlTzkBZj5fSL/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 25, Name = "Vain", StickerUrl = "https://drive.google.com/file/d/1Dsw_h3oZU77VAGEe_jBCUS5-LVTRkVWM/view?usp=drive_link", StickerStyleId = 5, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 26, Name = "Death", StickerUrl = "https://drive.google.com/file/d/1RUTccLKBbM0BweLedxPVMHH_GUEsB1WW/view?usp=drive_link", StickerStyleId = 6, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 27, Name = "Applle", StickerUrl = "https://drive.google.com/file/d/1cGP36rV-mEfpL9zducv4ptbBr8-okQDD/view?usp=drive_link", StickerStyleId = 6, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 28, Name = "Fire", StickerUrl = "https://drive.google.com/file/d/1Jmosmy8L9Q7ExF97BOKumNbPreQCf2kk/view?usp=drive_link", StickerStyleId = 6, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 29, Name = "Ghost", StickerUrl = "https://drive.google.com/file/d/1OMJxFNfHFyIqPodxPPFFQ4yzx60nP_E1/view?usp=drive_link", StickerStyleId = 6, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 30, Name = "Mushroom", StickerUrl = "https://drive.google.com/file/d/1jZrfpw0PuJyexI2mmkqq7hUGuFoobl6g/view?usp=drive_link", StickerStyleId = 6, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 31, Name = "Avocado", StickerUrl = "https://drive.google.com/file/d/1_jbxo4IvsU42kG37lJ6mTbmnTl2gR2aw/view?usp=drive_link", StickerStyleId = 7, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 32, Name = "French-fire", StickerUrl = "https://drive.google.com/file/d/1U7VFuaeGCX8JLuaNZRrWxPGYYdfAZWZ6/view?usp=drive_link", StickerStyleId = 7, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 33, Name = "Egg", StickerUrl = "https://drive.google.com/file/d/1HvJ9t_eAOQhU8b6nprtcE_0o7eL6NpQT/view?usp=drive_link", StickerStyleId = 7, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 34, Name = "Sushi", StickerUrl = "https://drive.google.com/file/d/1PUO0IZzwXEimJ6GgzDKcMNZRZ97_y5Tz/view?usp=drive_link", StickerStyleId = 7, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 35, Name = "Noodle", StickerUrl = "https://drive.google.com/file/d/16SBRl8VydidFGo3XfF7Cg3x0GcNKUyNA/view?usp=drive_link", StickerStyleId = 7, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 36, Name = "Pancake", StickerUrl = "https://drive.google.com/file/d/1UyCo_8TJ17P6QrMZ-ay_MAMd73D2LDkf/view?usp=drive_link", StickerStyleId = 7, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 37, Name = "Sandwich", StickerUrl = "https://drive.google.com/file/d/1MLS1lWh3KRXWeipFtu1fVdV9w_iEZRAy/view?usp=drive_link", StickerStyleId = 7, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 38, Name = "Bee", StickerUrl = "https://drive.google.com/file/d/1Mtl-RMkCfsjpDTAqH6sugHZUSlYIV8u0/view?usp=drive_link", StickerStyleId = 8, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 39, Name = "Dog", StickerUrl = "https://drive.google.com/file/d/1eUUK8ESQNaWK4CHkLTce-b5bSaPc7_w9/view?usp=drive_link", StickerStyleId = 8, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 40, Name = "Dragon", StickerUrl = "https://drive.google.com/file/d/1itHCAlot57G29C52RTXuuL27daTG7y4c/view?usp=drive_link", StickerStyleId = 8, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 41, Name = "Racoon eat", StickerUrl = "https://drive.google.com/file/d/1YYlEEBcmlr-YYQm0h4yaLBXOWhb-XW_6/view?usp=drive_link", StickerStyleId = 8, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 42, Name = "Frog", StickerUrl = "https://drive.google.com/file/d/1lCl9hBe7G4bhxiH_93S0aUXAV-C1HrfJ/view?usp=drive_link", StickerStyleId = 8, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 43, Name = "Rabbit", StickerUrl = "https://drive.google.com/file/d/1B8Tau0JpcvORiyW2700WKhTldrMVRxUb/view?usp=drive_link", StickerStyleId = 8, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 44, Name = "Racoon hi", StickerUrl = "https://drive.google.com/file/d/16B9YCLjjGRIaMhRUVcNOjw4DH9DzCACQ/view?usp=drive_link", StickerStyleId = 8, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 45, Name = "Panda", StickerUrl = "https://drive.google.com/file/d/17B-esPi5HUznad9qsSI3nd26XOn3Epya/view?usp=drive_link", StickerStyleId = 8, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 46, Name = "Panda1", StickerUrl = "https://drive.google.com/file/d/1WcbNbBcBYSFI4Z74GNmEAZka3mp14Ux2/view?usp=drive_link", StickerStyleId = 8, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 47, Name = "Unicorn", StickerUrl = "https://drive.google.com/file/d/1QicqtouCYx0OIIVYQyiK4l05ZYSS2ZUk/view?usp=drive_link", StickerStyleId = 8, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 48, Name = "Halloween", StickerUrl = "https://drive.google.com/file/d/1uKUZKYANvBWUkgh3Vt7rkzEXaYDVf-m_/view?usp=drive_link", StickerStyleId = 9, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 49, Name = "Christmas", StickerUrl = "https://drive.google.com/file/d/1AdzK562of-E3hRZ4vq8P-f-d4L3uEful/view?usp=drive_link", StickerStyleId = 9, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 50, Name = "Holly", StickerUrl = "https://drive.google.com/file/d/1o8gf01UtwDiLWAEs3H7iuLWgj0qrM-Aj/view?usp=drive_link", StickerStyleId = 9, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 51, Name = "Suitcase1", StickerUrl = "https://drive.google.com/file/d/1SAyAvx95IRaTAmtuSveRg_zQ8LRhxb2X/view?usp=drive_link", StickerStyleId = 9, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 52, Name = "Suitcase", StickerUrl = "https://drive.google.com/file/d/1nd1byBx1s-RKR5elRHJFZXMxOefhcYjI/view?usp=drive_link", StickerStyleId = 9, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 53, Name = "Umbrella", StickerUrl = "https://drive.google.com/file/d/1KhivbOMS774jDrXkOEZxhqZ5OlUrx7X8/view?usp=drive_link", StickerStyleId = 9, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 54, Name = "Cassette_love", StickerUrl = "https://drive.google.com/file/d/1-k12jADiL3iCpLY40obXoiN__mBWXlw8/view?usp=drive_link", StickerStyleId = 10, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 55, Name = "Love1", StickerUrl = "https://drive.google.com/file/d/1o8_xHQAsIxfI1p1okjcfrD3hcQy0ffVM/view?usp=drive_link", StickerStyleId = 10, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 56, Name = "Love2", StickerUrl = "https://drive.google.com/file/d/1o8Y2wAzmZ3Igf62iPC2kaKsZHLfZi7N6/view?usp=drive_link", StickerStyleId = 10, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 57, Name = "Love3", StickerUrl = "https://drive.google.com/file/d/1bK6bN1eOcUzFCdcrwMW7fdhy39M0N0He/view?usp=drive_link", StickerStyleId = 10, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 58, Name = "Love4", StickerUrl = "https://drive.google.com/file/d/1m6zPRIuwYzv7mZpj1C6NgQfHgfgakuP2/view?usp=drive_link", StickerStyleId = 10, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 59, Name = "Love5", StickerUrl = "https://drive.google.com/file/d/1C6I-u3TiUYsqPG8Q6rMCTsSGcwOWztcM/view?usp=drive_link", StickerStyleId = 10, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 60, Name = "Cupid", StickerUrl = "https://drive.google.com/file/d/12wNyy3TnUjHqsijTeh6Fu67flFnRAFD2/view?usp=drive_link", StickerStyleId = 10, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 61, Name = "Wedding", StickerUrl = "https://drive.google.com/file/d/1_2YO8YU7xqB6XkCQ_qRs9HMypMQuV60-/view?usp=drive_link", StickerStyleId = 10, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null }
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
                    new FrameStyle { Id = 1, Name = "Wedding", Description = "Elegant, romantic wooden frame, perfect for wedding photobooths", ImageUrl = "https://mlf2vmoci1kz.i.optimole.com/w:auto/h:auto/q:mauto/f:best/https://finetemplate.com/wp-content/uploads/2024/02/Mr-Mrs-Bergin-19th-august-2023-copy-Mockup-1-copy-scaled.jpg", CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new FrameStyle { Id = 2, Name = "Party", Description = "Colorful, vibrant frame that creates a fun atmosphere for party photobooths", ImageUrl = "https://i.etsystatic.com/25728422/r/il/f059c8/2728468113/il_570xN.2728468113_huxk.jpg", CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new FrameStyle { Id = 3, Name = "Retro", Description = "Vintage frame with a nostalgic feel for retro-themed photobooths", ImageUrl = "https://i.etsystatic.com/33935117/r/il/ef952a/3657377352/il_570xN.3657377352_sg9z.jpg", CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new FrameStyle { Id = 4, Name = "Holiday", Description = "Festive frame with cheerful decorations ideal for seasonal photobooths", ImageUrl = "https://i.etsystatic.com/21809061/r/il/10cd17/4430822420/il_570xN.4430822420_dmz3.jpg", CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new FrameStyle { Id = 5, Name = "Corporate", Description = "Sleek, modern frame designed for professional photobooths at corporate events", ImageUrl = "https://www.prostarra.com/wp-content/uploads/2018/08/sqsp-simple-modern-white-6x4-photo-booth-template-3.jpg", CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new FrameStyle { Id = 6, Name = "School", Description = "A fun, youthful frame style perfect for school events and graduation photobooths", ImageUrl = "https://i.etsystatic.com/21809061/r/il/5bfe4d/6014778330/il_570xN.6014778330_m1tp.jpg", CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new FrameStyle { Id = 7, Name = "Travel", Description = "A dynamic frame style capturing the spirit of adventure, ideal for travel photobooths", ImageUrl = "https://i.etsystatic.com/22697668/r/il/c2ae6d/5439314654/il_fullxfull.5439314654_9oxo.jpg", CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null }
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
                    new Frame { Id = 1, Name = "HappyBirthday", FrameUrl = "https://drive.google.com/file/d/1ZS1zXdUxBYRkirQadlEElwIgOMa60vad/view", FrameStyleId = 2, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 2, Name = "Instagram", FrameUrl = "https://drive.google.com/file/d/1VND0Z3ropoHb2twX8ILZeKlIfkyoWsTa/view", FrameStyleId = 5, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 3, Name = "Japan", FrameUrl = "https://drive.google.com/file/d/1WahRPkcVZzaoDuuk3SXk5qBoBUBPT6E6/view", FrameStyleId = 7, SlotCount = 4, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 4, Name = "Happy Wedding", FrameUrl = "https://drive.google.com/file/d/1_0kc_agy8ayR0sFYdK5Us3c02PEyyB3W/view?usp=drive_link", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 5, Name = "Graduation", FrameUrl = "https://drive.google.com/file/d/171-FoRED14gl2Cv3hPR6AgfOmcaw0_Xu/view?usp=drive_link", FrameStyleId = 6, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 6, Name = "90's Style", FrameUrl = "https://drive.google.com/file/d/1bNX_D93FLc4qbFtER412RSi9P_lgSUbA/view?usp=drive_link", FrameStyleId = 3, SlotCount = 4, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 7, Name = "Zoo", FrameUrl = "https://drive.google.com/file/d/1xMMXbjZpHfN-IFqWWVRgGzUjIxfZE945/view?usp=drive_link", FrameStyleId = 7, SlotCount = 4, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 8, Name = "Merry Christmas", FrameUrl = "https://drive.google.com/file/d/15geQ4wNFFXXS24t1UpDT152GtutRiv97/view?usp=drive_link", FrameStyleId = 4, SlotCount = 4, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 9, Name = "Halloween", FrameUrl = "https://drive.google.com/file/d/1SvQYnlAXBDssTuqoARfUHm6B2jazpZzy/view?usp=drive_link", FrameStyleId = 4, SlotCount = 4, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 10, Name = "Summer Sea", FrameUrl = "https://drive.google.com/file/d/10HMLOkIRT61bPwv2Z7p7TptyzRjV-aKF/view?usp=drive_link", FrameStyleId = 7, SlotCount = 4, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 11, Name = "Night Party", FrameUrl = "https://drive.google.com/file/d/1uAhr0mKlRly59xPKE_V1y_q8uTlYoWHQ/view?usp=drive_link", FrameStyleId = 2, SlotCount = 4, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 12, Name = "Vintage Camera", FrameUrl = "https://drive.google.com/file/d/1ssVFVEseRLDrPDz3cHKunEumIhXSg66S/view?usp=drive_link", FrameStyleId = 3, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 13, Name = "Happy New Year", FrameUrl = "https://drive.google.com/file/d/15lacrlKeqKg-OCKGC1RNxVbT6vv0IAsb/view?usp=drive_link", FrameStyleId = 4, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 14, Name = "Happy Mid-Autumn", FrameUrl = "https://drive.google.com/file/d/1nc7W2NCYH7Ac-xsglCwA4OOjFNnF6jyv/view?usp=drive_link", FrameStyleId = 4, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 15, Name = "VietNam Travel", FrameUrl = "https://drive.google.com/file/d/1F6YMTO9XmNbruj8Q1WwDvRpDhkxVct3o/view?usp=drive_link", FrameStyleId = 7, SlotCount = 4, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 16, Name = "Grunge Retro", FrameUrl = "https://drive.google.com/file/d/1yfWbe2Nu23VXsBKk1nC9Bhi8XIjNYkoZ/view?usp=drive_link", FrameStyleId = 3, SlotCount = 4, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null }
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
                    new Coordinate { Id = 1, FrameId = 1, X = 290, Y = 104, Width = 1414, Height = 943, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 2, FrameId = 2, X = 290, Y = 104, Width = 1414, Height = 943, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 3, FrameId = 4, X = 290, Y = 104, Width = 1414, Height = 943, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 4, FrameId = 5, X = 290, Y = 104, Width = 1414, Height = 943, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 5, FrameId = 3, X = 187, Y = 243, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 6, FrameId = 3, X = 1058, Y = 240, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 7, FrameId = 3, X = 187, Y = 833, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 8, FrameId = 3, X = 1056, Y = 831, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 9, FrameId = 6, X = 187, Y = 243, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 10, FrameId = 6, X = 1058, Y = 240, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 11, FrameId = 6, X = 187, Y = 833, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 12, FrameId = 6, X = 1056, Y = 831, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 13, FrameId = 7, X = 187, Y = 243, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 14, FrameId = 7, X = 1058, Y = 240, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 15, FrameId = 7, X = 187, Y = 833, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 16, FrameId = 7, X = 1056, Y = 831, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 17, FrameId = 8, X = 187, Y = 243, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 18, FrameId = 8, X = 1058, Y = 240, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 19, FrameId = 8, X = 187, Y = 833, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 20, FrameId = 8, X = 1056, Y = 831, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 21, FrameId = 9, X = 187, Y = 243, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 22, FrameId = 9, X = 1058, Y = 240, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 23, FrameId = 9, X = 187, Y = 833, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 24, FrameId = 9, X = 1056, Y = 831, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 25, FrameId = 10, X = 187, Y = 243, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 26, FrameId = 10, X = 1058, Y = 240, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 27, FrameId = 10, X = 187, Y = 833, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 28, FrameId = 10, X = 1056, Y = 831, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 29, FrameId = 11, X = 187, Y = 243, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 30, FrameId = 11, X = 1058, Y = 240, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 31, FrameId = 11, X = 187, Y = 833, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 32, FrameId = 11, X = 1056, Y = 831, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 33, FrameId = 12, X = 290, Y = 104, Width = 1414, Height = 943, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 34, FrameId = 13, X = 290, Y = 104, Width = 1414, Height = 943, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 35, FrameId = 14, X = 290, Y = 104, Width = 1414, Height = 943, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 36, FrameId = 15, X = 187, Y = 243, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 37, FrameId = 15, X = 1058, Y = 240, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 38, FrameId = 15, X = 187, Y = 833, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 39, FrameId = 15, X = 1056, Y = 831, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 40, FrameId = 16, X = 187, Y = 243, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 41, FrameId = 16, X = 1058, Y = 240, Width = 732, Height = 488, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 42, FrameId = 16, X = 187, Y = 833, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false },
                    new Coordinate { Id = 43, FrameId = 16, X = 1056, Y = 831, Width = 734, Height = 489, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false }
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

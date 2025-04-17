using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

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
                    new User { UserName = "staff", Email = "staff@example.com", PhoneNumber = "07774567897", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new User { UserName = "trang.doan.2003", Email = "trang.doan.2003@gmail.com", PhoneNumber = "07774567897", Gender = BussinessObject.Enums.UserGender.Male, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false }
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
                    { "staff", "Staff" },
                    { "trang.doan.2003", "Manager" }
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

                //Dummy Data
                int counter = 100;

                for (int i = 1; i <= 10; i++)
                {
                    var user = new User
                    {
                        UserName = $"admin_gen_{i}",
                        Email = $"admin_gen_{i}@example.com",
                        PhoneNumber = $"090000{counter++:D3}",
                        Gender = BussinessObject.Enums.UserGender.Male,
                        CreatedAt = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        IsDeleted = false
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(user, "Admin");
                }

                for (int i = 1; i <= 10; i++)
                {
                    var user = new User
                    {
                        UserName = $"manager_gen_{i}",
                        Email = $"manager_gen_{i}@example.com",
                        PhoneNumber = $"090000{counter++:D3}",
                        Gender = BussinessObject.Enums.UserGender.Male,
                        CreatedAt = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        IsDeleted = false
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(user, "Manager");
                }

                for (int i = 1; i <= 10; i++)
                {
                    var user = new User
                    {
                        UserName = $"staff_gen_{i}",
                        Email = $"staff_gen_{i}@example.com",
                        PhoneNumber = $"090000{counter++:D3}",
                        Gender = BussinessObject.Enums.UserGender.Female,
                        CreatedAt = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        IsDeleted = false
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(user, "Staff");
                }

                for (int i = 1; i <= 10; i++)
                {
                    var user = new User
                    {
                        UserName = $"customer_gen_{i}",
                        Email = $"customer_gen_{i}@example.com",
                        PhoneNumber = $"090000{counter++:D3}",
                        Gender = BussinessObject.Enums.UserGender.Male,
                        CreatedAt = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        IsDeleted = false
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(user, "Customer");
                }

            }

            var admin = await userManager.FindByNameAsync("admin1");
            var admin2 = await userManager.FindByNameAsync("raikungfu");
            var manager = await userManager.FindByNameAsync("trang.doan.2003");
            var manager2 = await userManager.FindByNameAsync("raikungfu98");
            var staff = await userManager.FindByNameAsync("trangdnhqe170154");
            var staff2 = await userManager.FindByNameAsync("staff");
            var customer = await userManager.FindByNameAsync("phuongtbde160476");
            var customer2 = await userManager.FindByNameAsync("hienpnxqe170189");

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
                    new Coupon { Id = 1, Name = "New Year Deal", Code = "NY2025", Description = "New Year 2025 promotion", Discount = 5000, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), MaxUse = 100, MaxDiscount = 100000, MinOrder = 10000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 2, Name = "Spring Sale", Code = "SPRING25", Description = "Spring discount", Discount = 3000,  StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(2), MaxUse = 50, MaxDiscount = 50000, MinOrder = 15000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 3, Name = "Summer Heat", Code = "SUMMERHEAT", Description = "Hot summer deal", Discount = 4000, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(2), MaxUse = 70, MaxDiscount = 60000, MinOrder = 10000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 4, Name = "Back to School", Code = "SCHOOL25", Description = "School season sale", Discount = 2500, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), MaxUse = 30, MaxDiscount = 40000, MinOrder = 12000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 5, Name = "Autumn Bonus", Code = "AUTUMN22", Description = "Fall special offer", Discount = 2200, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), MaxUse = 60, MaxDiscount = 45000, MinOrder = 13000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 6, Name = "Black Friday", Code = "BLKFRI50", Description = "Black Friday 50% OFF", DiscountPercent = 0.50m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(10), MaxUse = 500, MaxDiscount = 200000, MinOrder = 10000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 7, Name = "Cyber Monday", Code = "CYBER2025", Description = "Cyber Monday sale",  DiscountPercent = 0.35m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(7), MaxUse = 300, MaxDiscount = 150000, MinOrder = 20000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 8, Name = "Xmas Cheer", Code = "XMAS2025", Description = "Christmas discount",    StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1), MaxUse = 250, MaxDiscount = 180000, MinOrder = 200000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 9, Name = "Birthday Gift", Code = "BIRTHDAY", Description = "Special birthday offer",  DiscountPercent = 0.20m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(5), MaxUse = 100, MaxDiscount = 80000, MinOrder = 5000, CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new Coupon { Id = 10, Name = "Flash Sale", Code = "FLASH2025", Description = "Limited time discount", DiscountPercent = 0.15m, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(2), MaxUse = 80, MaxDiscount = 60000, MinOrder = 4000, CreatedById = admin.Id, LastModifiedById = admin.Id }
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
                new LevelMembership { Id = 1, Name = "Basic", Description = "Basic membership level", Point = 0, DiscountPercent = 0.02m, MaxDiscount = 1000, MinOrder = 10000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                new LevelMembership { Id = 2, Name = "Bronze", Description = "Bronze membership level", Point = 100000, DiscountPercent = 0.05m, MaxDiscount = 2500, MinOrder = 10000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                new LevelMembership { Id = 3, Name = "Silver", Description = "Silver membership level", Point = 300000, DiscountPercent = 0.10m, MaxDiscount = 5000, MinOrder = 10000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                new LevelMembership { Id = 4, Name = "Gold", Description = "Gold membership level", Point = 600000, DiscountPercent = 0.15m, MaxDiscount = 7500, MinOrder = 10000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                new LevelMembership { Id = 5, Name = "Platinum", Description = "Platinum membership level", Point = 1000000, DiscountPercent = 0.20m, MaxDiscount = 10000, MinOrder = 10000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id }
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
                        Prompt = "a person wearing a formal suit, white shirt, professional portrait, studio lighting, upper body, front view, neutral expression, clean background, high quality, photorealistic",
                        NegativePrompt = "blurry, low quality, cartoon, 3d, deformed, extra arms, bad hands, exaggerated makeup, jewelry, messy hair, hat.",
                        //Prompt = "Super Portrait, professional ID photo, studio lighting, sharp focus, neutral background, straight-on angle, natural skin texture, neutral expression, straight posture, ultra-high resolution.",
                        //NegativePrompt = "blur, distortion, extra hair, heavy makeup, artificial skin smoothing, exaggerated features, harsh shadows, unrealistic retouching, incorrect posture, head tilt.",
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
                    //new PhotoStyle
                    //{
                    //    Id = 2,
                    //    Name = "ID Photo (ControlNet)",
                    //    Description = "Professional ID photo with enhanced details using ControlNet.",
                    //    ImageUrl = "https://cdn.tgdd.vn/Files/2023/07/10/1537629/cachkhacphucloitaianh4x6lenhochieuonline-130723-145414-800-resize.jpg",
                    //    Prompt = "Super Portrait, professional ID photo, studio lighting, sharp focus, neutral background, ultra-high resolution, natural skin texture, neutral expression.",
                    //    NegativePrompt = "blur, distortion, extra hair, heavy makeup, artificial skin smoothing, exaggerated features, harsh shadows, unrealistic retouching.",
                    //    Controlnets = "softedge, depth",
                    //    Mode = InpaintMode.KeepStructure,
                    //    NumImagesPerGen = 6,
                    //    Height = null,
                    //    Width = null,
                    //    NumInferenceSteps = 30,
                    //    GuidanceScale = 7.5,
                    //    Strength = 0.8,
                    //    FaceImage = true,
                    //    BackgroundRemover = true,
                    //    CreatedAt = DateTime.Parse("2025-03-14T05:45:36.871683"),
                    //    LastModified = DateTime.Parse("2025-03-27T08:06:00.0000000"),
                    //    CreatedById = admin.Id,
                    //    LastModifiedById = admin.Id
                    //},
                    new PhotoStyle
                    {
                        Id = 2,
                        Name = "Anime",
                        Description = "Anime-style portrait with refined details, vibrant colors, and balanced composition.",
                        ImageUrl = "https://i.pinimg.com/564x/01/2d/cd/012dcd62de1e3ba6b356940ac83b0898.jpg",
                        Prompt = "Create a high-quality anime image featuring a youthful character with bright eyes and soft gradients. Use vibrant colors and perfect symmetry to enhance the character's appeal. The background should be detailed and colorful, complementing the character's design while maintaining a cohesive anime aesthetic.",
                        NegativePrompt = "ugly, extra fingers, mutated hands, extra limbs, long neck, watermark, text, logo, Rough lines, muted colors, photorealistic details, missing parts, extra limbs, or blurry outlines.",
                        //Prompt = "High-quality anime illustration, smooth shading, expressive eyes, perfect symmetry, vibrant color palette. The background is detailed yet balanced, complementing the character's design.",
                        //NegativePrompt = "blurred outlines, muted colors, photorealistic textures, distorted anatomy, excessive shading.",
                        Controlnets = "pose, canny",
                        Mode = InpaintMode.KeepStructure, 
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 26, 
                        GuidanceScale = 12.0,
                        Strength = 0.9,
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
                        //Prompt = "Editorial fashion portrait, striking pose, high-definition details, elegant outfit, perfect fit, confident expression, soft studio lighting. The background is clean and minimalistic, enhancing the subject.",
                        //NegativePrompt = "bad lighting, ill-fitting clothes, low-quality textures, unnatural expression, distorted proportions, cluttered background.",
                        Prompt = "Generate a high-fashion editorial image showcasing a model with radiant skin and a trendy outfit. The outfit should be perfectly fitted and elegant, with a striking pose that conveys confidence. Use soft studio lighting to enhance the model's features, and ensure the background is minimalistic to keep the focus on the fashion.",
                        NegativePrompt = "ugly, extra fingers, mutated hands, extra limbs, long neck, watermark, text, logo, messy hair, cheap fabrics, bad fit, wrinkles, dull colors, or distracting elements in the background.",
                        Controlnets = "canny, pose",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 26,
                        GuidanceScale = 9.0,
                        Strength = 0.8,
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
                        //Prompt = "Vibrant cartoon character, smooth shading, expressive eyes, bold outlines, dynamic pose. The background is colorful and playful, enhancing the lively personality.",
                        //NegativePrompt = "blurred lines, dull colors, photorealistic details, excessive shading, missing body parts, distorted proportions.",
                        Prompt = "Create a cartoon character with smooth features, big eyes, sharp outlines, and bright colors. The background should be playful and colorful. The character should have a lively expression and dynamic pose, capturing the essence of a classic cartoon style.",
                        NegativePrompt = "ugly, extra fingers, mutated hands, extra limbs, long neck, watermark, text, logo, rough textures, distorted, realistic anatomy, muted colors, blurred lines, missing body parts, unnatural proportions.",
                        Controlnets = "pose, canny",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 32,
                        GuidanceScale = 12,
                        Strength = 0.9,
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
                        //Prompt = "Fine art portrait, soft and balanced lighting, rich textures, warm color harmony. The subject exudes elegance and emotion, resembling classical masterpieces.",
                        //NegativePrompt = "oversaturated, artificial blur, harsh contrast, distorted proportions, lifeless expression, missing elements.",
                        Prompt = "Design a masterpiece portrait with elegant features and soft lighting. The colors should be balanced and glowing, creating a dreamy aesthetic. The subject should be posed gracefully, and the overall composition should evoke a sense of tranquility and beauty, reminiscent of classical art.",
                        NegativePrompt = "ugly, extra fingers, mutated hands, extra limbs, long neck, watermark, text, logo, harsh shadows, rough textures, dull colors, distortions, missing parts, or excessive blurring.",
                        Controlnets = "pose, depth",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 28,
                        GuidanceScale = 7.0,
                        Strength = 0.5,
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
                        //Prompt = "Classic vintage portrait, warm sepia tones, soft film-like contrast, subtle grain texture. The subject appears timeless with natural lighting and elegant styling.",
                        //NegativePrompt = "modern sharpness, digital artifacts, unnatural lighting, reflections, artificial blur, excessive grain.",
                        Prompt = "Create a classic vintage photo with a sepia tone and soft contrast. The image should evoke a nostalgic feel, featuring smooth skin and a natural expression. Use high-quality textures to enhance the vintage look, and ensure the background complements the overall aesthetic without being distracting.",
                        NegativePrompt = "ugly, extra fingers, mutated hands, extra limbs, long neck, watermark, text, logo, modern sharpness, digital artifacts, unnatural lighting, reflections, excessive grain, or aged skin.",
                        Controlnets = "pose, canny",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 25,
                        GuidanceScale = 7.5,
                        Strength = 0.6,
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
                        //Prompt = "Ethereal fantasy portrait, glowing atmosphere, intricate costume details, vibrant magical background. The subject radiates an enchanting aura with a dreamlike aesthetic.",
                        //NegativePrompt = "lifeless expression, dull tones, distorted anatomy, harsh shadows, CGI artifacts, unnatural blur.",
                        Prompt = "Generate an enchanting fantasy scene featuring a radiant character with an ethereal glow. Use soft lighting to create a magical aura, and focus on delicate and elegant details in the character's attire. The background should be a whimsical landscape, filled with vibrant colors and fantastical elements.",
                        NegativePrompt = "ugly, extra fingers, mutated hands, extra limbs, long neck, watermark, text, logo, harsh shadows, dull colors, distortions, missing parts, or lifeless expressions.",
                        Controlnets = "pose, depth",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 35,
                        GuidanceScale = 8.0,
                        Strength = 0.6,
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
                        Prompt = "Create a futuristic style image featuring a sleek model in a stylish outfit. Use sharp lighting to highlight the model's features and ensure perfect symmetry in the composition. The background should be modern and minimalistic, with vibrant colors that enhance the overall editorial feel of the shot.",
                        NegativePrompt = "ugly, extra fingers, mutated hands, extra limbs, long neck, watermark, text, logo, old-fashioned elements, dull colors, messy hair, asymmetry, wrinkles, or noisy backgrounds.",
                        //Prompt = "Sleek modern portrait, sharp lighting, high-definition textures, dynamic composition, stylish outfit. The background is minimalistic with a futuristic touch.",
                        //NegativePrompt = "low resolution, washed-out colors, bad lighting, asymmetry, distorted shapes, excessive blur.",
                        Controlnets = "canny, depth",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 25,
                        GuidanceScale = 7.5,
                        Strength = 0.5,
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
                        Prompt = "Design an outdoor photography scene with a model showcasing glowing skin and wind-blown hair. Use soft sunlight to create a fresh atmosphere, and incorporate vibrant colors in the natural surroundings. The setting should be peaceful and serene, capturing the beauty of nature.",
                        NegativePrompt = "ugly, extra fingers, mutated hands, extra limbs, long neck, watermark, text, logo, dry skin, dull tones, harsh shadows, distortions, cluttered backgrounds, or artificial lighting.",
                        //Prompt = "Natural outdoor portrait, warm sunlight, soft breeze, fresh atmosphere, glowing skin. The background features lush greenery or scenic landscapes, complementing the subject.",
                        //NegativePrompt = "harsh lighting, artificial colors, dull tones, cluttered background, distortion, missing details.",
                        Controlnets = "pose, depth",
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 30,
                        GuidanceScale = 8.0,
                        Strength = 0.6,
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
                        ImageUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/cyberpunk.webp19c68ca8-c452-4ce0-97cd-061d18a957dc?alt=media",
                        Prompt = "Futuristic cyberpunk aesthetic, neon lights, high-tech details, glowing cityscape, cybernetic enhancements.",
                        NegativePrompt = "low contrast, dull colors, historical elements, medieval designs, washed-out lighting.",
                        Controlnets = "depth, canny", 
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 30,
                        GuidanceScale = 7.5,
                        Strength = 0.5,   // 0.78 -> 0.5
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
                        ImageUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Watercolor.jpge2aa55c6-9cc2-4bcc-a6e5-7cdf5385c5b0?alt=media",
                        Prompt = "Soft and elegant watercolor painting, delicate brush strokes, smooth color blending, artistic expression.",
                        NegativePrompt = "pixelated edges, harsh shadows, digital artifacts, rigid outlines, unrealistic proportions.",
                        Controlnets = "pose, depth", // softedge,scribble -> pose, depth
                        Mode = null,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 25,
                        GuidanceScale = 8.0,  //6.8 -> 8.0
                        Strength = 0.6,     //0.72 -> 0.6
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
                        ImageUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/blackwhite.jpg5d9c012c-d715-4fb8-8970-e8b5b5af7ff2?alt=media",
                        Prompt = "Timeless black and white portrait, deep contrast, classic lighting, elegant composition.",
                        NegativePrompt = "grainy texture, washed-out details, weak contrast, digital noise, unbalanced lighting.",
                        Controlnets = "pose, canny", //depth, canny -> pose,canny
                        Mode = InpaintMode.KeepStructure,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 22,
                        GuidanceScale = 7.5, //6.2 -> 7.5
                        Strength = 0.6, //0.65 -> 0.6
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
                        ImageUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/oilpaint.jpgf9e0162a-2c9d-45e0-a70b-87d2f9f3c7b1?alt=media",
                        Prompt = "Rich and textured oil painting style, thick brush strokes, deep color saturation, artistic rendering.",
                        NegativePrompt = "flat colors, digital sharpness, pixelation, unrealistic lighting, blurred edges.",
                        Controlnets = "pose, canny", //scribble, softedge -> pose, canny
                        Mode = null,
                        NumImagesPerGen = 6,
                        Height = null,
                        Width = null,
                        NumInferenceSteps = 27,
                        GuidanceScale = 12.0, //7.3 -> 12
                        Strength = 0.9, //0.76 -> 0.9
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
                    new TypeSession { Id = 1, Name = "Photography", Description = "Photo session", Duration = 60, Price = 10000, isPrinting = false, AbleTakenNumber = 5, ForMobile = true, CreatedAt = DateTime.Parse("2025-03-14T05:45:36.4862448"), LastModified = DateTime.Parse("2025-03-14T05:45:36.4862448"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 2, Name = "Videography", Description = "Video session", Duration = 120, Price = 20000, isPrinting = true, AbleTakenNumber = 3, ForMobile = true, CreatedAt = DateTime.Parse("2025-03-14T05:45:36.4862448"), LastModified = DateTime.Parse("2025-03-14T05:45:36.4862448"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 3, Name = "Basic Package", Description = "Basic printing service package", Duration = 30, Price = 50000, isPrinting = true, AbleTakenNumber = 10, ForMobile = true, CreatedAt = DateTime.Parse("2025-03-28T04:24:45.6577221"), LastModified = DateTime.Parse("2025-03-28T04:24:45.6577221"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 4, Name = "Printing Consultation", Description = "Consultation for suitable printing type", Duration = 20, Price = 10000, isPrinting = false, AbleTakenNumber = 5, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:00:00"), LastModified = DateTime.Parse("2025-03-30T08:00:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 5, Name = "File Editing", Description = "Edit files before printing", Duration = 30, Price = 20000, isPrinting = false, AbleTakenNumber = 4, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:01:00"), LastModified = DateTime.Parse("2025-03-30T08:01:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 6, Name = "Poster Design", Description = "Design posters on demand", Duration = 45, Price = 40000, isPrinting = false, AbleTakenNumber = 3, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:02:00"), LastModified = DateTime.Parse("2025-03-30T08:02:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 7, Name = "Namecard Design", Description = "Create namecard designs", Duration = 25, Price = 15000, isPrinting = false, AbleTakenNumber = 6, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:03:00"), LastModified = DateTime.Parse("2025-03-30T08:03:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 8, Name = "Flyer Design", Description = "Design brochures and promotional flyers", Duration = 35, Price = 35000, isPrinting = false, AbleTakenNumber = 5, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:04:00"), LastModified = DateTime.Parse("2025-03-30T08:04:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 9, Name = "Layout Consultation", Description = "Consult on content layout for print", Duration = 20, Price = 10000, isPrinting = false, AbleTakenNumber = 7, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:05:00"), LastModified = DateTime.Parse("2025-03-30T08:05:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 10, Name = "Product Photography", Description = "Professional product photography", Duration = 60, Price = 25000, isPrinting = false, AbleTakenNumber = 2, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:06:00"), LastModified = DateTime.Parse("2025-03-30T08:06:00"), CreatedById = admin.Id, LastModifiedById = admin.Id }
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
                    new Wallet { Id = 1, CustomerId = customer.Id, Balance = 500000, IsLocked = false },
                    new Wallet { Id = 2, CustomerId = manager.Id, Balance = 350000, IsLocked = false },
                    new Wallet { Id = 3, CustomerId = staff.Id, Balance = 200000, IsLocked = false },
                    new Wallet { Id = 4, CustomerId = admin.Id, Balance = 100000, IsLocked = false }
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
                        Description = "Staff's Silver membership",
                        IsActive = true,
                        CreatedById = admin.Id,
                        LastModifiedById = admin.Id
                    },
                    new MembershipCard
                    {
                        Id = 4,
                        CustomerId = customer.Id,
                        LevelMemberShipId = 2, // Bronze
                        Points = 100,
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

            #region Seed data for TypeSessionProduct
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [TypeSessionProduct] ON");

            if (!context.TypeSessionProduct.Any())
            {
                var typeSessionProducts = new List<TypeSessionProduct>();
                int idCounter = 1;

                for (int typeSessionId = 1; typeSessionId <= 10; typeSessionId++)
                {
                    var typeSession = context.TypeSession.Find(typeSessionId);

                    for (int levelMembershipId = 1; levelMembershipId <= 5; levelMembershipId++)
                    {
                        var levelMembership = context.LevelMembership.Find(levelMembershipId);
                        typeSessionProducts.Add(new TypeSessionProduct
                        {
                            Id = idCounter++,
                            Name = $"{typeSession.Name}_{levelMembership.Name}",
                            TypeSessionId = typeSessionId,
                            LevelMembershipId = levelMembershipId,
                            ProductId = Normalize($"{typeSession.Name}_{levelMembership.Name}"),
                            CreatedById = admin.Id,
                            LastModifiedById = admin.Id
                        });
                    }

                    for (int couponId = 1; couponId <= 10; couponId++)
                    {
                        var coupon = context.Coupon.Find(couponId);
                        typeSessionProducts.Add(new TypeSessionProduct
                        {
                            Id = idCounter++,
                            Name = $"{typeSession.Name}_{coupon.Name}",
                            TypeSessionId = typeSessionId,
                            CouponId = couponId,
                            ProductId = Normalize($"{typeSession.Name}_{coupon.Name}"),
                            CreatedById = admin.Id,
                            LastModifiedById = admin.Id
                        });
                    }
                }

                context.TypeSessionProduct.AddRange(typeSessionProducts);
                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [TypeSessionProduct] OFF");
            #endregion

            #region Seed data for Order
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Order] ON");

            if (!context.Order.Any())
            {
                context.Order.AddRange(
            new Order { Id = 1, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 9, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 2, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 9, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 3, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 8, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 4, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 10, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 5, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 6, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 4, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 7, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 7, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 8, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 300000, Email = "customer@example.com", Phone = "123456789", TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 9, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 10, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 9, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 11, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 12, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 4, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 13, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 14, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 15, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 8, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 16, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 17, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 7, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 18, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 2, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 19, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 4, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 20, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 5, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 21, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 22, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 200000, Email = "customer@example.com", Phone = "123456789", TypeSessionId = 10, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 23, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 7, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 24, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer@example.com", Phone = "123456789", TypeSessionId = 3, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 25, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 8, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 26, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 27, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 4, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 28, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 9, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 29, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 30, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 10, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 31, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", TypeSessionId = 9, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 32, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 6, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 33, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 34, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 35, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 36, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 2, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 37, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 38, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 39, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 40, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 5, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 41, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 42, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 6, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 43, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 44, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 1, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 45, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 46, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 47, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 7, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 48, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 49, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 50, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 6, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 51, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 52, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 300000, Email = "customer@example.com", Phone = "123456789", TypeSessionId = 4, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 53, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 7, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 54, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 2, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 55, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 56, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 9, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 57, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 58, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 59, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 8, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 60, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 5, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 61, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 62, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 2, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 63, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 4, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 64, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 200000, Email = "customer@example.com", Phone = "123456789", TypeSessionId = 1, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 65, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 66, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 1, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 67, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 68, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 9, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 69, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 70, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 3, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 71, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 72, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 73, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 74, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 3, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 75, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 76, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 3, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 77, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 1, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 78, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer@example.com", Phone = "123456789", TypeSessionId = 1, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 79, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 9, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 80, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 10, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 81, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 82, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 2, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 83, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 84, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 200000, Email = "customer@example.com", Phone = "123456789", TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 85, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 7, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 86, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 87, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 88, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 89, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 90, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 1, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 91, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 1, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 92, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 93, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 94, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 9, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 95, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 8, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 96, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 5, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 97, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 98, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 9, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 99, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 100, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 101, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 102, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 6, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 103, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 104, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 1, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 105, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 106, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 107, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 108, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 10, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 109, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 1, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 110, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 2, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 111, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 112, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 113, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 9, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 114, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 1, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 115, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 7, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 116, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 117, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 118, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 1, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 119, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 120, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 6, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 121, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 122, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 10, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 123, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 124, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 4, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 125, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 7, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 126, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 127, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 128, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 2, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 129, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 130, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 10, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 131, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 132, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 4, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 133, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 7, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 134, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 9, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 135, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 9, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 136, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 3, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 137, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 138, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 139, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 140, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 141, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 142, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 3, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 143, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 4, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 144, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 5, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 145, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 8, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 146, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 5, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 147, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", TypeSessionId = 7, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 148, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 6, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 149, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 150, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 6, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 151, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 152, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 10, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 153, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 2, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 154, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 6, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 155, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 156, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 157, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Failed, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", TypeSessionId = 9, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 158, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 1, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 159, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 160, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 3, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 161, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 162, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 163, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 164, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 165, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 166, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 167, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 9, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 168, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 169, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 170, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 6, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 171, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", TypeSessionId = 4, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 172, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 5, TypeSessionId = 7, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 173, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 6, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 174, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 4, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 175, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 176, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 3, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 177, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 4, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 178, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 5, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 179, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 180, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 300000, Email = "customer@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 5, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 181, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Cancelled, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 182, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer@example.com", Phone = "123456789", TypeSessionId = 4, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 183, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 184, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 7, TypeSessionId = 5, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 185, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 10, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 186, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 10, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 187, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 188, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 5, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 189, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", CouponId = 1, TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 190, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Processing, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 2, TypeSessionId = 10, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 191, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 100000, Email = "customer2@example.com", Phone = "123456789", TypeSessionId = 5, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 192, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 6, TypeSessionId = 6, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 193, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Pending, Amount = 200000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 4, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 194, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Cancelled, Amount = 200000, Email = "customer@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 8, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 195, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 196, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Pending, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 5, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 197, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 500000, Email = "customer2@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 7, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 198, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 8, TypeSessionId = 3, CreatedById = customer.Id, LastModifiedById = customer.Id },
            new Order { Id = 199, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Processing, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 3, TypeSessionId = 3, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 200, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Failed, Amount = 100000, Email = "customer@example.com", Phone = "123456789", CouponId = 4, TypeSessionId = 2, CreatedById = customer.Id, LastModifiedById = customer.Id }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Order] OFF");
            #endregion

            #region Seed data for DepositProduct
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [DepositProduct] ON");
            if (!context.DepositProduct.Any())
            {
                context.DepositProduct.AddRange(
                new DepositProduct { Id = 1, Name = "Nạp 20 nghìn", Description = "Giá 20 nghìn kèm khuyến mãi 10%", AmountAdd = 2000, ProductId = "product_deposit_1", Price = 20000 },
                new DepositProduct { Id = 2, Name = "Nạp 15 nghìn", Description = "Giá 15 nghìn kèm khuyến mãi 10%", AmountAdd = 1500, ProductId = "product_deposit_2", Price = 15000 },
                new DepositProduct { Id = 3, Name = "Nạp 12 nghìn", Description = "Giá 12 nghìn kèm khuyến mãi 10%", AmountAdd = 1200, ProductId = "product_deposit_3", Price = 12000 },
                new DepositProduct { Id = 4, Name = "Nạp 10 nghìn", Description = "Giá 10 nghìn", AmountAdd = 0, ProductId = "product_deposit_4", Price = 10000 },
                new DepositProduct { Id = 5, Name = "Nạp 17 nghìn", Description = "Giá 17 nghìn kèm khuyến mãi 10%", AmountAdd = 1700, ProductId = "product_deposit_5", Price = 17000 }
            );
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [DepositProduct] OFF");
            #endregion

            #region Seed data for Deposit
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Deposit] ON");

            if (!context.Deposit.Any())
            {
                context.Deposit.AddRange(
                    new Deposit
                    {
                        Id = 1,
                        WalletId = 1,
                        Amount = 20000,
                        Status = DepositStatus.Success,
                        DepositProductId = 1,
                    },
                    new Deposit
                    {
                        Id = 2,
                        WalletId = 1,
                        Amount = 10000,
                        Status = DepositStatus.Success,
                        DepositProductId = 2,
                    },
                    new Deposit
                    {
                        Id = 3,
                        WalletId = 1,
                        Amount = 5000,
                        Status = DepositStatus.Success,
                        DepositProductId = 3,
                    },
                    new Deposit
                    {
                        Id = 4,
                        WalletId = 1,
                        Amount = 10000,
                        Status = DepositStatus.Success,
                        DepositProductId = 4,
                    },
                    new Deposit
                    {
                        Id = 5,
                        WalletId = 2,
                        Amount = 20000,
                        Status = DepositStatus.Success,
                        DepositProductId = 1,
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

            #region Seed data for Session
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Session] ON");

            if (!context.Session.Any())
            {
                context.Session.AddRange(
            new Session { Id = 1, Code = "SESSION", Expired = DateTime.Now.AddDays(60), IsActive = true, OrderId = 1 },
            new Session { Id = 2, IsActive = true, OrderId = 2 },
            new Session { Id = 3, IsActive = true, OrderId = 3 },
            new Session { Id = 4, IsActive = true, OrderId = 4 },
            new Session { Id = 5, IsActive = true, OrderId = 5 },
            new Session { Id = 6, IsActive = true, OrderId = 6 },
            new Session { Id = 7, IsActive = true, OrderId = 7 },
            new Session { Id = 8, IsActive = true, OrderId = 8 },
            new Session { Id = 9, IsActive = true, OrderId = 9 },
            new Session { Id = 10, IsActive = true, OrderId = 10 },
            new Session { Id = 11, IsActive = true, OrderId = 11 },
            new Session { Id = 12, IsActive = true, OrderId = 12 },
            new Session { Id = 13, IsActive = true, OrderId = 13 },
            new Session { Id = 14, IsActive = true, OrderId = 14 },
            new Session { Id = 15, IsActive = true, OrderId = 15 },
            new Session { Id = 16, IsActive = true, OrderId = 16 },
            new Session { Id = 17, IsActive = true, OrderId = 17 },
            new Session { Id = 18, IsActive = true, OrderId = 18 },
            new Session { Id = 19, IsActive = true, OrderId = 19 },
            new Session { Id = 20, IsActive = true, OrderId = 20 },
            new Session { Id = 21, IsActive = true, OrderId = 21 },
            new Session { Id = 22, IsActive = true, OrderId = 22 },
            new Session { Id = 23, IsActive = true, OrderId = 23 },
            new Session { Id = 24, IsActive = true, OrderId = 24 },
            new Session { Id = 25, IsActive = true, OrderId = 25 },
            new Session { Id = 26, IsActive = true, OrderId = 26 },
            new Session { Id = 27, IsActive = true, OrderId = 27 },
            new Session { Id = 28, IsActive = true, OrderId = 28 },
            new Session { Id = 29, IsActive = true, OrderId = 29 },
            new Session { Id = 30, IsActive = true, OrderId = 30 },
            new Session { Id = 31, IsActive = true, OrderId = 31 },
            new Session { Id = 32, IsActive = true, OrderId = 32 },
            new Session { Id = 33, IsActive = true, OrderId = 33 },
            new Session { Id = 34, IsActive = true, OrderId = 34 },
            new Session { Id = 35, IsActive = true, OrderId = 35 },
            new Session { Id = 36, IsActive = true, OrderId = 36 },
            new Session { Id = 37, IsActive = true, OrderId = 37 },
            new Session { Id = 38, IsActive = true, OrderId = 38 },
            new Session { Id = 39, IsActive = true, OrderId = 39 },
            new Session { Id = 40, IsActive = true, OrderId = 40 },
            new Session { Id = 41, IsActive = true, OrderId = 41 },
            new Session { Id = 42, IsActive = true, OrderId = 42 },
            new Session { Id = 43, IsActive = true, OrderId = 43 },
            new Session { Id = 44, IsActive = true, OrderId = 44 },
            new Session { Id = 45, IsActive = true, OrderId = 45 },
            new Session { Id = 46, IsActive = true, OrderId = 46 },
            new Session { Id = 47, IsActive = true, OrderId = 47 },
            new Session { Id = 48, IsActive = true, OrderId = 48 },
            new Session { Id = 49, IsActive = true, OrderId = 49 },
            new Session { Id = 50, IsActive = true, OrderId = 50 },
            new Session { Id = 51, IsActive = true, OrderId = 51 },
            new Session { Id = 52, IsActive = true, OrderId = 52 },
            new Session { Id = 53, IsActive = true, OrderId = 53 },
            new Session { Id = 54, IsActive = true, OrderId = 54 },
            new Session { Id = 55, IsActive = true, OrderId = 55 },
            new Session { Id = 56, IsActive = true, OrderId = 56 },
            new Session { Id = 57, IsActive = true, OrderId = 57 },
            new Session { Id = 58, IsActive = true, OrderId = 58 },
            new Session { Id = 59, IsActive = true, OrderId = 59 },
            new Session { Id = 60, IsActive = true, OrderId = 60 },
            new Session { Id = 61, IsActive = true, OrderId = 61 },
            new Session { Id = 62, IsActive = true, OrderId = 62 },
            new Session { Id = 63, IsActive = true, OrderId = 63 },
            new Session { Id = 64, IsActive = true, OrderId = 64 },
            new Session { Id = 65, IsActive = true, OrderId = 65 },
            new Session { Id = 66, IsActive = true, OrderId = 66 },
            new Session { Id = 67, IsActive = true, OrderId = 67 },
            new Session { Id = 68, IsActive = true, OrderId = 68 },
            new Session { Id = 69, IsActive = true, OrderId = 69 },
            new Session { Id = 70, IsActive = true, OrderId = 70 },
            new Session { Id = 71, IsActive = true, OrderId = 71 },
            new Session { Id = 72, IsActive = true, OrderId = 72 },
            new Session { Id = 73, IsActive = true, OrderId = 73 },
            new Session { Id = 74, IsActive = true, OrderId = 74 },
            new Session { Id = 75, IsActive = true, OrderId = 75 },
            new Session { Id = 76, IsActive = true, OrderId = 76 },
            new Session { Id = 77, IsActive = true, OrderId = 77 },
            new Session { Id = 78, IsActive = true, OrderId = 78 },
            new Session { Id = 79, IsActive = true, OrderId = 79 },
            new Session { Id = 80, IsActive = true, OrderId = 80 },
            new Session { Id = 81, IsActive = true, OrderId = 81 },
            new Session { Id = 82, IsActive = true, OrderId = 82 },
            new Session { Id = 83, IsActive = true, OrderId = 83 },
            new Session { Id = 84, IsActive = true, OrderId = 84 },
            new Session { Id = 85, IsActive = true, OrderId = 85 },
            new Session { Id = 86, IsActive = true, OrderId = 86 },
            new Session { Id = 87, IsActive = true, OrderId = 87 },
            new Session { Id = 88, IsActive = true, OrderId = 88 },
            new Session { Id = 89, IsActive = true, OrderId = 89 },
            new Session { Id = 90, IsActive = true, OrderId = 90 },
            new Session { Id = 91, IsActive = true, OrderId = 91 },
            new Session { Id = 92, IsActive = true, OrderId = 92 },
            new Session { Id = 93, IsActive = true, OrderId = 93 },
            new Session { Id = 94, IsActive = true, OrderId = 94 },
            new Session { Id = 95, IsActive = true, OrderId = 95 },
            new Session { Id = 96, IsActive = true, OrderId = 96 },
            new Session { Id = 97, IsActive = true, OrderId = 97 },
            new Session { Id = 98, IsActive = true, OrderId = 98 },
            new Session { Id = 99, IsActive = true, OrderId = 99 },
            new Session { Id = 100, IsActive = true, OrderId = 100 },
            new Session { Id = 101, IsActive = true, OrderId = 101 },
            new Session { Id = 102, IsActive = true, OrderId = 102 },
            new Session { Id = 103, IsActive = true, OrderId = 103 },
            new Session { Id = 104, IsActive = true, OrderId = 104 },
            new Session { Id = 105, IsActive = true, OrderId = 105 },
            new Session { Id = 106, IsActive = true, OrderId = 106 },
            new Session { Id = 107, IsActive = true, OrderId = 107 },
            new Session { Id = 108, IsActive = true, OrderId = 108 },
            new Session { Id = 109, IsActive = true, OrderId = 109 },
            new Session { Id = 110, IsActive = true, OrderId = 110 },
            new Session { Id = 111, IsActive = true, OrderId = 111 },
            new Session { Id = 112, IsActive = true, OrderId = 112 },
            new Session { Id = 113, IsActive = true, OrderId = 113 },
            new Session { Id = 114, IsActive = true, OrderId = 114 },
            new Session { Id = 115, IsActive = true, OrderId = 115 },
            new Session { Id = 116, IsActive = true, OrderId = 116 },
            new Session { Id = 117, IsActive = true, OrderId = 117 },
            new Session { Id = 118, IsActive = true, OrderId = 118 },
            new Session { Id = 119, IsActive = true, OrderId = 119 },
            new Session { Id = 120, IsActive = true, OrderId = 120 },
            new Session { Id = 121, IsActive = true, OrderId = 121 },
            new Session { Id = 122, IsActive = true, OrderId = 122 },
            new Session { Id = 123, IsActive = true, OrderId = 123 },
            new Session { Id = 124, IsActive = true, OrderId = 124 },
            new Session { Id = 125, IsActive = true, OrderId = 125 },
            new Session { Id = 126, IsActive = true, OrderId = 126 },
            new Session { Id = 127, IsActive = true, OrderId = 127 },
            new Session { Id = 128, IsActive = true, OrderId = 128 },
            new Session { Id = 129, IsActive = true, OrderId = 129 },
            new Session { Id = 130, IsActive = true, OrderId = 130 },
            new Session { Id = 131, IsActive = true, OrderId = 131 },
            new Session { Id = 132, IsActive = true, OrderId = 132 },
            new Session { Id = 133, IsActive = true, OrderId = 133 },
            new Session { Id = 134, IsActive = true, OrderId = 134 },
            new Session { Id = 135, IsActive = true, OrderId = 135 },
            new Session { Id = 136, IsActive = true, OrderId = 136 },
            new Session { Id = 137, IsActive = true, OrderId = 137 },
            new Session { Id = 138, IsActive = true, OrderId = 138 },
            new Session { Id = 139, IsActive = true, OrderId = 139 },
            new Session { Id = 140, IsActive = true, OrderId = 140 },
            new Session { Id = 141, IsActive = true, OrderId = 141 },
            new Session { Id = 142, IsActive = true, OrderId = 142 },
            new Session { Id = 143, IsActive = true, OrderId = 143 },
            new Session { Id = 144, IsActive = true, OrderId = 144 },
            new Session { Id = 145, IsActive = true, OrderId = 145 },
            new Session { Id = 146, IsActive = true, OrderId = 146 },
            new Session { Id = 147, IsActive = true, OrderId = 147 },
            new Session { Id = 148, IsActive = true, OrderId = 148 },
            new Session { Id = 149, IsActive = true, OrderId = 149 },
            new Session { Id = 150, IsActive = true, OrderId = 150 },
            new Session { Id = 151, IsActive = true, OrderId = 151 },
            new Session { Id = 152, IsActive = true, OrderId = 152 },
            new Session { Id = 153, IsActive = true, OrderId = 153 },
            new Session { Id = 154, IsActive = true, OrderId = 154 },
            new Session { Id = 155, IsActive = true, OrderId = 155 },
            new Session { Id = 156, IsActive = true, OrderId = 156 },
            new Session { Id = 157, IsActive = true, OrderId = 157 },
            new Session { Id = 158, IsActive = true, OrderId = 158 },
            new Session { Id = 159, IsActive = true, OrderId = 159 },
            new Session { Id = 160, IsActive = true, OrderId = 160 },
            new Session { Id = 161, IsActive = true, OrderId = 161 },
            new Session { Id = 162, IsActive = true, OrderId = 162 },
            new Session { Id = 163, IsActive = true, OrderId = 163 },
            new Session { Id = 164, IsActive = true, OrderId = 164 },
            new Session { Id = 165, IsActive = true, OrderId = 165 },
            new Session { Id = 166, IsActive = true, OrderId = 166 },
            new Session { Id = 167, IsActive = true, OrderId = 167 },
            new Session { Id = 168, IsActive = true, OrderId = 168 },
            new Session { Id = 169, IsActive = true, OrderId = 169 },
            new Session { Id = 170, IsActive = true, OrderId = 170 },
            new Session { Id = 171, IsActive = true, OrderId = 171 },
            new Session { Id = 172, IsActive = true, OrderId = 172 },
            new Session { Id = 173, IsActive = true, OrderId = 173 },
            new Session { Id = 174, IsActive = true, OrderId = 174 },
            new Session { Id = 175, IsActive = true, OrderId = 175 },
            new Session { Id = 176, IsActive = true, OrderId = 176 },
            new Session { Id = 177, IsActive = true, OrderId = 177 },
            new Session { Id = 178, IsActive = true, OrderId = 178 },
            new Session { Id = 179, IsActive = true, OrderId = 179 },
            new Session { Id = 180, IsActive = true, OrderId = 180 },
            new Session { Id = 181, IsActive = true, OrderId = 181 },
            new Session { Id = 182, IsActive = true, OrderId = 182 },
            new Session { Id = 183, IsActive = true, OrderId = 183 },
            new Session { Id = 184, IsActive = true, OrderId = 184 },
            new Session { Id = 185, IsActive = true, OrderId = 185 },
            new Session { Id = 186, IsActive = true, OrderId = 186 },
            new Session { Id = 187, IsActive = true, OrderId = 187 },
            new Session { Id = 188, IsActive = true, OrderId = 188 },
            new Session { Id = 189, IsActive = true, OrderId = 189 },
            new Session { Id = 190, IsActive = true, OrderId = 190 },
            new Session { Id = 191, IsActive = true, OrderId = 191 },
            new Session { Id = 192, IsActive = true, OrderId = 192 },
            new Session { Id = 193, IsActive = true, OrderId = 193 },
            new Session { Id = 194, IsActive = true, OrderId = 194 },
            new Session { Id = 195, IsActive = true, OrderId = 195 },
            new Session { Id = 196, IsActive = true, OrderId = 196 },
            new Session { Id = 197, IsActive = true, OrderId = 197 },
            new Session { Id = 198, IsActive = true, OrderId = 198 },
            new Session { Id = 199, IsActive = true, OrderId = 199 },
            new Session { Id = 200, IsActive = true, OrderId = 200 }
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
            new PhotoHistory { Id = 1, CustomerId = customer.Id, BoothId = 1, SessionId = 1 },
            new PhotoHistory { Id = 2, CustomerId = customer2.Id, BoothId = 2, SessionId = 2 },
            new PhotoHistory { Id = 3, CustomerId = customer.Id, BoothId = 3, SessionId = 3 },
            new PhotoHistory { Id = 4, CustomerId = customer2.Id, BoothId = 4, SessionId = 4 },
            new PhotoHistory { Id = 5, CustomerId = customer.Id, BoothId = 5, SessionId = 5 },
            new PhotoHistory { Id = 6, CustomerId = customer2.Id, BoothId = 6, SessionId = 6 },
            new PhotoHistory { Id = 7, CustomerId = customer.Id, BoothId = 7, SessionId = 7 },
            new PhotoHistory { Id = 8, CustomerId = customer2.Id, BoothId = 8, SessionId = 8 },
            new PhotoHistory { Id = 9, CustomerId = customer.Id, BoothId = 9, SessionId = 9 },
            new PhotoHistory { Id = 10, CustomerId = customer2.Id, BoothId = 10, SessionId = 10 },
            new PhotoHistory { Id = 11, CustomerId = customer.Id, BoothId = 11, SessionId = 11 },
            new PhotoHistory { Id = 12, CustomerId = customer2.Id, BoothId = 12, SessionId = 12 },
            new PhotoHistory { Id = 13, CustomerId = customer.Id, BoothId = 13, SessionId = 13 },
            new PhotoHistory { Id = 14, CustomerId = customer2.Id, BoothId = 14, SessionId = 14 },
            new PhotoHistory { Id = 15, CustomerId = customer.Id, BoothId = 15, SessionId = 15 },
            new PhotoHistory { Id = 16, CustomerId = customer2.Id, BoothId = 16, SessionId = 16 },
            new PhotoHistory { Id = 17, CustomerId = customer.Id, BoothId = 17, SessionId = 17 },
            new PhotoHistory { Id = 18, CustomerId = customer2.Id, BoothId = 18, SessionId = 18 },
            new PhotoHistory { Id = 19, CustomerId = customer.Id, BoothId = 19, SessionId = 19 },
            new PhotoHistory { Id = 20, CustomerId = customer2.Id, BoothId = 20, SessionId = 20 },
            new PhotoHistory { Id = 21, CustomerId = customer.Id, BoothId = 1, SessionId = 21 },
            new PhotoHistory { Id = 22, CustomerId = customer2.Id, BoothId = 2, SessionId = 22 },
            new PhotoHistory { Id = 23, CustomerId = customer.Id, BoothId = 3, SessionId = 23 },
            new PhotoHistory { Id = 24, CustomerId = customer2.Id, BoothId = 4, SessionId = 24 },
            new PhotoHistory { Id = 25, CustomerId = customer.Id, BoothId = 5, SessionId = 25 },
            new PhotoHistory { Id = 26, CustomerId = customer2.Id, BoothId = 6, SessionId = 26 },
            new PhotoHistory { Id = 27, CustomerId = customer.Id, BoothId = 7, SessionId = 27 },
            new PhotoHistory { Id = 28, CustomerId = customer2.Id, BoothId = 8, SessionId = 28 },
            new PhotoHistory { Id = 29, CustomerId = customer.Id, BoothId = 9, SessionId = 29 },
            new PhotoHistory { Id = 30, CustomerId = customer2.Id, BoothId = 10, SessionId = 30 },
            new PhotoHistory { Id = 31, CustomerId = customer.Id, BoothId = 11, SessionId = 31 },
            new PhotoHistory { Id = 32, CustomerId = customer2.Id, BoothId = 12, SessionId = 32 },
            new PhotoHistory { Id = 33, CustomerId = customer.Id, BoothId = 13, SessionId = 33 },
            new PhotoHistory { Id = 34, CustomerId = customer2.Id, BoothId = 14, SessionId = 34 },
            new PhotoHistory { Id = 35, CustomerId = customer.Id, BoothId = 15, SessionId = 35 },
            new PhotoHistory { Id = 36, CustomerId = customer2.Id, BoothId = 16, SessionId = 36 },
            new PhotoHistory { Id = 37, CustomerId = customer.Id, BoothId = 17, SessionId = 37 },
            new PhotoHistory { Id = 38, CustomerId = customer2.Id, BoothId = 18, SessionId = 38 },
            new PhotoHistory { Id = 39, CustomerId = customer.Id, BoothId = 19, SessionId = 39 },
            new PhotoHistory { Id = 40, CustomerId = customer2.Id, BoothId = 20, SessionId = 40 },
            new PhotoHistory { Id = 41, CustomerId = customer.Id, BoothId = 1, SessionId = 41 },
            new PhotoHistory { Id = 42, CustomerId = customer2.Id, BoothId = 2, SessionId = 42 },
            new PhotoHistory { Id = 43, CustomerId = customer.Id, BoothId = 3, SessionId = 43 },
            new PhotoHistory { Id = 44, CustomerId = customer2.Id, BoothId = 4, SessionId = 44 },
            new PhotoHistory { Id = 45, CustomerId = customer.Id, BoothId = 5, SessionId = 45 },
            new PhotoHistory { Id = 46, CustomerId = customer2.Id, BoothId = 6, SessionId = 46 },
            new PhotoHistory { Id = 47, CustomerId = customer.Id, BoothId = 7, SessionId = 47 },
            new PhotoHistory { Id = 48, CustomerId = customer2.Id, BoothId = 8, SessionId = 48 },
            new PhotoHistory { Id = 49, CustomerId = customer.Id, BoothId = 9, SessionId = 49 },
            new PhotoHistory { Id = 50, CustomerId = customer2.Id, BoothId = 10, SessionId = 50 },
            new PhotoHistory { Id = 51, CustomerId = customer.Id, BoothId = 11, SessionId = 51 },
            new PhotoHistory { Id = 52, CustomerId = customer2.Id, BoothId = 12, SessionId = 52 },
            new PhotoHistory { Id = 53, CustomerId = customer.Id, BoothId = 13, SessionId = 53 },
            new PhotoHistory { Id = 54, CustomerId = customer2.Id, BoothId = 14, SessionId = 54 },
            new PhotoHistory { Id = 55, CustomerId = customer.Id, BoothId = 15, SessionId = 55 },
            new PhotoHistory { Id = 56, CustomerId = customer2.Id, BoothId = 16, SessionId = 56 },
            new PhotoHistory { Id = 57, CustomerId = customer.Id, BoothId = 17, SessionId = 57 },
            new PhotoHistory { Id = 58, CustomerId = customer2.Id, BoothId = 18, SessionId = 58 },
            new PhotoHistory { Id = 59, CustomerId = customer.Id, BoothId = 19, SessionId = 59 },
            new PhotoHistory { Id = 60, CustomerId = customer2.Id, BoothId = 20, SessionId = 60 },
            new PhotoHistory { Id = 61, CustomerId = customer.Id, BoothId = 1, SessionId = 61 },
            new PhotoHistory { Id = 62, CustomerId = customer2.Id, BoothId = 2, SessionId = 62 },
            new PhotoHistory { Id = 63, CustomerId = customer.Id, BoothId = 3, SessionId = 63 },
            new PhotoHistory { Id = 64, CustomerId = customer2.Id, BoothId = 4, SessionId = 64 },
            new PhotoHistory { Id = 65, CustomerId = customer.Id, BoothId = 5, SessionId = 65 },
            new PhotoHistory { Id = 66, CustomerId = customer2.Id, BoothId = 6, SessionId = 66 },
            new PhotoHistory { Id = 67, CustomerId = customer.Id, BoothId = 7, SessionId = 67 },
            new PhotoHistory { Id = 68, CustomerId = customer2.Id, BoothId = 8, SessionId = 68 },
            new PhotoHistory { Id = 69, CustomerId = customer.Id, BoothId = 9, SessionId = 69 },
            new PhotoHistory { Id = 70, CustomerId = customer2.Id, BoothId = 10, SessionId = 70 },
            new PhotoHistory { Id = 71, CustomerId = customer.Id, BoothId = 11, SessionId = 71 },
            new PhotoHistory { Id = 72, CustomerId = customer2.Id, BoothId = 12, SessionId = 72 },
            new PhotoHistory { Id = 73, CustomerId = customer.Id, BoothId = 13, SessionId = 73 },
            new PhotoHistory { Id = 74, CustomerId = customer2.Id, BoothId = 14, SessionId = 74 },
            new PhotoHistory { Id = 75, CustomerId = customer.Id, BoothId = 15, SessionId = 75 },
            new PhotoHistory { Id = 76, CustomerId = customer2.Id, BoothId = 16, SessionId = 76 },
            new PhotoHistory { Id = 77, CustomerId = customer.Id, BoothId = 17, SessionId = 77 },
            new PhotoHistory { Id = 78, CustomerId = customer2.Id, BoothId = 18, SessionId = 78 },
            new PhotoHistory { Id = 79, CustomerId = customer.Id, BoothId = 19, SessionId = 79 },
            new PhotoHistory { Id = 80, CustomerId = customer2.Id, BoothId = 20, SessionId = 80 },
            new PhotoHistory { Id = 81, CustomerId = customer.Id, BoothId = 1, SessionId = 81 },
            new PhotoHistory { Id = 82, CustomerId = customer2.Id, BoothId = 2, SessionId = 82 },
            new PhotoHistory { Id = 83, CustomerId = customer.Id, BoothId = 3, SessionId = 83 },
            new PhotoHistory { Id = 84, CustomerId = customer2.Id, BoothId = 4, SessionId = 84 },
            new PhotoHistory { Id = 85, CustomerId = customer.Id, BoothId = 5, SessionId = 85 },
            new PhotoHistory { Id = 86, CustomerId = customer2.Id, BoothId = 6, SessionId = 86 },
            new PhotoHistory { Id = 87, CustomerId = customer.Id, BoothId = 7, SessionId = 87 },
            new PhotoHistory { Id = 88, CustomerId = customer2.Id, BoothId = 8, SessionId = 88 },
            new PhotoHistory { Id = 89, CustomerId = customer.Id, BoothId = 9, SessionId = 89 },
            new PhotoHistory { Id = 90, CustomerId = customer2.Id, BoothId = 10, SessionId = 90 },
            new PhotoHistory { Id = 91, CustomerId = customer.Id, BoothId = 11, SessionId = 91 },
            new PhotoHistory { Id = 92, CustomerId = customer2.Id, BoothId = 12, SessionId = 92 },
            new PhotoHistory { Id = 93, CustomerId = customer.Id, BoothId = 13, SessionId = 93 },
            new PhotoHistory { Id = 94, CustomerId = customer2.Id, BoothId = 14, SessionId = 94 },
            new PhotoHistory { Id = 95, CustomerId = customer.Id, BoothId = 15, SessionId = 95 },
            new PhotoHistory { Id = 96, CustomerId = customer2.Id, BoothId = 16, SessionId = 96 },
            new PhotoHistory { Id = 97, CustomerId = customer.Id, BoothId = 17, SessionId = 97 },
            new PhotoHistory { Id = 98, CustomerId = customer2.Id, BoothId = 18, SessionId = 98 },
            new PhotoHistory { Id = 99, CustomerId = customer.Id, BoothId = 19, SessionId = 99 },
            new PhotoHistory { Id = 100, CustomerId = customer2.Id, BoothId = 20, SessionId = 100 },
            new PhotoHistory { Id = 101, CustomerId = customer.Id, BoothId = 1, SessionId = 101 },
            new PhotoHistory { Id = 102, CustomerId = customer2.Id, BoothId = 2, SessionId = 102 },
            new PhotoHistory { Id = 103, CustomerId = customer.Id, BoothId = 3, SessionId = 103 },
            new PhotoHistory { Id = 104, CustomerId = customer2.Id, BoothId = 4, SessionId = 104 },
            new PhotoHistory { Id = 105, CustomerId = customer.Id, BoothId = 5, SessionId = 105 },
            new PhotoHistory { Id = 106, CustomerId = customer2.Id, BoothId = 6, SessionId = 106 },
            new PhotoHistory { Id = 107, CustomerId = customer.Id, BoothId = 7, SessionId = 107 },
            new PhotoHistory { Id = 108, CustomerId = customer2.Id, BoothId = 8, SessionId = 108 },
            new PhotoHistory { Id = 109, CustomerId = customer.Id, BoothId = 9, SessionId = 109 },
            new PhotoHistory { Id = 110, CustomerId = customer2.Id, BoothId = 10, SessionId = 110 },
            new PhotoHistory { Id = 111, CustomerId = customer.Id, BoothId = 11, SessionId = 111 },
            new PhotoHistory { Id = 112, CustomerId = customer2.Id, BoothId = 12, SessionId = 112 },
            new PhotoHistory { Id = 113, CustomerId = customer.Id, BoothId = 13, SessionId = 113 },
            new PhotoHistory { Id = 114, CustomerId = customer2.Id, BoothId = 14, SessionId = 114 },
            new PhotoHistory { Id = 115, CustomerId = customer.Id, BoothId = 15, SessionId = 115 },
            new PhotoHistory { Id = 116, CustomerId = customer2.Id, BoothId = 16, SessionId = 116 },
            new PhotoHistory { Id = 117, CustomerId = customer.Id, BoothId = 17, SessionId = 117 },
            new PhotoHistory { Id = 118, CustomerId = customer2.Id, BoothId = 18, SessionId = 118 },
            new PhotoHistory { Id = 119, CustomerId = customer.Id, BoothId = 19, SessionId = 119 },
            new PhotoHistory { Id = 120, CustomerId = customer2.Id, BoothId = 20, SessionId = 120 },
            new PhotoHistory { Id = 121, CustomerId = customer.Id, BoothId = 1, SessionId = 121 },
            new PhotoHistory { Id = 122, CustomerId = customer2.Id, BoothId = 2, SessionId = 122 },
            new PhotoHistory { Id = 123, CustomerId = customer.Id, BoothId = 3, SessionId = 123 },
            new PhotoHistory { Id = 124, CustomerId = customer2.Id, BoothId = 4, SessionId = 124 },
            new PhotoHistory { Id = 125, CustomerId = customer.Id, BoothId = 5, SessionId = 125 },
            new PhotoHistory { Id = 126, CustomerId = customer2.Id, BoothId = 6, SessionId = 126 },
            new PhotoHistory { Id = 127, CustomerId = customer.Id, BoothId = 7, SessionId = 127 },
            new PhotoHistory { Id = 128, CustomerId = customer2.Id, BoothId = 8, SessionId = 128 },
            new PhotoHistory { Id = 129, CustomerId = customer.Id, BoothId = 9, SessionId = 129 },
            new PhotoHistory { Id = 130, CustomerId = customer2.Id, BoothId = 10, SessionId = 130 },
            new PhotoHistory { Id = 131, CustomerId = customer.Id, BoothId = 11, SessionId = 131 },
            new PhotoHistory { Id = 132, CustomerId = customer2.Id, BoothId = 12, SessionId = 132 },
            new PhotoHistory { Id = 133, CustomerId = customer.Id, BoothId = 13, SessionId = 133 },
            new PhotoHistory { Id = 134, CustomerId = customer2.Id, BoothId = 14, SessionId = 134 },
            new PhotoHistory { Id = 135, CustomerId = customer.Id, BoothId = 15, SessionId = 135 },
            new PhotoHistory { Id = 136, CustomerId = customer2.Id, BoothId = 16, SessionId = 136 },
            new PhotoHistory { Id = 137, CustomerId = customer.Id, BoothId = 17, SessionId = 137 },
            new PhotoHistory { Id = 138, CustomerId = customer2.Id, BoothId = 18, SessionId = 138 },
            new PhotoHistory { Id = 139, CustomerId = customer.Id, BoothId = 19, SessionId = 139 },
            new PhotoHistory { Id = 140, CustomerId = customer2.Id, BoothId = 20, SessionId = 140 },
            new PhotoHistory { Id = 141, CustomerId = customer.Id, BoothId = 1, SessionId = 141 },
            new PhotoHistory { Id = 142, CustomerId = customer2.Id, BoothId = 2, SessionId = 142 },
            new PhotoHistory { Id = 143, CustomerId = customer.Id, BoothId = 3, SessionId = 143 },
            new PhotoHistory { Id = 144, CustomerId = customer2.Id, BoothId = 4, SessionId = 144 },
            new PhotoHistory { Id = 145, CustomerId = customer.Id, BoothId = 5, SessionId = 145 },
            new PhotoHistory { Id = 146, CustomerId = customer2.Id, BoothId = 6, SessionId = 146 },
            new PhotoHistory { Id = 147, CustomerId = customer.Id, BoothId = 7, SessionId = 147 },
            new PhotoHistory { Id = 148, CustomerId = customer2.Id, BoothId = 8, SessionId = 148 },
            new PhotoHistory { Id = 149, CustomerId = customer.Id, BoothId = 9, SessionId = 149 },
            new PhotoHistory { Id = 150, CustomerId = customer2.Id, BoothId = 10, SessionId = 150 },
            new PhotoHistory { Id = 151, CustomerId = customer.Id, BoothId = 11, SessionId = 151 },
            new PhotoHistory { Id = 152, CustomerId = customer2.Id, BoothId = 12, SessionId = 152 },
            new PhotoHistory { Id = 153, CustomerId = customer.Id, BoothId = 13, SessionId = 153 },
            new PhotoHistory { Id = 154, CustomerId = customer2.Id, BoothId = 14, SessionId = 154 },
            new PhotoHistory { Id = 155, CustomerId = customer.Id, BoothId = 15, SessionId = 155 },
            new PhotoHistory { Id = 156, CustomerId = customer2.Id, BoothId = 16, SessionId = 156 },
            new PhotoHistory { Id = 157, CustomerId = customer.Id, BoothId = 17, SessionId = 157 },
            new PhotoHistory { Id = 158, CustomerId = customer2.Id, BoothId = 18, SessionId = 158 },
            new PhotoHistory { Id = 159, CustomerId = customer.Id, BoothId = 19, SessionId = 159 },
            new PhotoHistory { Id = 160, CustomerId = customer2.Id, BoothId = 20, SessionId = 160 },
            new PhotoHistory { Id = 161, CustomerId = customer.Id, BoothId = 1, SessionId = 161 },
            new PhotoHistory { Id = 162, CustomerId = customer2.Id, BoothId = 2, SessionId = 162 },
            new PhotoHistory { Id = 163, CustomerId = customer.Id, BoothId = 3, SessionId = 163 },
            new PhotoHistory { Id = 164, CustomerId = customer2.Id, BoothId = 4, SessionId = 164 },
            new PhotoHistory { Id = 165, CustomerId = customer.Id, BoothId = 5, SessionId = 165 },
            new PhotoHistory { Id = 166, CustomerId = customer2.Id, BoothId = 6, SessionId = 166 },
            new PhotoHistory { Id = 167, CustomerId = customer.Id, BoothId = 7, SessionId = 167 },
            new PhotoHistory { Id = 168, CustomerId = customer2.Id, BoothId = 8, SessionId = 168 },
            new PhotoHistory { Id = 169, CustomerId = customer.Id, BoothId = 9, SessionId = 169 },
            new PhotoHistory { Id = 170, CustomerId = customer2.Id, BoothId = 10, SessionId = 170 },
            new PhotoHistory { Id = 171, CustomerId = customer.Id, BoothId = 11, SessionId = 171 },
            new PhotoHistory { Id = 172, CustomerId = customer2.Id, BoothId = 12, SessionId = 172 },
            new PhotoHistory { Id = 173, CustomerId = customer.Id, BoothId = 13, SessionId = 173 },
            new PhotoHistory { Id = 174, CustomerId = customer2.Id, BoothId = 14, SessionId = 174 },
            new PhotoHistory { Id = 175, CustomerId = customer.Id, BoothId = 15, SessionId = 175 },
            new PhotoHistory { Id = 176, CustomerId = customer2.Id, BoothId = 16, SessionId = 176 },
            new PhotoHistory { Id = 177, CustomerId = customer.Id, BoothId = 17, SessionId = 177 },
            new PhotoHistory { Id = 178, CustomerId = customer2.Id, BoothId = 18, SessionId = 178 },
            new PhotoHistory { Id = 179, CustomerId = customer.Id, BoothId = 19, SessionId = 179 },
            new PhotoHistory { Id = 180, CustomerId = customer2.Id, BoothId = 20, SessionId = 180 },
            new PhotoHistory { Id = 181, CustomerId = customer.Id, BoothId = 1, SessionId = 181 },
            new PhotoHistory { Id = 182, CustomerId = customer2.Id, BoothId = 2, SessionId = 182 },
            new PhotoHistory { Id = 183, CustomerId = customer.Id, BoothId = 3, SessionId = 183 },
            new PhotoHistory { Id = 184, CustomerId = customer2.Id, BoothId = 4, SessionId = 184 },
            new PhotoHistory { Id = 185, CustomerId = customer.Id, BoothId = 5, SessionId = 185 },
            new PhotoHistory { Id = 186, CustomerId = customer2.Id, BoothId = 6, SessionId = 186 },
            new PhotoHistory { Id = 187, CustomerId = customer.Id, BoothId = 7, SessionId = 187 },
            new PhotoHistory { Id = 188, CustomerId = customer2.Id, BoothId = 8, SessionId = 188 },
            new PhotoHistory { Id = 189, CustomerId = customer.Id, BoothId = 9, SessionId = 189 },
            new PhotoHistory { Id = 190, CustomerId = customer2.Id, BoothId = 10, SessionId = 190 },
            new PhotoHistory { Id = 191, CustomerId = customer.Id, BoothId = 11, SessionId = 191 },
            new PhotoHistory { Id = 192, CustomerId = customer2.Id, BoothId = 12, SessionId = 192 },
            new PhotoHistory { Id = 193, CustomerId = customer.Id, BoothId = 13, SessionId = 193 },
            new PhotoHistory { Id = 194, CustomerId = customer2.Id, BoothId = 14, SessionId = 194 },
            new PhotoHistory { Id = 195, CustomerId = customer.Id, BoothId = 15, SessionId = 195 },
            new PhotoHistory { Id = 196, CustomerId = customer2.Id, BoothId = 16, SessionId = 196 },
            new PhotoHistory { Id = 197, CustomerId = customer.Id, BoothId = 17, SessionId = 197 },
            new PhotoHistory { Id = 198, CustomerId = customer2.Id, BoothId = 18, SessionId = 198 },
            new PhotoHistory { Id = 199, CustomerId = customer.Id, BoothId = 19, SessionId = 199 },
            new PhotoHistory { Id = 200, CustomerId = customer2.Id, BoothId = 20, SessionId = 200 }
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
            new Photo { Id = 1, PhotoHistoryId = 1, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 2, PhotoHistoryId = 1, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 3, PhotoHistoryId = 1, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 4, PhotoHistoryId = 1, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 5, PhotoHistoryId = 1, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 6, PhotoHistoryId = 2, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 7, PhotoHistoryId = 2, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 8, PhotoHistoryId = 2, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 9, PhotoHistoryId = 2, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 10, PhotoHistoryId = 2, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 11, PhotoHistoryId = 3, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 12, PhotoHistoryId = 3, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 13, PhotoHistoryId = 3, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 14, PhotoHistoryId = 3, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 15, PhotoHistoryId = 3, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 16, PhotoHistoryId = 4, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 17, PhotoHistoryId = 4, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 18, PhotoHistoryId = 4, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 19, PhotoHistoryId = 4, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 20, PhotoHistoryId = 4, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 21, PhotoHistoryId = 5, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 22, PhotoHistoryId = 5, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 23, PhotoHistoryId = 5, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 24, PhotoHistoryId = 5, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 25, PhotoHistoryId = 5, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 26, PhotoHistoryId = 6, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 27, PhotoHistoryId = 6, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 28, PhotoHistoryId = 6, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 29, PhotoHistoryId = 6, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 30, PhotoHistoryId = 6, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 31, PhotoHistoryId = 7, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 32, PhotoHistoryId = 7, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 33, PhotoHistoryId = 7, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 34, PhotoHistoryId = 7, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 35, PhotoHistoryId = 7, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 36, PhotoHistoryId = 8, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 37, PhotoHistoryId = 8, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 38, PhotoHistoryId = 8, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 39, PhotoHistoryId = 8, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 40, PhotoHistoryId = 8, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 41, PhotoHistoryId = 9, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 42, PhotoHistoryId = 9, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 43, PhotoHistoryId = 9, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 44, PhotoHistoryId = 9, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 45, PhotoHistoryId = 9, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 46, PhotoHistoryId = 10, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 47, PhotoHistoryId = 10, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 48, PhotoHistoryId = 10, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 49, PhotoHistoryId = 10, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 50, PhotoHistoryId = 10, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 51, PhotoHistoryId = 11, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 52, PhotoHistoryId = 11, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 53, PhotoHistoryId = 11, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 54, PhotoHistoryId = 11, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 55, PhotoHistoryId = 11, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 56, PhotoHistoryId = 12, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 57, PhotoHistoryId = 12, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 58, PhotoHistoryId = 12, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 59, PhotoHistoryId = 12, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 60, PhotoHistoryId = 12, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 61, PhotoHistoryId = 13, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 62, PhotoHistoryId = 13, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 63, PhotoHistoryId = 13, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 64, PhotoHistoryId = 13, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 65, PhotoHistoryId = 13, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 66, PhotoHistoryId = 14, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 67, PhotoHistoryId = 14, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 68, PhotoHistoryId = 14, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 69, PhotoHistoryId = 14, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 70, PhotoHistoryId = 14, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 71, PhotoHistoryId = 15, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 72, PhotoHistoryId = 15, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 73, PhotoHistoryId = 15, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 74, PhotoHistoryId = 15, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 75, PhotoHistoryId = 15, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 76, PhotoHistoryId = 16, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 77, PhotoHistoryId = 16, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 78, PhotoHistoryId = 16, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 79, PhotoHistoryId = 16, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 80, PhotoHistoryId = 16, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 81, PhotoHistoryId = 17, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 82, PhotoHistoryId = 17, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 83, PhotoHistoryId = 17, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 84, PhotoHistoryId = 17, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 85, PhotoHistoryId = 17, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 86, PhotoHistoryId = 18, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 87, PhotoHistoryId = 18, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 88, PhotoHistoryId = 18, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 89, PhotoHistoryId = 18, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 90, PhotoHistoryId = 18, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 91, PhotoHistoryId = 19, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 92, PhotoHistoryId = 19, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 93, PhotoHistoryId = 19, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 94, PhotoHistoryId = 19, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 95, PhotoHistoryId = 19, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 96, PhotoHistoryId = 20, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 97, PhotoHistoryId = 20, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 98, PhotoHistoryId = 20, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 99, PhotoHistoryId = 20, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 100, PhotoHistoryId = 20, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 101, PhotoHistoryId = 21, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 102, PhotoHistoryId = 21, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 103, PhotoHistoryId = 21, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 104, PhotoHistoryId = 21, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 105, PhotoHistoryId = 21, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 106, PhotoHistoryId = 22, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 107, PhotoHistoryId = 22, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 108, PhotoHistoryId = 22, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 109, PhotoHistoryId = 22, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 110, PhotoHistoryId = 22, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 111, PhotoHistoryId = 23, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 112, PhotoHistoryId = 23, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 113, PhotoHistoryId = 23, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 114, PhotoHistoryId = 23, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 115, PhotoHistoryId = 23, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 116, PhotoHistoryId = 24, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 117, PhotoHistoryId = 24, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 118, PhotoHistoryId = 24, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 119, PhotoHistoryId = 24, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 120, PhotoHistoryId = 24, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 121, PhotoHistoryId = 25, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 122, PhotoHistoryId = 25, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 123, PhotoHistoryId = 25, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 124, PhotoHistoryId = 25, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 125, PhotoHistoryId = 25, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 126, PhotoHistoryId = 26, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 127, PhotoHistoryId = 26, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 128, PhotoHistoryId = 26, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 129, PhotoHistoryId = 26, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 130, PhotoHistoryId = 26, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 131, PhotoHistoryId = 27, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 132, PhotoHistoryId = 27, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 133, PhotoHistoryId = 27, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 134, PhotoHistoryId = 27, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 135, PhotoHistoryId = 27, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 136, PhotoHistoryId = 28, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 137, PhotoHistoryId = 28, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 138, PhotoHistoryId = 28, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 139, PhotoHistoryId = 28, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 140, PhotoHistoryId = 28, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 141, PhotoHistoryId = 29, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 142, PhotoHistoryId = 29, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 143, PhotoHistoryId = 29, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 144, PhotoHistoryId = 29, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 145, PhotoHistoryId = 29, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 146, PhotoHistoryId = 30, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 147, PhotoHistoryId = 30, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 148, PhotoHistoryId = 30, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 149, PhotoHistoryId = 30, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 150, PhotoHistoryId = 30, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 151, PhotoHistoryId = 31, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 152, PhotoHistoryId = 31, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 153, PhotoHistoryId = 31, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 154, PhotoHistoryId = 31, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 155, PhotoHistoryId = 31, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 156, PhotoHistoryId = 32, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 157, PhotoHistoryId = 32, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 158, PhotoHistoryId = 32, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 159, PhotoHistoryId = 32, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 160, PhotoHistoryId = 32, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 161, PhotoHistoryId = 33, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 162, PhotoHistoryId = 33, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 163, PhotoHistoryId = 33, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 164, PhotoHistoryId = 33, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 165, PhotoHistoryId = 33, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 166, PhotoHistoryId = 34, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 167, PhotoHistoryId = 34, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 168, PhotoHistoryId = 34, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 169, PhotoHistoryId = 34, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 170, PhotoHistoryId = 34, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 171, PhotoHistoryId = 35, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 172, PhotoHistoryId = 35, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 173, PhotoHistoryId = 35, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 174, PhotoHistoryId = 35, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 175, PhotoHistoryId = 35, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 176, PhotoHistoryId = 36, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 177, PhotoHistoryId = 36, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 178, PhotoHistoryId = 36, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 179, PhotoHistoryId = 36, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 180, PhotoHistoryId = 36, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 181, PhotoHistoryId = 37, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 182, PhotoHistoryId = 37, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 183, PhotoHistoryId = 37, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 184, PhotoHistoryId = 37, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 185, PhotoHistoryId = 37, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 186, PhotoHistoryId = 38, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 187, PhotoHistoryId = 38, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 188, PhotoHistoryId = 38, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 189, PhotoHistoryId = 38, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 190, PhotoHistoryId = 38, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 191, PhotoHistoryId = 39, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 192, PhotoHistoryId = 39, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 193, PhotoHistoryId = 39, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 194, PhotoHistoryId = 39, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 195, PhotoHistoryId = 39, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 196, PhotoHistoryId = 40, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 197, PhotoHistoryId = 40, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 198, PhotoHistoryId = 40, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 199, PhotoHistoryId = 40, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 200, PhotoHistoryId = 40, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 201, PhotoHistoryId = 41, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 202, PhotoHistoryId = 41, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 203, PhotoHistoryId = 41, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 204, PhotoHistoryId = 41, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 205, PhotoHistoryId = 41, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 206, PhotoHistoryId = 42, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 207, PhotoHistoryId = 42, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 208, PhotoHistoryId = 42, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 209, PhotoHistoryId = 42, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 210, PhotoHistoryId = 42, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 211, PhotoHistoryId = 43, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 212, PhotoHistoryId = 43, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 213, PhotoHistoryId = 43, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 214, PhotoHistoryId = 43, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 215, PhotoHistoryId = 43, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 216, PhotoHistoryId = 44, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 217, PhotoHistoryId = 44, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 218, PhotoHistoryId = 44, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 219, PhotoHistoryId = 44, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 220, PhotoHistoryId = 44, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 221, PhotoHistoryId = 45, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 222, PhotoHistoryId = 45, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 223, PhotoHistoryId = 45, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 224, PhotoHistoryId = 45, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 225, PhotoHistoryId = 45, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 226, PhotoHistoryId = 46, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 227, PhotoHistoryId = 46, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 228, PhotoHistoryId = 46, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 229, PhotoHistoryId = 46, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 230, PhotoHistoryId = 46, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 231, PhotoHistoryId = 47, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 232, PhotoHistoryId = 47, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 233, PhotoHistoryId = 47, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 234, PhotoHistoryId = 47, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 235, PhotoHistoryId = 47, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 236, PhotoHistoryId = 48, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 237, PhotoHistoryId = 48, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 238, PhotoHistoryId = 48, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 239, PhotoHistoryId = 48, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 240, PhotoHistoryId = 48, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 241, PhotoHistoryId = 49, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 242, PhotoHistoryId = 49, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 243, PhotoHistoryId = 49, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 244, PhotoHistoryId = 49, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 245, PhotoHistoryId = 49, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 246, PhotoHistoryId = 50, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 247, PhotoHistoryId = 50, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 248, PhotoHistoryId = 50, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 249, PhotoHistoryId = 50, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 250, PhotoHistoryId = 50, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 251, PhotoHistoryId = 51, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 252, PhotoHistoryId = 51, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 253, PhotoHistoryId = 51, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 254, PhotoHistoryId = 51, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 255, PhotoHistoryId = 51, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 256, PhotoHistoryId = 52, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 257, PhotoHistoryId = 52, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 258, PhotoHistoryId = 52, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 259, PhotoHistoryId = 52, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 260, PhotoHistoryId = 52, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 261, PhotoHistoryId = 53, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 262, PhotoHistoryId = 53, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 263, PhotoHistoryId = 53, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 264, PhotoHistoryId = 53, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 265, PhotoHistoryId = 53, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 266, PhotoHistoryId = 54, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 267, PhotoHistoryId = 54, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 268, PhotoHistoryId = 54, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 269, PhotoHistoryId = 54, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 270, PhotoHistoryId = 54, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 271, PhotoHistoryId = 55, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 272, PhotoHistoryId = 55, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 273, PhotoHistoryId = 55, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 274, PhotoHistoryId = 55, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 275, PhotoHistoryId = 55, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 276, PhotoHistoryId = 56, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 277, PhotoHistoryId = 56, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 278, PhotoHistoryId = 56, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 279, PhotoHistoryId = 56, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 280, PhotoHistoryId = 56, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 281, PhotoHistoryId = 57, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 282, PhotoHistoryId = 57, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 283, PhotoHistoryId = 57, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 284, PhotoHistoryId = 57, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 285, PhotoHistoryId = 57, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 286, PhotoHistoryId = 58, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 287, PhotoHistoryId = 58, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 288, PhotoHistoryId = 58, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 289, PhotoHistoryId = 58, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 290, PhotoHistoryId = 58, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 291, PhotoHistoryId = 59, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 292, PhotoHistoryId = 59, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 293, PhotoHistoryId = 59, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 294, PhotoHistoryId = 59, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 295, PhotoHistoryId = 59, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 296, PhotoHistoryId = 60, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 297, PhotoHistoryId = 60, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 298, PhotoHistoryId = 60, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 299, PhotoHistoryId = 60, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 300, PhotoHistoryId = 60, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 301, PhotoHistoryId = 61, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 302, PhotoHistoryId = 61, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 303, PhotoHistoryId = 61, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 304, PhotoHistoryId = 61, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 305, PhotoHistoryId = 61, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 306, PhotoHistoryId = 62, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 307, PhotoHistoryId = 62, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 308, PhotoHistoryId = 62, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 309, PhotoHistoryId = 62, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 310, PhotoHistoryId = 62, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 311, PhotoHistoryId = 63, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 312, PhotoHistoryId = 63, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 313, PhotoHistoryId = 63, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 314, PhotoHistoryId = 63, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 315, PhotoHistoryId = 63, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 316, PhotoHistoryId = 64, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 317, PhotoHistoryId = 64, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 318, PhotoHistoryId = 64, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 319, PhotoHistoryId = 64, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 320, PhotoHistoryId = 64, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 321, PhotoHistoryId = 65, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 322, PhotoHistoryId = 65, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 323, PhotoHistoryId = 65, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 324, PhotoHistoryId = 65, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 325, PhotoHistoryId = 65, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 326, PhotoHistoryId = 66, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 327, PhotoHistoryId = 66, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 328, PhotoHistoryId = 66, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 329, PhotoHistoryId = 66, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 330, PhotoHistoryId = 66, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 331, PhotoHistoryId = 67, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 332, PhotoHistoryId = 67, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 333, PhotoHistoryId = 67, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 334, PhotoHistoryId = 67, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 335, PhotoHistoryId = 67, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 336, PhotoHistoryId = 68, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 337, PhotoHistoryId = 68, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 338, PhotoHistoryId = 68, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 339, PhotoHistoryId = 68, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 340, PhotoHistoryId = 68, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 341, PhotoHistoryId = 69, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 342, PhotoHistoryId = 69, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 343, PhotoHistoryId = 69, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 344, PhotoHistoryId = 69, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 345, PhotoHistoryId = 69, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 346, PhotoHistoryId = 70, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 347, PhotoHistoryId = 70, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 348, PhotoHistoryId = 70, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 349, PhotoHistoryId = 70, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 350, PhotoHistoryId = 70, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 351, PhotoHistoryId = 71, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 352, PhotoHistoryId = 71, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 353, PhotoHistoryId = 71, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 354, PhotoHistoryId = 71, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 355, PhotoHistoryId = 71, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 356, PhotoHistoryId = 72, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 357, PhotoHistoryId = 72, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 358, PhotoHistoryId = 72, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 359, PhotoHistoryId = 72, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 360, PhotoHistoryId = 72, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 361, PhotoHistoryId = 73, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 362, PhotoHistoryId = 73, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 363, PhotoHistoryId = 73, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 364, PhotoHistoryId = 73, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 365, PhotoHistoryId = 73, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 366, PhotoHistoryId = 74, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 367, PhotoHistoryId = 74, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 368, PhotoHistoryId = 74, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 369, PhotoHistoryId = 74, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 370, PhotoHistoryId = 74, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 371, PhotoHistoryId = 75, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 372, PhotoHistoryId = 75, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 373, PhotoHistoryId = 75, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 374, PhotoHistoryId = 75, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 375, PhotoHistoryId = 75, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 376, PhotoHistoryId = 76, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 377, PhotoHistoryId = 76, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 378, PhotoHistoryId = 76, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 379, PhotoHistoryId = 76, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 380, PhotoHistoryId = 76, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 381, PhotoHistoryId = 77, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 382, PhotoHistoryId = 77, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 383, PhotoHistoryId = 77, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 384, PhotoHistoryId = 77, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 385, PhotoHistoryId = 77, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 386, PhotoHistoryId = 78, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 387, PhotoHistoryId = 78, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 388, PhotoHistoryId = 78, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 389, PhotoHistoryId = 78, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 390, PhotoHistoryId = 78, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 391, PhotoHistoryId = 79, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 392, PhotoHistoryId = 79, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 393, PhotoHistoryId = 79, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 394, PhotoHistoryId = 79, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 395, PhotoHistoryId = 79, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 396, PhotoHistoryId = 80, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 397, PhotoHistoryId = 80, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 398, PhotoHistoryId = 80, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 399, PhotoHistoryId = 80, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 400, PhotoHistoryId = 80, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 401, PhotoHistoryId = 81, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 402, PhotoHistoryId = 81, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 403, PhotoHistoryId = 81, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 404, PhotoHistoryId = 81, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 405, PhotoHistoryId = 81, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 406, PhotoHistoryId = 82, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 407, PhotoHistoryId = 82, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 408, PhotoHistoryId = 82, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 409, PhotoHistoryId = 82, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 410, PhotoHistoryId = 82, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 411, PhotoHistoryId = 83, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 412, PhotoHistoryId = 83, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 413, PhotoHistoryId = 83, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 414, PhotoHistoryId = 83, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 415, PhotoHistoryId = 83, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 416, PhotoHistoryId = 84, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 417, PhotoHistoryId = 84, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 418, PhotoHistoryId = 84, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 419, PhotoHistoryId = 84, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 420, PhotoHistoryId = 84, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 421, PhotoHistoryId = 85, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 422, PhotoHistoryId = 85, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 423, PhotoHistoryId = 85, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 424, PhotoHistoryId = 85, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 425, PhotoHistoryId = 85, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 426, PhotoHistoryId = 86, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 427, PhotoHistoryId = 86, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 428, PhotoHistoryId = 86, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 429, PhotoHistoryId = 86, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 430, PhotoHistoryId = 86, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 431, PhotoHistoryId = 87, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 432, PhotoHistoryId = 87, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 433, PhotoHistoryId = 87, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 434, PhotoHistoryId = 87, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 435, PhotoHistoryId = 87, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 436, PhotoHistoryId = 88, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 437, PhotoHistoryId = 88, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 438, PhotoHistoryId = 88, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 439, PhotoHistoryId = 88, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 440, PhotoHistoryId = 88, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 441, PhotoHistoryId = 89, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 442, PhotoHistoryId = 89, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 443, PhotoHistoryId = 89, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 444, PhotoHistoryId = 89, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 445, PhotoHistoryId = 89, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 446, PhotoHistoryId = 90, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 447, PhotoHistoryId = 90, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 448, PhotoHistoryId = 90, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 449, PhotoHistoryId = 90, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 450, PhotoHistoryId = 90, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 451, PhotoHistoryId = 91, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 452, PhotoHistoryId = 91, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 453, PhotoHistoryId = 91, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 454, PhotoHistoryId = 91, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 455, PhotoHistoryId = 91, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 456, PhotoHistoryId = 92, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 457, PhotoHistoryId = 92, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 458, PhotoHistoryId = 92, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 459, PhotoHistoryId = 92, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 460, PhotoHistoryId = 92, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 461, PhotoHistoryId = 93, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 462, PhotoHistoryId = 93, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 463, PhotoHistoryId = 93, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 464, PhotoHistoryId = 93, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 465, PhotoHistoryId = 93, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 466, PhotoHistoryId = 94, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 467, PhotoHistoryId = 94, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 468, PhotoHistoryId = 94, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 469, PhotoHistoryId = 94, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 470, PhotoHistoryId = 94, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 471, PhotoHistoryId = 95, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 472, PhotoHistoryId = 95, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 473, PhotoHistoryId = 95, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 474, PhotoHistoryId = 95, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 475, PhotoHistoryId = 95, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 476, PhotoHistoryId = 96, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 477, PhotoHistoryId = 96, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 478, PhotoHistoryId = 96, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 479, PhotoHistoryId = 96, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 480, PhotoHistoryId = 96, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 481, PhotoHistoryId = 97, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 482, PhotoHistoryId = 97, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 483, PhotoHistoryId = 97, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 484, PhotoHistoryId = 97, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 485, PhotoHistoryId = 97, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 486, PhotoHistoryId = 98, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 487, PhotoHistoryId = 98, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 488, PhotoHistoryId = 98, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 489, PhotoHistoryId = 98, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 490, PhotoHistoryId = 98, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 491, PhotoHistoryId = 99, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 492, PhotoHistoryId = 99, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 493, PhotoHistoryId = 99, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 494, PhotoHistoryId = 99, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 495, PhotoHistoryId = 99, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 496, PhotoHistoryId = 100, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 497, PhotoHistoryId = 100, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 498, PhotoHistoryId = 100, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 499, PhotoHistoryId = 100, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 500, PhotoHistoryId = 100, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 501, PhotoHistoryId = 101, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 502, PhotoHistoryId = 101, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 503, PhotoHistoryId = 101, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 504, PhotoHistoryId = 101, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 505, PhotoHistoryId = 101, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 506, PhotoHistoryId = 102, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 507, PhotoHistoryId = 102, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 508, PhotoHistoryId = 102, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 509, PhotoHistoryId = 102, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 510, PhotoHistoryId = 102, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 511, PhotoHistoryId = 103, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 512, PhotoHistoryId = 103, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 513, PhotoHistoryId = 103, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 514, PhotoHistoryId = 103, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 515, PhotoHistoryId = 103, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 516, PhotoHistoryId = 104, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 517, PhotoHistoryId = 104, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 518, PhotoHistoryId = 104, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 519, PhotoHistoryId = 104, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 520, PhotoHistoryId = 104, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 521, PhotoHistoryId = 105, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 522, PhotoHistoryId = 105, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 523, PhotoHistoryId = 105, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 524, PhotoHistoryId = 105, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 525, PhotoHistoryId = 105, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 526, PhotoHistoryId = 106, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 527, PhotoHistoryId = 106, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 528, PhotoHistoryId = 106, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 529, PhotoHistoryId = 106, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 530, PhotoHistoryId = 106, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 531, PhotoHistoryId = 107, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 532, PhotoHistoryId = 107, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 533, PhotoHistoryId = 107, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 534, PhotoHistoryId = 107, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 535, PhotoHistoryId = 107, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 536, PhotoHistoryId = 108, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 537, PhotoHistoryId = 108, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 538, PhotoHistoryId = 108, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 539, PhotoHistoryId = 108, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 540, PhotoHistoryId = 108, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 541, PhotoHistoryId = 109, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 542, PhotoHistoryId = 109, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 543, PhotoHistoryId = 109, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 544, PhotoHistoryId = 109, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 545, PhotoHistoryId = 109, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 546, PhotoHistoryId = 110, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 547, PhotoHistoryId = 110, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 548, PhotoHistoryId = 110, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 549, PhotoHistoryId = 110, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 550, PhotoHistoryId = 110, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 551, PhotoHistoryId = 111, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 552, PhotoHistoryId = 111, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 553, PhotoHistoryId = 111, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 554, PhotoHistoryId = 111, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 555, PhotoHistoryId = 111, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 556, PhotoHistoryId = 112, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 557, PhotoHistoryId = 112, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 558, PhotoHistoryId = 112, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 559, PhotoHistoryId = 112, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 560, PhotoHistoryId = 112, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 561, PhotoHistoryId = 113, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 562, PhotoHistoryId = 113, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 563, PhotoHistoryId = 113, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 564, PhotoHistoryId = 113, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 565, PhotoHistoryId = 113, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 566, PhotoHistoryId = 114, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 567, PhotoHistoryId = 114, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 568, PhotoHistoryId = 114, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 569, PhotoHistoryId = 114, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 570, PhotoHistoryId = 114, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 571, PhotoHistoryId = 115, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 572, PhotoHistoryId = 115, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 573, PhotoHistoryId = 115, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 574, PhotoHistoryId = 115, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 575, PhotoHistoryId = 115, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 576, PhotoHistoryId = 116, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 577, PhotoHistoryId = 116, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 578, PhotoHistoryId = 116, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 579, PhotoHistoryId = 116, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 580, PhotoHistoryId = 116, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 581, PhotoHistoryId = 117, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 582, PhotoHistoryId = 117, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 583, PhotoHistoryId = 117, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 584, PhotoHistoryId = 117, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 585, PhotoHistoryId = 117, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 586, PhotoHistoryId = 118, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 587, PhotoHistoryId = 118, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 588, PhotoHistoryId = 118, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 589, PhotoHistoryId = 118, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 590, PhotoHistoryId = 118, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 591, PhotoHistoryId = 119, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 592, PhotoHistoryId = 119, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 593, PhotoHistoryId = 119, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 594, PhotoHistoryId = 119, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 595, PhotoHistoryId = 119, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 596, PhotoHistoryId = 120, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 597, PhotoHistoryId = 120, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 598, PhotoHistoryId = 120, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 599, PhotoHistoryId = 120, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 600, PhotoHistoryId = 120, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 601, PhotoHistoryId = 121, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 602, PhotoHistoryId = 121, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 603, PhotoHistoryId = 121, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 604, PhotoHistoryId = 121, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 605, PhotoHistoryId = 121, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 606, PhotoHistoryId = 122, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 607, PhotoHistoryId = 122, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 608, PhotoHistoryId = 122, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 609, PhotoHistoryId = 122, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 610, PhotoHistoryId = 122, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 611, PhotoHistoryId = 123, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 612, PhotoHistoryId = 123, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 613, PhotoHistoryId = 123, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 614, PhotoHistoryId = 123, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 615, PhotoHistoryId = 123, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 616, PhotoHistoryId = 124, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 617, PhotoHistoryId = 124, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 618, PhotoHistoryId = 124, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 619, PhotoHistoryId = 124, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 620, PhotoHistoryId = 124, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 621, PhotoHistoryId = 125, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 622, PhotoHistoryId = 125, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 623, PhotoHistoryId = 125, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 624, PhotoHistoryId = 125, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 625, PhotoHistoryId = 125, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 626, PhotoHistoryId = 126, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 627, PhotoHistoryId = 126, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 628, PhotoHistoryId = 126, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 629, PhotoHistoryId = 126, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 630, PhotoHistoryId = 126, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 631, PhotoHistoryId = 127, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 632, PhotoHistoryId = 127, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 633, PhotoHistoryId = 127, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 634, PhotoHistoryId = 127, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 635, PhotoHistoryId = 127, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 636, PhotoHistoryId = 128, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 637, PhotoHistoryId = 128, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 638, PhotoHistoryId = 128, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 639, PhotoHistoryId = 128, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 640, PhotoHistoryId = 128, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 641, PhotoHistoryId = 129, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 642, PhotoHistoryId = 129, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 643, PhotoHistoryId = 129, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 644, PhotoHistoryId = 129, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 645, PhotoHistoryId = 129, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 646, PhotoHistoryId = 130, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 647, PhotoHistoryId = 130, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 648, PhotoHistoryId = 130, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 649, PhotoHistoryId = 130, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 650, PhotoHistoryId = 130, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 651, PhotoHistoryId = 131, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 652, PhotoHistoryId = 131, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 653, PhotoHistoryId = 131, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 654, PhotoHistoryId = 131, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 655, PhotoHistoryId = 131, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 656, PhotoHistoryId = 132, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 657, PhotoHistoryId = 132, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 658, PhotoHistoryId = 132, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 659, PhotoHistoryId = 132, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 660, PhotoHistoryId = 132, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 661, PhotoHistoryId = 133, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 662, PhotoHistoryId = 133, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 663, PhotoHistoryId = 133, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 664, PhotoHistoryId = 133, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 665, PhotoHistoryId = 133, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 666, PhotoHistoryId = 134, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 667, PhotoHistoryId = 134, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 668, PhotoHistoryId = 134, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 669, PhotoHistoryId = 134, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 670, PhotoHistoryId = 134, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 671, PhotoHistoryId = 135, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 672, PhotoHistoryId = 135, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 673, PhotoHistoryId = 135, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 674, PhotoHistoryId = 135, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 675, PhotoHistoryId = 135, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 676, PhotoHistoryId = 136, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 677, PhotoHistoryId = 136, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 678, PhotoHistoryId = 136, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 679, PhotoHistoryId = 136, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 680, PhotoHistoryId = 136, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 681, PhotoHistoryId = 137, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 682, PhotoHistoryId = 137, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 683, PhotoHistoryId = 137, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 684, PhotoHistoryId = 137, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 685, PhotoHistoryId = 137, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 686, PhotoHistoryId = 138, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 687, PhotoHistoryId = 138, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 688, PhotoHistoryId = 138, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 689, PhotoHistoryId = 138, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 690, PhotoHistoryId = 138, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 691, PhotoHistoryId = 139, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 692, PhotoHistoryId = 139, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 693, PhotoHistoryId = 139, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 694, PhotoHistoryId = 139, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 695, PhotoHistoryId = 139, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 696, PhotoHistoryId = 140, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 697, PhotoHistoryId = 140, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 698, PhotoHistoryId = 140, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 699, PhotoHistoryId = 140, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 700, PhotoHistoryId = 140, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 701, PhotoHistoryId = 141, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 702, PhotoHistoryId = 141, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 703, PhotoHistoryId = 141, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 704, PhotoHistoryId = 141, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 705, PhotoHistoryId = 141, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 706, PhotoHistoryId = 142, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 707, PhotoHistoryId = 142, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 708, PhotoHistoryId = 142, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 709, PhotoHistoryId = 142, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 710, PhotoHistoryId = 142, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 711, PhotoHistoryId = 143, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 712, PhotoHistoryId = 143, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 713, PhotoHistoryId = 143, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 714, PhotoHistoryId = 143, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 715, PhotoHistoryId = 143, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 716, PhotoHistoryId = 144, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 717, PhotoHistoryId = 144, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 718, PhotoHistoryId = 144, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 719, PhotoHistoryId = 144, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 720, PhotoHistoryId = 144, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 721, PhotoHistoryId = 145, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 722, PhotoHistoryId = 145, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 723, PhotoHistoryId = 145, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 724, PhotoHistoryId = 145, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 725, PhotoHistoryId = 145, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 726, PhotoHistoryId = 146, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 727, PhotoHistoryId = 146, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 728, PhotoHistoryId = 146, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 729, PhotoHistoryId = 146, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 730, PhotoHistoryId = 146, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 731, PhotoHistoryId = 147, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 732, PhotoHistoryId = 147, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 733, PhotoHistoryId = 147, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 734, PhotoHistoryId = 147, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 735, PhotoHistoryId = 147, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 736, PhotoHistoryId = 148, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 737, PhotoHistoryId = 148, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 738, PhotoHistoryId = 148, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 739, PhotoHistoryId = 148, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 740, PhotoHistoryId = 148, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 741, PhotoHistoryId = 149, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 742, PhotoHistoryId = 149, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 743, PhotoHistoryId = 149, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 744, PhotoHistoryId = 149, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 745, PhotoHistoryId = 149, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 746, PhotoHistoryId = 150, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 747, PhotoHistoryId = 150, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 748, PhotoHistoryId = 150, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 749, PhotoHistoryId = 150, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 750, PhotoHistoryId = 150, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 751, PhotoHistoryId = 151, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 752, PhotoHistoryId = 151, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 753, PhotoHistoryId = 151, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 754, PhotoHistoryId = 151, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 755, PhotoHistoryId = 151, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 756, PhotoHistoryId = 152, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 757, PhotoHistoryId = 152, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 758, PhotoHistoryId = 152, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 759, PhotoHistoryId = 152, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 760, PhotoHistoryId = 152, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 761, PhotoHistoryId = 153, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 762, PhotoHistoryId = 153, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 763, PhotoHistoryId = 153, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 764, PhotoHistoryId = 153, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 765, PhotoHistoryId = 153, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 766, PhotoHistoryId = 154, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 767, PhotoHistoryId = 154, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 768, PhotoHistoryId = 154, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 769, PhotoHistoryId = 154, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 770, PhotoHistoryId = 154, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 771, PhotoHistoryId = 155, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 772, PhotoHistoryId = 155, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 773, PhotoHistoryId = 155, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 774, PhotoHistoryId = 155, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 775, PhotoHistoryId = 155, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 776, PhotoHistoryId = 156, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 777, PhotoHistoryId = 156, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 778, PhotoHistoryId = 156, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 779, PhotoHistoryId = 156, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 780, PhotoHistoryId = 156, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 781, PhotoHistoryId = 157, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 782, PhotoHistoryId = 157, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 783, PhotoHistoryId = 157, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 784, PhotoHistoryId = 157, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 785, PhotoHistoryId = 157, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 786, PhotoHistoryId = 158, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 787, PhotoHistoryId = 158, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 788, PhotoHistoryId = 158, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 789, PhotoHistoryId = 158, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 790, PhotoHistoryId = 158, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 791, PhotoHistoryId = 159, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 792, PhotoHistoryId = 159, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 793, PhotoHistoryId = 159, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 794, PhotoHistoryId = 159, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 795, PhotoHistoryId = 159, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 796, PhotoHistoryId = 160, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 797, PhotoHistoryId = 160, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 798, PhotoHistoryId = 160, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 799, PhotoHistoryId = 160, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 800, PhotoHistoryId = 160, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 801, PhotoHistoryId = 161, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 802, PhotoHistoryId = 161, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 803, PhotoHistoryId = 161, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 804, PhotoHistoryId = 161, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 805, PhotoHistoryId = 161, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 806, PhotoHistoryId = 162, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 807, PhotoHistoryId = 162, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 808, PhotoHistoryId = 162, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 809, PhotoHistoryId = 162, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 810, PhotoHistoryId = 162, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 811, PhotoHistoryId = 163, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 812, PhotoHistoryId = 163, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 813, PhotoHistoryId = 163, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 814, PhotoHistoryId = 163, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 815, PhotoHistoryId = 163, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 816, PhotoHistoryId = 164, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 817, PhotoHistoryId = 164, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 818, PhotoHistoryId = 164, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 819, PhotoHistoryId = 164, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 820, PhotoHistoryId = 164, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 821, PhotoHistoryId = 165, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 822, PhotoHistoryId = 165, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 823, PhotoHistoryId = 165, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 824, PhotoHistoryId = 165, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 825, PhotoHistoryId = 165, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 826, PhotoHistoryId = 166, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 827, PhotoHistoryId = 166, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 828, PhotoHistoryId = 166, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 829, PhotoHistoryId = 166, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 830, PhotoHistoryId = 166, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 831, PhotoHistoryId = 167, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 832, PhotoHistoryId = 167, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 833, PhotoHistoryId = 167, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 834, PhotoHistoryId = 167, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 835, PhotoHistoryId = 167, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 836, PhotoHistoryId = 168, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 837, PhotoHistoryId = 168, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 838, PhotoHistoryId = 168, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 839, PhotoHistoryId = 168, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 840, PhotoHistoryId = 168, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 841, PhotoHistoryId = 169, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 842, PhotoHistoryId = 169, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 843, PhotoHistoryId = 169, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 844, PhotoHistoryId = 169, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 845, PhotoHistoryId = 169, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 846, PhotoHistoryId = 170, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 847, PhotoHistoryId = 170, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 848, PhotoHistoryId = 170, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 849, PhotoHistoryId = 170, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 850, PhotoHistoryId = 170, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 851, PhotoHistoryId = 171, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 852, PhotoHistoryId = 171, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 853, PhotoHistoryId = 171, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 854, PhotoHistoryId = 171, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 855, PhotoHistoryId = 171, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 856, PhotoHistoryId = 172, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 857, PhotoHistoryId = 172, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 858, PhotoHistoryId = 172, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 859, PhotoHistoryId = 172, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 860, PhotoHistoryId = 172, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 861, PhotoHistoryId = 173, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 862, PhotoHistoryId = 173, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 863, PhotoHistoryId = 173, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 864, PhotoHistoryId = 173, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 865, PhotoHistoryId = 173, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 866, PhotoHistoryId = 174, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 867, PhotoHistoryId = 174, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 868, PhotoHistoryId = 174, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 869, PhotoHistoryId = 174, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 870, PhotoHistoryId = 174, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 871, PhotoHistoryId = 175, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 872, PhotoHistoryId = 175, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 873, PhotoHistoryId = 175, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 874, PhotoHistoryId = 175, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 875, PhotoHistoryId = 175, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 876, PhotoHistoryId = 176, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 877, PhotoHistoryId = 176, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 878, PhotoHistoryId = 176, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 879, PhotoHistoryId = 176, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 880, PhotoHistoryId = 176, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 881, PhotoHistoryId = 177, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 882, PhotoHistoryId = 177, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 883, PhotoHistoryId = 177, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 884, PhotoHistoryId = 177, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 885, PhotoHistoryId = 177, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 886, PhotoHistoryId = 178, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 887, PhotoHistoryId = 178, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 888, PhotoHistoryId = 178, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 889, PhotoHistoryId = 178, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 890, PhotoHistoryId = 178, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 891, PhotoHistoryId = 179, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 892, PhotoHistoryId = 179, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 893, PhotoHistoryId = 179, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 894, PhotoHistoryId = 179, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 895, PhotoHistoryId = 179, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 896, PhotoHistoryId = 180, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 897, PhotoHistoryId = 180, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 898, PhotoHistoryId = 180, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 899, PhotoHistoryId = 180, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 900, PhotoHistoryId = 180, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 901, PhotoHistoryId = 181, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 902, PhotoHistoryId = 181, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 903, PhotoHistoryId = 181, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 904, PhotoHistoryId = 181, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 905, PhotoHistoryId = 181, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 906, PhotoHistoryId = 182, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 907, PhotoHistoryId = 182, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 908, PhotoHistoryId = 182, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 909, PhotoHistoryId = 182, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 910, PhotoHistoryId = 182, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 911, PhotoHistoryId = 183, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 912, PhotoHistoryId = 183, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 913, PhotoHistoryId = 183, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 914, PhotoHistoryId = 183, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 915, PhotoHistoryId = 183, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 916, PhotoHistoryId = 184, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 917, PhotoHistoryId = 184, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 918, PhotoHistoryId = 184, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 919, PhotoHistoryId = 184, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 920, PhotoHistoryId = 184, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 921, PhotoHistoryId = 185, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 922, PhotoHistoryId = 185, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 923, PhotoHistoryId = 185, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 924, PhotoHistoryId = 185, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 925, PhotoHistoryId = 185, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 926, PhotoHistoryId = 186, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 927, PhotoHistoryId = 186, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 928, PhotoHistoryId = 186, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 929, PhotoHistoryId = 186, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 930, PhotoHistoryId = 186, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 931, PhotoHistoryId = 187, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 932, PhotoHistoryId = 187, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 933, PhotoHistoryId = 187, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 934, PhotoHistoryId = 187, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 935, PhotoHistoryId = 187, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 936, PhotoHistoryId = 188, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 937, PhotoHistoryId = 188, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 938, PhotoHistoryId = 188, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 939, PhotoHistoryId = 188, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 940, PhotoHistoryId = 188, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 941, PhotoHistoryId = 189, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 942, PhotoHistoryId = 189, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 943, PhotoHistoryId = 189, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 944, PhotoHistoryId = 189, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 945, PhotoHistoryId = 189, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 946, PhotoHistoryId = 190, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 947, PhotoHistoryId = 190, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 948, PhotoHistoryId = 190, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 949, PhotoHistoryId = 190, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 950, PhotoHistoryId = 190, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 951, PhotoHistoryId = 191, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 952, PhotoHistoryId = 191, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 953, PhotoHistoryId = 191, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 954, PhotoHistoryId = 191, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 955, PhotoHistoryId = 191, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 956, PhotoHistoryId = 192, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 957, PhotoHistoryId = 192, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 958, PhotoHistoryId = 192, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 959, PhotoHistoryId = 192, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 960, PhotoHistoryId = 192, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 961, PhotoHistoryId = 193, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 962, PhotoHistoryId = 193, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 963, PhotoHistoryId = 193, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 964, PhotoHistoryId = 193, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 965, PhotoHistoryId = 193, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 966, PhotoHistoryId = 194, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 967, PhotoHistoryId = 194, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 968, PhotoHistoryId = 194, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 969, PhotoHistoryId = 194, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 970, PhotoHistoryId = 194, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 971, PhotoHistoryId = 195, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 972, PhotoHistoryId = 195, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 973, PhotoHistoryId = 195, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 974, PhotoHistoryId = 195, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 975, PhotoHistoryId = 195, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 976, PhotoHistoryId = 196, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 977, PhotoHistoryId = 196, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 978, PhotoHistoryId = 196, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 979, PhotoHistoryId = 196, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 980, PhotoHistoryId = 196, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 981, PhotoHistoryId = 197, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 982, PhotoHistoryId = 197, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 983, PhotoHistoryId = 197, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 984, PhotoHistoryId = 197, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 985, PhotoHistoryId = 197, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 986, PhotoHistoryId = 198, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 987, PhotoHistoryId = 198, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 988, PhotoHistoryId = 198, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QGWrb67YP9BEhYk0tLgAwcnc2Jk5227Z-w&s", PhotoStyleId = 13, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 989, PhotoHistoryId = 198, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_5WBmBTUvRdRKxyisCsXqWMgWIwM1J2f1gA&s", PhotoStyleId = 1, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 990, PhotoHistoryId = 198, Url = "https://imgcdn.stablediffusionweb.com/2024/4/2/c8816fe7-39bd-406e-af21-dbaabe2ada42.jpg", PhotoStyleId = 2, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 991, PhotoHistoryId = 199, Url = "https://cdn.prod.website-files.com/632ac1a36830f75c7e5b16f0/64f1152979367e06da5036bf_wq0VuZAd5uUoUnVvV0kT0etSNWrn1InRQkKeYi0KStM.webp", PhotoStyleId = 3, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 992, PhotoHistoryId = 199, Url = "https://image.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/de560615-b093-4b7a-95b6-90c82e33e04f/anim=false,width=450/_-QD47iVAaYTQsj0JNCcf.jpeg", PhotoStyleId = 4, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 993, PhotoHistoryId = 199, Url = "https://cdn.pixeldojo.ai/cdn-cgi/image/f=auto,q=75,w=768/pixeldojo/generated-images/1722808137493-nncip1nwp.png", PhotoStyleId = 5, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 994, PhotoHistoryId = 199, Url = "https://img.goodfon.com/wallpaper/big/2/6e/fantasy-art-stable-diffusion-neuronet-girl-armor-1.webp", PhotoStyleId = 6, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 995, PhotoHistoryId = 199, Url = "https://i.pinimg.com/474x/3f/e7/70/3fe770fd1340843ce3bdd7194b3da993.jpg", PhotoStyleId = 7, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 996, PhotoHistoryId = 200, Url = "https://assets.promptbase.com/DALLE_IMAGES%2FCzLMkNCNveZZpavk5TtibcvGQaH3%2Fresized%2F1678846934741_800x800.webp?alt=media&token=de5743c8-5716-4074-bd6d-65efb9147ff2", PhotoStyleId = 8, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 997, PhotoHistoryId = 200, Url = "https://imgcdn.stablediffusionweb.com/2024/12/5/e5280722-30ac-4001-8f96-a335006daebb.jpg", PhotoStyleId = 9, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 998, PhotoHistoryId = 200, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 999, PhotoHistoryId = 200, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFQjKHHu5gg_Sb0jsq8S9ddwgJwpnY3m8qHw&s", PhotoStyleId = 11, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
            new Photo { Id = 1000, PhotoHistoryId = 200, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4nDD6zHKKAe1OFYDOD1Xz2I48-XdseN1iKg&s", PhotoStyleId = 12, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false }
                );

                context.SaveChanges();
            }

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Photo] OFF");
            #endregion

            #region Seed data for Sticker
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Sticker] ON");

            if (!context.Sticker.Any())
            {
                context.Sticker.AddRange(
                    new Sticker { Id = 1, Name = "Pray", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/pray.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 2, Name = "Coffee", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/coffee.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 3, Name = "Cloud", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/cloud.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 4, Name = "Bean", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/bean.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 5, Name = "Ice_cream", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/ice-cream.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 6, Name = "Movie_Sticket", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/movie-ticket.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 7, Name = "Plant", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/plant.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 8, Name = "Rainbow", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/rainbow.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 9, Name = "Smile", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/smile.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 10, Name = "Wink", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/wink.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 11, Name = "Cool", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/cool.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 12, Name = "Scare", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/scare.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 13, Name = "Proud", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/gold-star.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 14, Name = "Swag", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/dab.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 15, Name = "Angry", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/angry.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 16, Name = "Confused", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/confused.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 17, Name = "Happy", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/happy.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 18, Name = "Love", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/love.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 19, Name = "Sad", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/sad.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 20, Name = "Sick", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/sick.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 21, Name = "Sleep", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/sleep.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 22, Name = "Smiley", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/smiley.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 23, Name = "Star", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/star.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 24, Name = "Suprises", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/surprised.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 25, Name = "Vain", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/vain.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 26, Name = "Death", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/angry.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 27, Name = "Applle", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/apple.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 28, Name = "Fire", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/fire.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 29, Name = "Ghost", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/ghost.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 30, Name = "Mushroom", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/mushroom.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 31, Name = "Avocado", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/avocado.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 32, Name = "French-fire", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/french-fries.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 33, Name = "Egg", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/fried-egg.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 34, Name = "Sushi", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/japanese-food.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 35, Name = "Noodle", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/noodle.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 36, Name = "Pancake", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/pancake.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 37, Name = "Sandwich", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/sandwich.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 38, Name = "Bee", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/bee.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 39, Name = "Dog", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/corgi.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 40, Name = "Dragon", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Dragon_read.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 41, Name = "Racoon eat", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/eat.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 42, Name = "Frog", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/frog.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 43, Name = "Rabbit", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/full.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 44, Name = "Racoon hi", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/hi.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 45, Name = "Panda", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/panda.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 46, Name = "Panda1", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/panda%20%281%29.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 47, Name = "Unicorn", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/unicorn.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 48, Name = "Halloween", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/castle-house.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 49, Name = "Christmas", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/christmas.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 50, Name = "Holly", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/holly.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 51, Name = "Suitcase1", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/suitcase%20%281%29.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 52, Name = "Suitcase", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/suitcase.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 53, Name = "Umbrella", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/sun-umbrella.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 54, Name = "Cassette_love", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/cassette-tape.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 55, Name = "Love1", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/love%20%281%29.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 56, Name = "Love2", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/love-badge.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 57, Name = "Love3", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/love-you.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 58, Name = "Love4", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/love.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 59, Name = "Love5", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/wink.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 60, Name = "Cupid", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/cupid.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Sticker { Id = 61, Name = "Wedding", StickerUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/wedding-couple.png?alt=media", StickerStyleId = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:23"), LastModified = DateTime.Parse("2025-03-28 13:15:23"), IsDeleted = false, CreatedById = null, LastModifiedById = null }
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
                    new Frame { Id = 1, Name = "HappyBirthday", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Frame1_Happy%20Birthday.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 2, Name = "Instagram", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Frame1_Instagram.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 3, Name = "Japan", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Japan.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 4, Name = "Happy Wedding", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Wedding.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 5, Name = "Graduation", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Graduation.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 6, Name = "90's Style", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Retro.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 7, Name = "Zoo", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Zoo.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 8, Name = "Merry Christmas", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Merry%20Christmas.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 9, Name = "Halloween", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Halloween.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 10, Name = "Summer Sea", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Summer.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 11, Name = "Night Party", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Night%20Party.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 12, Name = "Vintage Camera", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Vintage%20Camera.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 13, Name = "Happy New Year", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/New%20Year.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 14, Name = "Happy Mid-Autumn", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Mid%20Autumn%20Festival.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 15, Name = "VietNam Travel", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Vietnam%20Travel.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 16, Name = "Grunge Retro", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/Grunge%20Retro.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null },
                    new Frame { Id = 17, Name = "Happy 30/4", FrameUrl = "https://firebasestorage.googleapis.com/v0/b/social-networking-8c187.appspot.com/o/304%20%282%29.png?alt=media", FrameStyleId = 1, SlotCount = 1, CreatedAt = DateTime.Parse("2025-03-28 13:15:24"), LastModified = DateTime.Parse("2025-03-28 13:15:24"), IsDeleted = false, CreatedById = null, LastModifiedById = null }
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
    public static string Normalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var normalizedString = input.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }

        return stringBuilder.ToString()
            .Normalize(NormalizationForm.FormC)
            .Trim()
            .ToLower()
            .Replace(" ", "_");
    }
}

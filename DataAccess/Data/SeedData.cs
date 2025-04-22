using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using static Azure.Core.HttpHeader;

public static class SeedData
{
    public static class ProductIdHelper
    {
        public static string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string noDiacritics = RemoveDiacritics(input);

            return noDiacritics
                .Trim()
                .ToLower()
                .Replace(" ", "_")
                .Replace("__", "_");
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }

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
                new LevelMembership { Id = 5, Name = "Platinum", Description = "Platinum membership level", Point = 1000000, DiscountPercent = 0.20m, MaxDiscount = 10000, MinOrder = 10000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                new LevelMembership { Id = 4, NextLevelId = 5, Name = "Gold", Description = "Gold membership level", Point = 600000, DiscountPercent = 0.15m, MaxDiscount = 7500, MinOrder = 10000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                new LevelMembership { Id = 3, NextLevelId = 4, Name = "Silver", Description = "Silver membership level", Point = 300000, DiscountPercent = 0.10m, MaxDiscount = 5000, MinOrder = 10000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                new LevelMembership { Id = 2, NextLevelId = 3, Name = "Bronze", Description = "Bronze membership level", Point = 100000, DiscountPercent = 0.05m, MaxDiscount = 2500, MinOrder = 10000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id },
                new LevelMembership { Id = 1, NextLevelId = 2, Name = "Basic", Description = "Basic membership level", Point = 0, DiscountPercent = 0.02m, MaxDiscount = 1000, MinOrder = 10000, IsActive = true, CreatedById = admin.Id, LastModifiedById = admin.Id }
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
                    new TypeSession { Id = 1, Name = "Photography", Description = "Photo session", Duration = 60, Price = 10000, AbleTakenNumber = 5, ForMobile = true, CreatedAt = DateTime.Parse("2025-03-14T05:45:36.4862448"), LastModified = DateTime.Parse("2025-03-14T05:45:36.4862448"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 2, Name = "Videography", Description = "Video session", Duration = 120, Price = 20000, AbleTakenNumber = 3, ForMobile = true, CreatedAt = DateTime.Parse("2025-03-14T05:45:36.4862448"), LastModified = DateTime.Parse("2025-03-14T05:45:36.4862448"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 3, Name = "Basic Package", Description = "Basic printing service package", Duration = 30, Price = 50000, AbleTakenNumber = 10, ForMobile = true, CreatedAt = DateTime.Parse("2025-03-28T04:24:45.6577221"), LastModified = DateTime.Parse("2025-03-28T04:24:45.6577221"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 4, Name = "Printing Consultation", Description = "Consultation for suitable printing type", Duration = 20, Price = 10000, AbleTakenNumber = 5, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:00:00"), LastModified = DateTime.Parse("2025-03-30T08:00:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 5, Name = "File Editing", Description = "Edit files before printing", Duration = 30, Price = 20000, AbleTakenNumber = 4, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:01:00"), LastModified = DateTime.Parse("2025-03-30T08:01:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 6, Name = "Poster Design", Description = "Design posters on demand", Duration = 45, Price = 40000, AbleTakenNumber = 3, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:02:00"), LastModified = DateTime.Parse("2025-03-30T08:02:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 7, Name = "Namecard Design", Description = "Create namecard designs", Duration = 25, Price = 15000, AbleTakenNumber = 6, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:03:00"), LastModified = DateTime.Parse("2025-03-30T08:03:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 8, Name = "Flyer Design", Description = "Design brochures and promotional flyers", Duration = 35, Price = 35000, AbleTakenNumber = 5, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:04:00"), LastModified = DateTime.Parse("2025-03-30T08:04:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 9, Name = "Layout Consultation", Description = "Consult on content layout for print", Duration = 20, Price = 10000, AbleTakenNumber = 7, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:05:00"), LastModified = DateTime.Parse("2025-03-30T08:05:00"), CreatedById = admin.Id, LastModifiedById = admin.Id },
                    new TypeSession { Id = 10, Name = "Product Photography", Description = "Professional product photography", Duration = 60, Price = 25000, AbleTakenNumber = 2, ForMobile = false, CreatedAt = DateTime.Parse("2025-03-30T08:06:00"), LastModified = DateTime.Parse("2025-03-30T08:06:00"), CreatedById = admin.Id, LastModifiedById = admin.Id }
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

                for (int typeSessionId = 1; typeSessionId <= 3; typeSessionId++)
                {
                    var typeSession = context.TypeSession.Find(typeSessionId);

                    for (int levelMembershipId = 1; levelMembershipId <= 5; levelMembershipId++)
                    {
                        var levelMembership = context.LevelMembership.Find(levelMembershipId);
                        typeSessionProducts.Add(new TypeSessionProduct
                        {
                            Id = idCounter++,
                            Name = $"{typeSession.Name}({levelMembership.Name})",
                            TypeSessionId = typeSessionId,
                            LevelMembershipId = levelMembershipId,
                            ProductId = $"product_session_{ProductIdHelper.Normalize(typeSession.Name)}_{ProductIdHelper.Normalize(levelMembership.Name)}",
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
                            Name = $"{typeSession.Name}({coupon.Name})",
                            TypeSessionId = typeSessionId,
                            CouponId = couponId,
                            ProductId = $"product_session_{ProductIdHelper.Normalize(typeSession.Name)}_{ProductIdHelper.Normalize(coupon.Name)}",
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
            new Order { Id = 1, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer2.Id, Status = OrderStatus.Completed, Amount = 300000, Email = "customer2@example.com", Phone = "123456789", CouponId = 9, TypeSessionId = 9, CreatedById = customer2.Id, LastModifiedById = customer2.Id },
            new Order { Id = 2, Code = new Random().NextInt64(1000000000, 9999999999), CustomerId = customer.Id, Status = OrderStatus.Completed, Amount = 500000, Email = "customer@example.com", Phone = "123456789", CouponId = 10, TypeSessionId = 9, CreatedById = customer.Id, LastModifiedById = customer.Id }
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
                new DepositProduct { Id = 1, Name = "Nạp 20 nghìn", Description = "Giá 20 nghìn kèm khuyến mãi 10%", AmountAdd = 2000, ProductId = $"product_{ProductIdHelper.Normalize("Nạp 20 nghìn")}", Price = 20000 },
                new DepositProduct { Id = 2, Name = "Nạp 15 nghìn", Description = "Giá 15 nghìn kèm khuyến mãi 10%", AmountAdd = 1500, ProductId = $"product_{ProductIdHelper.Normalize("Nạp 15 nghìn")}", Price = 15000 },
                new DepositProduct { Id = 3, Name = "Nạp 12 nghìn", Description = "Giá 12 nghìn kèm khuyến mãi 10%", AmountAdd = 1200, ProductId = $"product_{ProductIdHelper.Normalize("Nạp 12 nghìn")}", Price = 12000 },
                new DepositProduct { Id = 4, Name = "Nạp 10 nghìn", Description = "Giá 10 nghìn", AmountAdd = 0, ProductId = $"product_{ProductIdHelper.Normalize("Nạp 10 nghìn")}", Price = 10000 },
                new DepositProduct { Id = 5, Name = "Nạp 17 nghìn", Description = "Giá 17 nghìn kèm khuyến mãi 10%", AmountAdd = 1700, ProductId = $"product_{ProductIdHelper.Normalize("Nạp 17 nghìn")}", Price = 17000 }
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
                    new Payment { Id = 3, DepositId = 1, PaymentMethodId = 4, Status = PaymentStatus.Success, Amount = 100000 },
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
            new Session { Id = 1, Code = "SESSION", Expired = DateTime.Now.AddDays(60), AbleTakenNumber = 100, IsActive = true, OrderId = 1 },
            new Session { Id = 2, Code = "SESSION2", IsActive = true, AbleTakenNumber = 10, OrderId = 2 }
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
            new PhotoHistory { Id = 2, CustomerId = customer2.Id, BoothId = 2, SessionId = 2 }
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
            new Photo { Id = 10, PhotoHistoryId = 2, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAOBfcQyFHS7kl03MUBzIoui2-oFOi9yoQLQ&s", PhotoStyleId = 10, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false }
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

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

        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
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
                        new Order { TypeSessionId = 1, UserId = user1.Id, Amount = 100000, Status = BussinessObject.Enums.OrderStatus.Pending, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id },
                        new Order { TypeSessionId = 2, Amount = 60000, Status = BussinessObject.Enums.OrderStatus.Pending, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false, CreatedById = admin.Id, LastModifiedById = admin.Id }
                    );
                }
                context.SaveChanges();

                if (!context.Session.Any())
                {
                    context.Session.AddRange(
                        new Session { OrderId = context.Order.FirstOrDefault(o => o.TypeSessionId == 1).Id, Code = "SESSION1", IsActive = true, CreatedAt = DateTime.UtcNow },
                        new Session { OrderId = context.Order.FirstOrDefault(o => o.TypeSessionId == 2).Id, Code = "SESSION2", IsActive = true, CreatedAt = DateTime.UtcNow }
                    );
                }
                context.SaveChanges();

            // Seed data for PhotoHistory
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoHistory] ON");
            if (!context.PhotoHistory.Any())
            {
                context.PhotoHistory.AddRange(
                    new PhotoHistory { Id = 1, CustomerId = user1.Id, LocationId = 1, Amount = 100.00m, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false },
                    new PhotoHistory { Id = 2, CustomerId = user1.Id, LocationId = 2, Amount = 150.00m, CreatedAt = DateTime.UtcNow, LastModified = DateTime.UtcNow, IsDeleted = false }
                );
            }
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoHistory] OFF");

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoStyle] ON");
            if (!context.PhotoStyle.Any())
            {
                context.PhotoStyle.AddRange(
                     new PhotoStyle { Id = 1, Name = "anime", Description = "Anime-style photography", ImageUrl = "https://w0.peakpx.com/wallpaper/539/597/HD-wallpaper-anime-anime-boys-anime-girls-unjem-brunette-school-uniform-vertical-umbrella-rail.jpg" },
                     new PhotoStyle { Id = 2, Name = "artistic_portrait", Description = "High artistic portraits", ImageUrl = "https://media.istockphoto.com/id/1329954764/vector/portrait-of-a-young-dreamy-girl-with-curly-brown-hair.jpg?s=612x612&w=0&k=20&c=_KN3rOKceycoPngD9g6XmwtMYek779BOyL6oybHZr9g=" },
                     new PhotoStyle { Id = 3, Name = "cartoon", Description = "Cartoon-themed photography", ImageUrl = "https://static.vecteezy.com/system/resources/thumbnails/034/210/204/small_2x/3d-cartoon-baby-genius-photo.jpg" },
                     new PhotoStyle { Id = 4, Name = "fantasy", Description = "Fantasy-themed photography", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSynwqUs6fsgDDN5tl8UG5NhyLK9nQcC7WCYg&s" },
                     new PhotoStyle { Id = 5, Name = "fashion_style", Description = "Fashion and stylish photos", ImageUrl = "https://voguesg.s3.ap-southeast-1.amazonaws.com/wp-content/uploads/2024/10/13223104/feature-mm-680x1020.jpg" },
                     new PhotoStyle { Id = 6, Name = "id_photo", Description = "Professional ID photos", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTOBM5AUXmvAUCExgL3m92AgtwrkFxNqwZFZQ&s" },
                     new PhotoStyle { Id = 7, Name = "modern", Description = "Modern photography styles", ImageUrl = "https://i.pinimg.com/236x/55/4c/fe/554cfed5f91562bef454b3b028cb7d4c.jpg" },
                     new PhotoStyle { Id = 8, Name = "nature", Description = "Nature-inspired photography", ImageUrl = "https://img.freepik.com/premium-photo/shot-young-man-experiencing-nature-created-with-generative-ai_762026-34726.jpg" },
                     new PhotoStyle { Id = 9, Name = "vintage", Description = "Vintage and classic photography", ImageUrl = "https://media.greatbigphotographyworld.com/wp-content/uploads/2022/04/vintage-photography-featured-image.jpg" }
                );
            }
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [PhotoStyle] OFF");

            // Seed data for Photo
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

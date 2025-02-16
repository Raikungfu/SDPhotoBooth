using BussinessObject.Models;
using DataAccess.Data;
using DataAccess.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ServerAPI.Extensions;
using ServerAPI.Middleware;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISaveChangesInterceptor, AuditInterceptor>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var environment = builder.Environment.EnvironmentName;

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").GetChildren().FirstOrDefault(x => x.Key == environment)?.Value?.Split(',');

if (allowedOrigins == null || allowedOrigins.Length == 0)
{
    Console.WriteLine("⚠️ Warning: No CORS origins configured. Defaulting to all origins.");
    allowedOrigins = new[] { "*" };
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .SetIsOriginAllowed(_ => true)
              .WithExposedHeaders("Authorization");
    });
});

builder.Services.AddDbContextConfiguration(connectionString);
builder.Services.AddIdentityConfiguration();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSessionConfiguration();


builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            Console.WriteLine($"Exception: {error.Error}");
            await context.Response.WriteAsJsonAsync(new { error = error.Error.ToString() });
        }
    });
});


app.UseCors("AllowSpecificOrigin");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<AIPhotoboothDbContext>();
        //dbContext.Database.Migrate();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await SeedData.Seed(dbContext, userManager, roleManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error seeding data: {ex}");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseHttpsRedirection();

app.UseSession();


app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

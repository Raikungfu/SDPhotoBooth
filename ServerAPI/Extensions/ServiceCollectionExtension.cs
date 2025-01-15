using BusinessLogic.Service.IService;
using BusinessLogic.Service;
using DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Mapster;

namespace ServerAPI.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AIPhotoboothDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("DataAccess"));
            });

            services.AddMapster();
            services.AddScoped<IBoothStyleService, BoothStyleService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
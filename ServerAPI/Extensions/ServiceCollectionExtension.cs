using BusinessLogic.Service.IService;
using BusinessLogic.Service;
using DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BusinessLogic.ConfigMapper;

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

            services.AddAutoMapper(typeof(MapperProfile));
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<ITypeSessionService, TypeSessionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
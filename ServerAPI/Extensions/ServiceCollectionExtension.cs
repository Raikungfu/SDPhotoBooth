using BusinessLogic.Service.IService;
using BusinessLogic.Service;
using DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BusinessLogic.ConfigMapper;
using DataAccess.Extensions;
using ServerAPI.Services;


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
            services.AddScoped<IBoothService, BoothService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IPhotoStyleService, PhotoStyleService>();
            services.AddScoped<IStickerService, StickerService>();
            services.AddScoped<IStickerStyleService, StickerStyleService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<RabbitMqService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISaveChangesInterceptor, AuditInterceptor>();
            services.AddScoped<IPayOSService, PayOSService>();
            services.AddScoped<IPhotoHistoryService, PhotoHistoryService>();

            return services;
        }
    }
}
using Abstraction.Interfaces.Services;
using BLL.Factories;
using BLL.Services;
using MapsterMapper;

namespace Timely1.Extensions
{
    // вспомогательный класс для регистрации наших сервисов в билде
    public static class BllServicesServiceExtension
    {
        public static void AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IGuitarService, GuitarService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDiscountService, DiscountService>();

            // аутентификация
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            // mapper
            services.AddSingleton<IMapper, Mapper>();

            // fabrics
            services.AddSingleton<DiscountFactory>();

            // seeder
            services.AddScoped<ISeedService, SeedService>();
        }
    }
}

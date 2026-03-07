using DAL.EF;
using Microsoft.AspNetCore.Identity;
using Models.Entities;

namespace Timely1.Extensions
{
    // вспомогательный класс для настройки системы пользователей
    public static class IdentityExtension
    {
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            }) // настройка паролей для пользователей
            .AddEntityFrameworkStores<StreamingServiceDbContext>() // говорим чтобы всё хранилось в БД через наш класс
            .AddDefaultTokenProviders(); // генерация дефолтных токенов для сброса пароля, смены почты и тд

            services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddCookie(options => // по куки
                { // пишем пути для логина и логаута
                    options.LoginPath = "/api/auth/login";
                    options.LogoutPath = "/api/auth/logout";
                });

            services.AddAuthorization(); // включаем возможность проверять права доступа
        }
    }
}

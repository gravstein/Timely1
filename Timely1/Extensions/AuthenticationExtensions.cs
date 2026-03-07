using Microsoft.AspNetCore.Authentication.JwtBearer; // библиотека для проверки по JSONWebToken-ам для аутентификации
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Timely1.Extensions
{
    // вспомогательный класс для настройки JWT и их проверки
    public static class AuthenticationExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtSettings"); // получаем данные настроек JWT (секретный ключ, издателя, аудиторию)

            services.AddAuthentication(opt =>
            {   // меняем стандарнтые методы аутентификации на методы по JWT
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {   // устанавливаем правила проверки по JWT
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // проверяет кто выпустил
                    ValidateAudience = true, // для кого предназначен
                    ValidateLifetime = true, // не истёк ли срок (30 минут)
                    ValidateIssuerSigningKey = true, // проверка подписи по секретному ключу
                    ValidIssuer = jwtSettings["Issuer"], // устанавливаем того кто выпустил
                    ValidAudience = jwtSettings["Audience"], // устанавливаем кому предназначен
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])) // устанавливаем валидный секретный ключ
                };
            });
        }
    }
}

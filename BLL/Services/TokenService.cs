using Abstraction.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    // Сервис для создания JWT токенов  
    public class TokenService : ITokenService
    {
        private readonly IConfiguration config; // доступ к настройкам нашего приложения
        public TokenService(IConfiguration config)
        {
            this.config = config;
        }

        public JwtSecurityToken GenerateAccessToken(AppUser user, IList<string> roles)
        {
            var claims = new List<Claim> // тут храниться детализированная инфа о пользователе
            {
                new Claim(ClaimTypes.Email, user.Email) // пара тип значение для описания свойства пользователя
            };

            foreach (var role in roles) // добавляем в пользователю переданные роли
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            }

            var token = new JwtSecurityToken( // создаём JWT токен
                issuer: config["JwtSettings:Issuer"], // кто выдал
                audience: config["JwtSettings:Audience"], // кому
                claims: claims,                           // данные пользователя
                expires: DateTime.UtcNow.AddMinutes(30), // наш токен будет жить 30 минут
                signingCredentials: new SigningCredentials( // цифровая подпись для безопасности
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"])),
                    SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public string GenerateRefreshToken() // токен для обновления Jwt токена
        {
            return Guid.NewGuid().ToString(); // генерируем случайную строку
        }
    }
}

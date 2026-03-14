using Abstraction.Interfaces.Services;
using Common.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Models.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService tokenService; // получаем сервис для генерации токенов
        private readonly UserManager<AppUser> userManager;
        private readonly IStringLocalizer _localizer; // локализатор для наших ошибок

        public AuthService(ITokenService tokenService, UserManager<AppUser> userManager, IStringLocalizerFactory factory, ILogger<AuthService> logger)
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
            _localizer = factory.Create(typeof(ErrorMessages)); // берём локализацию из factory
        }

        public async Task<AuthResponseDTO> LoginUser(LoginDTO userForLogin)
        {
            var user = await userManager.FindByEmailAsync(userForLogin.Email); // ищем пользователя в БД по почте

            if (user == null || !await userManager.CheckPasswordAsync(user, userForLogin.Password)) // если пользователя не существует или если не совпадает пароль
                throw new UnauthorizedAccessException(_localizer["UserNotFound"].Value); // вызываем ошибку с соответсвующим текстом из файла ресурсов
               
            var roles = await userManager.GetRolesAsync(user); // собираем роли пользователей
            var token = tokenService.GenerateAccessToken(user, roles);
            var refreshToken = tokenService.GenerateRefreshToken();

            // придаём конкретному пользователю в БД его токен обновления
            await userManager.SetAuthenticationTokenAsync(user, "Default", "RefreshToken", refreshToken);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token); // превращаем объект JWT в строку

            return new AuthResponseDTO // возвращаем созданный нами объект ответа с токенами
            {
                AccessToken = tokenString,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponseDTO> RefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentNullException("Refresh token is required");

            var user = userManager.Users.AsEnumerable() // берём из базы всех пользователей и находим того чей это RefreshToken
                .FirstOrDefault(x => userManager.GetAuthenticationTokenAsync(x, "Default", "RefreshToken").Result == refreshToken);

            if (user == null) throw new UnauthorizedAccessException("Invalid refresh token"); // ошибка если мы не нашли пользователя с таким RefreshToken-ом

            var roles = await userManager.GetRolesAsync(user); // если всё ОК то мы
            var token = tokenService.GenerateAccessToken(user, roles); // генерируем новый AccessToken 

            return new AuthResponseDTO // и точно также возращаем его
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
        }
    }
}

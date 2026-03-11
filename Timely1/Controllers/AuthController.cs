using Abstraction.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace Timely1.Controllers
{
    // контроллер для Аутентификации
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService; // принимаем сервис для аутентификации
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet("test-error")]
        public IActionResult TestError() // тест middleware
        {
            throw new Exception("Middleware поймал ошибку");
        }

        [HttpPost("login")]
        public async Task<AuthResponseDTO> Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null) // если данные для логина пустые мы вызываем ошибку
                throw new ArgumentNullException(nameof(loginDTO), "Login data cannot be null");
            var tokens = await authService.LoginUser(loginDTO); // получаем токены
            return tokens;
        }
        [HttpPost("refresh")]
        [Authorize] // только авторизованные пользователи
        public async Task<AuthResponseDTO> Refresh([FromBody] RefreshRequest request) // принимаем данные request-а
        {
            if (string.IsNullOrEmpty(request.RefreshToken)) throw new ArgumentNullException("Null refresh request");

            var response = await authService.RefreshToken(request.RefreshToken); // обновляем токен
            return response;
        }
    }
}

using Abstraction.Interfaces.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Models.DTO;
using Models.Entities;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager; // инструмент из библиотеки для работы с пользователями
        private readonly IMapper mapper; // mapper выделенный в отдельную переменную

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<int> RegisterUser(RegistrationDTO registrationDTO)
        {
            if (registrationDTO is null) // пришли пустые данные
                throw new Exception("You've missed field");

            var user = mapper.Map<AppUser>(registrationDTO); // берём данные из DTO и создаём пользователя
            
            // тут мы регистрируем нашего пользователя с помощью библиотеки которая сама зашифрует пароль
            var result = await userManager.CreateAsync(user, registrationDTO.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description)); // собираем все ошибки от Identity в одну строку
                throw new Exception($"Identity Error: {errors}");
            }

            await userManager.AddToRoleAsync(user, "User"); // добавляем к нему роль User

            return StatusCodes.Status200OK; // возращаем статус 200. OK
        }
    }
}

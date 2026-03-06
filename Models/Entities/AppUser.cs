using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    // Это модель для создания пользователя. Наследуется уже из готовой модели описанной в библиотеке
    // Фактически является обёрткой для этой модели чтобы мы могли добавлять сами дополнительные поля

    public class AppUser : IdentityUser
    {
    }
}
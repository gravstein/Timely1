using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // библиотека для атрибутов для данных (Required)
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    // Модель для регистрации пользователя. Тут мы определяем какие нам для этого нужны поля
    // Чтобы сделать модель можно сразу сделать DTO?
    public class RegistrationDTO
    {
        [Required(ErrorMessage = "Email is required")] // валидация. обязательно для заполнения
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm the password")]
        public string ConfirmPassword { get; set; }
    }
}
